# 📊 Performance Analytics Screen (Screen 4)

*Complete guide for team performance tracking and coaching impact analysis*

## 🎮 **Navigation & Controls**

### **Screen Navigation Buttons**
- **"Hire Coaches"** → Switch to Coach Hiring Market screen
- **"My Team"** → Switch to Current Team Coaches screen
- **"Analytics"** → Stay on Performance Analytics (current screen)
- **"Back to Main"** → Return to main Coach Hiring interface

### **Performance Analytics Demo Controls**
**Note**: These controls require PerformanceAnalyticsDemo script to be attached to a GameObject in the scene.

#### **PerformanceAnalyticsDemo Controls (R/S/C)**
- **R** - Run demo manually
  - Executes the complete performance analytics demonstration
  - Loads team data and generates sample performance metrics
  - Useful for testing the analytics system
- **S** - Populate sample data manually
  - Generates sample performance data for testing
  - Creates before/after coaching comparison data
  - Populates UI with demonstration statistics
- **C** - Clear data
  - Clears all performance analytics data
  - Resets the analytics display
  - Useful for starting fresh tests

### **Analytics View Controls**
- **No Direct Keyboard Shortcuts**: Main screen uses automated data loading
- **Automatic Data Loading**: Performance metrics load automatically from API/database
- **Statistical Display**: Shows before/after coaching comparison data
- **Interactive Elements**: UI toggles handled by StatViewToggle script (if present)

## 📍 **Scene Information**
- **Scene File**: `Assets/Scenes/FMGCOACH.unity`
- **Screen Location**: Screen 4 within main scene
- **Primary Script**: `StatCardPopulator.cs`
- **Purpose**: Before/after coaching comparison with ROI tracking
- **Navigation**: Accessible from main interface as Screen 4

## 🚀 **Quick Start**

1. **Open Scene**: Load `FMGCOACH.unity` in Unity
2. **Navigate to Screen 4**: Access Performance Analytics from main interface
3. **Add Required Components**: Ensure StatCardPopulator exists
4. **Play Scene**: Performance data loads automatically
5. **View Analytics**: Compare before/after coaching statistics

## 📊 **Analytics Dashboard**

### **Core Metrics Display**
The dashboard shows comprehensive team performance data:

#### **Before/After Comparison**
- **Team Performance**: Win/Loss ratios before and after coaching
- **Offensive Stats**: Scoring averages, passing efficiency, rushing yards
- **Defensive Stats**: Points allowed, turnovers forced, sacks
- **Special Teams**: Field goal percentage, return averages

#### **Visual Indicators**
- **Green Arrows ↗**: Performance improvements
- **Red Arrows ↘**: Performance declines  
- **Percentage Changes**: Quantified impact of coaching
- **Trend Lines**: Performance trajectory over time

### **Weekly Breakdown**
```
Week 1-10 (Before Coaching): Average Performance
Week 11-20 (After Coaching): Enhanced Performance
Impact Analysis: +15% offensive efficiency, +8% defensive stops
```

## 🔧 **System Setup**

### **Required Components**
Ensure these elements exist in your scene:

```
Hierarchy:
├── StatCardPopulator (with StatCardPopulator script)
├── BeforeContainer (Transform for before-coaching stats)
├── AfterContainer (Transform for after-coaching stats)
├── WeeklyBreakdownContainer (Transform for timeline data)
├── StatRowPrefab (Prefab for individual stat rows)
└── SummaryTextComponents (TMP_Text for analysis summary)
```

### **Auto-Setup with SystemSetupHelper**
```csharp
// Add to any GameObject for automatic setup
[ContextMenu("Setup Performance Analytics")]
public void SetupPerformanceAnalytics()
{
    // Creates all required containers and components
}
```

## 📈 **Data Integration**

### **Performance Data Sources**

#### **API Backend Integration**
```csharp
// Load performance data from server
GET /api/performance/{teamId}
GET /api/performance/matches/{teamId}

// Calculate coaching impact
POST /api/performance/calculate
{
    "teamId": "team_123",
    "beforePeriod": "weeks_1_10", 
    "afterPeriod": "weeks_11_20"
}
```

#### **Local JSON Fallback**
```json
// Assets/StreamingAssets/Database/performance.json
{
    "teamId": "default_team",
    "beforeCoaching": {
        "wins": 4,
        "losses": 6,
        "pointsScored": 18.5,
        "pointsAllowed": 22.3
    },
    "afterCoaching": {
        "wins": 8,
        "losses": 2, 
        "pointsScored": 24.7,
        "pointsAllowed": 16.8
    }
}
```

### **Stat Categories**

#### **Offensive Metrics**
- **Points Per Game**: Average scoring output
- **Total Yards**: Offensive yardage production
- **Passing Efficiency**: Quarterback rating and completion %
- **Rushing Average**: Yards per carry and attempts
- **Red Zone Conversion**: Scoring percentage in red zone
- **Third Down Conversion**: Success rate on 3rd downs

#### **Defensive Metrics**  
- **Points Allowed**: Average points given up per game
- **Total Defense**: Yards allowed per game
- **Sacks**: Quarterback sacks per game
- **Interceptions**: Passes intercepted
- **Fumbles Recovered**: Turnover generation
- **Third Down Defense**: Opponent conversion rate

#### **Special Teams Metrics**
- **Field Goal Percentage**: Kicking accuracy
- **Punt Average**: Distance and hang time
- **Kickoff Return Average**: Return yardage
- **Punt Return Average**: Return efficiency

## 🎯 **Coaching Impact Analysis**

### **ROI Calculation**
The system calculates Return on Investment for coaching expenses:

```csharp
// ROI Formula
float coachingCost = (defenseCoach.weeklySalary + offenseCoach.weeklySalary) * 10; // 10 weeks
float performanceGain = (afterWinRate - beforeWinRate) * 100f;
float roi = (performanceGain / coachingCost) * 1000000f; // ROI per million spent

// Example: 15% win rate improvement, $500K coaching cost = 30.0 ROI
```

### **Performance Metrics**
```csharp
public class PerformanceMetrics
{
    public float winRateImprovement;    // Percentage point change
    public float offensiveImprovement;  // Points per game increase
    public float defensiveImprovement;  // Points allowed decrease
    public float specialTeamsImpact;    // Overall ST efficiency change
    public float overallTeamRating;     // Combined performance score
}
```

## 🎨 **UI Components**

### **Stat Row Display**
Each statistic is displayed with:
```csharp
// StatRow Components
- Icon: Sport-specific imagery (football, trophy, etc.)
- Label: Stat category name
- Before Value: Performance before coaching
- After Value: Performance after coaching  
- Arrow: Visual improvement/decline indicator
- Percentage: Quantified change amount
```

### **Visual Settings**
```csharp
[Header("Visual Settings")]
public Vector2 iconSize = new Vector2(20, 20);
public Vector2 arrowSize = new Vector2(20, 20);
public float labelFontSize = 16f;
public float valueFontSize = 16f;
public Color improvementColor = Color.green;
public Color declineColor = Color.red;
```

### **Weekly Timeline**
```csharp
// Weekly breakdown display
Week 1-10: Building foundation, establishing systems
Week 11-15: Implementing coaching changes
Week 16-20: Seeing results, performance improvements
```

## 💰 **Investment Analysis**

### **Cost-Benefit Summary**
```csharp
public class InvestmentSummary
{
    public float totalCoachingInvestment;  // Total spent on coaches
    public float weeklyCoachingCost;       // Ongoing weekly expenses
    public float performanceROI;           // Return on investment percentage
    public string investmentRecommendation; // Continue/Adjust/Replace recommendation
}
```

### **Budget Impact Display**
- **Total Investment**: Sum of all coaching salaries and hiring costs
- **Weekly Expense**: Ongoing operational costs
- **Performance Gain**: Measurable improvement metrics
- **ROI Analysis**: Whether investment is profitable

## 🔄 **Automatic Data Updates**

### **Real-time Integration**
```csharp
// Auto-refresh when coaches change
private void OnEnable()
{
    CoachManager.OnCoachHired += RefreshPerformanceData;
    CoachManager.OnCoachFired += RefreshPerformanceData;
}

// Update performance when new match data available
public void RefreshPerformanceData()
{
    StartCoroutine(LoadPerformanceDataFromAPI());
}
```

### **Data Refresh Triggers**
- **Coach Hiring**: New performance projections
- **Coach Firing**: Impact assessment of coaching changes
- **Season Progression**: Updated statistics from recent games
- **Manual Refresh**: User-initiated data reload

## 🧪 **Testing & Validation**

### **Demo Mode**
```csharp
// PerformanceAnalyticsDemo script for testing
[ContextMenu("Generate Demo Data")]
public void GenerateDemoData()
{
    // Creates realistic before/after scenarios
    // Useful for testing UI layout and calculations
}
```

### **Debug Features**
```csharp
[Header("Debug Settings")]
[SerializeField] private bool showDebugCalculations = true;
[SerializeField] private bool logPerformanceData = true;
[SerializeField] private bool simulateAPIFailure = false;
```

### **Validation Checks**
- **Data Consistency**: Verify before/after periods don't overlap
- **Calculation Accuracy**: Validate ROI and percentage calculations
- **UI Responsiveness**: Ensure smooth updates during data changes

## 📱 **Mobile Optimization**

### **Responsive Design**
- **Adaptive Layouts**: Adjusts to different screen sizes
- **Touch-Friendly**: Large tap targets for interaction
- **Scrollable Content**: Handles extensive statistics lists

### **Performance Optimization**
```csharp
// Efficient data loading
private void LoadPerformanceDataAsync()
{
    // Background loading prevents UI freezing
    // Incremental updates for smooth user experience
}
```

## 🔧 **Troubleshooting**

### **Common Issues**

#### **No Performance Data Displayed**
```
Causes:
- StatCardPopulator not found in scene
- Missing data containers (BeforeContainer, AfterContainer)
- API server not providing performance data

Solutions:
1. Add StatCardPopulator script to GameObject
2. Assign container references in Inspector
3. Check backend server endpoints
4. Verify JSON fallback data exists
```

#### **Incorrect Calculations**
```
Causes:
- Coach salary data missing
- Performance period misconfiguration
- Division by zero in ROI calculations

Solutions:
1. Verify coach data integrity
2. Check before/after period definitions
3. Add safety checks for zero values
```

#### **UI Layout Issues**
```
Causes:
- Missing prefab references
- Incorrect container hierarchy
- Font or asset loading failures

Solutions:
1. Reassign StatRowPrefab in Inspector
2. Verify container parent-child relationships
3. Check TextMeshPro package installation
```

## 🚀 **Advanced Features**

### **Predictive Analytics**
```csharp
// Project future performance based on current trends
public class PerformancePrediction
{
    public float projectedWinRate;      // Expected win rate next 10 games
    public float confidenceInterval;    // Statistical confidence in prediction
    public string recommendation;       // Coaching strategy suggestions
}
```

### **Coaching Efficiency Metrics**
```csharp
// Measure coaching effectiveness
public class CoachingEfficiency
{
    public float impactPerDollar;      // Performance gain per salary dollar
    public float timeToImprovement;    // Weeks to see coaching results
    public float sustainabilityScore;  // Likelihood improvements will continue
}
```

### **Historical Comparison**
- **Season-over-Season**: Compare current performance to previous years
- **League Averages**: Benchmark against other teams
- **Coach Track Record**: Historical performance of current coaches

---

## 🎯 **Performance Analytics Checklist**

### **Setup Verification**
- [ ] FMGCOACH.unity scene loads correctly
- [ ] Performance Analytics (Screen 4) displays properly within main scene
- [ ] StatCardPopulator script attached to GameObject
- [ ] Before/After containers properly assigned
- [ ] StatRowPrefab reference set in Inspector
- [ ] Summary text components linked

### **Data Integration Testing**
- [ ] Performance data loads from API when available
- [ ] JSON fallback works when API unavailable
- [ ] Coach hiring triggers performance recalculation
- [ ] ROI calculations produce reasonable results
- [ ] Before/after periods are logically consistent

### **UI Functionality Testing**
- [ ] Stat rows display with proper formatting
- [ ] Improvement arrows show correct direction
- [ ] Percentage calculations are accurate
- [ ] Weekly breakdown displays chronologically
- [ ] Investment summary shows coaching costs

### **Cross-Screen Integration**
- [ ] Hired coaches from Coach Hiring Market appear in analytics
- [ ] Coach Profile details link to performance impact
- [ ] Budget changes reflect in investment analysis
- [ ] System works with both API and offline modes

**The Performance Analytics screen provides crucial insights into coaching effectiveness and helps justify investment decisions through data-driven analysis.**
