using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoachHiringMarket : MonoBehaviour
{
    public CoachData assignedCoach1;
    public CoachData assignedCoach2;

    [Header("UI States")]
    public GameObject emptyState;  // The "Empty" GameObject
    public GameObject hiredState;  // The "Hired" GameObject with Name, Salary, Rating, etc.

    [Header("Hired State Elements 1")]
    public TextMeshProUGUI nameText1;
    public TextMeshProUGUI salaryText1;
    public TextMeshProUGUI ratingText1;
    public TextMeshProUGUI Type1;
    public Button viewCoachButton1;

    [Header("Hired State Elements 2")]
    public TextMeshProUGUI nameText2;
    public TextMeshProUGUI salaryText2;
    public TextMeshProUGUI ratingText2;
    public TextMeshProUGUI Type2;
    public Button viewCoachButton2;

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
        UpdateHiredStateDisplay1(assignedCoach1);
    }
    private void Update()
    {
        UpdateHiredStateDisplay1(assignedCoach1);
        UpdateHiredStateDisplay2(assignedCoach2);
    }

    public void Initialize(CoachType type)
    {
        slotType = type;
    }



    private void UpdateHiredStateDisplay1(CoachData coach)
    {
        if (nameText1 != null)
            nameText1.text = "Name : " + coach.coachName;

        if (salaryText1 != null)
            salaryText1.text = "Salary : " + $"${coach.weeklySalary:N0}/wk";

        if (ratingText1 != null)
            ratingText1.text = "Rating : " + $"{coach.starRating} Stars";

        if (Type1 != null)
            Type1.text = "Type: " + $"{coach.type}";
    }
    private void UpdateHiredStateDisplay2(CoachData coach)
    {
        if (nameText2 != null)
            nameText2.text = "Name : " + coach.coachName;

        if (salaryText2 != null)
            salaryText2.text = "Salary : " + $"${coach.weeklySalary:N0}/wk";

        if (ratingText2 != null)
            ratingText2.text = "Rating : " + $"{coach.starRating} Stars";

        if (Type2 != null)
            Type2.text = "Type: " + $"{coach.type}";
    }


    public void HireCoach1() {
        CoachManager.instance.HireCoach(assignedCoach1);
    }
    public void HireCoach2()
    {
        CoachManager.instance.HireCoach(assignedCoach2);
    }
}
