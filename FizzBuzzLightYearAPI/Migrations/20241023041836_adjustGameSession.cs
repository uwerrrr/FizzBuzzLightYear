using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FizzBuzzLightYearAPI.Migrations
{
    /// <inheritdoc />
    public partial class adjustGameSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "GameSessions",
                newName: "IsActive");

            migrationBuilder.AlterColumn<string>(
                name: "PlayerAnswer",
                table: "Questions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCorrect",
                table: "Questions",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "ExpectedAnswer",
                table: "Questions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "GeneratedAt",
                table: "Questions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswerNum",
                table: "GameSessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DurationSeconds",
                table: "GameSessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IncorrectAnswerNum",
                table: "GameSessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "GameSessions",
                keyColumn: "SessionId",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CorrectAnswerNum", "DurationSeconds", "EndTime", "IncorrectAnswerNum", "IsActive", "StartTime" },
                values: new object[] { 3, 60, new DateTime(2024, 10, 23, 4, 19, 35, 544, DateTimeKind.Utc).AddTicks(7640), 2, false, new DateTime(2024, 10, 23, 4, 18, 35, 544, DateTimeKind.Utc).AddTicks(7640) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 23, 4, 18, 35, 544, DateTimeKind.Utc).AddTicks(7400));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 23, 4, 18, 35, 544, DateTimeKind.Utc).AddTicks(7410));

            migrationBuilder.InsertData(
                table: "Rules",
                columns: new[] { "RuleId", "DivisibleBy", "GameId", "ReplaceWith" },
                values: new object[,]
                {
                    { new Guid("40f1b84c-3c46-4efd-9b56-512b6925091c"), 11, new Guid("22222222-2222-2222-2222-222222222222"), "Boo" },
                    { new Guid("47e9b0d3-125a-4013-bc2e-d0358ab2f456"), 5, new Guid("11111111-1111-1111-1111-111111111111"), "Buzz" },
                    { new Guid("86bbbeb4-94a5-4257-8378-b013941359ed"), 3, new Guid("11111111-1111-1111-1111-111111111111"), "Fizz" },
                    { new Guid("8be2bbf0-c936-4189-9a04-e6d8f399b21b"), 8, new Guid("11111111-1111-1111-1111-111111111111"), "Loo" },
                    { new Guid("d640f9a5-be4d-4f8f-aa20-191cb181604e"), 103, new Guid("22222222-2222-2222-2222-222222222222"), "Loo" },
                    { new Guid("ecbf213e-0826-4025-9c51-91adc75c1b2e"), 7, new Guid("22222222-2222-2222-2222-222222222222"), "Foo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rules",
                keyColumn: "RuleId",
                keyValue: new Guid("40f1b84c-3c46-4efd-9b56-512b6925091c"));

            migrationBuilder.DeleteData(
                table: "Rules",
                keyColumn: "RuleId",
                keyValue: new Guid("47e9b0d3-125a-4013-bc2e-d0358ab2f456"));

            migrationBuilder.DeleteData(
                table: "Rules",
                keyColumn: "RuleId",
                keyValue: new Guid("86bbbeb4-94a5-4257-8378-b013941359ed"));

            migrationBuilder.DeleteData(
                table: "Rules",
                keyColumn: "RuleId",
                keyValue: new Guid("8be2bbf0-c936-4189-9a04-e6d8f399b21b"));

            migrationBuilder.DeleteData(
                table: "Rules",
                keyColumn: "RuleId",
                keyValue: new Guid("d640f9a5-be4d-4f8f-aa20-191cb181604e"));

            migrationBuilder.DeleteData(
                table: "Rules",
                keyColumn: "RuleId",
                keyValue: new Guid("ecbf213e-0826-4025-9c51-91adc75c1b2e"));

            migrationBuilder.DropColumn(
                name: "ExpectedAnswer",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "GeneratedAt",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CorrectAnswerNum",
                table: "GameSessions");

            migrationBuilder.DropColumn(
                name: "DurationSeconds",
                table: "GameSessions");

            migrationBuilder.DropColumn(
                name: "IncorrectAnswerNum",
                table: "GameSessions");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "GameSessions",
                newName: "Score");

            migrationBuilder.AlterColumn<string>(
                name: "PlayerAnswer",
                table: "Questions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCorrect",
                table: "Questions",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "GameSessions",
                keyColumn: "SessionId",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "EndTime", "Score", "StartTime" },
                values: new object[] { new DateTime(2024, 10, 18, 7, 28, 9, 675, DateTimeKind.Utc).AddTicks(7790), 0, new DateTime(2024, 10, 18, 7, 27, 9, 675, DateTimeKind.Utc).AddTicks(7790) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 18, 7, 27, 9, 675, DateTimeKind.Utc).AddTicks(7530));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 18, 7, 27, 9, 675, DateTimeKind.Utc).AddTicks(7540));

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
        }
    }
}
