using Microsoft.EntityFrameworkCore;

public class TeamService
{
    private readonly GameDbContext _db;

    public TeamService(GameDbContext db)
    {
        _db = db;
    }

    public async Task<Team> GetTeamById(string teamId)
    {
        var team = await _db.Teams.FirstOrDefaultAsync(t => t.teamId == teamId);

        if (team == null)
        {
            throw new NotFoundException($"Team with ID {teamId}");
        }

        return team;
    }
}
