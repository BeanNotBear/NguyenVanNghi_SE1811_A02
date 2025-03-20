using System;
using System.Collections.Generic;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Enums;
using Shared.PasswordHasher;

namespace DAL.Data;

public partial class FunewsManagementSystemContext : DbContext
{
	private readonly IConfiguration configuration;

	public FunewsManagementSystemContext(DbContextOptions<FunewsManagementSystemContext> options, IConfiguration configuration)
		: base(options)
	{
		this.configuration = configuration;
	}

	public virtual DbSet<Category> Categories { get; set; }

	public virtual DbSet<NewsArticle> NewsArticles { get; set; }

	public virtual DbSet<SystemAccount> SystemAccounts { get; set; }

	public virtual DbSet<Tag> Tags { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

		#region seeding admin account
		modelBuilder.Entity<SystemAccount>(ac =>
		{
			ac.HasData(new SystemAccount()
			{
				AccountId = 1,
				AccountEmail = configuration["AccountAdmin:Account"],
				AccountPassword = PasswordHasher.Instance.Hash(configuration["AccountAdmin:Password"]),
				AccountName = "Admin",
				AccountRole = (int)AccountRole.Admin,
			});
		});
		#endregion
		modelBuilder.Entity<Category>(entity =>
		{
			entity.ToTable("Category");

			entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
			entity.Property(e => e.CategoryDescription).HasColumnType("ntext");
			entity.Property(e => e.CategoryName).HasMaxLength(50);
			entity.Property(e => e.IsActive).HasDefaultValue(true);
			entity.Property(e => e.ParentCategoryId).HasColumnName("ParentCategoryID");

			entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
				.HasForeignKey(d => d.ParentCategoryId)
				.HasConstraintName("FK_Category_Category");
		});

		modelBuilder.Entity<NewsArticle>(entity =>
		{
			entity.ToTable("NewsArticle");

			entity.Property(e => e.NewsArticleId).HasColumnName("NewsArticleID");
			entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
			entity.Property(e => e.CreatedById).HasColumnName("CreatedByID");
			entity.Property(e => e.CreatedDate)
				.HasDefaultValueSql("(getdate())")
				.HasColumnType("datetime");
			entity.Property(e => e.Headline).HasColumnType("nvarchar(MAX)");
			entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
			entity.Property(e => e.NewsContent).HasColumnType("ntext");
			entity.Property(e => e.NewsSource).HasMaxLength(255);
			entity.Property(e => e.NewsTitle).HasColumnType("nvarchar(MAX)");
			entity.Property(e => e.UpdatedById).HasColumnName("UpdatedByID");

			entity.HasOne(d => d.Category).WithMany(p => p.NewsArticles)
				.HasForeignKey(d => d.CategoryId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_NewsArticle_Category");

			entity.HasOne(d => d.CreatedBy).WithMany(p => p.NewsArticleCreatedBies)
				.HasForeignKey(d => d.CreatedById)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_NewsArticle_SystemAccount");

			entity.HasOne(d => d.UpdatedBy).WithMany(p => p.NewsArticleUpdatedBies)
				.HasForeignKey(d => d.UpdatedById)
				.HasConstraintName("FK_NewsArticle_SystemAccount1");

			entity.HasMany(d => d.Tags).WithMany(p => p.NewsArticles)
				.UsingEntity<Dictionary<string, object>>(
					"NewsTag",
					r => r.HasOne<Tag>().WithMany()
						.HasForeignKey("TagId")
						.OnDelete(DeleteBehavior.ClientSetNull)
						.HasConstraintName("FK_NewsTag_Tag"),
					l => l.HasOne<NewsArticle>().WithMany()
						.HasForeignKey("NewsArticleId")
						.OnDelete(DeleteBehavior.ClientSetNull)
						.HasConstraintName("FK_NewsTag_NewsArticle"),
					j =>
					{
						j.HasKey("NewsArticleId", "TagId");
						j.ToTable("NewsTag");
						j.IndexerProperty<int>("NewsArticleId").HasColumnName("NewsArticleID");
						j.IndexerProperty<int>("TagId").HasColumnName("TagID");
					});
		});

		modelBuilder.Entity<SystemAccount>(entity =>
		{
			entity.HasKey(e => e.AccountId);

			entity.ToTable("SystemAccount");

			entity.Property(e => e.AccountId).HasColumnName("AccountID");
			entity.Property(e => e.AccountEmail)
				.HasMaxLength(50)
				.IsUnicode(false);
			entity.Property(e => e.AccountName).HasMaxLength(50);
			entity.Property(e => e.AccountPassword)
				.HasMaxLength(255)
				.IsUnicode(false);
		});

		modelBuilder.Entity<Tag>(entity =>
		{
			entity.ToTable("Tag");

			entity.Property(e => e.TagId).HasColumnName("TagID");
			entity.Property(e => e.Note).HasColumnType("ntext");
			entity.Property(e => e.TagName).HasMaxLength(100);
		});

		OnModelCreatingPartial(modelBuilder);
	}


	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
