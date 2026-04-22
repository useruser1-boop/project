// File: EmployeeSeed.cs | Author: Team 05 | Course: ISTM 415
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JasperGreen.Models.SeedData;

/// <summary>
/// Seeds 15 employee records.
/// </summary>
public class EmployeeSeed : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasData(
            new Employee { EmployeeID = 1,  EmployeeFirstName = "Jose",    EmployeeLastName = "Lopez",    SSN = "123-45-6001", JobTitle = "Crew Foreman",  HireDate = new DateTime(2021, 2,  10), HourlyRate = 18.00m },
            new Employee { EmployeeID = 2,  EmployeeFirstName = "Emma",    EmployeeLastName = "Brooks",   SSN = "123-45-6002", JobTitle = "Crew Member",   HireDate = new DateTime(2021, 6,   3), HourlyRate = 15.00m },
            new Employee { EmployeeID = 3,  EmployeeFirstName = "Malik",   EmployeeLastName = "Turner",   SSN = "123-45-6003", JobTitle = "Crew Member",   HireDate = new DateTime(2022, 1,  14), HourlyRate = 15.00m },
            new Employee { EmployeeID = 4,  EmployeeFirstName = "Olivia",  EmployeeLastName = "Santos",   SSN = "123-45-6004", JobTitle = "Crew Foreman",  HireDate = new DateTime(2020, 9,  22), HourlyRate = 18.00m },
            new Employee { EmployeeID = 5,  EmployeeFirstName = "Noah",    EmployeeLastName = "Campbell", SSN = "123-45-6005", JobTitle = "Crew Member",   HireDate = new DateTime(2023, 3,   5), HourlyRate = 15.00m },
            new Employee { EmployeeID = 6,  EmployeeFirstName = "Jasmine", EmployeeLastName = "Kim",      SSN = "123-45-6006", JobTitle = "Crew Member",   HireDate = new DateTime(2019, 7,  18), HourlyRate = 15.50m },
            new Employee { EmployeeID = 7,  EmployeeFirstName = "Carter",  EmployeeLastName = "Mills",    SSN = "123-45-6007", JobTitle = "Crew Foreman",  HireDate = new DateTime(2024, 1,   9), HourlyRate = 18.00m },
            new Employee { EmployeeID = 8,  EmployeeFirstName = "Ava",     EmployeeLastName = "Ramirez",  SSN = "123-45-6008", JobTitle = "Crew Member",   HireDate = new DateTime(2022, 8,  30), HourlyRate = 15.00m },
            new Employee { EmployeeID = 9,  EmployeeFirstName = "Logan",   EmployeeLastName = "Howard",   SSN = "123-45-6009", JobTitle = "Crew Member",   HireDate = new DateTime(2020, 4,  12), HourlyRate = 15.50m },
            new Employee { EmployeeID = 10, EmployeeFirstName = "Mia",     EmployeeLastName = "Bell",     SSN = "123-45-6010", JobTitle = "Crew Foreman",  HireDate = new DateTime(2023, 11,  2), HourlyRate = 18.00m },
            new Employee { EmployeeID = 11, EmployeeFirstName = "Ethan",   EmployeeLastName = "Parker",   SSN = "123-45-6011", JobTitle = "Crew Member",   HireDate = new DateTime(2018, 12,  1), HourlyRate = 16.00m },
            new Employee { EmployeeID = 12, EmployeeFirstName = "Grace",   EmployeeLastName = "Diaz",     SSN = "123-45-6012", JobTitle = "Crew Member",   HireDate = new DateTime(2024, 5,  15), HourlyRate = 15.00m },
            new Employee { EmployeeID = 13, EmployeeFirstName = "Lucas",   EmployeeLastName = "Wright",   SSN = "123-45-6013", JobTitle = "Crew Foreman",  HireDate = new DateTime(2021, 10,  8), HourlyRate = 18.50m },
            new Employee { EmployeeID = 14, EmployeeFirstName = "Harper",  EmployeeLastName = "Jenkins",  SSN = "123-45-6014", JobTitle = "Crew Member",   HireDate = new DateTime(2019, 2,  25), HourlyRate = 15.50m },
            new Employee { EmployeeID = 15, EmployeeFirstName = "Mason",   EmployeeLastName = "Foster",   SSN = "123-45-6015", JobTitle = "Crew Member",   HireDate = new DateTime(2024, 10,  6), HourlyRate = 15.00m }
        );
    }
}
