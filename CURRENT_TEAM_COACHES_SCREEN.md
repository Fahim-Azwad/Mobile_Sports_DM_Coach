# 👥 Current Team Coaches Screen

*Complete guide for viewing and managing coaches currently on your team*

## 📍 **Scene Information**
- **Scene File**: `Assets/Scenes/FMGCOACH.unity`
- **Screen Location**: Current Team Coaches display within main scene
- **Primary Script**: `CoachManager.cs` + Team Management Components
- **Purpose**: View, manage, and analyze coaches currently hired by your team
- **Navigation**: Accessible from main FMGCOACH interface

## 🚀 **Quick Start**

1. **Open Scene**: Load `FMGCOACH.unity` in Unity
2. **Navigate to Team View**: Access Current Team Coaches screen from main interface
3. **View Hired Coaches**: See all coaches currently on your team
4. **Manage Team**: Fire coaches, view stats, check salaries
5. **Team Analysis**: Review total team cost and performance impact

## 👥 **Current Team Display**

### **Team Roster Overview**
```
┌─────────────────────────────────────────────┐
│           CURRENT TEAM COACHES              │
├─────────────────────────────────────────────┤
│  [Coach Slot 1] - Bill Belichick    (D)    │
│  Salary: $8,000,000  Rating: 4.8/5.0       │
│  Status: Active      Experience: 25 years   │
├─────────────────────────────────────────────┤
│  [Coach Slot 2] - Sean McVay        (O)    │
│  Salary: $7,500,000  Rating: 4.6/5.0       │
│  Status: Active      Experience: 8 years    │
├─────────────────────────────────────────────┤
│  [Coach Slot 3] - John Harbaugh     (S)    │
│  Salary: $6,200,000  Rating: 4.4/5.0       │
│  Status: Active      Experience: 15 years   │
└─────────────────────────────────────────────┘
Total Team Salary: $21,700,000
Remaining Budget: $28,300,000
```

### **Coach Information Display**
Each hired coach shows:
- **Coach Name & Type** (D=Defense, O=Offense, S=Special Teams)
- **Current Salary** (annual cost)
- **Overall Rating** (out of 5.0)
- **Experience** (years coaching)
- **Hire Date** (when added to team)
- **Performance Impact** (team improvement metrics)

## 🎮 **Team Management Controls**

### **Available Actions**
- **View Coach Details** - Click coach to see full profile
- **Fire Coach** - Remove coach from team (frees up salary)
- **Compare Coaches** - Side-by-side coach statistics
- **Team Summary** - Overall team performance metrics
- **Salary Management** - Budget allocation and tracking

### **Keyboard Shortcuts**
- **T** - Toggle between hired/available coaches view
- **F** - Filter current team coaches by type
- **S** - Sort team coaches by salary/rating/experience
- **R** - Refresh team data from API/database
- **B** - Show team budget breakdown

## 💰 **Budget Management**

### **Salary Tracking**
```csharp
// Example budget display
Total Budget: $50,000,000
Current Salaries: $21,700,000
Available Funds: $28,300,000
Recommended Reserve: $5,000,000
```

### **Cost Analysis**
- **Individual Salaries** - Each coach's annual cost
- **Total Team Cost** - Combined salary expenses
- **Budget Utilization** - Percentage of budget used
- **Projected Costs** - Future contract obligations
- **Savings Potential** - Cost optimization opportunities

## 📊 **Team Performance Metrics**

### **Coaching Impact Analysis**
```
Defense Coaching: ⭐⭐⭐⭐⭐ (Excellent)
Offense Coaching: ⭐⭐⭐⭐⭐ (Excellent)  
Special Teams:    ⭐⭐⭐⭐⚬ (Very Good)

Overall Team Rating: 4.6/5.0
Coaching Balance: Well-Rounded
Areas for Improvement: None
```

### **Statistics Displayed**
- **Overall Team Rating** - Combined coaching effectiveness
- **Coaching Balance** - Even distribution across D/O/S
- **Experience Level** - Average years of experience
- **Salary Efficiency** - Performance per dollar spent
- **Team Chemistry** - How well coaches work together

## 🔧 **Unity Implementation**

### **Key Components**
```csharp
// Essential GameObjects in FMGCOACH scene
TeamCoachesPanel          // Main team display container
├── TeamRosterScrollView  // Scrollable list of hired coaches
├── TeamStatsPanel        // Team performance metrics
├── BudgetDisplayPanel    // Salary and budget information
├── TeamActionsPanel      // Management buttons and controls
└── CoachComparisonPanel  // Side-by-side coach comparison
```

### **Required Scripts**
- **`CoachManager.cs`** - Core team management logic
- **`TeamDisplayManager.cs`** - Current team UI management
- **`BudgetTracker.cs`** - Salary and budget calculations
- **`TeamAnalytics.cs`** - Performance metrics calculation
- **`CoachComparer.cs`** - Coach comparison functionality

## 🎯 **Navigation Within FMGCOACH Scene**

### **Screen Access Points**
1. **From Main Menu** → "View Current Team" button
2. **From Coach Hiring** → "My Team" tab
3. **From Performance Analytics** → "Team Coaches" section
4. **Direct Navigation** → Team management panel

### **Screen Transitions**
```
Main Interface → Current Team Coaches
     ↓
[View Coach Details] → Individual Coach Profile
     ↓
[Compare Coaches] → Coach Comparison View
     ↓
[Team Analytics] → Performance Analysis
```

## 📱 **Mobile-Friendly Design**

### **Touch Controls**
- **Tap Coach** - View details
- **Long Press** - Quick actions menu
- **Swipe Left** - Fire coach
- **Swipe Right** - View coach stats
- **Pinch Zoom** - Adjust display size

### **Responsive Layout**
- **Portrait Mode** - Vertical coach list
- **Landscape Mode** - Grid view with more details
- **Tablet View** - Side-by-side comparison panels
- **Phone View** - Compact single-column layout

## 🔄 **Data Integration**

### **API Endpoints Used**
- **GET** `/api/team/coaches` - Get current team roster
- **DELETE** `/api/coach/fire/{coachId}` - Remove coach from team
- **GET** `/api/team/stats` - Get team performance metrics
- **GET** `/api/team/budget` - Get current budget status

### **Offline Mode**
```json
{
  "currentTeam": {
    "coaches": [
      {
        "coach_id": "1",
        "coach_name": "Bill Belichick",
        "coach_type": "D",
        "salary": 8000000,
        "hire_date": "2024-01-15",
        "performance_impact": 4.8
      }
    ],
    "totalSalary": 21700000,
    "remainingBudget": 28300000,
    "teamRating": 4.6
  }
}
```

## 🛠️ **Setup Instructions**

### **1. Scene Setup**
```csharp
// Add to any GameObject in FMGCOACH scene
public class TeamScreenSetup : MonoBehaviour 
{
    void Start() 
    {
        SetupTeamDisplay();
        LoadCurrentTeam();
        InitializeBudgetTracking();
    }
}
```

### **2. UI Configuration**
- Add **TeamCoachesPanel** to Canvas
- Configure **ScrollView** for coach list
- Setup **Budget Display** elements
- Add **Action Buttons** for team management

### **3. Data Binding**
```csharp
// Example team data binding
public void PopulateTeamDisplay(List<Coach> currentTeam)
{
    foreach(Coach coach in currentTeam)
    {
        CreateTeamCoachSlot(coach);
        UpdateBudgetDisplay();
        CalculateTeamMetrics();
    }
}
```

## 🧪 **Testing & Validation**

### **Test Scenarios**
- [ ] Current team coaches display correctly
- [ ] Budget calculations are accurate
- [ ] Coach firing removes coach and updates budget
- [ ] Team performance metrics calculate properly
- [ ] Navigation between team and hiring screens works
- [ ] API integration loads real team data
- [ ] Offline mode shows fallback team data

### **Debug Tools**
```csharp
// Testing keyboard shortcuts
[Debug] T - Toggle team/market view
[Debug] F - Fire selected coach
[Debug] S - Show team statistics
[Debug] B - Display budget breakdown
[Debug] R - Reload team data
```

## 📈 **Performance Optimization**

### **Efficient Loading**
- **Lazy Loading** - Load coach details on demand
- **Caching** - Store team data locally
- **Pooling** - Reuse coach display elements
- **Pagination** - Handle large team rosters efficiently

### **Memory Management**
- **Dispose** unused coach objects
- **Compress** coach images and assets
- **Stream** large team datasets
- **Cache** frequently accessed team data

## 🚨 **Troubleshooting**

### **Common Issues**

#### **Team Not Loading**
```
Issue: Current team coaches not displaying
Solution: 
1. Check CoachManager.LoadCurrentTeam()
2. Verify API endpoint /api/team/coaches
3. Validate team data structure
4. Check UI binding in TeamDisplayManager
```

#### **Budget Calculations Wrong**
```
Issue: Salary totals incorrect
Solution:
1. Verify coach salary data types
2. Check BudgetTracker calculations
3. Validate API response format
4. Review salary update triggers
```

#### **Coach Actions Not Working**
```
Issue: Fire/manage coach buttons unresponsive
Solution:
1. Check button event bindings
2. Verify coach ID references
3. Test API delete endpoints
4. Validate UI state management
```

## 🎉 **Ready for Production**

The Current Team Coaches screen provides:
- **Complete Team Overview** - All hired coaches in one view
- **Budget Management** - Real-time salary and budget tracking
- **Performance Analysis** - Team effectiveness metrics
- **Easy Management** - Intuitive coach hiring/firing
- **Mobile Optimized** - Touch-friendly interface
- **API Integrated** - Live data with offline fallbacks

### **Integration Points**
- Seamlessly connects with Coach Hiring Market
- Links to Individual Coach Profiles
- Integrates with Performance Analytics
- Supports Budget Planning workflows

**The Current Team Coaches screen completes the full coaching management ecosystem within FMGCOACH.unity!** 🏆
