using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoachSlotUI : MonoBehaviour
{
    [Header("UI States")]
    public GameObject emptyState;  // The "Empty" GameObject
    public GameObject hiredState;  // The "Hired" GameObject with Name, Salary, Rating, etc.

    [Header("Hired State Elements")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI salaryText;
    public TextMeshProUGUI ratingText;
    public TextMeshProUGUI typeText;
    public Button viewCoachButton;
    public Button fireCoachButton;

    [Header("Empty State Elements")]
    public Button hireCoachButton;

    private CoachData currentCoach;
    private CoachType slotType;
   // private CoachManager coachManager;

    private void Start()
    {
        //coachManager = FindObjectOfType<CoachManager>();
/*
        // Setup buttons
        if (viewCoachButton != null)
           // viewCoachButton.onClick.AddListener(ViewCoachDetails);
        if (fireCoachButton != null)
            fireCoachButton.onClick.AddListener(FireCoach);
        if (hireCoachButton != null)
            hireCoachButton.onClick.AddListener(OpenHiringMarket);*/
    }

    public void Initialize(CoachType type)
    {
        slotType = type;
        UpdateDisplay(null); // Start with empty state
    }

    public void UpdateDisplay(CoachData coach)
    {
        currentCoach = coach;

        if (coach == null)
        {
            // Show empty state
            emptyState.SetActive(true);
            hiredState.SetActive(false);
        }
        else
        {
            // Show hired state
            emptyState.SetActive(false);
            hiredState.SetActive(true);

            // Update hired state UI
            UpdateHiredStateDisplay(coach);
        }
    }

    private void UpdateHiredStateDisplay(CoachData coach)
    {
        if (nameText != null)
            nameText.text = coach.coachName;

        if (salaryText != null)
            salaryText.text = $"${coach.weeklySalary:N0}/wk";

        if (ratingText != null)
            ratingText.text = $"{coach.starRating} Stars";

        if (typeText != null)
            typeText.text = coach.position.ToString();
    }


/*
    private void FireCoach()
    {
        if (currentCoach != null && coachManager != null)
        {
            coachManager.FireCoach(slotType);
        }
    }*/

}
