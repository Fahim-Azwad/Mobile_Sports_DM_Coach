using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Linq;

public class CoachHiringMarket : MonoBehaviour
{
    [Header("Database Integration")]
    [SerializeField] private bool loadFromDatabase = true;
    
    [Header("Current Coach Slots")]
    public CoachData assignedCoach1;
    public CoachData assignedCoach2;

    [Header("UI States")]
    public GameObject emptyState;  // The "Empty" GameObject
    public GameObject hiredState;  // The "Hired" GameObject with Name, Salary, Rating, etc.

    [Header("Coach Slot 1 Elements")]
    public TextMeshProUGUI nameText1;
    public TextMeshProUGUI salaryText1;
    public TextMeshProUGUI ratingText1;
    public TextMeshProUGUI Type1;
    public Button viewCoachButton1;
    public Transform specialtiesContainer1;
    public GameObject specialtyPrefab1;

    [Header("Coach Slot 2 Elements")]
    public TextMeshProUGUI nameText2;
    public TextMeshProUGUI salaryText2;
    public TextMeshProUGUI ratingText2;
    public TextMeshProUGUI Type2;
    public Button viewCoachButton2;
    public Transform specialtiesContainer2;
    public GameObject specialtyPrefab2;

    [Header("Coach Filtering")]
    public Dropdown filterDropdown;
    public TextMeshProUGUI budgetText;
    public float staticBudget = 500000f; // Static budget for now

    [Header("Testing Controls")]
    public TextMeshProUGUI instructionsText;

    // Database variables
    private static List<CoachDatabaseRecord> cachedCoaches = null;
    private static bool isCacheInitialized = false;
    private string currentFilter = "ALL";
    private string[] filterTypes = { "ALL", "D", "O", "S" };
    private int currentFilterIndex = 0;

    // Dynamic coach data from database
    private CoachDatabaseRecord dbCoach1;
    private CoachDatabaseRecord dbCoach2;

    private CoachData currentCoach;
    private CoachType slotType;

    private void Start()
    {
        // Initialize UI
        if (instructionsText != null)
        {
            instructionsText.text = "N = New Coaches, T = Toggle Filter, F = Filter Type";
        }

        // Set static budget display
        if (budgetText != null)
        {
            budgetText.text = $"Budget: ${staticBudget:N0}";
        }

        // Setup filter dropdown
        SetupFilterDropdown();

        // Load initial coaches
        if (loadFromDatabase)
        {
            LoadNewCoaches();
        }
        else
        {
            // Use existing assigned coaches
            UpdateCoachDisplay();
        }
    }

    private void Update()
    {
        // Testing controls
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadNewCoaches();
        }
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleFilter();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            CycleFilterType();
        }

        // Update displays if using database
        if (loadFromDatabase)
        {
            UpdateCoachDisplay();
        }
        else
        {
            // Legacy behavior for assigned coaches
            UpdateHiredStateDisplay1(assignedCoach1);
            UpdateHiredStateDisplay2(assignedCoach2);
        }
    }

    #region Database Loading Methods

    /// <summary>
    /// Load coaches from database with filtering
    /// </summary>
    private List<CoachDatabaseRecord> ExtractCoachesFromDatabase()
    {
        if (isCacheInitialized && cachedCoaches != null)
        {
            return cachedCoaches;
        }

        try
        {
            string jsonPath = Path.Combine(Application.streamingAssetsPath, "Database", "coach.json");
            
            if (!File.Exists(jsonPath))
            {
                Debug.LogError($"[CoachHiringMarket] Coach JSON file not found at: {jsonPath}");
                return new List<CoachDatabaseRecord>();
            }

            string jsonContent = File.ReadAllText(jsonPath);
            string wrappedJson = $"{{\"Items\":{jsonContent}}}";
            var wrapper = JsonUtility.FromJson<JsonWrapper>(wrappedJson);
            
            if (wrapper == null || wrapper.Items == null)
            {
                Debug.LogError("[CoachHiringMarket] Failed to parse JSON");
                return new List<CoachDatabaseRecord>();
            }
            
            cachedCoaches = new List<CoachDatabaseRecord>(wrapper.Items);
            isCacheInitialized = true;
            
            Debug.Log($"[CoachHiringMarket] Successfully loaded {wrapper.Items.Length} coaches from JSON");
            return cachedCoaches;
        }
        catch (Exception e)
        {
            Debug.LogError($"[CoachHiringMarket] Failed to load from JSON: {e.Message}");
            return new List<CoachDatabaseRecord>();
        }
    }

    /// <summary>
    /// Load new coaches based on current filter
    /// </summary>
    public void LoadNewCoaches()
    {
        if (!loadFromDatabase) return;

        try
        {
            var allCoaches = ExtractCoachesFromDatabase();
            var filteredCoaches = FilterCoaches(allCoaches, currentFilter);

            if (filteredCoaches.Count >= 2)
            {
                // Load two random coaches
                var randomCoaches = GetRandomCoaches(filteredCoaches, 2);
                dbCoach1 = randomCoaches[0];
                dbCoach2 = randomCoaches[1];

                Debug.Log($"[CoachHiringMarket] Loaded 2 new coaches with filter: {GetFilterDisplayName(currentFilter)}");
            }
            else if (filteredCoaches.Count == 1)
            {
                // Load one coach and a default
                dbCoach1 = filteredCoaches[0];
                dbCoach2 = CreateDefaultCoachRecord();
                Debug.Log($"[CoachHiringMarket] Loaded 1 coach + default with filter: {GetFilterDisplayName(currentFilter)}");
            }
            else
            {
                // No coaches found, load defaults
                dbCoach1 = CreateDefaultCoachRecord();
                dbCoach2 = CreateDefaultCoachRecord();
                Debug.LogWarning($"[CoachHiringMarket] No coaches found for filter: {GetFilterDisplayName(currentFilter)}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[CoachHiringMarket] Error loading new coaches: {e.Message}");
        }
    }

    /// <summary>
    /// Filter coaches by type
    /// </summary>
    private List<CoachDatabaseRecord> FilterCoaches(List<CoachDatabaseRecord> allCoaches, string filter)
    {
        if (filter == "ALL")
        {
            return new List<CoachDatabaseRecord>(allCoaches);
        }

        var filtered = new List<CoachDatabaseRecord>();
        foreach (var coach in allCoaches)
        {
            if (coach.coach_type.Equals(filter, StringComparison.OrdinalIgnoreCase))
            {
                filtered.Add(coach);
            }
        }
        return filtered;
    }

    /// <summary>
    /// Get random coaches from filtered list
    /// </summary>
    private List<CoachDatabaseRecord> GetRandomCoaches(List<CoachDatabaseRecord> coaches, int count)
    {
        var result = new List<CoachDatabaseRecord>();
        var availableCoaches = new List<CoachDatabaseRecord>(coaches);

        for (int i = 0; i < count && availableCoaches.Count > 0; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableCoaches.Count);
            result.Add(availableCoaches[randomIndex]);
            availableCoaches.RemoveAt(randomIndex);
        }

        return result;
    }

    #endregion

    #region Filter Management

    /// <summary>
    /// Setup filter dropdown
    /// </summary>
    private void SetupFilterDropdown()
    {
        if (filterDropdown != null)
        {
            filterDropdown.ClearOptions();
            var options = new List<string> { "All", "Defense", "Offense", "Special Teams" };
            filterDropdown.AddOptions(options);
            filterDropdown.onValueChanged.AddListener(OnFilterChanged);
        }
    }

    /// <summary>
    /// Handle filter dropdown change
    /// </summary>
    public void OnFilterChanged(int index)
    {
        string[] filterMappings = { "ALL", "D", "O", "S" };
        if (index >= 0 && index < filterMappings.Length)
        {
            currentFilter = filterMappings[index];
            currentFilterIndex = index;
            LoadNewCoaches();
            Debug.Log($"[CoachHiringMarket] Filter changed to: {GetFilterDisplayName(currentFilter)}");
        }
    }

    /// <summary>
    /// Toggle filter via T key
    /// </summary>
    public void ToggleFilter()
    {
        LoadNewCoaches();
        Debug.Log("[CoachHiringMarket] Refreshed coaches");
    }

    /// <summary>
    /// Cycle filter type via F key
    /// </summary>
    public void CycleFilterType()
    {
        currentFilterIndex = (currentFilterIndex + 1) % filterTypes.Length;
        currentFilter = filterTypes[currentFilterIndex];
        
        if (filterDropdown != null)
        {
            filterDropdown.value = currentFilterIndex;
        }
        
        LoadNewCoaches();
        Debug.Log($"[CoachHiringMarket] Filter cycled to: {GetFilterDisplayName(currentFilter)}");
    }

    /// <summary>
    /// Get display name for filter
    /// </summary>
    private string GetFilterDisplayName(string filter)
    {
        switch (filter)
        {
            case "ALL": return "All";
            case "D": return "Defense";
            case "O": return "Offense";
            case "S": return "Special Teams";
            default: return "Unknown";
        }
    }

    #endregion



    #region UI Update Methods

    /// <summary>
    /// Update coach displays based on data source
    /// </summary>
    private void UpdateCoachDisplay()
    {
        if (loadFromDatabase)
        {
            UpdateDatabaseCoachDisplay1(dbCoach1);
            UpdateDatabaseCoachDisplay2(dbCoach2);
        }
        else
        {
            UpdateHiredStateDisplay1(assignedCoach1);
            UpdateHiredStateDisplay2(assignedCoach2);
        }
    }

    /// <summary>
    /// Update coach slot 1 with database data
    /// </summary>
    private void UpdateDatabaseCoachDisplay1(CoachDatabaseRecord coach)
    {
        if (coach == null) return;

        if (nameText1 != null)
            nameText1.text = "Name: " + coach.coach_name;

        if (salaryText1 != null)
        {
            float weeklySalary = (coach.salary * 1000000f) / 52f;
            salaryText1.text = "Salary: " + $"${weeklySalary:N0}/wk";
        }

        if (ratingText1 != null)
        {
            int starRating = CalculateStarRating(coach.overall_rating);
            ratingText1.text = "Rating: " + $"{starRating} Stars";
        }

        if (Type1 != null)
            Type1.text = "Type: " + GetCoachTypeDisplayName(coach.coach_type);

        // Update top 3 specialties
        UpdateSpecialtiesDisplay1(coach);
    }

    /// <summary>
    /// Update coach slot 2 with database data
    /// </summary>
    private void UpdateDatabaseCoachDisplay2(CoachDatabaseRecord coach)
    {
        if (coach == null) return;

        if (nameText2 != null)
            nameText2.text = "Name: " + coach.coach_name;

        if (salaryText2 != null)
        {
            float weeklySalary = (coach.salary * 1000000f) / 52f;
            salaryText2.text = "Salary: " + $"${weeklySalary:N0}/wk";
        }

        if (ratingText2 != null)
        {
            int starRating = CalculateStarRating(coach.overall_rating);
            ratingText2.text = "Rating: " + $"{starRating} Stars";
        }

        if (Type2 != null)
            Type2.text = "Type: " + GetCoachTypeDisplayName(coach.coach_type);

        // Update top 3 specialties
        UpdateSpecialtiesDisplay2(coach);
    }

    /// <summary>
    /// Update top 3 specialties for coach slot 1
    /// </summary>
    private void UpdateSpecialtiesDisplay1(CoachDatabaseRecord coach)
    {
        if (specialtiesContainer1 == null || specialtyPrefab1 == null) return;

        // Clear existing specialties
        foreach (Transform child in specialtiesContainer1)
        {
            Destroy(child.gameObject);
        }

        // Get top 3 specialties
        var specialties = GetTopSpecialties(coach, 3);

        // Create specialty UI elements
        foreach (var specialty in specialties)
        {
            GameObject specialtyObj = Instantiate(specialtyPrefab1, specialtiesContainer1);
            TextMeshProUGUI specialtyText = specialtyObj.GetComponent<TextMeshProUGUI>();
            if (specialtyText != null)
            {
                specialtyText.text = $"{specialty.key}: {specialty.value}%";
            }
        }
    }

    /// <summary>
    /// Update top 3 specialties for coach slot 2
    /// </summary>
    private void UpdateSpecialtiesDisplay2(CoachDatabaseRecord coach)
    {
        if (specialtiesContainer2 == null || specialtyPrefab2 == null) return;

        // Clear existing specialties
        foreach (Transform child in specialtiesContainer2)
        {
            Destroy(child.gameObject);
        }

        // Get top 3 specialties
        var specialties = GetTopSpecialties(coach, 3);

        // Create specialty UI elements
        foreach (var specialty in specialties)
        {
            GameObject specialtyObj = Instantiate(specialtyPrefab2, specialtiesContainer2);
            TextMeshProUGUI specialtyText = specialtyObj.GetComponent<TextMeshProUGUI>();
            if (specialtyText != null)
            {
                specialtyText.text = $"{specialty.key}: {specialty.value}%";
            }
        }
    }

    /// <summary>
    /// Legacy method for assigned coach data
    /// </summary>
    private void UpdateHiredStateDisplay1(CoachData coach)
    {
        if (coach == null) return;

        if (nameText1 != null)
            nameText1.text = "Name: " + coach.coachName;

        if (salaryText1 != null)
            salaryText1.text = "Salary: " + $"${coach.weeklySalary:N0}/wk";

        if (ratingText1 != null)
            ratingText1.text = "Rating: " + $"{coach.starRating} Stars";

        if (Type1 != null)
            Type1.text = "Type: " + $"{coach.type}";
    }

    /// <summary>
    /// Legacy method for assigned coach data
    /// </summary>
    private void UpdateHiredStateDisplay2(CoachData coach)
    {
        if (coach == null) return;

        if (nameText2 != null)
            nameText2.text = "Name: " + coach.coachName;

        if (salaryText2 != null)
            salaryText2.text = "Salary: " + $"${coach.weeklySalary:N0}/wk";

        if (ratingText2 != null)
            ratingText2.text = "Rating: " + $"{coach.starRating} Stars";

        if (Type2 != null)
            Type2.text = "Type: " + $"{coach.type}";
    }

    #endregion


    #region Helper Methods

    /// <summary>
    /// Get top N specialties for a coach
    /// </summary>
    private List<SpecialtyEntry> GetTopSpecialties(CoachDatabaseRecord coach, int count)
    {
        List<SpecialtyEntry> allSpecialties = new List<SpecialtyEntry>();

        switch (coach.coach_type.ToUpper())
        {
            case "D": // Defense Coach
                if (coach.run_defence > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Run Defense", value = CalculateBonus(coach.run_defence) });
                if (coach.pressure_control > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Pressure Control", value = CalculateBonus(coach.pressure_control) });
                if (coach.coverage_discipline > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Coverage Discipline", value = CalculateBonus(coach.coverage_discipline) });
                if (coach.turnover > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Turnover Generation", value = CalculateBonus(coach.turnover) });
                break;

            case "O": // Offense Coach
                if (coach.passing_efficiency > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Passing Efficiency", value = CalculateBonus(coach.passing_efficiency) });
                if (coach.rush > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Rushing Attack", value = CalculateBonus(coach.rush) });
                if (coach.red_zone_conversion > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Red Zone Conversion", value = CalculateBonus(coach.red_zone_conversion) });
                if (coach.play_variation > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Play Variation", value = CalculateBonus(coach.play_variation) });
                break;

            case "S": // Special Teams Coach
                if (coach.field_goal_accuracy > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Field Goal Accuracy", value = CalculateBonus(coach.field_goal_accuracy) });
                if (coach.kickoff_instance > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Kickoff Distance", value = CalculateBonus(coach.kickoff_instance) });
                if (coach.return_speed > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Return Speed", value = CalculateBonus(coach.return_speed) });
                if (coach.return_coverage > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Return Coverage", value = CalculateBonus(coach.return_coverage) });
                break;
        }

        // Sort by value and take top N
        allSpecialties.Sort((a, b) => b.value.CompareTo(a.value));
        
        int actualCount = Mathf.Min(count, allSpecialties.Count);
        return allSpecialties.GetRange(0, actualCount);
    }

    /// <summary>
    /// Calculate percentage bonus from database stat
    /// </summary>
    private int CalculateBonus(float statValue)
    {
        return Mathf.RoundToInt(Mathf.Clamp(statValue * 5f, 0f, 50f));
    }

    /// <summary>
    /// Calculate star rating from overall rating
    /// </summary>
    private int CalculateStarRating(float overallRating)
    {
        return Mathf.RoundToInt(Mathf.Clamp(overallRating, 1f, 5f));
    }

    /// <summary>
    /// Get coach type display name
    /// </summary>
    private string GetCoachTypeDisplayName(string coachType)
    {
        switch (coachType?.ToUpper())
        {
            case "D": return "Defense";
            case "O": return "Offense";
            case "S": return "Special Teams";
            default: return "Unknown";
        }
    }

    /// <summary>
    /// Create default coach record when none available
    /// </summary>
    private CoachDatabaseRecord CreateDefaultCoachRecord()
    {
        return new CoachDatabaseRecord
        {
            coach_name = "Available Slot",
            coach_type = "D",
            experience = 1,
            salary = 0.1f,
            overall_rating = 2.0f,
            run_defence = 3.0f,
            pressure_control = 2.5f,
            coverage_discipline = 2.0f,
            turnover = 2.5f
        };
    }

    #endregion

    #region Public Methods (for buttons)

    /// <summary>
    /// Hire coach from slot 1
    /// </summary>
    public void HireCoach1() 
    {
        if (loadFromDatabase && dbCoach1 != null)
        {
            // Convert database coach to CoachData if needed
            Debug.Log($"[CoachHiringMarket] Hiring coach: {dbCoach1.coach_name}");
            // TODO: Convert to CoachData and use CoachManager.instance.HireCoach()
        }
        else if (assignedCoach1 != null)
        {
            CoachManager.instance.HireCoach(assignedCoach1);
        }
    }

    /// <summary>
    /// Hire coach from slot 2
    /// </summary>
    public void HireCoach2()
    {
        if (loadFromDatabase && dbCoach2 != null)
        {
            // Convert database coach to CoachData if needed
            Debug.Log($"[CoachHiringMarket] Hiring coach: {dbCoach2.coach_name}");
            // TODO: Convert to CoachData and use CoachManager.instance.HireCoach()
        }
        else if (assignedCoach2 != null)
        {
            CoachManager.instance.HireCoach(assignedCoach2);
        }
    }

    /// <summary>
    /// Initialize slot type (legacy compatibility)
    /// </summary>
    public void Initialize(CoachType type)
    {
        slotType = type;
    }

    #endregion
}
