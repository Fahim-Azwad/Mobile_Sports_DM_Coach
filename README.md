# 🏈 Mobile Sports DM Coach - Enhanced Database Integration

A Unity-based mobile sports management game with advanced coach database integration and dynamic UI systems.

*Summer Game AI Research Project under Prof. Scott John Easley*

## 🎯 **Latest Features (Complete Coach Database Integration System)**

### **Screen 4 Performance Dashboard - NEW** ✅
- ✅ **Dynamic Analytics**: Real-time performance metrics from database
- ✅ **ELT Integration**: Extract team data → Load performance metrics → Transform for UI
- ✅ **Before/After Comparison**: Shows coaching impact with visual indicators
- ✅ **Weekly Breakdown**: Detailed coach contributions and team improvements
- ✅ **ROI Analysis**: Investment tracking with profitability indicators
- ✅ **Database-Driven**: All data dynamically loaded from coaching staff

### **Dynamic Coach Loading System**
- ✅ **Dual Data Sources**: Supports both Unity ScriptableObjects and JSON database files
- ✅ **Real-time Filtering**: Filter coaches by type (All/Defense/Offense/Special Teams)
- ✅ **Database Integration**: Loads from `StreamingAssets/Database/coach.json`
- ✅ **ELT Pipeline**: Extract → Load → Transform data processing

### **Enhanced Coach Management**
- ✅ **CoachData.cs**: Unified data structure supporting both static and dynamic coaches
- ✅ **CoachManager.cs**: Handles hiring/firing with dual data source support
- ✅ **CoachProfilePopulator.cs**: Dynamic coach details screen with top 4 specialties
- ✅ **CoachHiringMarket.cs**: Enhanced hiring interface with filtering and comparison

### **Advanced UI Features**
- ✅ **Top Specialties Display**: Calculates and shows best coach attributes
- ✅ **Star Rating System**: Dynamic 1-5 star ratings based on overall performance
- ✅ **Salary Conversion**: Annual salary to weekly payment calculations
- ✅ **Specialty Progress Bars**: Visual representation of coach strengths

## 🎮 **Testing Controls**
- **N Key**: Load new coaches
- **T Key**: Toggle/refresh current filter
- **F Key**: Cycle through filter types (All → Defense → Offense → Special Teams)

## 📁 **Project Structure**

### **Core Scripts**
```
Assets/Scripts/
├── CoachData.cs              # Unified coach data structure (ScriptableObject + Database)
├── CoachManager.cs           # Main coach management system
├── CoachProfilePopulator.cs  # Coach details screen with database integration
├── CoachHiringMarket.cs      # Enhanced hiring market with filtering
└── CoachSlotUI.cs           # Individual coach slot display
```

### **Database Files**
```
Assets/StreamingAssets/Database/
├── coach.json               # Coach database with detailed stats
├── team.json               # Team data
├── game.json               # Game statistics
└── FMGDatabase_Test.db     # SQLite database (backup)
```

### **Coach Assets**
```
Assets/CoachData/
├── Aryan.asset             # Example coach ScriptableObject
├── Bill Belichick.asset    # Example coach ScriptableObject
├── Dhruv.asset            # Example coach ScriptableObject
└── ...                    # Additional coach assets
```

## 🔧 **Technical Implementation**

### **Database Schema (coach.json)**
```json
{
  "coach_id": "unique_id",
  "coach_name": "Coach Name",
  "coach_type": "D|O|S",     // Defense/Offense/Special Teams
  "experience": 5,
  "salary": 2.5,             // In millions (converted to weekly)
  "overall_rating": 4.2,     // 1-5 scale
  
  // Detailed Stats (0-10 scale)
  "run_defence": 8.5,
  "pressure_control": 7.2,
  "coverage_discipline": 9.1,
  "turnover": 6.8,
  
  "passing_efficiency": 7.5,
  "rush": 8.2,
  "red_zone_conversion": 9.0,
  "play_variation": 6.5,
  
  "field_goal_accuracy": 8.8,
  "kickoff_instance": 7.3,
  "return_speed": 8.1,
  "return_coverage": 7.9
}
```

### **Coach Types**
- **Defense (D)**: Run Defense, Pressure Control, Coverage Discipline, Turnover Generation
- **Offense (O)**: Passing Efficiency, Rushing Attack, Red Zone Conversion, Play Variation
- **Special Teams (S)**: Field Goal Accuracy, Kickoff Distance, Return Speed, Return Coverage

## 🚀 **Getting Started**

### **Prerequisites**
- Unity 2022.3 LTS or later
- TextMeshPro package installed

### **Setup Instructions**
1. **Clone Repository**
   ```bash
   git clone https://github.com/Fahim-Azwad/Mobile_Sports_DM_Coach.git
   ```

2. **Open in Unity**
   - Launch Unity Hub
   - Click "Open" and select the project folder
   - Wait for Unity to import assets

3. **Configure Database**
   - Ensure `Assets/StreamingAssets/Database/coach.json` exists
   - Database is automatically loaded at runtime

4. **Test the System**
   - Open `Assets/Scenes/FMGCOACH.unity`
   - Play the scene
   - Use N/T/F keys to test coach loading

## 🎯 **Key Features Breakdown**

### **CoachData.cs Enhancement**
- **Dual System Support**: Works with both Unity Inspector and JSON database
- **Auto-Conversion**: `CreateFromDatabaseRecord()` bridges database to ScriptableObject
- **Extended Stats**: Added all database fields while maintaining Unity compatibility
- **Backward Compatible**: Existing coach assets continue working unchanged

### **Dynamic Filtering System**
- **Dropdown Filter**: User-friendly selection interface
- **Keyboard Controls**: Development-friendly testing shortcuts
- **Real-time Updates**: Instant coach loading based on filter selection
- **Smart Fallbacks**: Default coaches when filtered results are insufficient

### **UI Enhancements**
- **Specialty Calculation**: Converts 0-10 database stats to 0-50% bonuses
- **Top N Display**: Shows best 3-4 specialties per coach
- **Currency Formatting**: Professional salary display with thousands separators
- **Error Handling**: Graceful fallbacks for missing data or files

## 📖 **Documentation**

### **Setup Guides**
- `COACHDATA_CONSOLIDATION.md` - Data structure unification details
- `COACH_HIRING_CLEANED.md` - Clean implementation guide
- `UNITY_DEMO_SCRIPT.md` - Development workflow documentation

### **Implementation Details**
- `COACH_DETAILS_INTEGRATED.md` - Coach details screen implementation
- `COACH_DATABASE_INTEGRATION_SIMPLE.md` - Database integration guide
- `COACH_SETUP_GUIDE.md` - Complete setup instructions

## 🛠 **Development**

### **Adding New Coaches**
1. **Via JSON Database**: Add entries to `coach.json`
2. **Via Unity Inspector**: Create new CoachData ScriptableObject assets
3. **Hybrid Approach**: Use both systems simultaneously

### **Extending Coach Types**
1. Update `CoachType` enum in `CoachData.cs`
2. Add new stat categories to `CoachDatabaseRecord`
3. Update specialty calculation logic in UI scripts

### **Testing**
- Use keyboard controls (N/T/F) for rapid testing
- Monitor Unity Console for debug information
- Verify both ScriptableObject and database coaches load correctly

## 🤝 **Contributing**

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly with both data sources
5. Submit a pull request

## 📄 **License**

This project is part of a mobile sports management game development. Please respect intellectual property rights.

---

## 🎯 **Recent Major Update**

**Complete Coach Database Integration System** - This update consolidates the coach data architecture, providing seamless integration between Unity's ScriptableObject system and external JSON databases. The system now supports dynamic coach loading, advanced filtering, and comprehensive UI enhancements while maintaining full backward compatibility.

**Key Benefits:**
- Game designers can continue using Unity Inspector for coach creation
- Developers can dynamically load coaches from external databases
- Players get enhanced UI with filtering and detailed coach information
- System is ready for future expansion and mobile deployment

Ready for Unity testing and further development! 🚀