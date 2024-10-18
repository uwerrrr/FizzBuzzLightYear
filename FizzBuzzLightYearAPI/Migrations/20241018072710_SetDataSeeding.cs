using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FizzBuzzLightYearAPI.Migrations
{
    /// <inheritdoc />
    public partial class SetDataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameId", "Author", "CreatedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Alex", new DateTime(2024, 10, 18, 7, 27, 9, 675, DateTimeKind.Utc).AddTicks(7530), "FizzBuzz" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "John", new DateTime(2024, 10, 18, 7, 27, 9, 675, DateTimeKind.Utc).AddTicks(7540), "FooBooLoo" }
                });

            migrationBuilder.InsertData(
                table: "GameSessions",
                columns: new[] { "SessionId", "EndTime", "GameId", "Player", "Score", "StartTime" },
                values: new object[] { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2024, 10, 18, 7, 28, 9, 675, DateTimeKind.Utc).AddTicks(7790), new Guid("11111111-1111-1111-1111-111111111111"), "TestPlayer", 0, new DateTime(2024, 10, 18, 7, 27, 9, 675, DateTimeKind.Utc).AddTicks(7790) });

            migrationBuilder.InsertData(
                table: "Rules",
                columns: new[] { "RuleId", "DivisibleBy", "GameId", "ReplaceWith" },
                values: new object[,]
                {
                    { new Guid("2f684d1e-76ee-4af9-97e0-3ddfe1155459"), 7, new Guid("22222222-2222-2222-2222-222222222222"), "Foo" },
                    { new Guid("5aec4713-344a-4686-81bb-86b06d4d6a4a"), 3, new Guid("11111111-1111-1111-1111-111111111111"), "Fizz" },
                    { new Guid("83463eb4-d76b-4c2b-bd6a-ba872340f7a6"), 103, new Guid("22222222-2222-2222-2222-222222222222"), "Loo" },
                    { new Guid("946b8f35-169e-4386-99da-e4a7ea5f11b9"), 8, new Guid("11111111-1111-1111-1111-111111111111"), "Loo" },
                    { new Guid("c1f50f16-77b9-4972-9976-8a8676cbcee0"), 5, new Guid("11111111-1111-1111-1111-111111111111"), "Buzz" },
                    { new Guid("fbd02815-9107-4382-992d-d7dc662f60e8"), 11, new Guid("22222222-2222-2222-2222-222222222222"), "Boo" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "IsCorrect", "Number", "PlayerAnswer", "SessionId" },
                values: new object[,]
                {
                    { new Guid("2f7dfad5-586d-44be-b53f-c63c3ecaae43"), false, 8, "Fizz", new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("3b912e47-4b12-4b2b-86f9-07e5e70eff9d"), true, 5, "Buzz", new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("b2f90ae4-f147-4620-a91d-86292c9dd3e5"), true, 3, "Fizz", new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("d8a1ac9e-5eea-4717-8201-abd45269ee87"), true, 15, "FizzBuzz", new Guid("33333333-3333-3333-3333-333333333333") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: new Guid("2f7dfad5-586d-44be-b53f-c63c3ecaae43"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: new Guid("3b912e47-4b12-4b2b-86f9-07e5e70eff9d"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: new Guid("b2f90ae4-f147-4620-a91d-86292c9dd3e5"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: new Guid("d8a1ac9e-5eea-4717-8201-abd45269ee87"));

            migrationBuilder.DeleteData(
                table: "Rules",
                keyColumn: "RuleId",
                keyValue: new Guid("2f684d1e-76ee-4af9-97e0-3ddfe1155459"));

            migrationBuilder.DeleteData(
                table: "Rules",
                keyColumn: "RuleId",
                keyValue: new Guid("5aec4713-344a-4686-81bb-86b06d4d6a4a"));

            migrationBuilder.DeleteData(
                table: "Rules",
                keyColumn: "RuleId",
                keyValue: new Guid("83463eb4-d76b-4c2b-bd6a-ba872340f7a6"));

            migrationBuilder.DeleteData(
                table: "Rules",
                keyColumn: "RuleId",
                keyValue: new Guid("946b8f35-169e-4386-99da-e4a7ea5f11b9"));

            migrationBuilder.DeleteData(
                table: "Rules",
                keyColumn: "RuleId",
                keyValue: new Guid("c1f50f16-77b9-4972-9976-8a8676cbcee0"));

            migrationBuilder.DeleteData(
                table: "Rules",
                keyColumn: "RuleId",
                keyValue: new Guid("fbd02815-9107-4382-992d-d7dc662f60e8"));

            migrationBuilder.DeleteData(
                table: "GameSessions",
                keyColumn: "SessionId",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));
        }
    }
}
