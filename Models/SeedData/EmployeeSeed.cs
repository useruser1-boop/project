// File: EmployeeSeed.cs | Author: Team ## | Course: ISTM 415
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JasperGreen.Models.SeedData;

/// <summary>
/// Seeds employee records.
/// </summary>
public class EmployeeSeed : IEntityTypeConfiguration<Employee>
{
    /// <summary>
    /// Configures employee seed data.
    /// </summary>
    /// <param name="builder">Entity builder.</param>
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasData(
            new Employee { EmployeeID = 1, FirstName = "Jose", LastName = "Lopez", Phone = "9795556001", Email = "jlopez@jaspergreen.com", HireDate = new DateTime(2021, 2, 10) },
            new Employee { EmployeeID = 2, FirstName = "Emma", LastName = "Brooks", Phone = "9795556002", Email = "ebrooks@jaspergreen.com", HireDate = new DateTime(2021, 6, 3) },
            new Employee { EmployeeID = 3, FirstName = "Malik", LastName = "Turner", Phone = "9795556003", Email = "mturner@jaspergreen.com", HireDate = new DateTime(2022, 1, 14) },
            new Employee { EmployeeID = 4, FirstName = "Olivia", LastName = "Santos", Phone = "9795556004", Email = "osantos@jaspergreen.com", HireDate = new DateTime(2020, 9, 22) },
            new Employee { EmployeeID = 5, FirstName = "Noah", LastName = "Campbell", Phone = "9795556005", Email = "ncampbell@jaspergreen.com", HireDate = new DateTime(2023, 3, 5) },
            new Employee { EmployeeID = 6, FirstName = "Jasmine", LastName = "Kim", Phone = "9795556006", Email = "jkim@jaspergreen.com", HireDate = new DateTime(2019, 7, 18) },
            new Employee { EmployeeID = 7, FirstName = "Carter", LastName = "Mills", Phone = "9795556007", Email = "cmills@jaspergreen.com", HireDate = new DateTime(2024, 1, 9) },
            new Employee { EmployeeID = 8, FirstName = "Ava", LastName = "Ramirez", Phone = "9795556008", Email = "aramirez@jaspergreen.com", HireDate = new DateTime(2022, 8, 30) },
            new Employee { EmployeeID = 9, FirstName = "Logan", LastName = "Howard", Phone = "9795556009", Email = "lhoward@jaspergreen.com", HireDate = new DateTime(2020, 4, 12) },
            new Employee { EmployeeID = 10, FirstName = "Mia", LastName = "Bell", Phone = "9795556010", Email = "mbell@jaspergreen.com", HireDate = new DateTime(2023, 11, 2) },
            new Employee { EmployeeID = 11, FirstName = "Ethan", LastName = "Parker", Phone = "9795556011", Email = "eparker@jaspergreen.com", HireDate = new DateTime(2018, 12, 1) },
            new Employee { EmployeeID = 12, FirstName = "Grace", LastName = "Diaz", Phone = "9795556012", Email = "gdiaz@jaspergreen.com", HireDate = new DateTime(2024, 5, 15) },
            new Employee { EmployeeID = 13, FirstName = "Lucas", LastName = "Wright", Phone = "9795556013", Email = "lwright@jaspergreen.com", HireDate = new DateTime(2021, 10, 8) },
            new Employee { EmployeeID = 14, FirstName = "Harper", LastName = "Jenkins", Phone = "9795556014", Email = "hjenkins@jaspergreen.com", HireDate = new DateTime(2019, 2, 25) },
            new Employee { EmployeeID = 15, FirstName = "Mason", LastName = "Foster", Phone = "9795556015", Email = "mfoster@jaspergreen.com", HireDate = new DateTime(2024, 10, 6) }
        );
    }
}
