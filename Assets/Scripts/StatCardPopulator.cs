using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

public class StatCardPopulator : MonoBehaviour
{
    [Header("Prefabs & Containers")]
    public GameObject statRowPrefab;
    public Transform beforeContainer;
    public Transform afterContainer;
    public Transform weeklyBreakdownContainer;
    public GameObject weeklyTextPrefab;

    [Header("Sprites")]
    public Sprite arrowUp;
    public Sprite arrowDown;

    [Header("Data")]
    public List<StatEntry> stats = new();
    public List<string> weeklyBreakdownLines = new();
    public WeeklySummaryData summaryData = new();

    [Header("Weekly Summary Texts")]
    public TMP_Text investmentLine;
    public TMP_Text bonusLine;
    public TMP_Text conclusionLine;

    // Configurable sizes
    [Header("Visual Settings")]
    public Vector2 iconSize = new Vector2(20, 20);
    public Vector2 arrowSize = new Vector2(20, 20);
    public float labelFontSize = 16f;
    public float valueFontSize = 16f;
    public float weeklyFontSize = 14f;

    void Start()
    {
        LoadPerformanceDataFromDatabase();
        PopulateUI();
    }

    /// <summary>
    /// ELT Pattern: Extract → Load → Transform performance data from database
    /// Following CoachingStats_v4.cs ROI approach with Dhruv's clean structure
    /// </summary>
    public void LoadPerformanceDataFromDatabase()
    {
        // Extract team and coach data from database
        var teamData = ExtractTeamDataFromDatabase();
        var coachData = ExtractCoachDataFromDatabase();
        
        // Load and Transform data into UI format
        stats = TransformToPerformanceStats(teamData, coachData);
        weeklyBreakdownLines = CreateWeeklyBreakdown(teamData, coachData);
        summaryData = CreateSummaryData(teamData, coachData);
        
        Debug.Log($"[StatCardPopulator] Loaded {stats.Count} performance stats from database");
    }

    /// <summary>
    /// Extract team data from database
    /// </summary>
    private TeamDatabaseRecord ExtractTeamDataFromDatabase()
    {
        try
        {
            string jsonPath = Path.Combine(Application.streamingAssetsPath, "Database", "team.json");
            
            if (!File.Exists(jsonPath))
            {
                Debug.LogWarning("[StatCardPopulator] Team database not found, using defaults");
                return CreateDefaultTeamRecord();
            }

            string jsonContent = File.ReadAllText(jsonPath);
            string wrappedJson = $"{{\"Items\":{jsonContent}}}";
            var wrapper = JsonUtility.FromJson<TeamJsonWrapper>(wrappedJson);
            
            if (wrapper?.Items != null && wrapper.Items.Length > 0)
            {
                return wrapper.Items[0]; // Get first team
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[StatCardPopulator] Failed to load team data: {e.Message}");
        }
        
        return CreateDefaultTeamRecord();
    }

    /// <summary>
    /// Extract coach data from database
    /// </summary>
    private List<CoachDatabaseRecord> ExtractCoachDataFromDatabase()
    {
        try
        {
            string jsonPath = Path.Combine(Application.streamingAssetsPath, "Database", "coach.json");
            
            if (!File.Exists(jsonPath))
            {
                Debug.LogWarning("[StatCardPopulator] Coach database not found");
                return new List<CoachDatabaseRecord>();
            }

            string jsonContent = File.ReadAllText(jsonPath);
            string wrappedJson = $"{{\"Items\":{jsonContent}}}";
            var wrapper = JsonUtility.FromJson<JsonWrapper>(wrappedJson);
            
            if (wrapper?.Items != null)
            {
                return new List<CoachDatabaseRecord>(wrapper.Items);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[StatCardPopulator] Failed to load coach data: {e.Message}");
        }
        
        return new List<CoachDatabaseRecord>();
    }

    /// <summary>
    /// Transform database data into performance statistics
    /// Following CoachingStats_v4.cs methodology for ROI calculation
    /// </summary>
    private List<StatEntry> TransformToPerformanceStats(TeamDatabaseRecord team, List<CoachDatabaseRecord> coaches)
    {
        var performanceStats = new List<StatEntry>();

        // 🏆 Win Rate Performance (inspired by CoachingStats_v4.GetWinRateDelta)
        int baselineWinRate = 42; // Baseline without coaching
        int currentWinRate = CalculateCurrentWinRate(team, coaches);
        
        performanceStats.Add(new StatEntry
        {
            stat = "🏆 Win Rate %",
            beforeValue = baselineWinRate,
            afterValue = currentWinRate
        });

        // 🏈 Offensive Rating
        int baselineOffense = 40;
        int currentOffense = CalculateOffenseRating(team, coaches);
        
        performanceStats.Add(new StatEntry
        {
            stat = "🏈 Offense Rating",
            beforeValue = baselineOffense,
            afterValue = currentOffense
        });

        // 🛡️ Defensive Rating  
        int baselineDefense = 38;
        int currentDefense = CalculateDefenseRating(team, coaches);
        
        performanceStats.Add(new StatEntry
        {
            stat = "🛡️ Defense Rating",
            beforeValue = baselineDefense,
            afterValue = currentDefense
        });

        // 🚀 Special Teams Rating
        int baselineSpecialTeams = 35;
        int currentSpecialTeams = CalculateSpecialTeamsRating(team, coaches);
        
        performanceStats.Add(new StatEntry
        {
            stat = "🚀 Special Teams",
            beforeValue = baselineSpecialTeams,
            afterValue = currentSpecialTeams
        });

        // 💰 Weekly Investment
        int baselineInvestment = 0;
        int currentInvestment = CalculateWeeklyInvestment(coaches);
        
        performanceStats.Add(new StatEntry
        {
            stat = "💰 Weekly Investment",
            beforeValue = baselineInvestment,
            afterValue = currentInvestment
        });

        return performanceStats;
    }

    /// <summary>
    /// Create weekly breakdown with emoji support
    /// </summary>
    private List<string> CreateWeeklyBreakdown(TeamDatabaseRecord team, List<CoachDatabaseRecord> coaches)
    {
        var lines = new List<string>();
        
        lines.Add($"Week 1 📋 {team.team_name} baseline established");
        lines.Add($"Week 2 💰 Budget: ${team.budget:F1}M allocated");
        
        int week = 3;
        foreach (var coach in coaches.Take(4)) // Show up to 4 coaches
        {
            string emoji = GetCoachTypeEmoji(coach.coach_type);
            int impact = CalculateCoachImpact(coach);
            lines.Add($"Week {week} {emoji} {coach.coach_name}: +{impact}% boost");
            week++;
        }
        
        // Add performance summary
        if (coaches.Count > 0)
        {
            float avgRating = coaches.Average(c => c.overall_rating);
            int totalImpact = Mathf.RoundToInt((avgRating - 2.5f) * 15f);
            lines.Add($"Week {week} 📈 Combined Impact: +{totalImpact}%");
        }
        
        return lines;
    }

    /// <summary>
    /// Create summary data following CoachingStats_v4.cs ROI methodology
    /// </summary>
    private WeeklySummaryData CreateSummaryData(TeamDatabaseRecord team, List<CoachDatabaseRecord> coaches)
    {
        int weeklyInvestment = CalculateWeeklyInvestment(coaches);
        float performanceGain = CalculatePerformanceGain(coaches);
        int playoffBonus = CalculatePlayoffBonus(team, coaches);

        return new WeeklySummaryData
        {
            weeklyInvestment = weeklyInvestment,
            performanceGainPercent = performanceGain,
            playoffBonus = playoffBonus
        };
    }

    /// <summary>
    /// Dhruv's original clean UI population method
    /// </summary>
    void PopulateUI()
    {
        foreach (var stat in stats)
        {
            // === BEFORE ROW ===
            GameObject beforeRow = Instantiate(statRowPrefab, beforeContainer);
            SetRowUI(beforeRow, stat.stat, stat.icon, stat.beforeValue, null);

            // === AFTER ROW ===
            GameObject afterRow = Instantiate(statRowPrefab, afterContainer);

            Sprite arrowSprite = null;
            if (stat.afterValue > stat.beforeValue)
                arrowSprite = arrowUp;
            else if (stat.afterValue < stat.beforeValue)
                arrowSprite = arrowDown;

            SetRowUI(afterRow, stat.stat, stat.icon, stat.afterValue, arrowSprite);
        }

        foreach (string line in weeklyBreakdownLines)
        {
            GameObject entry = Instantiate(weeklyTextPrefab, weeklyBreakdownContainer);

            // Parse the line into: "Week 5" and "Mike +12% Rush"
            string[] parts = line.Split(' ', 3);
            string weekPart = parts.Length >= 2 ? $"{parts[0]} {parts[1]}" : line;
            string contentPart = parts.Length == 3 ? parts[2] : "";

            // Assign text components
            TMP_Text weekText = entry.transform.Find("WeekLabel").GetComponent<TMP_Text>();
            TMP_Text contentText = entry.transform.Find("ContentLabel").GetComponent<TMP_Text>();

            weekText.text = weekPart;
            contentText.text = contentPart;

            // Set font size
            weekText.fontSize = weeklyFontSize;
            contentText.fontSize = weeklyFontSize;

            // Optional: bold for the week label
            weekText.fontStyle = FontStyles.Bold;

            // Optional layout element control (for the row itself)
            LayoutElement layout = entry.GetComponent<LayoutElement>();
            if (layout != null)
            {
                layout.preferredWidth = 400f;
                layout.preferredHeight = 40f;
            }
            else
            {
                RectTransform rt = entry.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(400f, 40f);
            }
        }

        // Populate summary section
        investmentLine.text = $"Weekly Investment: ${summaryData.weeklyInvestment:N0} | Performance Gain: +{summaryData.performanceGainPercent:F1}%";
        bonusLine.text = $"Playoff Bonus Potential: ${summaryData.playoffBonus:N0}";

        bool isProfitable = summaryData.performanceGainPercent > 0 && summaryData.playoffBonus > 0;

        conclusionLine.text = isProfitable
            ? "✅ COACHING IS PROFITABLE"
            : "❌ COACHING IS NOT PROFITABLE";

        conclusionLine.color = isProfitable ? Color.green : Color.red;
    }

    void SetRowUI(GameObject row, string label, Sprite icon, int value, Sprite arrowSprite)
    {
        // Set label text + font size
        var labelText = row.transform.Find("Stat").GetComponent<TMP_Text>();
        labelText.text = label;
        labelText.fontSize = labelFontSize;

        // Set value text + font size
        var valueText = row.transform.Find("Number").GetComponent<TMP_Text>();
        valueText.text = value.ToString();
        valueText.fontSize = valueFontSize;

        // Set icon + size
        var iconImg = row.transform.Find("Icon").GetComponent<Image>();
        iconImg.sprite = icon;
        iconImg.rectTransform.sizeDelta = iconSize;

        // Handle arrow logic (optional)
        var arrowObj = row.transform.Find("UpDown");
        if (arrowObj != null)
        {
            var arrowImage = arrowObj.GetComponent<Image>();
            if (arrowSprite != null)
            {
                arrowImage.sprite = arrowSprite;
                arrowImage.rectTransform.sizeDelta = arrowSize;
                arrowObj.gameObject.SetActive(true);
            }
            else
            {
                arrowObj.gameObject.SetActive(false);
            }
        }
    }

    #region Performance Calculation Methods (Following CoachingStats_v4.cs)

    /// <summary>
    /// Calculate current win rate based on team and coaching improvements
    /// </summary>
    private int CalculateCurrentWinRate(TeamDatabaseRecord team, List<CoachDatabaseRecord> coaches)
    {
        int baselineWinRate = 42;
        float teamBonus = team.overall_rating * 8f; // Team quality contributes
        float coachBonus = coaches.Sum(c => (c.overall_rating - 2.5f) * 6f); // Coach improvement
        
        int currentWinRate = Mathf.RoundToInt(baselineWinRate + teamBonus + coachBonus);
        return Mathf.Clamp(currentWinRate, 0, 95); // Cap at realistic values
    }

    /// <summary>
    /// Calculate offense rating based on team and offensive coaches
    /// </summary>
    private int CalculateOffenseRating(TeamDatabaseRecord team, List<CoachDatabaseRecord> coaches)
    {
        int baseRating = 40;
        float teamContribution = team.offence_rating * 12f;
        
        var offenseCoaches = coaches.Where(c => c.coach_type?.ToUpper() == "O").ToList();
        float coachContribution = offenseCoaches.Sum(c => c.overall_rating * 8f);
        
        int totalRating = Mathf.RoundToInt(baseRating + teamContribution + coachContribution);
        return Mathf.Clamp(totalRating, 0, 90);
    }

    /// <summary>
    /// Calculate defense rating based on team and defensive coaches
    /// </summary>
    private int CalculateDefenseRating(TeamDatabaseRecord team, List<CoachDatabaseRecord> coaches)
    {
        int baseRating = 38;
        float teamContribution = team.defence_rating * 12f;
        
        var defenseCoaches = coaches.Where(c => c.coach_type?.ToUpper() == "D").ToList();
        float coachContribution = defenseCoaches.Sum(c => c.overall_rating * 8f);
        
        int totalRating = Mathf.RoundToInt(baseRating + teamContribution + coachContribution);
        return Mathf.Clamp(totalRating, 0, 90);
    }

    /// <summary>
    /// Calculate special teams rating based on team and special teams coaches
    /// </summary>
    private int CalculateSpecialTeamsRating(TeamDatabaseRecord team, List<CoachDatabaseRecord> coaches)
    {
        int baseRating = 35;
        float teamContribution = team.special_teams_rating * 12f;
        
        var specialTeamsCoaches = coaches.Where(c => c.coach_type?.ToUpper() == "S").ToList();
        float coachContribution = specialTeamsCoaches.Sum(c => c.overall_rating * 8f);
        
        int totalRating = Mathf.RoundToInt(baseRating + teamContribution + coachContribution);
        return Mathf.Clamp(totalRating, 0, 90);
    }

    /// <summary>
    /// Calculate total weekly investment in coaching
    /// </summary>
    private int CalculateWeeklyInvestment(List<CoachDatabaseRecord> coaches)
    {
        float totalSalary = coaches.Sum(c => c.salary);
        float weeklyInvestment = (totalSalary * 1000000f) / 52f; // Convert to weekly
        return Mathf.RoundToInt(weeklyInvestment);
    }

    /// <summary>
    /// Calculate performance gain percentage (following CoachingStats_v4.GetWinRateDelta)
    /// </summary>
    private float CalculatePerformanceGain(List<CoachDatabaseRecord> coaches)
    {
        if (coaches.Count == 0) return 0f;
        
        float avgRating = coaches.Average(c => c.overall_rating);
        float performanceGain = (avgRating - 2.5f) * 12f; // Scale improvement
        
        return Mathf.Max(0f, performanceGain);
    }

    /// <summary>
    /// Calculate playoff bonus potential (following CoachingStats_v4.GetPerformanceROI)
    /// </summary>
    private int CalculatePlayoffBonus(TeamDatabaseRecord team, List<CoachDatabaseRecord> coaches)
    {
        float performanceGain = CalculatePerformanceGain(coaches);
        float teamFactor = team.budget * 1000f; // Budget affects bonus potential
        float bonusMultiplier = (performanceGain / 100f) * teamFactor;
        
        return Mathf.RoundToInt(Mathf.Max(0f, bonusMultiplier));
    }

    /// <summary>
    /// Calculate individual coach impact
    /// </summary>
    private int CalculateCoachImpact(CoachDatabaseRecord coach)
    {
        float impact = (coach.overall_rating - 2.5f) * 8f; // Scale from 2.5-5.0 to percentage
        return Mathf.RoundToInt(Mathf.Max(0f, impact));
    }

    /// <summary>
    /// Get emoji for coach type
    /// </summary>
    private string GetCoachTypeEmoji(string coachType)
    {
        switch (coachType?.ToUpper())
        {
            case "D": return "🛡️";
            case "O": return "🏈";
            case "S": return "🚀";
            default: return "👤";
        }
    }

    /// <summary>
    /// Create default team record for fallback
    /// </summary>
    private TeamDatabaseRecord CreateDefaultTeamRecord()
    {
        return new TeamDatabaseRecord
        {
            team_name = "Default Team",
            league = "Practice League",
            budget = 25.0f,
            overall_rating = 2.5f,
            defence_rating = 2.5f,
            offence_rating = 2.5f,
            special_teams_rating = 2.5f,
            description = "Default team for testing"
        };
    }

    #endregion
}