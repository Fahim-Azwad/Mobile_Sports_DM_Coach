using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

public class CoachManager : MonoBehaviour
{
    public static CoachManager instance;

    [Header("Available Coaches")]
    public List<CoachData> allCoaches = new List<CoachData>();

    [Header("Current Staff - Defense and Offense Only")]
    public CoachData defenseCoach;
    public CoachData offenseCoach;
    public CoachData SpecialCoach;

    private void Awake()
    {
        // If an instance already exists and it's not this one, destroy this one.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // This is the one and only instance.
        instance = this;
}


    // Events
    public static event Action<CoachData, CoachType> OnCoachHired;
    public static event Action<CoachType> OnCoachFired;

    private void Start()
    {
        InitializeSystem();
    }

    private void InitializeSystem()
    {
        // Load coaches from Resources
        LoadCoaches();
    }

    private void LoadCoaches()
    {
        // Load from ScriptableObject Resources (static coaches)
        CoachData[] coaches = Resources.LoadAll<CoachData>("Coaches");
        allCoaches.Clear();
        allCoaches.AddRange(coaches);

        // Also load from database if available
        LoadCoachesFromDatabase();

        Debug.Log($"Loaded {allCoaches.Count} coaches total");
    }
    
    private void LoadCoachesFromDatabase()
    {
        string databasePath = Path.Combine(Application.streamingAssetsPath, "Database", "coach.json");
        
        if (!File.Exists(databasePath))
        {
            Debug.Log("No database file found, using only ScriptableObject coaches");
            return;
        }

        try
        {
            string jsonContent = File.ReadAllText(databasePath);
            JsonWrapper wrapper = JsonUtility.FromJson<JsonWrapper>("{\"Items\":" + jsonContent + "}");
            
            if (wrapper?.Items != null)
            {
                foreach (var dbRecord in wrapper.Items)
                {
                    // Convert database record to CoachData
                    CoachData coachData = CoachData.CreateFromDatabaseRecord(dbRecord);
                    allCoaches.Add(coachData);
                }
                Debug.Log($"Loaded {wrapper.Items.Length} coaches from database");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load coaches from database: {e.Message}");
        }
    }

    public bool HireCoach(CoachData coach)
    {
        if (coach == null || coach.isHired)
        {
            Debug.Log("Coach is null or already hired");
            return false;
        }
        // Only handle Defense and Offense for now
        if (coach.position != CoachType.Defense && coach.position != CoachType.Offense)
        {
            Debug.Log("Only Defense and Offense coaches supported currently");
            return false;
        }
        // Fire existing coach of same type if any
        if (coach.position == CoachType.Defense && defenseCoach != null)
        {
            FireCoach(CoachType.Defense);
        }
        else if (coach.position == CoachType.Offense && offenseCoach != null)
        {
            FireCoach(CoachType.Offense);
        }
        // Hire new coach
        coach.isHired = true;
        //coach.contractWeeksRemaining = 
        if (coach.position == CoachType.Defense)
        {
            defenseCoach = coach;
        }
        else if (coach.position == CoachType.Offense)
        {
            offenseCoach = coach;
        }
        OnCoachHired?.Invoke(coach, coach.position);
        Debug.Log($"Hired {coach.coachName} for {coach.position}");
        return true;
    }

    public bool FireCoach(CoachType position)
    {
        CoachData coachToFire = null;
        CoachSlotUI slotToUpdate = null;

        if (position == CoachType.Defense)
        {
            coachToFire = defenseCoach;
            defenseCoach = null;
        }
        else if (position == CoachType.Offense)
        {
            coachToFire = offenseCoach;
            offenseCoach = null;
        }

        if (coachToFire == null)
        {
            Debug.Log($"No coach to fire for {position}");
            return false;
        }

        // Reset coach status
        coachToFire.isHired = false;

        //coachToFire.weeksEmployed = 0;

        // Update UI
        if (slotToUpdate != null)
            slotToUpdate.UpdateDisplay(null);

        OnCoachFired?.Invoke(position);
        Debug.Log($"Fired {coachToFire.coachName} from {position}");
        return true;
    }

    public List<CoachData> GetAvailableCoaches()
    {
        List<CoachData> available = new List<CoachData>();

        foreach (CoachData coach in allCoaches)
        {
            if (!coach.isHired)
            {
                // Only show Defense and Offense coaches for now
                if (coach.position == CoachType.Defense || coach.position == CoachType.Offense)
                {
                    available.Add(coach);
                }
            }
        }
        return available;
    }

    public List<CoachData> GetAvailableCoachesByType(CoachType type)
    {
        List<CoachData> available = new List<CoachData>();

        foreach (CoachData coach in allCoaches)
        {
            if (!coach.isHired && coach.position == type)
            {
                // Only Defense and Offense for now
                if (type == CoachType.Defense || type == CoachType.Offense)
                {
                    available.Add(coach);
                }
            }
        }

        return available;
    }

    public CoachData GetCoachByType(CoachType type)
    {
        switch (type)
        {
            case CoachType.Defense:
                return defenseCoach;
            case CoachType.Offense:
                return offenseCoach;
            default:
                return null;
        }
    }

    public bool HasCoachForPosition(CoachType position)
    {
        return GetCoachByType(position) != null;
    }

    // Get current team bonuses (Defense and Offense only)
    public TeamBonus GetCurrentTeamBonus()
    {
        TeamBonus bonus = new TeamBonus();

        if (defenseCoach != null)
            bonus.defenseBonus = defenseCoach.GetEffectiveDefenseBonus();

        if (offenseCoach != null)
            bonus.offenseBonus = offenseCoach.GetEffectiveOffenseBonus();

        return bonus;
    }
}

[System.Serializable]
public class TeamBonus
{
    public int offenseBonus;
    public int defenseBonus;
    public int specialTeamsBonus; // Keep for future use
    public int TotalBonus => offenseBonus + defenseBonus + specialTeamsBonus;
}
