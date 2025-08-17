# 👤 Coach Profile/Details Screen

*Complete guide for individual coach details and information display*

## 🎮 **Navigation & Controls**

### **Quick Access Shortcuts** (After Quick Start section)
- **F** - Toggle between JSON and API mode (switches data source)
- **N** - Load random coach from database/API  
- **T** - Load next coach by type (Defense → Offense → Special Teams → Defense)

*Note: No UI navigation buttons found in current implementation - navigation appears to be through keyboard shortcuts only*

## 📍 **Scene Information**
- **Scene File**: `Assets/Scenes/FMGCOACH.unity`
- **Screen Location**: Coach Details screen within main scene
- **Primary Script**: `CoachProfilePopulator.cs`
- **Purpose**: Detailed individual coach information and statistics
- **Navigation**: Accessible from main Coach Hiring Market interface

## 🚀 **Quick Start**

1. **Open Scene**: Load `FMGCOACH.unity` in Unity
2. **Add Required Components**: Ensure CoachProfilePopulator script is attached to a GameObject
3. **Configure UI References**: Assign all UI elements in the CoachProfilePopulator inspector
4. **Set Coach Display Mode**: 
   - Enable "Load From Database" for dynamic coach loading
   - Choose "Use API" for live data or disable for JSON fallback
5. **Play Scene**: Coach profile loads automatically
6. **Test Profile Display**: Use keyboard shortcuts F/N/T for testing different coaches

### **Essential Script Setup**
- **CoachProfilePopulator**: The ONLY script needed for coach profile functionality
  - Handles coach data loading from API/JSON
  - Manages UI population and display
  - Provides keyboard shortcuts for testing (F/N/T)

### **Coach Profile Controls**

#### **CoachProfilePopulator Controls (F/N/T)** - Built into the main script
- **F** - Toggle between API and JSON data modes
  - Switches between live API data and local JSON fallback
  - Useful for testing offline functionality
- **N** - Load random coach from database/API  
  - Loads a random coach for testing different profiles
  - Uses currently selected data source (API or JSON)
- **T** - Cycle through coach types (Defense → Offense → Special Teams → Defense)
  - Loads the next coach of a different type
  - Useful for testing different coach specializations

**Note**: These are the ONLY controls for the Coach Profile screen. Other testing scripts (CoachPreloadTester, CoachDebugHelper) are for different screens and don't affect coach profile functionality.

### **Script Dependencies**
**Essential Scripts:**
- **CoachProfilePopulator**: The complete coach profile system
  - Handles all coach data loading (API/JSON)
  - Manages UI population and display  
  - Provides built-in testing controls (F/N/T)
  - Requires no additional scripts to function

**Backend Dependencies:**
- **CoachManager**: Must exist in scene for team context (optional)
- **API Backend**: For live coach data (optional - has JSON fallback)

**UI Requirements:**
- Various UI Text/Image components assigned in inspector
- Coach photo loading system
- Stat display containers and prefabs

### **Quick Access Shortcuts**
- **F** - Toggle between API and JSON data modes
- **N** - Load random coach from database/API  
- **T** - Cycle through coach types (Defense → Offense → Special Teams → Defense)

*These controls are built into CoachProfilePopulator and require no additional scripts*

## 👤 **Coach Profile Display**

### **Core Information**
The profile shows comprehensive coach details:

#### **Basic Information**
- **Coach Name**: Full name and nickname
- **Photo**: Professional headshot (64x64 to 256x256)
- **Coach Type**: Defense/Offense/Special Teams designation
- **Overall Rating**: 1-5 star rating system
- **Experience**: Years of coaching experience
- **Current Team**: Team assignment status

#### **Financial Information**
- **Annual Salary**: Full yearly compensation
- **Weekly Cost**: Operational expense per week
- **Contract Length**: Duration of current contract
- **Bonus Structure**: Performance-based incentives

### **Detailed Statistics**

#### **Defensive Coach Stats**
```csharp
// Primary defensive specialties (0-10 scale converted to 0-50% bonuses)
- Run Defense: 8.5/10 → +42.5% run stopping
- Pressure Control: 7.2/10 → +36% pass rush
- Coverage Discipline: 9.1/10 → +45.5% pass coverage
- Turnover Generation: 6.8/10 → +34% forced turnovers
```

#### **Offensive Coach Stats**
```csharp
// Primary offensive specialties 
- Passing Efficiency: 7.5/10 → +37.5% passing game
- Rush Attack: 8.2/10 → +41% running game
- Red Zone Conversion: 9.0/10 → +45% scoring efficiency
- Play Variation: 6.5/10 → +32.5% unpredictability
```

#### **Special Teams Coach Stats**
```csharp
// Special teams specialties
- Field Goal Accuracy: 8.8/10 → +44% kicking success
- Kickoff Distance: 7.3/10 → +36.5% field position
- Return Speed: 8.1/10 → +40.5% return yards
- Return Coverage: 7.9/10 → +39.5% coverage units
```

## 🔧 **System Setup**

### **Required Components**
Ensure these UI elements exist in your scene:

```
Hierarchy:
├── CoachProfilePopulator (with CoachProfilePopulator script)
├── CoachPhotoImage (Image component for photo)
├── CoachNameText (TMP_Text for name display)
├── CoachTypeText (TMP_Text for position)
├── RatingStars (Star rating visual system)
├── SalaryText (TMP_Text for financial info)
├── StatContainer (Transform for statistics list)
├── StatRowPrefab (Prefab for individual stat rows)
└── ExperienceBar (Visual experience indicator)
```

### **Inspector Configuration**
```csharp
[Header("Coach Selection")]
[SerializeField] private string targetCoachId = ""; // Specific coach to display
[SerializeField] private bool useRandomCoach = true; // Load random coach if ID empty

[Header("UI References")]
[SerializeField] private Image coachPhoto;
[SerializeField] private TMP_Text coachNameText;
[SerializeField] private TMP_Text coachTypeText;
[SerializeField] private Transform statContainer;
[SerializeField] private GameObject statRowPrefab;
```

## 📊 **Data Integration**

### **Coach Data Sources**

#### **API Backend Integration**
```csharp
// Load specific coach details
GET /api/coach/{coachId}

// Example response:
{
    "coach_id": "coach_123",
    "coach_name": "Bill Belichick",
    "coach_type": "D",
    "experience": 25,
    "salary": 8000000,
    "overall_rating": 4.8,
    "run_defence": 9.5,
    "pressure_control": 9.2,
    "coverage_discipline": 8.8,
    "turnover": 8.1
}
```

#### **Local JSON Fallback**
```json
// Assets/StreamingAssets/Database/coach.json
{
    "coaches": [
        {
            "coach_id": "belichick_001",
            "coach_name": "Bill Belichick",
            "coach_type": "D",
            "experience": 25,
            "salary": 8000000,
            "overall_rating": 4.8,
            "run_defence": 9.5,
            "pressure_control": 9.2
        }
    ]
}
```

### **Dynamic Coach Selection**
```csharp
// Load coach by ID or random selection
public void LoadCoachProfile(string coachId = "")
{
    if (string.IsNullOrEmpty(coachId))
    {
        // Load random coach from available pool
        coachId = GetRandomAvailableCoach();
    }
    
    StartCoroutine(LoadCoachFromAPI(coachId));
}
```

## 🎨 **UI Components**

### **Coach Photo System**
```csharp
// Photo loading with fallbacks
private void LoadCoachPhoto(string coachName)
{
    // Try to load from Resources/CoachPhotos/
    Sprite photo = Resources.Load<Sprite>($"CoachPhotos/{coachName}");
    
    if (photo != null)
    {
        coachPhotoImage.sprite = photo;
    }
    else
    {
        // Use default placeholder photo
        coachPhotoImage.sprite = defaultCoachPhoto;
    }
}
```

### **Star Rating Display**
```csharp
// Convert overall rating to 5-star visual
private void UpdateStarRating(float rating)
{
    int fullStars = Mathf.FloorToInt(rating);
    bool hasHalfStar = (rating % 1) >= 0.5f;
    
    for (int i = 0; i < starImages.Length; i++)
    {
        if (i < fullStars)
            starImages[i].sprite = fullStarSprite;
        else if (i == fullStars && hasHalfStar)
            starImages[i].sprite = halfStarSprite;
        else
            starImages[i].sprite = emptyStarSprite;
    }
}
```

### **Specialty Stat Rows**
```csharp
// Create stat row for each specialty
private void PopulateStatRows(CoachDatabaseRecord coach)
{
    var specialties = GetTopSpecialties(coach);
    
    foreach (var specialty in specialties)
    {
        GameObject statRow = Instantiate(statRowPrefab, statContainer);
        
        // Configure stat row components
        statRow.GetComponent<StatRow>().Configure(
            specialty.name,
            specialty.value,
            specialty.bonusPercentage,
            specialty.icon
        );
    }
}
```

## 💼 **Coach Management Integration**

### **Hiring from Profile**
```csharp
// Hire button functionality
[Header("Management Actions")]
[SerializeField] private Button hireButton;
[SerializeField] private Button fireButton;

public void OnHireButtonClicked()
{
    if (CoachManager.instance != null)
    {
        bool success = CoachManager.instance.HireCoach(currentCoach);
        
        if (success)
        {
            UpdateHireStatus("Hired");
            hireButton.interactable = false;
            fireButton.interactable = true;
        }
        else
        {
            ShowInsufficientFundsMessage();
        }
    }
}
```

### **Budget Validation**
```csharp
// Check if team can afford coach
private bool CanAffordCoach(CoachDatabaseRecord coach)
{
    float hiringCost = coach.weeklySalary * 4; // 4 weeks upfront
    float currentBudget = TeamManager.GetCurrentBudget();
    
    return currentBudget >= hiringCost;
}
```

## 🔄 **Cross-Screen Navigation**

### **Coach Selection Integration**
```csharp
// Called from other screens to show specific coach
public static void ShowCoachProfile(string coachId)
{
    // Store coach ID for profile scene
    PlayerPrefs.SetString("SelectedCoachId", coachId);
    
    // Load coach profile scene
    SceneManager.LoadScene("CoachProfile");
}

private void Start()
{
    // Check if specific coach was selected
    string selectedCoachId = PlayerPrefs.GetString("SelectedCoachId", "");
    
    if (!string.IsNullOrEmpty(selectedCoachId))
    {
        LoadCoachProfile(selectedCoachId);
        PlayerPrefs.DeleteKey("SelectedCoachId");
    }
    else
    {
        // Load random coach
        LoadRandomCoach();
    }
}
```

### **Team Context**
```csharp
// Show coach's relationship to current team
private void UpdateTeamContext(CoachDatabaseRecord coach)
{
    if (coach.currentTeam == TeamManager.GetCurrentTeamId())
    {
        teamStatusText.text = "Current Team Member";
        teamStatusText.color = Color.green;
    }
    else if (!string.IsNullOrEmpty(coach.currentTeam))
    {
        teamStatusText.text = "Contracted to Other Team";
        teamStatusText.color = Color.red;
    }
    else
    {
        teamStatusText.text = "Available for Hire";
        teamStatusText.color = Color.blue;
    }
}
```

## 📈 **Performance History**

### **Career Statistics**
```csharp
public class CoachCareerStats
{
    public int totalGamesCoached;
    public int totalWins;
    public int totalLosses;
    public float careerWinPercentage;
    public int championshipsWon;
    public string previousTeams;
    public string achievements;
}
```

### **Historical Performance Display**
```csharp
// Show coach's track record
private void DisplayCareerHistory(CoachCareerStats stats)
{
    careerWinsText.text = $"Career Record: {stats.totalWins}-{stats.totalLosses}";
    winPercentageText.text = $"Win %: {stats.careerWinPercentage:F1}%";
    championshipsText.text = $"Championships: {stats.championshipsWon}";
    
    if (stats.championshipsWon > 0)
    {
        championshipIcon.SetActive(true);
    }
}
```

## 🎯 **Interactive Features**

### **Stat Comparison**
```csharp
// Compare with current team coaches
[Header("Comparison Features")]
[SerializeField] private bool showComparison = true;

private void ShowStatComparison(CoachDatabaseRecord newCoach)
{
    var currentCoach = CoachManager.instance.GetCoachByType(newCoach.coachType);
    
    if (currentCoach != null)
    {
        // Highlight improvements/downgrades
        foreach (var statRow in statRows)
        {
            statRow.ShowComparison(currentCoach, newCoach);
        }
    }
}
```

### **Detailed Analysis**
```csharp
// Show coaching philosophy and strategy
[Header("Detailed Information")]
[SerializeField] private TMP_Text coachingPhilosophyText;
[SerializeField] private TMP_Text strengthsText;
[SerializeField] private TMP_Text areasForImprovementText;

private void DisplayDetailedAnalysis(CoachDatabaseRecord coach)
{
    coachingPhilosophyText.text = GenerateCoachingPhilosophy(coach);
    strengthsText.text = GenerateStrengthsList(coach);
    areasForImprovementText.text = GenerateImprovementAreas(coach);
}
```

## 🔧 **Troubleshooting**

### **Common Issues**

#### **Coach Profile Not Loading**
```
Causes:
- Invalid coach ID specified
- API server not responding
- Missing JSON fallback data

Solutions:
1. Verify coach ID exists in database
2. Check backend server status
3. Ensure coach.json contains target coach
4. Enable random coach fallback
```

#### **Missing UI Elements**
```
Causes:
- Unassigned references in Inspector
- Missing prefabs or UI components
- Incorrect hierarchy structure

Solutions:
1. Assign all UI references in CoachProfilePopulator
2. Verify StatRowPrefab exists and is properly configured
3. Check scene hierarchy matches expected structure
```

#### **Photo Loading Issues**
```
Causes:
- Missing coach photos in Resources folder
- Incorrect file naming convention
- Image format not supported

Solutions:
1. Add coach photos to Resources/CoachPhotos/
2. Use coach name as filename (e.g., "Bill Belichick.png")
3. Ensure images are PNG or JPG format
4. Verify default placeholder photo is assigned
```

## 🚀 **Advanced Features**

### **Coach Comparison Tool**
```csharp
// Side-by-side coach comparison
public class CoachComparison
{
    public CoachDatabaseRecord coachA;
    public CoachDatabaseRecord coachB;
    public Dictionary<string, float> statDifferences;
    public string recommendation;
}
```

### **Scouting Reports**
```csharp
// Detailed analysis of coach capabilities
public class ScoutingReport
{
    public string overallAssessment;
    public string[] strengths;
    public string[] weaknesses;
    public string fitWithTeam;
    public float recommendationScore; // 0-100
}
```

### **Contract Negotiations**
```csharp
// Negotiation mini-game for coach salaries
public class ContractNegotiation
{
    public float requestedSalary;
    public float offeredSalary;
    public string[] negotiationPoints;
    public bool negotiationSuccessful;
}
```

---

## 🎯 **Coach Profile Screen Checklist**

### **Setup Verification**
- [ ] FMGCOACH.unity scene loads without errors
- [ ] Coach Details screen displays correctly within main scene
- [ ] CoachProfilePopulator script attached to GameObject
- [ ] All UI references assigned in Inspector
- [ ] StatRowPrefab properly configured
- [ ] Photo loading system functional

### **Data Integration Testing**
- [ ] Coach data loads from API when available
- [ ] JSON fallback works when API unavailable
- [ ] Random coach selection works when no ID specified
- [ ] Specific coach ID selection functions correctly
- [ ] Cross-scene navigation preserves coach selection

### **UI Functionality Testing**
- [ ] Coach photo displays correctly or shows placeholder
- [ ] Star rating accurately reflects overall rating
- [ ] Stat rows show correct specialties and bonuses
- [ ] Financial information displays with proper formatting
- [ ] Hire/Fire buttons function correctly

### **Integration Testing**
- [ ] Hiring from profile updates Coach Hiring Market
- [ ] Profile changes reflect in Performance Analytics
- [ ] Budget validation prevents overspending
- [ ] Team context shows current assignment status

**The Coach Profile screen provides the detailed information needed for informed hiring decisions and serves as the comprehensive database for all coach-related data.**
