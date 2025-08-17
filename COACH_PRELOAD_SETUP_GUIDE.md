# Coach Pre-loading Setup Guide

## 🚀 **Quick Setup (5 minutes):**

### 1. **CoachManager Configuration**
1. **Find CoachManager** in your FMGCOACH scene
2. **Set Inspector Values**:
   - ✅ `loadFromAPI = true` (to enable pre-loading)
   - ✅ `baseURL = "http://localhost:5175"` (your API server)
   - ✅ `teamId = "4d1c8be1-c9f0-4f0f-9e91-b424d8343f86"` (default team)

### 2. **CoachSlotUI Setup**
1. **Find your coach slot GameObjects** in the scene
2. **For each CoachSlotUI component**:
   - ✅ Set `type = CoachType.Offense` for offense slot
   - ✅ Set `type = CoachType.Defense` for defense slot
   - ✅ Assign all UI references (nameText, salaryText, etc.)

### 3. **Testing Setup (Optional)**
1. **Add CoachPreloadTester** to any GameObject
2. **Enable showDebugInfo** in inspector
3. **Play scene** and use P/F keys for testing

## 🎮 **Expected Behavior:**

### On Scene Start:
```
[CoachManager] Starting pre-load of team coaches from API...
[CoachManager] API connection successful  
[CoachManager] Pre-loaded defense coach: Bill Belichick
[CoachManager] Pre-loaded offense coach: Andy Reid
```

### UI Display:
- **Defense Slot**: Shows hired defense coach details
- **Offense Slot**: Shows hired offense coach details
- **Empty Slots**: Show "Hire Coach" button if no coach assigned

## 🔧 **Troubleshooting:**

### Issue: "API connection failed"
**Solution**: 
1. Make sure backend API server is running on localhost:5175
2. Check `baseURL` in CoachManager inspector
3. System will automatically fall back to JSON file

### Issue: "No coaches pre-loaded"
**Solution**:
1. Verify `loadFromAPI = true` in CoachManager
2. Check console for error messages
3. Ensure API has coaches with types "D" and "O"

### Issue: "CoachSlotUI not updating"
**Solution**:
1. Check `type` field is set correctly (Offense/Defense)
2. Verify all UI references assigned in inspector
3. Make sure CoachManager instance exists in scene

## 🎯 **API Requirements:**

### Required Endpoint:
- **GET /api/coach/all** - Must return array of coaches
- **Coach Format**: Each coach needs `coachType` field ("D" or "O")

### Fallback File:
- **Path**: `Assets/StreamingAssets/Database/coach.json`
- **Format**: Array of coach objects with `coach_type` field

## ✅ **Verification Checklist:**

- [ ] CoachManager has `loadFromAPI = true`
- [ ] API server running on correct URL
- [ ] CoachSlotUI components have correct `type` values
- [ ] All UI references assigned in inspector
- [ ] Console shows successful pre-loading messages
- [ ] UI displays coach names and details

**Ready to test!** 🚀 Play your FMGCOACH scene and watch the coaches automatically load from your database.
