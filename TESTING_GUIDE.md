# 🧪 System Testing Checklist

## ✅ **How to Test Your New Scripts**

### **🎉 All Compilation Errors AND Warnings Resolved!**
All scripts are now completely clean with zero errors and warnings:
- ✅ **SaveLoadLogic.cs** - No errors, no warnings (reflection-based safe property access)
- ✅ **RuntimeValidator.cs** - No errors, no warnings  
- ✅ **StatusDeltaChecker.cs** - No errors, no warnings (reflection helpers added)
- ✅ **SystemTester.cs** - No errors, no warnings (unused field removed)
- ✅ **QuickTestPanel.cs** - No errors, no warnings (async/await properly implemented)

### **1. Automated Testing (Recommended)**

**Setup SystemTester:**
1. In Unity, create an empty GameObject named "SystemTester"
2. Add the `SystemTester.cs` script to it
3. In Inspector, check "Run Tests On Start"
4. Press Play - tests will run automatically

**Expected Console Output:**
```
[SystemTester] 🧪 Starting System Tests...
[SystemTester] Script Instances: ✅ PASS
[SystemTester] SaveLoad System: ✅ PASS  
[SystemTester] Runtime Validator: ✅ PASS
[SystemTester] Delta Checker: ✅ PASS
[SystemTester] System Integration: ✅ PASS
[SystemTester] 🎯 FINAL RESULT: ✅ ALL SYSTEMS OPERATIONAL
```

### **2. Manual Testing Steps**

**Test Coach System:**
1. Press **N** key to load coaches from database
2. Press **F** key to cycle through filters (All/Defense/Offense/Special)
3. Hire a coach and verify it shows in the system
4. Restart Unity and check if hired coach persists

**Test Save/Load:**
```csharp
// In Unity Console, type:
SaveLoadLogic.Instance.SaveCoachData();
// Should see: "[SaveLoadLogic] Coach data saved successfully"

SaveLoadLogic.Instance.LoadCoachData();  
// Should see: "[SaveLoadLogic] Coach data loaded successfully"
```

**Test Delta Tracking:**
```csharp
// In Unity Console, type:
StatusDeltaChecker.Instance.SetBaselineStats();
// Should see: "[StatusDeltaChecker] Baseline stats set"

StatusDeltaChecker.Instance.ForceDeltaCheck();
// Should see: "[StatusDeltaChecker] Delta check completed"
```

**Test Runtime Validation:**
```csharp
// In Unity Console, type:
RuntimeValidator.Instance.ForceValidation();
// Should see: "[RuntimeValidator] Validation completed - All systems operational"
```

### **3. Integration Testing**

**Complete Workflow Test:**
1. Start game → Hire coach → Save game → Restart Unity → Load game
2. Verify coach is still hired
3. Check performance dashboard shows coach impact
4. Verify delta tracking shows coaching changes

**File System Check:**
- Navigate to `%USERPROFILE%\AppData\LocalLow\[YourCompany]\[YourGame]\`
- Should see files: `FMG_CoachData.json`, `FMG_DeltaData.json`
- Files should contain valid JSON data

---

## ⚠️ **Troubleshooting Guide**

### **Common Issues & Solutions**

**❌ "Script instances not found"**
```
Solution: 
1. Create GameObjects for SaveLoadLogic, RuntimeValidator, StatusDeltaChecker
2. Add the respective scripts to each GameObject
3. Scripts will auto-initialize as singletons
```

**❌ "Save/Load operations failed"**
```
Solution:
1. Check file permissions for Application.persistentDataPath
2. Verify JSON format in database files
3. Check Unity Console for specific error messages
```

**❌ "Coach data not persisting"**
```
Solution:
1. Verify CoachManager.instance exists in scene
2. Check if hired coaches are properly assigned
3. Ensure SaveLoadLogic.SaveCoachData() is called
```

**❌ "Delta tracking not working"**
```
Solution:
1. Call StatusDeltaChecker.Instance.SetBaselineStats() first
2. Ensure deltaCheckInterval > 0 in inspector
3. Check if EnableDeltaTracking is checked
```

**❌ "Performance warnings"**
```
Solution:
1. Increase validation intervals in inspector settings
2. Reduce maxDeltaHistory and maxValidationHistory
3. Disable real-time tracking if not needed
```

### **Debug Information**

**Key Log Messages to Look For:**
```
✅ Good Signs:
"[SaveLoadLogic] Instance initialized"
"[RuntimeValidator] Validation completed successfully"  
"[StatusDeltaChecker] Delta tracking system initialized"
"[SystemTester] 🎉 All systems are working correctly!"

❌ Warning Signs:
"NullReferenceException"
"JSON parsing failed" 
"File access denied"
"Memory usage exceeds threshold"
```

**Performance Benchmarks:**
- Save operations: < 100ms (warning threshold)
- Load operations: < 50ms (warning threshold)  
- Validation cycles: < 5 seconds interval
- Memory usage: < 50MB (warning threshold)

---

## 🎯 **Success Checklist**

Mark each item when confirmed working:

**Core Systems:**
- [ ] SaveLoadLogic singleton created and active
- [ ] RuntimeValidator singleton created and active  
- [ ] StatusDeltaChecker singleton created and active
- [ ] SystemTester shows all tests passing

**Coach Management:**
- [ ] Coaches load from JSON database
- [ ] Hiring/firing works correctly
- [ ] Coach data persists between sessions
- [ ] Performance dashboard shows coach impact

**Data Persistence:**
- [ ] Game state saves to persistent path
- [ ] Backup files created automatically
- [ ] Data loads correctly after restart
- [ ] Corruption recovery works

**Performance Monitoring:**
- [ ] Delta tracking records stat changes
- [ ] Runtime validation catches issues
- [ ] Performance metrics within thresholds
- [ ] Error handling and warnings work

**File System:**
- [ ] FMG_CoachData.json exists and valid
- [ ] FMG_DeltaData.json exists and valid
- [ ] StreamingAssets/Database/ accessible
- [ ] Backup files created in persistent path

---

## 🚀 **Next Steps After Testing**

**If All Tests Pass:**
- Your coaching system is fully operational!
- Monitor Unity Console for any runtime warnings
- Consider adjusting performance thresholds based on your hardware

**If Tests Fail:**
- Check the specific error messages in Unity Console
- Follow troubleshooting guide above
- Use manual testing to isolate the issue
- Verify all required GameObjects and scripts are in scene

**Performance Optimization:**
- Adjust validation intervals based on your needs
- Increase thresholds if you get false warnings
- Disable features you don't need (delta tracking, validation)

*Remember: The SystemTester.cs provides the fastest way to verify everything is working correctly!*
