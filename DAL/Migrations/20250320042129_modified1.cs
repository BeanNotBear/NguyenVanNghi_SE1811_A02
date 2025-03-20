using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
	/// <inheritdoc />
	public partial class modified1 : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "NewsTitle",
				table: "NewsArticle",
				type: "ntext",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "NewsTitle",
				table: "NewsArticle",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: false,
				oldClrType: typeof(string),
				oldType: "ntext");
		}
	}
}
