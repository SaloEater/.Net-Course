using System;
using AuthentificationService.Contract;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthentificationService.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("" +
                "INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES " +
                "(N'd773788e-93fb-4857-91d6-d8e52ff94544', N'TaskService', N'TASKSERVICE', N'a.b@c.d', N'A.B@C.D', 0, N'AQAAAAEAACcQAAAAEGny3VfschuEfRN3YebA+fY9TlPa8zo4Rq6OzSWjcwm7APr3qHT/QNJpXirONEbwrA==', N'PB3G7MPJEMOCSB5AHKP5PUUKKJDUTD2K', N'a34fcaef-9072-43ee-afd8-2843b163f441', NULL, 0, 0, NULL, 1, 0)"
            );

            migrationBuilder.Sql("" +
                "INSERT INTO [dbo].[AspNetUserTokens] ([UserId], [LoginProvider], [Name], [Value]) VALUES " +
                "(N'd773788e-93fb-4857-91d6-d8e52ff94544', N'Admin', N'Admin Token', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiZDc3Mzc4OGUtOTNmYi00ODU3LTkxZDYtZDhlNTJmZjk0NTQ0IiwianRpIjoiYTJmYzFlNmEtY2M0OC00YjQ2LWI4ZDgtZTFkOTEwZTRlNDY5IiwiZXhwIjoxNjIxNDI3MzIxLCJpc3MiOiJBdXRoZW50aWZpY2F0aW9uU2VydmljZSIsImF1ZCI6IkF1dGhlbnRpZmljYXRpb25TZXJ2aWNlIn0.MhCQIAud9Mhv-jXSiEQ6qP-Im050MegcdN0KjvAoEeY')"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE [AspNetUsers]");
            migrationBuilder.Sql("TRUNCATE TABLE [AspNetUserTokens]");
        }
    }
}
