using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.EnsureSchema(
                name: "alter");

            migrationBuilder.CreateSequence(
                name: "orderalterationseq",
                schema: "alter",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "LocalIntegrationEvents",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: true),
                    ModelName = table.Column<string>(nullable: true),
                    ModelNamespace = table.Column<string>(nullable: true),
                    JsonBoby = table.Column<string>(nullable: true),
                    BinaryBody = table.Column<byte[]>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalIntegrationEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderAlterations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ShortenSleeves_Left = table.Column<short>(nullable: false),
                    ShortenSleeves_Right = table.Column<short>(nullable: false),
                    ShortenTrousers_Left = table.Column<short>(nullable: false),
                    ShortenTrousers_Right = table.Column<short>(nullable: false),
                    OrderStatusId = table.Column<byte>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAlterations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalIntegrationEvents");

            migrationBuilder.DropTable(
                name: "OrderAlterations",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "orderalterationseq",
                schema: "alter");
        }
    }
}
