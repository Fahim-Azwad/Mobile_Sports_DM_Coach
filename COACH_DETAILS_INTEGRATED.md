# Coach Details - Integrated ELT Database Loading

## Overview
The `CoachProfilePopulator.cs` script now includes built-in ELT (Extract, Load, Transform) data pipeline functionality for loading coach details directly from the database with JSON fallback.

## Key Features
✅ **Integrated Database Loading** - Built into existing CoachProfilePopulator script
✅ **ELT Data Pipeline** - Extract from DB/JSON → Load data → Transform for UI
✅ **N/T Key Testing** - Simple keyboard controls for testing
✅ **Top 4 Specialties Only** - Shows highest-value specialties from database stats
✅ **Exactly 4 Contract Terms** - Weekly Salary, Contract Length, Performance Bonus, Total Cost
✅ **No Separate Scripts** - Everything consolidated into one script

## How to Use

### 1. Setup in Unity
- Attach `CoachProfilePopulator` script to your GameObject
- Set `loadFromDatabase = true` in the inspector
- Connect all UI references (coachNameText, specialtyContainer, etc.)
- Optional: Connect `instructionText` to show "N = Next Coach, T = Next Coach Type"

### 2. Testing Controls (Play Mode)
- **N Key** = Load random coach from database
- **T Key** = Load next coach type (Defense → Offense → Special Teams → Defense)

### 3. Database Requirements
- Place `coach.json` file in `StreamingAssets/Database/` folder
- JSON should contain array of coach records with required fields

## ELT Data Pipeline

### EXTRACT Phase
```csharp
private List<CoachDatabaseRecord> ExtractCoachesFromDatabase()
```
- Tries SQLite first (future implementation)
- Falls back to JSON file reading from StreamingAssets
- Caches results for performance

### LOAD Phase
```csharp
private CoachDatabaseRecord LoadRandomCoachFromDatabase(string coachType = null)
```
- Filters coaches by type (D, O, S) if specified
- Selects random coach from filtered list
- Returns null if no coaches found

### TRANSFORM Phase
```csharp
private CoachProfileWrapper TransformToCoachProfile(CoachDatabaseRecord dbCoach)
```
- Converts database record to UI-ready format
- Calculates top 4 specialties from database stats
- Generates exactly 4 contract terms with calculated values

## Specialty Calculation
Database stats (0-10 scale) → UI bonus percentages (0-50% scale):
- **Defense**: Run Defense, Pressure Control, Coverage Discipline, Turnover Generation
- **Offense**: Passing Efficiency, Rushing Attack, Red Zone Conversion, Play Variation  
- **Special Teams**: Field Goal Accuracy, Kickoff Distance, Return Speed, Return Coverage

## Contract Terms Generation
- **Weekly Salary**: Converted from annual salary (millions → weekly)
- **Contract Length**: Direct from database contract_length field
- **Performance Bonus**: 15% of weekly salary for 80%+ wins
- **Total Cost**: Weekly salary × minimum contract length

## Error Handling
- Graceful fallback to default coach if database loading fails
- Comprehensive logging for debugging
- Null-safe UI updates
- Exception handling throughout ELT pipeline

## No Additional Scripts Required
Everything is now self-contained in `CoachProfilePopulator.cs`:
- No need for separate API scripts
- No need for separate tester scripts
- No need for connection testing scripts
- Simple, clean, single-file solution
