
using UnityEngine;

[CreateAssetMenu(fileName = "New Coach", menuName = "FMG/Coach Data")]
public class CoachData : ScriptableObject
{
    [Header("Basic Information")]
    public string coachName;
    public CoachType position;
    public Sprite coachPortrait;

    [Header("Contract Details")]
    [Range(1000, 50000)]
    public int weeklySalary = 5000;

    [Range(1, 5)]
    public int starRating = 3;

    [Header("Performance Bonuses")]
    [Range(0, 50)]
    public int offenseBonus = 0;

    [Range(0, 50)]
    public int defenseBonus = 0;

    [Range(0, 50)]
    public int specialTeamsBonus = 0;

    [Header("Coach Personality")]
    [TextArea(3, 5)]
    public string coachDescription;

    [Header("Dynamic Data - Runtime Only")]
    [System.NonSerialized]
    public float currentPerformance = 1.0f;

    [System.NonSerialized]
    public int gamesCoached = 0;

    [System.NonSerialized]
    public bool isHired = false;

    // Effective bonuses based on performance
    

    public CoachType type;

    // Calculated properties
    public int TotalBonus => offenseBonus + defenseBonus + specialTeamsBonus;
    public bool IsSpecialist => TotalBonus > 0 && GetSpecialtyCount() == 1;

    private int GetSpecialtyCount()
    {
        int count = 0;
        if (offenseBonus > 0) count++;
        if (defenseBonus > 0) count++;
        if (specialTeamsBonus > 0) count++;
        return count;
    }

    // Validation
    private void OnValidate()
    {
        // Ensure coach has at least one specialty
        if (TotalBonus == 0)
        {
            Debug.LogWarning($"Coach {coachName} has no bonuses assigned!");
        }

        // Validate salary ranges
        if (weeklySalary < 1000)
            weeklySalary = 1000;
    }


    public int GetEffectiveDefenseBonus()
    {
        return Mathf.RoundToInt(defenseBonus * currentPerformance);
    }

    public int GetEffectiveOffenseBonus()
    {
        return Mathf.RoundToInt(offenseBonus * currentPerformance);
    }

    public int GetEffectiveSpecialBonus()
    {
        return Mathf.RoundToInt(specialTeamsBonus * currentPerformance);
    }

}

public enum CoachType
{
    Offense,
    Defense,
    SpecialTeams
}


