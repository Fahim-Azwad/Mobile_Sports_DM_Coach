# 🏈 Mobile Sports DM Coach

*Unity-based mobile sports management game with comprehensive coaching system*

## 🚀 **Quick Start**

1. **Open Unity** → Load `Assets/Scenes/FMGCOACH.unity`
2. **Play Scene** → Use N/T/F keys to test
3. **Start Backend** (Optional) → Navigate to `TeammateInteraction/Database/FMG-Coach-Backend-main/` and run `dotnet run`
4. **Test All Screens** → Navigate between Coach Hiring, Performance Analytics, and Coach Profile screens

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

| Screen | Scene File | Purpose |
|--------|------------|---------|
| 🏈 **Coach Hiring Market** | `FMGCOACH.unity` | Main coach hiring and management |
| 📊 **Performance Analytics** | `PerformanceAnalytics.unity` | Team performance tracking |
| 👤 **Coach Profile** | `CoachProfile.unity` | Individual coach details |

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

This project uses **5 focused documents** for complete coverage:

1. **README.md** - This overview and quick start guide
2. **BACKEND_SERVER.md** - API server setup and endpoints
3. **COACH_HIRING_MARKET_SCREEN.md** - Main hiring interface guide  
4. **PERFORMANCE_ANALYTICS_SCREEN.md** - Analytics dashboard setup
5. **COACH_PROFILE_SCREEN.md** - Individual coach details system

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

| Script | Purpose | Location |
|--------|---------|----------|
| `CoachManager.cs` | Main coach management singleton | All scenes |
| `StatCardPopulator.cs` | Performance analytics UI | PerformanceAnalytics |
| `CoachProfilePopulator.cs` | Coach details display | CoachProfile |
| `SystemTester.cs` | Automated testing framework | Any scene |
| `SystemSetupHelper.cs` | Auto-setup missing components | Any scene |

## 🎯 **Getting Started Checklist**

- [ ] Clone repository and open in Unity
- [ ] Verify all 3 scenes load without errors
- [ ] Test Coach Hiring Market with N/T/F keys
- [ ] Check Performance Analytics displays data
- [ ] Verify Coach Profile shows individual details
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

All screens are fully functional with comprehensive testing frameworks. The system supports both online API integration and offline JSON fallbacks, making it suitable for immediate deployment or further development.

**Ready to build and deploy!** 🚀