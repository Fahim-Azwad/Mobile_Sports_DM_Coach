using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class StatEntry
{
    public string stat;
    public Sprite icon;
    public int beforeValue; // Value before change
    public int afterValue;  // Value after change
}