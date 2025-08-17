# 🌐 Backend Server Setup & API Guide

*Complete guide for running and using the .NET API backend server*

## 🚀 **Quick Start**

### **Start the Server**
```powershell
cd "D:\eSports AI\Mobile_Sports_DM_Coach\TeammateInteraction\Database\FMG-Coach-Backend-main"
dotnet run
```

**Expected Output:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5175
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

### **Verify Server is Running**
- **Browser Test**: Open `http://localhost:5175`
- **API Test**: Visit `http://localhost:5175/api/coach/all`

## 📡 **API Endpoints**

### **Coach Management**
| Method | Endpoint | Description | Example |
|--------|----------|-------------|---------|
| `GET` | `/api/coach/all` | Get all coaches | `http://localhost:5175/api/coach/all` |
| `GET` | `/api/coach/{id}` | Get specific coach | `http://localhost:5175/api/coach/123` |
| `GET` | `/api/coach/teamDetails/{teamId}` | Get team details | `http://localhost:5175/api/coach/teamDetails/4d1c8be1-c9f0-4f0f-9e91-b424d8343f86` |
| `POST` | `/api/coach/isAffordable` | Check affordability | Body: `{"coachId":"123","teamBudget":5000000}` |
| `PATCH` | `/api/coach/hire` | Hire a coach | Body: `{"coachId":"123","teamId":"456"}` |

### **Team Management**
| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/team` | Get team information |
| `POST` | `/api/team` | Create new team |
| `PUT` | `/api/team/{id}` | Update team |

### **Performance Analytics**
| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/performance/{teamId}` | Get team performance data |
| `GET` | `/api/performance/matches/{teamId}` | Get match history |
| `POST` | `/api/performance/calculate` | Calculate coaching impact |

## 🧪 **Testing with Postman**

### **1. Get All Coaches**
```
GET http://localhost:5175/api/coach/all
Headers: Content-Type: application/json
```

**Expected Response:**
```json
[
  {
    "coach_id": "1",
    "coach_name": "Bill Belichick",
    "coach_type": "D",
    "experience": 25,
    "salary": 8000000,
    "overall_rating": 4.8,
    "run_defence": 9.5,
    "pressure_control": 9.2
  }
]
```

### **2. Check Coach Affordability**
```
POST http://localhost:5175/api/coach/isAffordable
Content-Type: application/json

{
  "coachId": "1",
  "teamBudget": 10000000
}
```

### **3. Hire a Coach**
```
PATCH http://localhost:5175/api/coach/hire
Content-Type: application/json

{
  "coachId": "1",
  "teamId": "4d1c8be1-c9f0-4f0f-9e91-b424d8343f86"
}
```

## 🔧 **Unity Integration**

### **API Configuration in Unity**
In your Unity scripts, configure these settings:

```csharp
[Header("API Configuration")]
[SerializeField] private string baseURL = "http://localhost:5175";
[SerializeField] private bool useAPI = true; // Toggle API vs offline mode
```

### **Components with API Integration**
1. **CoachManager.cs** - Coach hiring and management
2. **StatCardPopulator.cs** - Performance analytics data
3. **CoachProfilePopulator.cs** - Individual coach details
4. **TeamManager.cs** - Team creation and management

### **API Status Detection**
The system automatically detects if the API server is available:

```csharp
// Test API connection
yield return StartCoroutine(TestAPIConnection());

if (isAPIAvailable)
{
    Debug.Log("✅ API server is available - using dynamic API data");
    // Load from API
}
else
{
    Debug.Log("⚠️ API server unavailable - using offline JSON fallback");
    // Load from local JSON files
}
```

## 📊 **Database Information**

### **Database File**
- **Location**: `FMG-Coach-Backend-main/FMGDatabase_Test.db`
- **Type**: SQLite database
- **Tables**: `coach`, `team`, `matches`, `performance`

### **Coach Table Schema**
```sql
CREATE TABLE coach (
    coach_id TEXT PRIMARY KEY,
    coach_name TEXT NOT NULL,
    coach_type TEXT NOT NULL,
    experience INTEGER,
    salary INTEGER,
    overall_rating REAL,
    run_defence REAL,
    pressure_control REAL,
    coverage_discipline REAL,
    turnover REAL,
    passing_efficiency REAL,
    rush REAL,
    red_zone_conversion REAL,
    play_variation REAL,
    field_goal_accuracy REAL,
    kickoff_instance REAL,
    return_speed REAL,
    return_coverage REAL
);
```

## 🛠️ **Development Setup**

### **Prerequisites**
- **.NET 6.0 SDK** or later
- **Entity Framework Core**
- **SQLite database** (included)

### **Project Structure**
```
FMG-Coach-Backend-main/
├── Controllers/
│   ├── CoachController.cs      # Coach API endpoints
│   ├── TeamController.cs       # Team management
│   └── PerformanceController.cs # Analytics endpoints
├── Models/
│   ├── Coach.cs               # Coach data model
│   ├── Team.cs                # Team data model
│   └── Performance.cs         # Performance metrics
├── Data/
│   └── ApplicationDbContext.cs # Database context
├── FMGDatabase_Test.db        # SQLite database
└── Properties/
    └── launchSettings.json    # Server configuration
```

### **Adding New Endpoints**
1. **Create Model** in `Models/` folder
2. **Add Controller** in `Controllers/` folder
3. **Update DbContext** in `Data/ApplicationDbContext.cs`
4. **Test with Postman**

## 🔄 **API Response Formats**

### **Success Response**
```json
{
  "success": true,
  "data": { ... },
  "message": "Operation completed successfully"
}
```

### **Error Response**
```json
{
  "success": false,
  "error": "Error description",
  "details": "Additional error information"
}
```

## 🚨 **Troubleshooting**

### **Common Issues**

#### **Port 5175 Already in Use**
```powershell
# Find process using port 5175
netstat -ano | findstr :5175

# Kill the process (replace PID with actual process ID)
taskkill /PID 1234 /F
```

#### **Database Connection Errors**
- Verify `FMGDatabase_Test.db` exists in project root
- Check database file permissions
- Ensure Entity Framework packages are installed

#### **CORS Issues (if accessing from web)**
The server includes CORS configuration for Unity WebGL builds.

### **Performance Optimization**
- **Connection Pooling**: Enabled by default
- **Database Indexing**: Applied to frequently queried fields
- **Response Caching**: Implemented for static coach data

## 🔐 **Security Considerations**

### **Development Mode**
- Server runs on localhost only
- CORS enabled for Unity development
- Detailed error messages for debugging

### **Production Deployment**
- Update connection strings for production database
- Configure proper CORS policies
- Enable HTTPS and authentication
- Implement rate limiting

## 📈 **Monitoring & Logging**

### **Server Logs**
The server provides detailed logging for:
- API request/response cycles
- Database query performance
- Error tracking and debugging

### **Unity Integration Logs**
```
[CoachManager] ✅ API server is available - using dynamic API data
[StatCardPopulator] Loaded performance data from API
[TeamManager] Team created successfully via API
```

## 🚀 **Deployment Options**

### **Local Development**
- Use `dotnet run` for development
- Automatic reload on code changes
- Full debugging capabilities

### **Production Hosting**
- **Azure App Service**: Recommended for cloud deployment
- **IIS**: For Windows server hosting
- **Docker**: Containerized deployment option

---

## 🎯 **Integration Workflow**

1. **Start Backend Server** → `dotnet run`
2. **Launch Unity** → All scripts auto-detect API availability
3. **Test API Endpoints** → Use Postman for validation
4. **Monitor Logs** → Check both server and Unity console
5. **Fallback Mode** → System automatically uses offline data if API unavailable

**The backend server provides live data integration while maintaining full offline capability for robust development and deployment scenarios.**
