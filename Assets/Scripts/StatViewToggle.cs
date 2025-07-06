using UnityEngine;

public class StatViewToggle : MonoBehaviour
{
    public GameObject coachingStatsContainer;
    public GameObject weeklyBreakdownContainer;

    void Start()
    {
        // Initial visibility
        coachingStatsContainer.SetActive(true);
        weeklyBreakdownContainer.SetActive(false);
    }

    public void ShowCoachingStats()
    {
        coachingStatsContainer.SetActive(true);
        weeklyBreakdownContainer.SetActive(false);
    }

    public void ShowWeeklyStats()
    {
        coachingStatsContainer.SetActive(false);
        weeklyBreakdownContainer.SetActive(true);
    }
}