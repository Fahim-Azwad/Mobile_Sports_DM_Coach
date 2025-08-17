# 🏈 Coach Hiring Market Screen (Main Interface)

*Complete guide for the main coach hiring and management interface*

## 📍 **Scene Information**
- **Scene File**: `Assets/Scenes/FMGCOACH.unity`
- **Screen Location**: Main interface (default screen)
- **Primary Script**: `CoachManager.cs`
- **UI Components**: Coach slots, filter buttons, budget display
- **Navigation Hub**: Access point to Performance Analytics (Screen 4) and Coach Details screens

## 🚀 **Quick Start**

1. **Open Scene**: Load `FMGCOACH.unity` in Unity
2. **Play Scene**: Press Play button
3. **Test Controls**: Use N/T/F keyboard shortcuts
4. **Hire Coaches**: Click on available coach slots

## 🎮 **Controls & Features**

### **Keyboard Shortcuts**
| Key | Action | Description |
|-----|--------|-------------|
| **N** | Load New Coaches | Fetch fresh coaches from API/database |
| **T** | Toggle Filter | Switch between filter modes |
| **F** | Cycle Filters | Rotate through All/Defense/Offense/Special Teams |
| **P** | Print Status | Show current coach status in console |
| **R** | Reload System | Force reload coaches from API |

### **UI Elements**
- **Coach Slots**: Display available coaches with stats
- **Filter Dropdown**: Select coach type (All/Defense/Offense/Special Teams)
- **Budget Display**: Current team budget and spending
- **Hire/Fire Buttons**: Coach management actions

## 🔧 **System Setup**

### **Required Components**
Ensure these GameObjects exist in your scene:

```
Hierarchy:
├── CoachManager (with CoachManager script)
├── CoachSlotUI_1 (with CoachSlotUI script)
├── CoachSlotUI_2 (with CoachSlotUI script) 
├── FilterDropdown (Dropdown UI component)
├── BudgetDisplay (Text/TMP component)
└── HireFireButtons (Button components)
```

### **Auto-Setup Option**
Add `SystemSetupHelper` to any GameObject:
```csharp
// SystemSetupHelper will automatically create missing components
public class SystemSetupHelper : MonoBehaviour
{
    [ContextMenu("Setup All Systems")]
    public void SetupAllSystems() { ... }
}
```

## 📊 **Coach Data Integration**

### **Data Sources**
The system supports dual data sources:

1. **API Backend** (Primary)
   - Live data from `http://localhost:5175/api/coach/all`
   - Real-time budget updates
   - Server-side validation

2. **JSON Fallback** (Offline)
   - Local file: `Assets/StreamingAssets/Database/coach.json`
   - Cached coach data
   - Offline development mode

### **Coach Information Display**
Each coach slot shows:
- **Name & Photo**: Coach identity
- **Type & Rating**: Defense/Offense/Special Teams with star rating
- **Salary**: Weekly cost with currency formatting
- **Specialties**: Top 3-4 skills with percentage bonuses
- **Experience**: Years of coaching experience
- **Hire Status**: Available/Hired indicator

### **Specialty Calculation**
Database stats (0-10 scale) convert to gameplay bonuses:
```csharp
// Example: Run Defense 8.5/10 = 42.5% bonus
float bonus = (databaseStat / 10f) * 50f;
```

**Defense Coach Specialties:**
- Run Defense → Defensive stopping power
- Pressure Control → Pass rush effectiveness  
- Coverage Discipline → Secondary coverage
- Turnover → Forced fumbles and interceptions

**Offense Coach Specialties:**
- Passing Efficiency → Quarterback effectiveness
- Rush → Running game power
- Red Zone Conversion → Scoring efficiency
- Play Variation → Unpredictability bonus

## 🎯 **Hiring Workflow**

### **Standard Hiring Process**
1. **Browse Coaches**: Use filters to find desired coach type
2. **Check Budget**: Verify sufficient funds for salary
3. **Review Stats**: Examine specialties and ratings
4. **Hire Coach**: Click hire button (validates budget server-side)
5. **Budget Update**: Automatic deduction of hiring cost

### **API-Enabled Hiring**
```csharp
// Server-side validation
POST /api/coach/isAffordable
{
    "coachId": "coach_123",
    "teamBudget": 5000000
}

// Hire coach with server update
PATCH /api/coach/hire
{
    "coachId": "coach_123", 
    "teamId": "team_456"
}
```

### **Offline Hiring**
- Local budget calculation
- JSON file updates
- Immediate UI refresh

## 🔄 **Filter System**

### **Available Filters**
| Filter | Description | Coach Types |
|--------|-------------|-------------|
| **All** | Show all available coaches | D, O, S |
| **Defense** | Defensive specialists only | D |
| **Offense** | Offensive coordinators only | O |
| **Special Teams** | Special teams coaches only | S |

### **Smart Loading**
- **Dynamic Loading**: Fetches 2-4 coaches per filter
- **Fallback Logic**: Loads default coaches if filter returns empty
- **Cache Management**: Prevents duplicate API calls

## 💰 **Budget Management**

### **Budget System**
- **Starting Budget**: $5,000,000 (configurable)
- **Coach Salaries**: Weekly payments (annual salary ÷ 52)
- **Hiring Costs**: One-time payment equal to 4 weeks salary
- **Budget Updates**: Real-time display after each transaction

### **Budget Validation**
```csharp
// Check affordability before hiring
bool canAfford = (currentBudget >= coach.weeklySalary * 4);

if (canAfford)
{
    // Proceed with hiring
    HireCoach(coach);
}
else
{
    // Show insufficient funds message
    ShowBudgetError();
}
```

## 🔍 **Testing & Debugging**

### **Development Tools**
1. **CoachPreloadTester**: Attach to GameObject for testing controls
2. **SystemTester**: Comprehensive system validation
3. **Console Logging**: Detailed debug information

### **Debug Features**
```csharp
// Enable debug mode in CoachManager
[Header("Debug Settings")]
[SerializeField] private bool enableDebugLogs = true;
[SerializeField] private bool showDetailedStats = true;
```

### **Common Debug Messages**
```
[CoachManager] ✅ API server is available - using dynamic API data
[CoachManager] Loaded 3 coaches from API with filter: Defense
[CoachManager] Coach hired: Bill Belichick - Budget remaining: $4,200,000
[CoachManager] ⚠️ API server unavailable - using offline JSON fallback
```

## 🎨 **UI Customization**

### **Visual Settings**
```csharp
[Header("Visual Settings")]
public Vector2 iconSize = new Vector2(64, 64);
public float salaryFontSize = 16f;
public Color hiredCoachColor = Color.green;
public Color unavailableColor = Color.gray;
```

### **Coach Slot Layout**
- **Coach Photo**: 64x64 pixel images
- **Name Text**: Bold, 18pt font
- **Rating Stars**: 5-star visual system
- **Salary Display**: Currency formatted ($1,234,567)
- **Specialty Tags**: Color-coded skill indicators

## 📱 **Mobile Optimization**

### **Touch Controls**
- **Tap to Hire**: Single tap hiring interface
- **Swipe Filters**: Gesture-based filter navigation
- **Responsive Layout**: Adapts to different screen sizes

### **Performance Optimization**
- **Object Pooling**: Reuse coach slot UI elements
- **Lazy Loading**: Load coach images on demand
- **Memory Management**: Automatic cleanup of unused coaches

## 🔧 **Troubleshooting**

### **Common Issues**

#### **CoachManager Instance Not Found**
```
Solution: Add CoachManager script to GameObject in scene
1. Create Empty GameObject
2. Name it "CoachManager"  
3. Add CoachManager script component
```

#### **No Coaches Loading**
```
Causes:
- API server not running (start with 'dotnet run')
- JSON file missing (check StreamingAssets/Database/)
- Network connectivity issues

Solutions:
- Verify backend server status
- Check console for error messages
- Test with offline JSON fallback
```

#### **Budget Not Updating**
```
Causes:
- API integration disabled
- Budget calculation errors
- UI refresh issues

Solutions:
- Enable useAPI in CoachManager
- Check budget validation logic
- Force UI refresh after transactions
```

## 🚀 **Advanced Features**

### **Coach Pre-loading System**
Automatically loads 2 coaches (1 Defense, 1 Offense) on scene start:
```csharp
private void Start()
{
    StartCoroutine(PreLoadTeamCoaches());
}
```

### **Dynamic Coach Generation**
```csharp
// Generate coaches based on team needs
CoachType neededType = AnalyzeTeamNeeds();
List<CoachData> coaches = LoadCoachesByType(neededType);
```

### **Save/Load Integration**
- **Auto-Save**: Saves coach assignments and budget changes
- **Session Persistence**: Maintains state between Unity sessions
- **Backup System**: Automatic backup of coach data

---

## 🎯 **Coach Hiring Market Checklist**

### **Setup Verification**
- [ ] FMGCOACH.unity scene loads without errors
- [ ] CoachManager GameObject exists with script attached
- [ ] Coach slot UI elements are properly configured
- [ ] Filter dropdown is connected to CoachManager
- [ ] Budget display updates correctly

### **Functionality Testing**
- [ ] N key loads new coaches from API/database
- [ ] T key toggles between filter modes
- [ ] F key cycles through coach type filters
- [ ] Hire button successfully recruits coaches
- [ ] Budget decreases appropriately after hiring
- [ ] Fired coaches return to available pool

### **Integration Testing**
- [ ] API backend provides live coach data
- [ ] Offline JSON fallback works when API unavailable
- [ ] Performance Analytics screen shows hired coaches
- [ ] Coach Profile screen displays detailed information

**The Coach Hiring Market is the central hub for team building and provides the foundation for all other game screens.**
