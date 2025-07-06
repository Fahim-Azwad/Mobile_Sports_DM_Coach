using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class WeeklySummaryData
{
    public int weeklyInvestment;
    public float performanceGainPercent;
    public int playoffBonus;
}

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
        investmentLine.text = $"Weekly Investment: ${summaryData.weeklyInvestment:N0} | Performance Gain: +{summaryData.performanceGainPercent}%";
        bonusLine.text = $"Playoff Bonus Potential: ${summaryData.playoffBonus:N0}";

        bool isProfitable = summaryData.performanceGainPercent > 0 && summaryData.playoffBonus > 0;

        conclusionLine.text = isProfitable
            ? "COACHING IS PROFITABLE"
            : "COACHING IS NOT PROFITABLE";

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
}