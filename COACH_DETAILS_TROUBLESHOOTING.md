# Coach Details Setup Instructions

## Current Issue: Hardcoded coaches still showing instead of database coaches

### Debugging Steps:

1. **Add DatabaseConnectionTest script to any GameObject**
   - Attach `DatabaseConnectionTest.cs` to any GameObject in the scene
   - Check console for database connectivity logs
   - Press F1 in play mode to run test again

2. **Verify CoachDetailsTester Setup**
   - Find the GameObject with `CoachProfilePopulator` component
   - Add `CoachDetailsTester` component to the same GameObject
   - Ensure `profilePopulator` field is assigned in inspector
   - Enable `enableDebugLogs` for detailed logging

3. **Check CoachProfilePopulator Settings**
   - Find GameObject with `CoachProfilePopulator` component
   - Ensure `loadFromDatabase` is checked (true)
   - Leave `specificCoachName` empty for random loading
   - Verify all UI references are assigned

4. **Verify File Paths**
   - Check that `Assets/StreamingAssets/Database/coach.json` exists
   - File should be ~1025 lines with coach data
   - Check Unity Console for path-related errors

### Expected Console Output on Working Setup:

```
[DatabaseConnectionTest] StreamingAssets Path: [path]
[DatabaseConnectionTest] Database Directory Exists: True
[DatabaseConnectionTest] Coach JSON Exists: True  
[DatabaseConnectionTest] Coach JSON Size: [size] bytes
[DatabaseConnectionTest] API loaded 50+ coaches
[DatabaseConnectionTest] First coach: [name] ([type])
```

### Expected Behavior When Pressing Keys:

- **N Key**: Load random coach, see new name/specialties
- **T Key**: Cycle through Defense → Offense → Special Teams
- **R Key**: Refresh cache and reload
- **L Key**: Load Bill Belichick specifically

### Troubleshooting:

**If no console logs appear when pressing keys:**
- CoachDetailsTester component not attached
- GameObject is inactive
- Script compilation errors

**If database connection fails:**
- StreamingAssets folder missing or incorrect path
- coach.json file missing or corrupted
- JSON parsing errors

**If coaches load but UI doesn't update:**
- UI references not assigned in CoachProfilePopulator
- Prefabs missing for specialty/contract rows
- Transform containers not set

### Manual Verification:

1. Play the scene
2. Open Console window (Window → General → Console)
3. Press F1 to run database test
4. Check for any red error messages
5. Press N/T/R/L keys and watch for logs
6. Verify coach name changes in UI

### Quick Fix if Still Not Working:

Set `loadFromDatabase = false` in CoachProfilePopulator to use fallback data, then gradually re-enable database loading once issues are resolved.
