# 🔧 Technical Implementation Guide - FMG Coaching System

*Detailed technical documentation for developers working on the coaching system*

## 🏗️ **System Architecture**

### **Three-Layer Architecture**
```
┌─────────────────────────────────────────┐
│           UI Layer                      │
│  CoachHiringMarket, StatCardPopulator   │
└─────────────────────────────────────────┘
┌─────────────────────────────────────────┐
│         Business Logic Layer            │
│  SaveLoadLogic, RuntimeValidator,       │
│  StatusDeltaChecker                     │
└─────────────────────────────────────────┘
┌─────────────────────────────────────────┐
│           Data Layer                    │
│  JSON Database, ScriptableObjects,      │
│  Application.persistentDataPath         │
└─────────────────────────────────────────┘
```

---

## 💾 **Data Flow Architecture**

### **Coach Database Integration**
```mermaid
StreamingAssets/Database/coach.json 
    ↓
CoachData.cs (Unified Structure)
    ↓
CoachManager.cs (Singleton Management)
    ↓
UI Components (Dynamic Population)
```

### **Save/Load Pipeline**
```csharp
SaveLoadLogic.SaveCoachData()
    ↓
GatherCoachData() // Extract current state
    ↓
JSON Serialization // Convert to persistent format
    ↓
Application.persistentDataPath // Write to disk
    ↓
CreateBackup() // Safety backup
```

### **Delta Tracking Flow**
```csharp
StatusDeltaChecker.SetBaselineStats()
    ↓
UpdateCurrentStats() // Monitor changes
    ↓
RecordStatDelta() // Log significant changes
    ↓
AnalyzePerformanceTrends() // Calculate trends
    ↓
OnStatDeltaDetected event // Notify system
```

---

## 🛠️ **Core Components Deep Dive**

### **SaveLoadLogic.cs**
**Purpose**: Central save/load manager with backup system
```csharp
Key Methods:
├── SaveCoachData() -> Async save with error handling
├── LoadCoachData() -> Restore with fallback to backup
├── GatherCoachData() -> Extract current game state
├── CreateBackup() -> Safety backup system
└── RestoreFromBackup() -> Recovery mechanism

Event System:
├── OnSaveCompleted -> Notify save success/failure
├── OnLoadCompleted -> Notify load completion
└── OnSaveError -> Error handling and user notification
```

### **RuntimeValidator.cs**
**Purpose**: JSON/ScriptableObject bridge validator with performance monitoring
```csharp
Validation Types:
├── ValidateJSONBridge() -> JSON parsing integrity
├── ValidateScriptableObjectBridge() -> Object conversion
├── PerformRuntimeValidation() -> System health checks
└── TestRoundTripConversion() -> Data consistency

Performance Monitoring:
├── StartPerformanceMeasurement() -> Begin timing
├── EndPerformanceMeasurement() -> Record metrics
└── CheckPerformanceThresholds() -> Warning system
```

### **StatusDeltaChecker.cs**
**Purpose**: Stat delta persistence and coaching impact analysis
```csharp
Delta Tracking:
├── SetBaselineStats() -> Establish starting point
├── UpdateCurrentStats() -> Monitor changes
├── RecordStatDelta() -> Log meaningful changes
└── AnalyzePerformanceTrends() -> Calculate trends

Coach Impact:
├── RecordCoachHired() -> Track hiring events
├── RecordCoachFired() -> Log firing events
├── CalculateCoachImpact() -> ROI analysis
└── GetCoachImpactRecords() -> Historical data
```

---

## 🔗 **Integration Patterns**

### **Singleton Pattern Usage**
```csharp
// All core systems use singleton pattern for global access
public static SaveLoadLogic Instance { get; private set; }
public static RuntimeValidator Instance { get; private set; }
public static StatusDeltaChecker Instance { get; private set; }

// Thread-safe initialization in Awake()
private void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    else
    {
        Destroy(gameObject);
    }
}
```

### **Event-Driven Communication**
```csharp
// SaveLoadLogic events
public static event System.Action<bool> OnSaveCompleted;
public static event System.Action<bool> OnLoadCompleted;
public static event System.Action<string> OnSaveError;

// RuntimeValidator events
public static event System.Action<ValidationResult> OnValidationCompleted;
public static event System.Action<string> OnValidationWarning;

// StatusDeltaChecker events
public static event System.Action<StatDelta> OnStatDeltaDetected;
public static event System.Action<TrendAnalysis> OnTrendAnalysisUpdated;
```

### **ELT Data Pipeline**
```csharp
// Extract-Load-Transform pattern
public TeamPerformanceData ExtractTeamPerformance()
{
    // Extract raw data from multiple sources
    var teamData = new TeamPerformanceData();
    teamData.budget = HiringMarketAPI.GetTeamBudget();
    teamData.coaches = FMGCoachingStaffAPI.GetHiredCoaches();
    return teamData;
}

public List<StatEntry> LoadPerformanceStats(TeamPerformanceData data)
{
    // Transform extracted data into UI-ready format
    var stats = new List<StatEntry>();
    stats.Add(CalculateOffenseRating(data));
    stats.Add(CalculateDefenseRating(data));
    return stats;
}
```

---

## 🎛️ **Configuration & Customization**

### **Performance Tuning**
```csharp
// Adjust these based on your game's needs:

// SaveLoadLogic
private float autoSaveInterval = 60f; // Auto-save frequency
private int maxBackupFiles = 5; // Backup retention

// RuntimeValidator  
private float validationInterval = 5f; // Validation frequency
private float saveTimeWarningThreshold = 100f; // Performance warning

// StatusDeltaChecker
private float deltaCheckInterval = 1f; // Delta monitoring rate
private int maxDeltaHistory = 200; // Historical data retention
```

### **Debug Configuration**
```csharp
// Enable/disable debug features
[SerializeField] private bool enableRuntimeValidation = true;
[SerializeField] private bool enablePerformanceMonitoring = true;
[SerializeField] private bool enableDeltaTracking = true;
[SerializeField] private bool showDebugLogs = true;
```

---

## 📊 **Data Structures**

### **CoachSaveData**
```csharp
[System.Serializable]
public class CoachSaveData
{
    public string saveVersion;
    public string saveTimestamp;
    public CoachDataSave defenseCoachData;
    public CoachDataSave offenseCoachData;
    public CoachDataSave specialTeamsCoachData;
    public CoachingStatsSave coachingStats;
    public float teamBudget;
}
```

### **StatDelta**
```csharp
[System.Serializable]
public class StatDelta
{
    public DateTime timestamp;
    public StatType statType;
    public float oldValue;
    public float newValue;
    public float deltaValue;
    public float deltaPercentage;
    public DeltaSource source;
    public string additionalInfo;
}
```

### **ValidationResult**
```csharp
[System.Serializable]
public class ValidationResult
{
    public DateTime timestamp;
    public ValidationType validationType;
    public string targetType;
    public bool isValid;
    public string errorMessage;
    public List<string> validationDetails;
    public long validationTime; // milliseconds
}
```

---

## 🧪 **Testing Framework**

### **SystemTester.cs Usage**
```csharp
// Automated testing
public IEnumerator RunAllTests()
{
    // 1. Check script instances
    bool scriptsLoaded = TestScriptInstances();
    
    // 2. Test SaveLoadLogic
    bool saveLoadWorks = TestSaveLoadLogic();
    
    // 3. Test RuntimeValidator
    bool validatorWorks = TestRuntimeValidator();
    
    // 4. Test StatusDeltaChecker
    bool deltaWorks = TestStatusDeltaChecker();
    
    // 5. Integration test
    bool integrationWorks = TestSystemIntegration();
}
```

### **Manual Testing Commands**
```csharp
// Test individual components
SaveLoadLogic.Instance.SaveCoachData();
RuntimeValidator.Instance.ForceValidation();
StatusDeltaChecker.Instance.ForceDeltaCheck();

// Check system state
var saveData = SaveLoadLogic.Instance.GetCurrentCoachData();
var validationHistory = RuntimeValidator.Instance.GetValidationHistory();
var deltaHistory = StatusDeltaChecker.Instance.GetDeltaHistory();
```

---

## 🚨 **Error Handling & Recovery**

### **Common Error Scenarios**
```csharp
// 1. Save file corruption
catch (JsonException e)
{
    Debug.LogError($"JSON parsing failed: {e.Message}");
    RestoreFromBackup();
}

// 2. File system access issues
catch (UnauthorizedAccessException e)
{
    Debug.LogError($"File access denied: {e.Message}");
    // Fallback to PlayerPrefs or temporary storage
}

// 3. Missing coach data
if (coach == null)
{
    Debug.LogWarning("Coach data missing, creating default");
    coach = CreateDefaultCoachData();
}
```

### **Recovery Mechanisms**
```csharp
// Backup system
public void RestoreFromBackup()
{
    var backupFiles = GetBackupFiles();
    foreach (var backup in backupFiles.OrderByDescending(f => f.CreationTime))
    {
        if (TryLoadBackup(backup.FullName))
        {
            Debug.Log($"Restored from backup: {backup.Name}");
            return;
        }
    }
}
```

---

## 📈 **Performance Optimization**

### **Memory Management**
```csharp
// Efficient data handling
private void Update()
{
    // Only validate periodically, not every frame
    if (Time.time - lastValidationTime > validationInterval)
    {
        PerformRuntimeValidation();
        lastValidationTime = Time.time;
    }
}

// Memory monitoring
long currentMemory = GC.GetTotalMemory(false) / (1024 * 1024);
if (currentMemory > memoryWarningThreshold)
{
    OnValidationWarning?.Invoke($"High memory usage: {currentMemory} MB");
}
```

### **Async Operations**
```csharp
// Non-blocking save operations
public async void SaveCoachDataAsync()
{
    await Task.Run(() =>
    {
        string jsonData = JsonUtility.ToJson(coachSaveData, true);
        File.WriteAllText(saveFilePath, jsonData);
    });
}
```

---

## 🔄 **Version Management**

### **Save Data Versioning**
```csharp
public class CoachSaveData
{
    public string saveVersion = "1.0"; // Track save format version
    
    // Migration logic for version updates
    public void MigrateFromVersion(string oldVersion)
    {
        switch (oldVersion)
        {
            case "0.9":
                // Migrate from 0.9 to 1.0
                ConvertOldFormat();
                break;
        }
    }
}
```

*This technical guide covers the implementation details for developers extending or maintaining the coaching system.*
