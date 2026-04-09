using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Migrations
{
    /// <inheritdoc />
    public partial class update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Portfolio");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "Services",
                newSchema: "Portfolio");

            migrationBuilder.RenameTable(
                name: "Service_Tags",
                newName: "Service_Tags",
                newSchema: "Portfolio");

            migrationBuilder.RenameTable(
                name: "Service_ImageUrls",
                newName: "Service_ImageUrls",
                newSchema: "Portfolio");

            migrationBuilder.RenameTable(
                name: "Service_Faqs",
                newName: "Service_Faqs",
                newSchema: "Portfolio");

            migrationBuilder.RenameTable(
                name: "Service_Awards",
                newName: "Service_Awards",
                newSchema: "Portfolio");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Reviews",
                newSchema: "Portfolio");

            migrationBuilder.RenameTable(
                name: "Portfolios",
                newName: "Portfolios",
                newSchema: "Portfolio");

            migrationBuilder.RenameTable(
                name: "Portfolio_SocialLinks",
                newName: "Portfolio_SocialLinks",
                newSchema: "Portfolio");

            migrationBuilder.RenameTable(
                name: "Portfolio_Skills",
                newName: "Portfolio_Skills",
                newSchema: "Portfolio");

            migrationBuilder.RenameTable(
                name: "Portfolio_ProfessionalInfos",
                newName: "Portfolio_ProfessionalInfos",
                newSchema: "Portfolio");

            migrationBuilder.RenameTable(
                name: "Portfolio_LocationInfos",
                newName: "Portfolio_LocationInfos",
                newSchema: "Portfolio");

            migrationBuilder.RenameTable(
                name: "Portfolio_GeneralInfos",
                newName: "Portfolio_GeneralInfos",
                newSchema: "Portfolio");

            migrationBuilder.RenameTable(
                name: "Portfolio_ContactInfos",
                newName: "Portfolio_ContactInfos",
                newSchema: "Portfolio");

            migrationBuilder.RenameTable(
                name: "Experiences",
                newName: "Experiences",
                newSchema: "Portfolio");

            migrationBuilder.RenameTable(
                name: "Certifications",
                newName: "Certifications",
                newSchema: "Portfolio");

            migrationBuilder.AlterColumn<string>(
                name: "Pricing_Currency",
                schema: "Portfolio",
                table: "Services",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Services",
                schema: "Portfolio",
                newName: "Services");

            migrationBuilder.RenameTable(
                name: "Service_Tags",
                schema: "Portfolio",
                newName: "Service_Tags");

            migrationBuilder.RenameTable(
                name: "Service_ImageUrls",
                schema: "Portfolio",
                newName: "Service_ImageUrls");

            migrationBuilder.RenameTable(
                name: "Service_Faqs",
                schema: "Portfolio",
                newName: "Service_Faqs");

            migrationBuilder.RenameTable(
                name: "Service_Awards",
                schema: "Portfolio",
                newName: "Service_Awards");

            migrationBuilder.RenameTable(
                name: "Reviews",
                schema: "Portfolio",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "Portfolios",
                schema: "Portfolio",
                newName: "Portfolios");

            migrationBuilder.RenameTable(
                name: "Portfolio_SocialLinks",
                schema: "Portfolio",
                newName: "Portfolio_SocialLinks");

            migrationBuilder.RenameTable(
                name: "Portfolio_Skills",
                schema: "Portfolio",
                newName: "Portfolio_Skills");

            migrationBuilder.RenameTable(
                name: "Portfolio_ProfessionalInfos",
                schema: "Portfolio",
                newName: "Portfolio_ProfessionalInfos");

            migrationBuilder.RenameTable(
                name: "Portfolio_LocationInfos",
                schema: "Portfolio",
                newName: "Portfolio_LocationInfos");

            migrationBuilder.RenameTable(
                name: "Portfolio_GeneralInfos",
                schema: "Portfolio",
                newName: "Portfolio_GeneralInfos");

            migrationBuilder.RenameTable(
                name: "Portfolio_ContactInfos",
                schema: "Portfolio",
                newName: "Portfolio_ContactInfos");

            migrationBuilder.RenameTable(
                name: "Experiences",
                schema: "Portfolio",
                newName: "Experiences");

            migrationBuilder.RenameTable(
                name: "Certifications",
                schema: "Portfolio",
                newName: "Certifications");

            migrationBuilder.AlterColumn<string>(
                name: "Pricing_Currency",
                table: "Services",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5,
                oldNullable: true);
        }
    }
}
