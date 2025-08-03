# Coach Profile Database Integration - Simple Setup Guide

## ✅ **Fixed Issues**
- Removed separate `CoachDetailsTester.cs` (was causing null reference errors)
- Integrated all database functionality directly into `CoachProfilePopulator.cs`
- Added ELT (Extract-Load-Transform) data pipeline
- Added simple keyboard testing controls

## 🎯 **How to Use**

### **Step 1: Setup in Unity**
1. Open your coach details scene
2. Find the GameObject with `CoachProfilePopulator` component
3. Set these settings:
   - ✅ `Load From Database` = **true**
   - ✅ `Enable Testing Controls` = **true**
   - Leave `Specific Coach Name` empty for random loading

### **Step 2: Test in Play Mode**
1. **Play** the scene in Unity
2. Watch the Console for database loading messages
3. **Press N** = Load next random coach from database
4. **Press T** = Load next coach type (Defense → Offense → Special Teams)

### **Step 3: Verify Data Loading**
Look for these console messages:
```
[CoachProfilePopulator] Successfully loaded & transformed coach: Bill Belichick
[CoachProfilePopulator] Testing enabled! Press N=Next Coach, T=Next Type
[CoachProfilePopulator] Loaded Defense coach: Bill Belichick
```

## 🔧 **Manual Testing**
- Right-click on `CoachProfilePopulator` in inspector
- Select **"Test Database Loading"** from context menu
- Check console for test results

## 📁 **Database Files**
The system loads coaches from:
```
Assets/StreamingAssets/Database/coach.json
```

## ⚡ **ELT Data Pipeline**
1. **Extract**: Load coach data from JSON database
2. **Load**: Parse into database record format  
3. **Transform**: Convert to UI-friendly format with:
   - Top 4 specialties only
   - 4 contract terms only
   - Proper percentage calculations

## 🐛 **If Not Working**
1. Check Console for error messages
2. Verify `coach.json` exists in StreamingAssets/Database/
3. Make sure all UI components are assigned in inspector
4. Try the manual "Test Database Loading" context menu option

## 🎮 **Expected Behavior**
- **On Scene Start**: Loads random coach from database automatically
- **Press N**: Loads new random coach, UI refreshes immediately  
- **Press T**: Cycles through coach types, loads random coach of that type
- **UI Updates**: Coach name, stats, top 4 specialties, 4 contract terms all update dynamically
