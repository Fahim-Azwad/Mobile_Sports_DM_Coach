using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
public class SpecialtyEntry
{
    public string key;
    public int value;
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

    // Start is called before the first frame update
    void Start()
    {
        // Set header texts
        coachNameText.text = coach.name;

        // Set metadata texts
        experienceText.text = $"Experience: {coach.experience} years";
        previousTeamText.text = $"Previous Team: {coach.previousTeam}";
        championshipWinsText.text = $"Championship Wins: {coach.championshipWins}";

        // Populate specialties
        foreach (var specialty in coach.specialties)
        {
            GameObject row = Instantiate(specialityRowPrefab, specialityContainer);
            TMP_Text keyText = row.transform.Find("Label").GetComponent<TMP_Text>();
            TMP_Text valueText = row.transform.Find("PercentText").GetComponent<TMP_Text>();
            RectTransform fillBar = row.transform.Find("ProgressBar").Find("ProgressBarFill").GetComponent<RectTransform>();

            keyText.text = specialty.key;
            valueText.text = specialty.value.ToString() + "%";

            float maxValue = 80f; // Assuming max value is 100%
            float clampedPercent = Mathf.Clamp(specialty.value, 0, 100);
            fillBar.sizeDelta = new Vector2(maxValue * (clampedPercent / 50f), fillBar.sizeDelta.y);
        }

        // Populate contract terms
        foreach (var term in coach.contractTerms)
        {
            GameObject row = Instantiate(contractRowPrefab, contractRowContainer);
            TMP_Text keyText = row.transform.Find("Label").GetComponent<TMP_Text>();
            TMP_Text valueText = row.transform.Find("Value").GetComponent<TMP_Text>();

            keyText.text = term.key;
            valueText.text = term.value;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
