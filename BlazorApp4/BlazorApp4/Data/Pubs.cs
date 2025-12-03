using BlazorApp4.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp4.Data
{
    // ★★★ DbContext クラス ★★★
    public partial class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
        }

        // ★★★ TestItem テーブルに対応する DbSet ★★★
        public DbSet<TestItem> TestItems { get; set; }

        // ★★★ テーブルとエンティティの紐づけ設定 ★★★


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestItem>(entity =>
            {
                // 対応テーブル名
                entity.ToTable("test_items");

                // 主キー
                entity.HasKey(e => e.Id);

                // カラムマッピング
                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Name)
                      .HasColumnName("name")
                      .HasMaxLength(200)
                      .IsRequired();

                entity.Property(e => e.Description)
                      .HasColumnName("description");

                entity.Property(e => e.CreatedAt)
                      .HasColumnName("created_at")
                      .HasColumnType("timestamp")
                      .HasDefaultValueSql("NOW()");
            });

            base.OnModelCreating(modelBuilder);
        }
    }

    // ★★★ TestItem エンティティ（テーブルと1:1対応）★★★
    [Table("test_items")]
    public class TestItem
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
