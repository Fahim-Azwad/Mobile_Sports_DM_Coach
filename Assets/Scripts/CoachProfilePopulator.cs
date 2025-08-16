using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;
using System.Linq;

[System.Serializable]
public class CoachProfileWrapper
{
    public string name;
    public int experience;
    public string previousTeam;
    public int championshipWins;
    public SpecialtyEntry[] specialties;
    public ContractTermEntry[] contractTerms;
}

[System.Serializable]
public class ContractTermEntry
{
    public string key;
    public string value;
}

public class CoachProfilePopulator : MonoBehaviour
{
    public CoachProfileWrapper coach;

    [Header("Database Integration")]
    [SerializeField] private bool loadFromDatabase = true;

    [Header("Header Texts")]
    public TMP_Text coachNameText;

    [Header("Coach Metadata")]
    public TMP_Text experienceText;
    public TMP_Text previousTeamText;
    public TMP_Text championshipWinsText;

    [Header("Prefabs & Containers")]
    public GameObject specialityRowPrefab;
    public GameObject contractRowPrefab;
    public Transform specialityContainer;
    public Transform contractRowContainer;

    // Database cache
    private static List<CoachDatabaseRecord> cachedCoaches = null;
    private static bool isCacheInitialized = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize coach from database if enabled
        if (loadFromDatabase)
        {
            LoadCoachFromDatabase();
        }

        // Ensure we have a coach to display
        if (coach == null)
        {
            coach = CreateDefaultCoach();
        }

        PopulateUI();
    }

    /// <summary>
    /// Load coach from database
    /// </summary>
    private void LoadCoachFromDatabase()
    {
        try
        {
            var dbCoach = LoadRandomCoachFromDatabase();
            if (dbCoach != null)
            {
                coach = TransformToCoachProfile(dbCoach);
                Debug.Log($"[CoachProfilePopulator] Loaded coach from database: {coach.name}");
            }
            else
            {
                Debug.LogWarning("[CoachProfilePopulator] No coach loaded from database, using default");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[CoachProfilePopulator] Error loading from database: {e.Message}");
        }
    }

    /// <summary>
    /// Extract coaches from JSON database
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
                Debug.LogError($"[CoachProfilePopulator] Coach JSON file not found at: {jsonPath}");
                return new List<CoachDatabaseRecord>();
            }

            string jsonContent = File.ReadAllText(jsonPath);
            
            // Wrap array in object for JsonUtility
            string wrappedJson = $"{{\"Items\":{jsonContent}}}";
            var wrapper = JsonUtility.FromJson<JsonWrapper>(wrappedJson);
            
            if (wrapper == null || wrapper.Items == null)
            {
                Debug.LogError("[CoachProfilePopulator] Failed to parse JSON");
                return new List<CoachDatabaseRecord>();
            }
            
            cachedCoaches = new List<CoachDatabaseRecord>(wrapper.Items);
            isCacheInitialized = true;
            
            Debug.Log($"[CoachProfilePopulator] Successfully loaded {wrapper.Items.Length} coaches from JSON");
            return cachedCoaches;
        }
        catch (Exception e)
        {
            Debug.LogError($"[CoachProfilePopulator] Failed to load from JSON: {e.Message}");
            return new List<CoachDatabaseRecord>();
        }
    }

    /// <summary>
    /// Load random coach from database
    /// </summary>
    private CoachDatabaseRecord LoadRandomCoachFromDatabase()
    {
        var allCoaches = ExtractCoachesFromDatabase();
        
        if (allCoaches.Count == 0)
        {
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, allCoaches.Count);
        return allCoaches[randomIndex];
    }

    /// <summary>
    /// Transform database record to UI format
    /// </summary>
    private CoachProfileWrapper TransformToCoachProfile(CoachDatabaseRecord dbCoach)
    {
        var profile = new CoachProfileWrapper
        {
            name = dbCoach.coach_name,
            experience = dbCoach.experience,
            previousTeam = string.IsNullOrEmpty(dbCoach.prev_team) ? "Free Agent" : "Previous Team",
            championshipWins = dbCoach.championship_won,
            specialties = TransformToSpecialties(dbCoach),
            contractTerms = TransformToContractTerms(dbCoach)
        };

        return profile;
    }

    /// <summary>
    /// Convert database stats to top 4 specialties
    /// </summary>
    private SpecialtyEntry[] TransformToSpecialties(CoachDatabaseRecord dbCoach)
    {
        List<SpecialtyEntry> allSpecialties = new List<SpecialtyEntry>();

        switch (dbCoach.coach_type.ToUpper())
        {
            case "D": // Defense Coach
                if (dbCoach.run_defence > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Run Defense", value = CalculateBonus(dbCoach.run_defence) });
                if (dbCoach.pressure_control > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Pressure Control", value = CalculateBonus(dbCoach.pressure_control) });
                if (dbCoach.coverage_discipline > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Coverage Discipline", value = CalculateBonus(dbCoach.coverage_discipline) });
                if (dbCoach.turnover > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Turnover Generation", value = CalculateBonus(dbCoach.turnover) });
                break;

            case "O": // Offense Coach
                if (dbCoach.passing_efficiency > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Passing Efficiency", value = CalculateBonus(dbCoach.passing_efficiency) });
                if (dbCoach.rush > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Rushing Attack", value = CalculateBonus(dbCoach.rush) });
                if (dbCoach.red_zone_conversion > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Red Zone Conversion", value = CalculateBonus(dbCoach.red_zone_conversion) });
                if (dbCoach.play_variation > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Play Variation", value = CalculateBonus(dbCoach.play_variation) });
                break;

            case "S": // Special Teams Coach
                if (dbCoach.field_goal_accuracy > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Field Goal Accuracy", value = CalculateBonus(dbCoach.field_goal_accuracy) });
                if (dbCoach.kickoff_instance > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Kickoff Distance", value = CalculateBonus(dbCoach.kickoff_instance) });
                if (dbCoach.return_speed > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Return Speed", value = CalculateBonus(dbCoach.return_speed) });
                if (dbCoach.return_coverage > 0)
                    allSpecialties.Add(new SpecialtyEntry { key = "Return Coverage", value = CalculateBonus(dbCoach.return_coverage) });
                break;
        }

        // Sort by value and take top 4
        allSpecialties.Sort((a, b) => b.value.CompareTo(a.value));
        
        int count = Mathf.Min(4, allSpecialties.Count);
        SpecialtyEntry[] topSpecialties = new SpecialtyEntry[count];
        for (int i = 0; i < count; i++)
        {
            topSpecialties[i] = allSpecialties[i];
        }

        return topSpecialties;
    }

    /// <summary>
    /// Generate exactly 4 contract terms
    /// </summary>
    private ContractTermEntry[] TransformToContractTerms(CoachDatabaseRecord dbCoach)
    {
        float weeklySalary = (dbCoach.salary * 1000000f) / 52f;
        float performanceBonus = weeklySalary * 0.15f;
        float totalCost = weeklySalary * dbCoach.contract_length;

        return new ContractTermEntry[]
        {
            new ContractTermEntry { key = "Weekly Salary", value = $"${weeklySalary:N0}" },
            new ContractTermEntry { key = "Contract Length", value = $"{dbCoach.contract_length} games minimum" },
            new ContractTermEntry { key = "Performance Bonus", value = $"+${performanceBonus:N0} for 80%+ wins" },
            new ContractTermEntry { key = "Total Cost", value = $"${totalCost:N0}" }
        };
    }

    /// <summary>
    /// Calculate bonus percentage from database stat
    /// </summary>
    private int CalculateBonus(float statValue)
    {
        return Mathf.RoundToInt(Mathf.Clamp(statValue * 5f, 0f, 50f));
    }

    /// <summary>
    /// Create default coach when database fails
    /// </summary>
    private CoachProfileWrapper CreateDefaultCoach()
    {
        return new CoachProfileWrapper
        {
            name = "Default Coach",
            experience = 1,
            previousTeam = "None",
            championshipWins = 0,
            specialties = new SpecialtyEntry[] 
            {
                new SpecialtyEntry { key = "General Coaching", value = 15 }
            },
            contractTerms = new ContractTermEntry[]
            {
                new ContractTermEntry { key = "Weekly Salary", value = "$5,000" },
                new ContractTermEntry { key = "Contract Length", value = "4 games minimum" },
                new ContractTermEntry { key = "Performance Bonus", value = "+$750 for 80%+ wins" },
                new ContractTermEntry { key = "Total Cost", value = "$20,000" }
            }
        };
    }

    /// <summary>
    /// Populate UI with null safety
    /// </summary>
    private void PopulateUI()
    {
        // Set header texts with null checking
        if (coachNameText != null && coach != null)
            coachNameText.text = coach.name;

        // Set metadata texts with null checking
        if (experienceText != null && coach != null)
            experienceText.text = $"Experience: {coach.experience} years";
        if (previousTeamText != null && coach != null)
            previousTeamText.text = $"Previous Team: {coach.previousTeam}";
        if (championshipWinsText != null && coach != null)
            championshipWinsText.text = $"Championship Wins: {coach.championshipWins}";

        // Clear existing UI elements
        ClearUI();

        // Populate specialties with null checking
        if (coach?.specialties != null && specialityContainer != null && specialityRowPrefab != null)
        {
            foreach (var specialty in coach.specialties)
            {
                GameObject row = Instantiate(specialityRowPrefab, specialityContainer);
                TMP_Text keyText = row.transform.Find("Label")?.GetComponent<TMP_Text>();
                TMP_Text valueText = row.transform.Find("PercentText")?.GetComponent<TMP_Text>();
                RectTransform fillBar = row.transform.Find("ProgressBar")?.Find("ProgressBarFill")?.GetComponent<RectTransform>();

                if (keyText != null) keyText.text = specialty.key;
                if (valueText != null) valueText.text = specialty.value.ToString() + "%";

                if (fillBar != null)
                {
                    float maxValue = 80f;
                    float clampedPercent = Mathf.Clamp(specialty.value, 0, 100);
                    fillBar.sizeDelta = new Vector2(maxValue * (clampedPercent / 50f), fillBar.sizeDelta.y);
                }
            }
        }

        // Populate contract terms with null checking
        if (coach?.contractTerms != null && contractRowContainer != null && contractRowPrefab != null)
        {
            foreach (var term in coach.contractTerms)
            {
                GameObject row = Instantiate(contractRowPrefab, contractRowContainer);
                TMP_Text keyText = row.transform.Find("Label")?.GetComponent<TMP_Text>();
                TMP_Text valueText = row.transform.Find("Value")?.GetComponent<TMP_Text>();

                if (keyText != null) keyText.text = term.key;
                if (valueText != null) valueText.text = term.value;
            }
        }
    }

    /// <summary>
    /// Clear existing UI elements
    /// </summary>
    private void ClearUI()
    {
        if (specialityContainer != null)
        {
            foreach (Transform child in specialityContainer)
            {
                Destroy(child.gameObject);
            }
        }

        if (contractRowContainer != null)
        {
            foreach (Transform child in contractRowContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Testing controls for N and T keys
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadRandomCoach();
        }
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            LoadNextCoachType();
        }
    }

    /// <summary>
    /// Load random coach (N key)
    /// </summary>
    public void LoadRandomCoach()
    {
        if (!loadFromDatabase) return;

        try
        {
            var dbCoach = LoadRandomCoachFromDatabase();
            if (dbCoach != null)
            {
                coach = TransformToCoachProfile(dbCoach);
                ClearUI();
                PopulateUI();
                Debug.Log($"[CoachProfilePopulator] Loaded random coach: {coach.name}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[CoachProfilePopulator] Error loading random coach: {e.Message}");
        }
    }

    /// <summary>
    /// Load next coach type D→O→S→D (T key)
    /// </summary>
    public void LoadNextCoachType()
    {
        if (!loadFromDatabase) return;

        try
        {
            string[] types = { "D", "O", "S" };
            string currentType = "D"; // Default to Defense
            
            // Cycle through types
            int currentIndex = System.Array.IndexOf(types, currentType);
            currentIndex = (currentIndex + 1) % types.Length;
            currentType = types[currentIndex];

            var dbCoach = LoadCoachByType(currentType);
            if (dbCoach != null)
            {
                coach = TransformToCoachProfile(dbCoach);
                ClearUI();
                PopulateUI();
                string typeName = GetCoachTypeName(currentType);
                Debug.Log($"[CoachProfilePopulator] Loaded {typeName} coach: {coach.name}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[CoachProfilePopulator] Error loading coach type: {e.Message}");
        }
    }

    /// <summary>
    /// Load coach by specific type
    /// </summary>
    private CoachDatabaseRecord LoadCoachByType(string coachType)
    {
        var allCoaches = ExtractCoachesFromDatabase();
        var filteredCoaches = new List<CoachDatabaseRecord>();

        foreach (var coach in allCoaches)
        {
            if (coach.coach_type.Equals(coachType, StringComparison.OrdinalIgnoreCase))
            {
                filteredCoaches.Add(coach);
            }
        }

        if (filteredCoaches.Count == 0) return null;

        int randomIndex = UnityEngine.Random.Range(0, filteredCoaches.Count);
        return filteredCoaches[randomIndex];
    }

    /// <summary>
    /// Get coach type display name
    /// </summary>
    private string GetCoachTypeName(string coachType)
    {
        switch (coachType.ToUpper())
        {
            case "D": return "Defense";
            case "O": return "Offense";
            case "S": return "Special Teams";
            default: return "Unknown";
        }
    }
}