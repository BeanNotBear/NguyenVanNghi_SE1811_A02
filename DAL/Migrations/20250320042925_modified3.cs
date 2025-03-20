using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
	/// <inheritdoc />
	public partial class modified3 : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "Headline",
				table: "NewsArticle",
				type: "nvarchar(MAX)",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "ntext");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "Headline",
				table: "NewsArticle",
				type: "ntext",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(MAX)");
		}
	}
}
