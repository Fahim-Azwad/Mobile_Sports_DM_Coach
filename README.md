# 🏈 Mobile Sports DM Coach

*Unity-based mobile sports management game with comprehensive coaching system*

## 🚀 **Quick Start**

1. **Open Unity** → Load `Assets/Scenes/FMGCOACH.unity`
2. **Play Scene** → Use N/T/F keys to test main interface
3. **Start Backend** (Optional) → Navigate to `TeammateInteraction/Database/FMG-Coach-Backend-main/` and run `dotnet run`
4. **Test All Screens** → Navigate between Coach Hiring Market (main), Current Team Coaches (team view), Performance Analytics (Screen 4), and Coach Details screens within the same scene

## 🌐 **Backend Server**

The project includes a complete .NET API backend server:
- **Location**: `TeammateInteraction/Database/FMG-Coach-Backend-main/`
- **Source**: Based on [FMG-Coach-Backend](https://github.com/Aryan-2602/FMG-Coach-Backend/tree/main)
- **Database**: SQLite with coach, team, and performance data
- **API**: RESTful endpoints for all coach management operations
- **Setup**: See [BACKEND_SERVER.md](BACKEND_SERVER.md) for complete guide

### **Start Backend Server**
```powershell
cd "TeammateInteraction/Database/FMG-Coach-Backend-main"
dotnet run
```
**Server runs on**: `http://localhost:5175`

## 📱 **Available Screens**

All screens are integrated within the main `FMGCOACH.unity` scene:

| Screen | Navigation | Purpose |
|--------|------------|---------|
| 🏈 **Coach Hiring Market** | Main screen in `FMGCOACH.unity` | Coach hiring and management interface |
| � **Current Team Coaches** | Team view in `FMGCOACH.unity` | View and manage coaches currently on team |
| �📊 **Performance Analytics** | Screen 4 in `FMGCOACH.unity` | Team performance tracking and ROI analysis |
| 👤 **Coach Profile/Details** | Coach Details screen in `FMGCOACH.unity` | Individual coach information display |

## ✅ **Core Features**

- 🏆 **Dynamic Coach Management**: Hire/fire coaches with real-time budget updates
- 📊 **Performance Analytics**: Before/after coaching comparison with ROI tracking
- 💾 **Save/Load System**: Persistent game state with backup/restore
- 🌐 **API Integration**: Live backend data with offline fallbacks
- 🎯 **Smart Filtering**: Filter coaches by type, rating, and budget
- � **Real-time Updates**: Live data synchronization between screens

## 🔧 **System Requirements**

- **Unity**: 2022.3 LTS or later
- **TextMeshPro**: Installed via Package Manager
- **.NET Backend** (Optional): For live API data
- **Platform**: Windows, Android, iOS compatible

## 🎮 **Controls & Testing**

### **Keyboard Shortcuts**
- **N** - Load new coaches
- **T** - Toggle filter mode
- **F** - Cycle through filters
- **P** - Print coach status (CoachPreloadTester)
- **R** - Reload coaches from API/database
- **C** - Check system status

### **Quick Setup**
1. Add `SystemSetupHelper` to any GameObject for automatic system setup
2. Add `SystemTester` for comprehensive testing
3. All required components will be created automatically

## 📖 **Documentation Structure**

This project uses **6 focused documents** for complete coverage of the single-scene architecture:

1. **README.md** - This overview and quick start guide
2. **BACKEND_SERVER.md** - API server setup and endpoints
3. **COACH_HIRING_MARKET_SCREEN.md** - Main interface guide (FMGCOACH.unity)
4. **CURRENT_TEAM_COACHES_SCREEN.md** - Current team management and display (FMGCOACH.unity)
5. **PERFORMANCE_ANALYTICS_SCREEN.md** - Screen 4 analytics setup (FMGCOACH.unity)
6. **COACH_PROFILE_SCREEN.md** - Coach Details screen system (FMGCOACH.unity)

**Note**: All screens are integrated within the single `FMGCOACH.unity` scene file.

## 🗃️ **Database Schema**

```json
{
  "coach_id": "unique_id",
  "coach_name": "Coach Name", 
  "coach_type": "D|O|S",
  "experience": 5,
  "salary": 2500000,
  "overall_rating": 4.2,
  "run_defence": 8.5,
  "pressure_control": 7.2,
  "passing_efficiency": 7.5,
  "field_goal_accuracy": 8.8
}
```

## 🔄 **Data Flow**

```
Backend API ←→ Unity Scripts ←→ UI Components
     ↓              ↓              ↓
Database ←→ Coach Objects ←→ Visual Display
```

## 🛠️ **Key Scripts**

| Script | Purpose | Scene Location |
|--------|---------|---------|
| `CoachManager.cs` | Main coach management singleton | FMGCOACH.unity |
| `TeamDisplayManager.cs` | Current team coaches display | FMGCOACH.unity |
| `StatCardPopulator.cs` | Performance analytics UI (Screen 4) | FMGCOACH.unity |
| `CoachProfilePopulator.cs` | Coach details display | FMGCOACH.unity |
| `SystemTester.cs` | Automated testing framework | FMGCOACH.unity |
| `SystemSetupHelper.cs` | Auto-setup missing components | FMGCOACH.unity |

## 🎯 **Getting Started Checklist**

- [ ] Clone repository and open in Unity
- [ ] Open and verify `FMGCOACH.unity` scene loads without errors
- [ ] Test main Coach Hiring Market interface with N/T/F keys
- [ ] Navigate to and test Current Team Coaches display
- [ ] Navigate to and test Performance Analytics (Screen 4)
- [ ] Navigate to and verify Coach Profile/Details screen
- [ ] (Optional) Start backend server for live API data
- [ ] Run SystemTester to validate all components

## � **Deployment Ready**

This system is fully prepared for:
- **Mobile Deployment** (Android/iOS)
- **Standalone Builds** (Windows/Mac/Linux)
- **WebGL** (Browser-based)
- **API Integration** (Live backend data)
- **Offline Mode** (JSON fallbacks)

---

## 🎉 **Project Status: Production Ready**

All screens are fully integrated within the single `FMGCOACH.unity` scene with comprehensive testing frameworks. The system supports both online API integration and offline JSON fallbacks, making it suitable for immediate deployment or further development.

**Single Scene Architecture** - All four screens (Coach Hiring Market, Current Team Coaches, Performance Analytics, Coach Details) are seamlessly integrated within one Unity scene for optimal performance and user experience.

**Ready to build and deploy!** 🚀