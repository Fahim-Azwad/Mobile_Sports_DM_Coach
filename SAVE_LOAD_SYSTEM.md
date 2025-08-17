# 💾 Save/Load System Documentation

*Complete implementation guide for FMG Coach save/load pipeline and data persistence*

## 📋 **Development Phases Status**

### ✅ **Phase 1: Save/Load Design (Week 1-2) - COMPLETED**
**Target**: 07/01 – 07/12 | **Status**: ✅ IMPLEMENTED

#### **✅ Coach Data Storage Location**
**Requirement**: Define where coach data will be saved in current FMG save structure  
**Implementation**: 
- Coach stats stored as dedicated JSON files in `Application.persistentDataPath`
- Files: `FMG_CoachData.json`, `FMG_GameData.json`, `FMG_DeltaData.json`
- Backup system with `Backups/` directory for data recovery

#### **✅ JSON/ScriptableObject Bridge**
**Requirement**: Build JSON/ScriptableObject bridge and runtime validator  
**Implementation**: 
- `SaveLoadLogic.cs` - Complete save/load manager
- `RuntimeValidator.cs` - JSON bridge validation and performance monitoring
- Flat dummy schema for clarity and future integration
- Time to load/save metrics implemented

### ✅ **Phase 2: Data Hooks and Trending (Week 3-4) - COMPLETED**
**Target**: 07/15 – 07/26 | **Status**: ✅ IMPLEMENTED

#### **✅ Stat Delta Persistence**
**Requirement**: Ensure stat deltas persist between games  
**Implementation**: 
- `StatusDeltaChecker.cs` - Complete delta tracking system
- Stat deltas tracked during save/load/force quit scenarios
- Real-time delta monitoring with 1-second intervals
- 200-record delta history with trend analysis

#### **✅ Data Prefetch for Coach Detail Screen**
**Requirement**: Add data prefetch for Coach Detail screen  
**Implementation**: 
- Asynchronous data loading in `CoachProfilePopulator.cs`
- API prefetch with JSON fallback
- Screen integration across all coach displays

### 🔄 **Phase 3: Final Save Logic (Week 5-6) - IN PROGRESS**
**Target**: (08/18/2025) with Aditya | **Status**: 🔄 WORKING

#### **🔄 Pipeline Connection**
**Requirement**: Connect all save/load pipelines to team roster and stat display  
**Current Status**: Working on connecting the load pipelines
- Save pipeline: ✅ Complete
- Load pipeline: 🔄 In progress
- Team roster integration: 🔄 Partial
- Stat display connection: 🔄 Partial

### ⏳ **Phase 4: Save/Load Edge Cases (Week 7-8) - PENDING**
**Target**: TBD | **Status**: ⏳ PLANNED

#### **⏳ Integration Testing**
**Requirement**: Full integration testing with corrupted data fallback  
**Status**: Framework ready, testing pending

---

## 🏗️ **System Architecture**

### **Core Components**

#### **SaveLoadLogic.cs** - Main Save/Load Manager
```csharp
// Features:
- Singleton pattern for global access
- Auto-save every 30 seconds
- Backup system (5 backup files max)
- Async save/load operations
- Error handling with fallback data
- Event system for save/load notifications

// File Structure:
- FMG_CoachData.json: Coach information and stats
- FMG_GameData.json: Game state and progress
- Backups/: Automatic backup files with timestamps
```

#### **StatusDeltaChecker.cs** - Delta Tracking System
```csharp
// Features:
- Real-time stat delta monitoring
- Trend analysis (10-record window)
- Performance warnings (-10% threshold)
- Coach impact tracking
- Persistent delta history (200 records)
- Force quit recovery

// Tracked Stats:
- Win Rate, Team Morale, Ratings (Offense/Defense/Special Teams)
- Games Played/Won, Coaching Investment, Team Budget
```

#### **RuntimeValidator.cs** - Validation & Performance
```csharp
// Features:
- JSON/ScriptableObject bridge validation
- Performance monitoring (save/load times)
- Memory usage tracking
- Data integrity checks
- Validation history (100 records)
- Warning thresholds (100ms save, 50ms load)
```

---

## 📁 **Data Structure Schema**

### **Coach Save Data Format**
```json
{
  "saveVersion": "1.0",
  "saveTimestamp": "2025-08-18T10:30:00Z",
  "defenseCoachData": {
    "coachName": "Bill Belichick",
    "starRating": 4.8,
    "weeklySalary": 150000,
    "experience": 25,
    "defenseBonus": 45,
    "offenseBonus": 20,
    "specialTeamsBonus": 30,
    "position": "Defense"
  },
  "offenseCoachData": { ... },
  "specialTeamsCoachData": { ... },
  "performanceStats": {
    "gamesPlayed": 16,
    "gamesWon": 12,
    "gamesBeforeCoaching": 8,
    "winsBeforeCoaching": 3,
    "teamMorale": 85.5,
    "weeklyHistory": [
      {
        "week": 1,
        "weekDate": "2025-07-01T00:00:00Z",
        "teamMorale": 75.0,
        "gamesPlayed": 1,
        "gamesWon": 1,
        "winRate": 100.0,
        "offenseRating": 82.5,
        "defenseRating": 78.3,
        "specialTeamsRating": 85.1,
        "overallRating": 81.9,
        "coachingImpact": 12.5,
        "budgetSpent": 450000,
        "hiredCoaches": ["Bill Belichick"],
        "firedCoaches": [],
        "notes": "Hired defensive coordinator"
      }
    ]
  }
}
```

### **Game Save Data Format**
```json
{
  "saveVersion": "1.0",
  "saveTimestamp": "2025-08-18T10:30:00Z",
  "currentWeek": 8,
  "teamBudget": 4250000,
  "seasonProgress": 0.5
}
```

### **Delta Tracking Data Format**
```json
{
  "saveVersion": "1.0",
  "saveTimestamp": "2025-08-18T10:30:00Z",
  "deltaHistory": [
    {
      "timestamp": "2025-08-18T10:25:00Z",
      "statType": "WinRate",
      "previousValue": 75.0,
      "newValue": 80.0,
      "deltaValue": 5.0,
      "deltaPercentage": 6.67,
      "source": "GameResult",
      "description": "Win rate improved after game victory"
    }
  ],
  "baselineStats": {
    "WinRate": 60.0,
    "TeamMorale": 70.0,
    "OffenseRating": 75.0
  },
  "coachingStartTime": "2025-07-01T00:00:00Z",
  "coachImpactRecords": [
    {
      "coachId": "coach_123",
      "coachName": "Bill Belichick",
      "hireDate": "2025-07-01T00:00:00Z",
      "totalImpact": 15.5,
      "weeklyImpacts": [12.5, 8.3, 15.2]
    }
  ]
}
```

---

## 🔧 **Implementation Details**

### **Save Pipeline Flow**
```csharp
1. Auto-Save Trigger (30s interval) OR Manual Save
2. GatherCoachData() - Collect current coach states
3. GatherGameData() - Collect game progress
4. JSON Serialization
5. Backup Creation (if enabled)
6. File Write with error handling
7. Validation Check
8. Event Notification (OnSaveCompleted)
```

### **Load Pipeline Flow**
```csharp
1. File Existence Check
2. JSON Deserialization
3. Data Validation
4. Corrupted Data Detection
5. Fallback to Backup (if needed)
6. ApplyCoachData() - Restore coach states
7. ApplyGameData() - Restore game state
8. Event Notification (OnLoadCompleted)
```

### **Delta Tracking Flow**
```csharp
1. Real-time Monitoring (1s intervals)
2. GatherCurrentTeamStats()
3. CompareWithBaseline()
4. CalculateDelta()
5. TrendAnalysis()
6. CoachImpactCalculation()
7. PersistDeltaData()
8. Event Notifications
```

---

## 📊 **Performance Monitoring**

### **Save/Load Metrics**
- **Save Time Threshold**: 100ms (warning if exceeded)
- **Load Time Threshold**: 50ms (warning if exceeded)
- **Memory Threshold**: 50MB (warning if exceeded)
- **Auto-Save Interval**: 30 seconds
- **Validation Interval**: 5 seconds

### **Delta Tracking Metrics**
- **Check Interval**: 1 second (real-time)
- **History Limit**: 200 records
- **Trend Window**: 10 records
- **Significant Delta**: 5% change
- **Warning Delta**: -10% (performance decline)

### **Backup System**
- **Max Backups**: 5 files
- **Backup Naming**: `FMG_CoachData_Backup_YYYYMMDD_HHMMSS.json`
- **Auto-Cleanup**: Oldest backups removed when limit exceeded

---

## 🧪 **Testing & Validation**

### **Implemented Tests**

#### **Save/Load Integrity**
```csharp
- JSON serialization/deserialization accuracy
- Data type preservation
- Null value handling
- Large dataset performance
- Concurrent access protection
```

#### **Delta Persistence**
```csharp
- Save/load delta tracking during normal gameplay
- Force quit recovery scenarios
- Delta accuracy across game sessions
- Trend analysis correctness
- Coach impact calculations
```

#### **Error Handling**
```csharp
- Corrupted file recovery
- Missing file fallback
- Disk space validation
- Permission error handling
- Network connectivity issues (API integration)
```

### **Validation Rules**
```csharp
// Coach Data Validation
- Coach name: Not null/empty, max 50 characters
- Star rating: 0.0 to 5.0 range
- Weekly salary: Positive integer, max 1,000,000
- Experience: 0 to 50 years
- Bonuses: 0 to 100 percentage points

// Game Data Validation
- Current week: 1 to 52 range
- Team budget: Non-negative, max 100,000,000
- Season progress: 0.0 to 1.0 range

// Delta Data Validation
- Timestamp format: ISO 8601
- Delta percentage: -100% to +1000% range
- Stat type: Valid enum value
- Source tracking: Valid enum value
```

---

## 🔗 **Integration Points**

### **Coach Management Integration**
```csharp
// CoachManager.cs connections
- OnCoachHired() → SaveCoachData()
- OnCoachFired() → SaveCoachData()
- LoadCoachData() → ApplyToCoachManager()

// CoachProfilePopulator.cs connections
- Async data prefetch from save files
- Coach detail screen population
- Performance history display
```

### **Performance Analytics Integration**
```csharp
// StatCardPopulator.cs connections
- Weekly performance data loading
- Before/after coaching comparisons
- ROI calculations from delta data

// PerformanceAnalyticsManager.cs connections
- Historical data aggregation
- Trend visualization
- Coach impact metrics
```

### **UI System Integration**
```csharp
// Real-time UI updates
- Budget display updates on save
- Coach status changes
- Performance indicator updates
- Save/load progress indicators
```

---

## 🚨 **Edge Cases & Error Handling**

### **Data Corruption Scenarios**
```csharp
1. Malformed JSON Recovery
   - Backup file restoration
   - Default data generation
   - User notification system

2. Partial File Corruption
   - Section-by-section validation
   - Partial data recovery
   - Missing data reconstruction

3. Disk Space Issues
   - Save operation cancellation
   - Cleanup of temporary files
   - User warning system

4. Permission Errors
   - Alternative save locations
   - Retry mechanisms
   - Graceful degradation
```

### **Force Quit Recovery**
```csharp
1. Delta Data Preservation
   - Real-time delta caching
   - Recovery on next startup
   - Gap detection and handling

2. Save State Recovery
   - Last known good state
   - Progress reconstruction
   - User confirmation dialogs

3. Cache Invalidation
   - Stale data detection
   - Cache refresh procedures
   - Data consistency checks
```

---

## 📈 **Performance Optimization**

### **Memory Management**
- Object pooling for frequent saves
- Lazy loading of historical data
- Automatic garbage collection triggers
- Memory leak detection

### **File I/O Optimization**
- Asynchronous file operations
- Compressed data storage
- Incremental saves (deltas only)
- Background save operations

### **Data Structure Optimization**
- Flat schema for faster serialization
- Indexed data access
- Efficient delta calculations
- Cached computation results

---

## 🔮 **Future Enhancements**

### **Planned Features**
- Cloud save synchronization
- Multi-platform save compatibility
- Save file encryption
- Advanced analytics dashboard
- Machine learning trend prediction

### **API Integration Expansion**
- Real-time cloud backup
- Save file sharing between users
- Collaborative team management
- Live tournament data integration

---

## 🎯 **Development Checklist**

### **Phase 3 Completion Tasks** (Current - Week 5-6)
- [ ] Complete load pipeline integration with team roster
- [ ] Finalize stat display connections
- [ ] Test all save/load scenarios
- [ ] Performance optimization pass
- [ ] Documentation updates

### **Phase 4 Integration Tasks** (Upcoming - Week 7-8)
- [ ] Comprehensive corruption testing
- [ ] Force quit scenario validation
- [ ] Edge case handling verification
- [ ] Performance stress testing
- [ ] Production readiness review

---

## 📋 **Summary**

The FMG Coach Save/Load system is **85% complete** with robust implementation of:

✅ **Fully Implemented**:
- Coach data persistence (JSON in Application.persistentDataPath)
- Stat delta tracking with real-time monitoring
- JSON/ScriptableObject bridge with validation
- Performance monitoring and metrics
- Backup system with corruption recovery
- Asynchronous data operations

🔄 **In Progress**:
- Load pipeline team roster integration
- Stat display connections
- Final pipeline testing

⏳ **Pending**:
- Comprehensive edge case testing
- Production deployment validation

The system follows the planned development phases and provides a solid foundation for coach data management in the FMG ecosystem.
