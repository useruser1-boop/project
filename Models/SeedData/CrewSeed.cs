// File: CrewSeed.cs | Author: Team ## | Course: ISTM 415
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JasperGreen.Models.SeedData;

/// <summary>
/// Seeds crew records.
/// </summary>
public class CrewSeed : IEntityTypeConfiguration<Crew>
{
    /// <summary>
    /// Configures crew seed data.
    /// </summary>
    /// <param name="builder">Entity builder.</param>
    public void Configure(EntityTypeBuilder<Crew> builder)
    {
        // Each crew uses three distinct employee IDs.
        builder.HasData(
            new Crew { CrewID = 1, CrewName = "Crew Alpha", CrewForemanID = 1, CrewMember1ID = 2, CrewMember2ID = 3 },
            new Crew { CrewID = 2, CrewName = "Crew Bravo", CrewForemanID = 4, CrewMember1ID = 5, CrewMember2ID = 6 },
            new Crew { CrewID = 3, CrewName = "Crew Charlie", CrewForemanID = 7, CrewMember1ID = 8, CrewMember2ID = 9 },
            new Crew { CrewID = 4, CrewName = "Crew Delta", CrewForemanID = 10, CrewMember1ID = 11, CrewMember2ID = 12 },
            new Crew { CrewID = 5, CrewName = "Crew Echo", CrewForemanID = 13, CrewMember1ID = 14, CrewMember2ID = 15 }
        );
    }
}
