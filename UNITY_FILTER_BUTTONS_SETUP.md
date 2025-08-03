# Unity Editor Setup Guide: Filter Buttons for Coach Hiring Market

## 🎯 **Quick Setup Using FilterButtonSetup Script**

### **Method 1: Automatic Setup (Recommended)**

1. **Add FilterButtonSetup Script**
   - In Unity, go to your Coach Hiring Market scene
   - Find the CoachHiringMarket GameObject (or create an empty GameObject)
   - Add the `FilterButtonSetup` component to it

2. **Configure References**
   - In the FilterButtonSetup inspector:
     - Drag the CoachHiringMarket GameObject to the "Coach Hiring Market" field
     - Optionally set a parent container (Canvas or UI Panel)

3. **Run Auto-Setup**
   - **Option A**: Click the ⚙️ gear icon next to FilterButtonSetup → "Setup Filter Buttons"
   - **Option B**: Press **F1** while in Play mode
   - **Option C**: Right-click the component → "Setup Filter Buttons"

4. **Verify Setup**
   - Check the CoachHiringMarket inspector
   - All 4 button fields should now be assigned
   - Remove the FilterButtonSetup script after setup

---

## 🛠️ **Method 2: Manual Setup**

### **Step 1: Create Button Container**
```
Canvas
└── CoachHiringMarket UI
    └── FilterButtonsContainer (New)
        ├── ALL_FilterButton
        ├── D_FilterButton  
        ├── O_FilterButton
        └── S_FilterButton
```

### **Step 2: Create Individual Buttons**

For each button (ALL, D, O, S):

1. **Right-click** in Hierarchy → UI → Button - TextMeshPro
2. **Rename** to: `ALL_FilterButton`, `D_FilterButton`, etc.
3. **Set RectTransform**:
   - Width: 60, Height: 40
   - Position as needed
4. **Set Button Text**:
   - Select the Text child object
   - Change text to "ALL", "D", "O", or "S"
5. **Style Button** (optional):
   - Set background color, font size, etc.

### **Step 3: Assign Button References**

1. **Select CoachHiringMarket GameObject**
2. **In Inspector**, find the "Filter Toggle Buttons" section
3. **Drag buttons** to corresponding fields:
   - `ALL_FilterButton` → **allFilterButton**
   - `D_FilterButton` → **defenseFilterButton**
   - `O_FilterButton` → **offenseFilterButton**
   - `S_FilterButton` → **specialTeamsFilterButton**

---

## 📋 **Recommended Button Layout**

### **Layout Option 1: Horizontal Row**
```css
Position: Top of coach hiring screen
Layout: [ALL] [D] [O] [S]   [Existing Dropdown ▼]
Size: 60x40 each, 10px spacing
```

### **Layout Option 2: Compact Grid**
```css
Position: Side panel
Layout: [ALL] [D]
        [O]   [S]
Size: 50x50 each
```

---

## 🎨 **Styling Recommendations**

### **Button Appearance**
- **Size**: 60x40 pixels (or 50x50 for square)
- **Font**: Bold, 14pt
- **Colors**: White background, black text
- **Active State**: Handled automatically by script (blue highlight)

### **Container Setup**
- **Add Horizontal Layout Group** for automatic spacing
- **Content Size Fitter** for dynamic sizing
- **Position**: Near existing filter dropdown

---

## ✅ **Testing Instructions**

### **After Setup:**
1. **Enter Play Mode**
2. **Click each button** (ALL, D, O, S)
3. **Verify**:
   - Active button turns blue
   - Coach list updates with filtered coaches
   - Dropdown syncs with button selection
4. **Test keyboard controls**:
   - **N**: Load new coaches
   - **T**: Toggle/refresh
   - **F**: Cycle filters

---

## 🔧 **Troubleshooting**

### **Buttons Not Working:**
- Check that all 4 button references are assigned in CoachHiringMarket inspector
- Verify buttons have the Button component
- Ensure CoachHiringMarket script has no compilation errors

### **No Visual Feedback:**
- Buttons should automatically change color when clicked
- If not working, check button ColorBlock settings
- Verify button Interactable is enabled

### **Database Not Loading:**
- Place coach.json in `StreamingAssets/Database/` folder
- Check console for loading errors
- Verify JSON format matches CoachDatabaseRecord structure

---

## 📝 **Notes**

- **FilterButtonSetup.cs** is a helper script - remove it after setup
- **All existing functionality** remains unchanged (dropdown, keyboard controls)
- **Visual feedback** is handled automatically by the enhanced CoachHiringMarket script
- **Buttons are optional** - if not assigned, dropdown filter still works
