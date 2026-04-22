using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JasperGreen.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BillingAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BillingCity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BillingState = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    BillingZIP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CustomerPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmployeeLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SSN = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_Payments_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    PropertyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    PropertyAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PropertyCity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PropertyState = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    PropertyZIP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ServiceFee = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.PropertyID);
                    table.ForeignKey(
                        name: "FK_Properties_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Crews",
                columns: table => new
                {
                    CrewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CrewName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CrewForemanID = table.Column<int>(type: "int", nullable: false),
                    CrewMember1ID = table.Column<int>(type: "int", nullable: false),
                    CrewMember2ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crews", x => x.CrewID);
                    table.ForeignKey(
                        name: "FK_Crews_Employees_CrewForemanID",
                        column: x => x.CrewForemanID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Crews_Employees_CrewMember1ID",
                        column: x => x.CrewMember1ID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Crews_Employees_CrewMember2ID",
                        column: x => x.CrewMember2ID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProvideServices",
                columns: table => new
                {
                    ServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CrewID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    ServiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceFee = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaymentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvideServices", x => x.ServiceID);
                    table.ForeignKey(
                        name: "FK_ProvideServices_Crews_CrewID",
                        column: x => x.CrewID,
                        principalTable: "Crews",
                        principalColumn: "CrewID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProvideServices_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProvideServices_Payments_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "Payments",
                        principalColumn: "PaymentID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ProvideServices_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "BillingAddress", "BillingCity", "BillingState", "BillingZIP", "CustomerPhone", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "2101 Longmire Dr", "College Station", "TX", "77845", "9795551122", "maria.garcia@example.com", "Maria", "Garcia" },
                    { 2, "4203 Rock Prairie Rd", "College Station", "TX", "77845", "9795552233", "d.nguyen@example.com", "Derek", "Nguyen" },
                    { 3, "1708 Deacon Dr", "College Station", "TX", "77840", "9795553344", "sreed@example.com", "Samantha", "Reed" },
                    { 4, "3315 Barron Rd", "College Station", "TX", "77845", "9795554455", "thughes@example.com", "Tyler", "Hughes" },
                    { 5, "805 Southwest Pkwy E", "College Station", "TX", "77840", "9795555566", "apatel@example.com", "Alyssa", "Patel" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "EmployeeFirstName", "EmployeeLastName", "HireDate", "HourlyRate", "JobTitle", "SSN" },
                values: new object[,]
                {
                    { 1, "Jose", "Lopez", new DateTime(2021, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 18.00m, "Crew Foreman", "123-45-6001" },
                    { 2, "Emma", "Brooks", new DateTime(2021, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, "Crew Member", "123-45-6002" },
                    { 3, "Malik", "Turner", new DateTime(2022, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, "Crew Member", "123-45-6003" },
                    { 4, "Olivia", "Santos", new DateTime(2020, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 18.00m, "Crew Foreman", "123-45-6004" },
                    { 5, "Noah", "Campbell", new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, "Crew Member", "123-45-6005" },
                    { 6, "Jasmine", "Kim", new DateTime(2019, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 15.50m, "Crew Member", "123-45-6006" },
                    { 7, "Carter", "Mills", new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 18.00m, "Crew Foreman", "123-45-6007" },
                    { 8, "Ava", "Ramirez", new DateTime(2022, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, "Crew Member", "123-45-6008" },
                    { 9, "Logan", "Howard", new DateTime(2020, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 15.50m, "Crew Member", "123-45-6009" },
                    { 10, "Mia", "Bell", new DateTime(2023, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 18.00m, "Crew Foreman", "123-45-6010" },
                    { 11, "Ethan", "Parker", new DateTime(2018, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 16.00m, "Crew Member", "123-45-6011" },
                    { 12, "Grace", "Diaz", new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, "Crew Member", "123-45-6012" },
                    { 13, "Lucas", "Wright", new DateTime(2021, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 18.50m, "Crew Foreman", "123-45-6013" },
                    { 14, "Harper", "Jenkins", new DateTime(2019, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 15.50m, "Crew Member", "123-45-6014" },
                    { 15, "Mason", "Foster", new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, "Crew Member", "123-45-6015" }
                });

            migrationBuilder.InsertData(
                table: "Crews",
                columns: new[] { "CrewID", "CrewForemanID", "CrewMember1ID", "CrewMember2ID", "CrewName" },
                values: new object[,]
                {
                    { 1, 1, 2, 3, "Crew Alpha" },
                    { 2, 4, 5, 6, "Crew Bravo" },
                    { 3, 7, 8, 9, "Crew Charlie" },
                    { 4, 10, 11, 12, "Crew Delta" },
                    { 5, 13, 14, 15, "Crew Echo" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentID", "CustomerID", "PaymentAmount", "PaymentDate" },
                values: new object[,]
                {
                    { 1, 1, 125m, new DateTime(2025, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, 110m, new DateTime(2025, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, 145m, new DateTime(2025, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 2, 115m, new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 3, 100m, new DateTime(2025, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyID", "CustomerID", "PropertyAddress", "PropertyCity", "PropertyState", "PropertyZIP", "ServiceFee" },
                values: new object[,]
                {
                    { 1, 1, "2101 Longmire Dr", "College Station", "TX", "77845", 120m },
                    { 2, 1, "2205 Longmire Ct", "College Station", "TX", "77845", 105m },
                    { 3, 2, "4203 Rock Prairie Rd", "College Station", "TX", "77845", 140m },
                    { 4, 2, "1512 Birmingham Dr", "College Station", "TX", "77845", 110m },
                    { 5, 3, "1708 Deacon Dr", "College Station", "TX", "77840", 98m },
                    { 6, 3, "1801 Harvey Mitchell Pkwy S", "College Station", "TX", "77840", 125m },
                    { 7, 4, "3315 Barron Rd", "College Station", "TX", "77845", 130m },
                    { 8, 4, "3410 Finch Ln", "College Station", "TX", "77845", 115m },
                    { 9, 5, "3910 Copperfield Dr", "Bryan", "TX", "77807", 95m },
                    { 10, 5, "1820 Briarcrest Dr", "Bryan", "TX", "77802", 108m }
                });

            migrationBuilder.InsertData(
                table: "ProvideServices",
                columns: new[] { "ServiceID", "CrewID", "CustomerID", "PaymentID", "PropertyID", "ServiceDate", "ServiceFee" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 1, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 125m },
                    { 2, 3, 1, 2, 2, new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 110m },
                    { 3, 2, 2, 3, 3, new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 145m },
                    { 4, 4, 2, 4, 4, new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 115m },
                    { 5, 1, 3, 5, 5, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 100m },
                    { 6, 5, 3, null, 6, new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 128m },
                    { 7, 2, 4, null, 7, new DateTime(2025, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 132m },
                    { 8, 4, 4, null, 8, new DateTime(2025, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 118m },
                    { 9, 5, 5, null, 9, new DateTime(2025, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 98m },
                    { 10, 3, 5, null, 10, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 112m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Crews_CrewForemanID",
                table: "Crews",
                column: "CrewForemanID");

            migrationBuilder.CreateIndex(
                name: "IX_Crews_CrewMember1ID",
                table: "Crews",
                column: "CrewMember1ID");

            migrationBuilder.CreateIndex(
                name: "IX_Crews_CrewMember2ID",
                table: "Crews",
                column: "CrewMember2ID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CustomerID",
                table: "Payments",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CustomerID",
                table: "Properties",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_ProvideServices_CrewID",
                table: "ProvideServices",
                column: "CrewID");

            migrationBuilder.CreateIndex(
                name: "IX_ProvideServices_CustomerID",
                table: "ProvideServices",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_ProvideServices_PaymentID",
                table: "ProvideServices",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_ProvideServices_PropertyID",
                table: "ProvideServices",
                column: "PropertyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProvideServices");

            migrationBuilder.DropTable(
                name: "Crews");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
