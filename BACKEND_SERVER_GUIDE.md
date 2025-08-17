# 🚀 How to Run Ruturaj's Backend API Server

## 📍 Backend Location Found
Your backend API server is located at:
```
D:\eSports AI\Mobile_Sports_DM_Coach\TeammateInteraction\Database\FMG-Coach-Backend-main\
```

## ⚡ Quick Start Guide

### Step 1: Open PowerShell/Command Prompt
1. Press `Win + R`, type `cmd` or `powershell`, press Enter
2. Or open Terminal in VS Code (`Ctrl + Shift + ` `)

### Step 2: Navigate to Backend Directory
```powershell
cd "D:\eSports AI\Mobile_Sports_DM_Coach\TeammateInteraction\Database\FMG-Coach-Backend-main"
```

### Step 3: Run the Backend Server
```powershell
dotnet run
```

### Expected Output
You should see something like:
```
Building...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5175
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

## 🎯 Verify It's Working

### Option 1: Check in Browser
Open your browser and go to: `http://localhost:5175`

### Option 2: Test API Endpoints
You can test these endpoints in your browser or Postman:
- `http://localhost:5175/api/coach/all` - Get all coaches
- `http://localhost:5175/api/coach/teamDetails/4d1c8be1-c9f0-4f0f-9e91-b424d8343f86` - Get team details

### Option 3: Watch Unity Logs
In Unity, you should now see:
```
✅ API server is available - using dynamic API data
Loaded team budget: $X,XXX,XXX
Loaded X coaches from API with filter: All
```

## 🛠 Troubleshooting

### If you get "dotnet not found":
1. Install .NET SDK from: https://dotnet.microsoft.com/download
2. Restart your terminal
3. Try `dotnet --version` to verify installation

### If you get port 5175 already in use:
1. Kill any existing processes: `netstat -ano | findstr :5175`
2. Or change the port in `Properties/launchSettings.json`

### If you get database errors:
The SQLite database `FMGDatabase_Test.db` is already present, so it should work immediately.

## 🎮 Integration with Unity

Once the server is running:
1. **Start Unity** - Your game should automatically detect the API
2. **Check Console** - Look for "✅ API server is available"
3. **Test Coach Hiring** - All features now use live backend data
4. **Dynamic Updates** - Budget, coaches, and hiring all work with real API calls

## 📋 API Endpoints Available

Based on your Unity integration, these endpoints are active:
- `GET /api/coach/all` - Get all coaches
- `GET /api/coach/teamDetails/{teamId}` - Get team budget and details  
- `POST /api/coach/isAffordable` - Check if coach is affordable
- `PATCH /api/coach/hire` - Hire a coach

## 🔄 Development Workflow

1. **Start Backend**: `dotnet run` in backend directory
2. **Start Unity**: Your game will detect and use the API
3. **Make Changes**: Backend auto-reloads on file changes
4. **Test**: Unity immediately uses updated API responses

## 🎉 You're Ready!

Your backend server is now running at `http://localhost:5175` and your Unity game will use dynamic API data instead of static fallbacks!
