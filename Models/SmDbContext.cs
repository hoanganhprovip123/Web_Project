using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WEB.DAL.Models
{
    public partial class SmDbContext : DbContext
    {
        public SmDbContext() { }
        public SmDbContext(DbContextOptions<SmDbContext> options) : base(options) { }
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Notify> Notifies { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<React> Reacts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .HasColumnName("content")
                    .IsFixedLength();

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("create_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ParentId)
                    .HasColumnName("parent_id");

                entity.Property(e => e.PostId)
                    .HasColumnName("post_id");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasDefaultValueSql("((7))");
                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(p => p.ParentId)
                    .HasConstraintName("FK_Comment_Reply");
                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_Comment_Post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_User");
            });
            modelBuilder.Entity<Notify>(entity =>
            {
                entity.ToTable("Notify");

                entity.HasIndex(e => new { e.UserId, e.PostId, e.Type, e.CommentId }, "Unique_Key_Notify");
                
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.IsRead)
                    .HasColumnName("is_read")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .HasColumnName("type")
                    .IsFixedLength();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Notifies)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK_Notify_Comment");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Notifies)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_Notify_Post");

                entity.HasOne(d => d.User)
                   .WithMany(p => p.Notifs)
                   .HasForeignKey(d => d.UserId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Notif_User");
            });
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .HasColumnName("content")
                    .IsFixedLength();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.Hashtag)
                    .HasMaxLength(127)
                    .HasColumnName("hashtag")
                    .IsFixedLength();

                entity.Property(e => e.Image)
                    .HasMaxLength(150)
                    .HasColumnName("image")
                    .IsFixedLength();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_USER");
            });
            modelBuilder.Entity<React>(entity =>
            {
                entity.ToTable("React");

                entity.HasIndex(e => new { e.PostId, e.UserId, e.CommentId }, "Unique_Key_React")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasDefaultValueSql("((7))");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Reacts)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK_React_Comment");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Reacts)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_React_Post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reacts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_React_User");
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "Unique_Key_Email")
                    .IsUnique();

                entity.HasIndex(e => e.Uuid, "Unique_Key_UUID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address")
                    .IsFixedLength();

                entity.Property(e => e.Avatar)
                    .HasMaxLength(255)
                    .HasColumnName("avatar")
                    .IsFixedLength();

                entity.Property(e => e.BirthDay)
                    .HasColumnType("date")
                    .HasColumnName("birthday");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email")
                    .IsFixedLength();

                entity.Property(e => e.Enable)
                    .IsRequired()
                    .HasColumnName("enable")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("firstname")
                    .IsFixedLength();

                entity.Property(e => e.HomeTown)
                    .HasMaxLength(50)
                    .HasColumnName("hometown")
                    .IsFixedLength();

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("lastname")
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password")
                    .IsFixedLength();

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("phone")
                    .IsFixedLength();

                entity.Property(e => e.UserRole)
                    .HasMaxLength(20)
                    .HasColumnName("user_role")
                    .HasDefaultValueSql("(N'ROLE_USER')")
                    .IsFixedLength();

                entity.Property(e => e.Uuid)
                    .HasMaxLength(100)
                    .HasColumnName("UUID")
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }
    }
}
