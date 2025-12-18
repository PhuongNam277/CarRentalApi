using Microsoft.EntityFrameworkCore;
using NewCarRental.Domain.Entities;
namespace NewCarRental.Infrastructure.Contexts;

public partial class CarRentalDbContext : DbContext
{
    public CarRentalDbContext()
    {
    }

    public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<ConversationReadState> ConversationReadStates { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        
    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    public virtual DbSet<TenantCategory> TenantCategories { get; set; }

    public virtual DbSet<TenantMembership> TenantMemberships { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserBranch> UserBranches { get; set; }

    public virtual DbSet<UserConversationVisibility> UserConversationVisibilities { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__Blog__54379E30930C48EE");

            entity.ToTable("Blog");

            entity.HasIndex(e => e.AuthorId, "IX_Blog_AuthorId");

            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.PublishedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Draft");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Author).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Blog__AuthorId__628FA481");
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("PK__Cars__68A0342EFCD139E9");

            entity.HasIndex(e => e.BaseLocationId, "IX_Cars_BaseLocationId");

            entity.HasIndex(e => e.CategoryId, "IX_Cars_CategoryId");

            entity.HasIndex(e => e.EnergyType, "IX_Cars_EnergyType");

            entity.HasIndex(e => e.RentalPricePerDay, "IX_Cars_RentalPricePerDay");

            entity.HasIndex(e => e.SeatNumber, "IX_Cars_SeatNumber");

            entity.HasIndex(e => e.TenantId, "IX_Cars_TenantId");

            entity.HasIndex(e => e.TransmissionType, "IX_Cars_TransmissionType");

            entity.HasIndex(e => new { e.TenantId, e.LicensePlate }, "UX_Cars_Tenant_LicensePlate").IsUnique();

            entity.Property(e => e.Brand).HasMaxLength(50);
            entity.Property(e => e.CarName).HasMaxLength(100);
            entity.Property(e => e.EnergyType).HasMaxLength(20);
            entity.Property(e => e.EngineType).HasMaxLength(20);
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.LicensePlate).HasMaxLength(20);
            entity.Property(e => e.Model).HasMaxLength(50);
            entity.Property(e => e.RentalPricePerDay).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Available");
            entity.Property(e => e.TransmissionType).HasMaxLength(20);
            entity.Property(e => e.VehicleType)
                .HasMaxLength(20)
                .HasDefaultValue("Car");

            entity.HasOne(d => d.BaseLocation).WithMany(p => p.Cars)
                .HasForeignKey(d => d.BaseLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Category).WithMany(p => p.Cars)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cars__CategoryId__412EB0B6");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Cars)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cars_Tenants");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0BDF788EA5");

            entity.HasIndex(e => e.CategoryName, "UQ__Categori__8517B2E0F39EC167").IsUnique();

            entity.HasIndex(e => new { e.VehicleType, e.Slug }, "UX_Categories_VT_Slug")
                .IsUnique()
                .HasFilter("([Slug] IS NOT NULL)");

            entity.Property(e => e.CategoryName).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Slug).HasMaxLength(120);
            entity.Property(e => e.VehicleType)
                .HasMaxLength(20)
                .HasDefaultValue("Car");

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
                .HasForeignKey(d => d.ParentCategoryId)
                .HasConstraintName("FK_Categories_Parent");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasIndex(e => e.ConversationId, "IX_ChatMessages_ConversationId");

            entity.HasIndex(e => e.SentAt, "IX_ChatMessages_SentAt");

            entity.Property(e => e.Content).HasMaxLength(4000);
            entity.Property(e => e.SentAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Conversation).WithMany(p => p.ChatMessages)
                .HasForeignKey(d => d.ConversationId)
                .HasConstraintName("FK_ChatMessages_Conversation");

            entity.HasOne(d => d.Sender).WithMany(p => p.ChatMessages)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChatMessages_Sender");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comment");

            entity.HasIndex(e => e.BlogId, "IX_Comment_BlogId");

            entity.HasIndex(e => e.UserId, "IX_Comment_UserId");

            entity.Property(e => e.AuthorName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Comment_User_UserId");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PK__Contact__5C66259B5FDEEF7E");

            entity.ToTable("Contact");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.SubmittedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.HasIndex(e => new { e.CustomerId, e.Status }, "IX_Conversations_Customer_Status");

            entity.HasIndex(e => e.LastMessageAt, "IX_Conversations_LastMessageAt");

            entity.HasIndex(e => e.StaffId, "IX_Conversations_StaffId");

            entity.Property(e => e.ClosedAt).HasPrecision(0);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.LastMessageAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Open");

            entity.HasOne(d => d.Customer).WithMany(p => p.ConversationCustomers)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Conversations_Customer");

            entity.HasOne(d => d.Staff).WithMany(p => p.ConversationStaffs)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK_Conversations_Staff");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Conversations)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Conversations_Tenants");
        });

        modelBuilder.Entity<ConversationReadState>(entity =>
        {
            entity.HasKey(e => new { e.ConversationId, e.UserId }).HasName("PK_ConversationReadState");

            entity.HasIndex(e => e.UserId, "IX_CRS_User");

            entity.HasOne(d => d.Conversation).WithMany(p => p.ConversationReadStates)
                .HasForeignKey(d => d.ConversationId)
                .HasConstraintName("FK_CRS_Conv");

            entity.HasOne(d => d.User).WithMany(p => p.ConversationReadStates)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_CRS_User");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Location__E7FEA497401B4413");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Lat).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Lng).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.TimeZone).HasMaxLength(50);

            entity.HasOne(d => d.Tenant).WithMany(p => p.Locations)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Locations_Tenants");
        });

        modelBuilder.Entity<MaintenanceRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__Maintena__FBDF78E9D1F84E3D");

            entity.HasIndex(e => e.CarId, "IX_MaintenanceRecords_CarId");

            entity.Property(e => e.Cost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MaintenanceDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Car).WithMany(p => p.MaintenanceRecords)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Maintenan__CarId__534D60F1");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A3856F28699");

            entity.HasIndex(e => e.ReservationId, "IX_Payments_ReservationId");

            entity.HasIndex(e => e.TenantId, "IX_Payments_TenantId");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(20);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Unpaid");

            entity.HasOne(d => d.Reservation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ReservationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__Reserv__4CA06362");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Payments)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Tenants");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PK__Promotio__52C42FCFDB38B80D");

            entity.HasIndex(e => e.Code, "UQ__Promotio__A25C5AA7537857ED").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.DiscountPercent).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Active");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("PK__Reservat__B7EE5F24B88A996B");

            entity.HasIndex(e => e.CarId, "IX_Reservations_CarId");

            entity.HasIndex(e => e.DropoffLocationId, "IX_Reservations_Dropoff");

            entity.HasIndex(e => e.PickupLocationId, "IX_Reservations_Pickup");

            entity.HasIndex(e => e.TenantId, "IX_Reservations_TenantId");

            entity.HasIndex(e => e.UserId, "IX_Reservations_UserId");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.FromCity)
                .HasMaxLength(100)
                .HasDefaultValue("");
            entity.Property(e => e.ReservationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.ToCity)
                .HasMaxLength(100)
                .HasDefaultValue("");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Car).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__CarId__46E78A0C");

            entity.HasOne(d => d.DropoffLocation).WithMany(p => p.ReservationDropoffLocations)
                .HasForeignKey(d => d.DropoffLocationId)
                .HasConstraintName("FK_Reservations_Locations_Dropoff");

            entity.HasOne(d => d.PickupLocation).WithMany(p => p.ReservationPickupLocations)
                .HasForeignKey(d => d.PickupLocationId)
                .HasConstraintName("FK_Reservations_Locations_Pickup");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservations_Tenants");

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__UserI__45F365D3");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79CE5D90B607");

            entity.HasIndex(e => e.CarId, "IX_Reviews_CarId");

            entity.HasIndex(e => e.UserId, "IX_Reviews_UserId");

            entity.Property(e => e.JobName).HasMaxLength(30);
            entity.Property(e => e.ReviewDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Car).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__CarId__571DF1D5");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviews_Tenants");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__UserId__5812160E");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1ADB3C0800");

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasKey(e => e.TenantId).HasName("PK__Tenants__2E9B47E1180B7A1C");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<TenantCategory>(entity =>
        {
            entity.HasKey(e => new { e.TenantId, e.CategoryId });

            entity.Property(e => e.DisplayNameOverride).HasMaxLength(200);

            entity.HasOne(d => d.Category).WithMany(p => p.TenantCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TenantCategories_Categories");

            entity.HasOne(d => d.Tenant).WithMany(p => p.TenantCategories)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TenantCategories_Tenants");
        });

        modelBuilder.Entity<TenantMembership>(entity =>
        {
            entity.HasKey(e => new { e.TenantId, e.UserId });

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Role).HasMaxLength(50);

            entity.HasOne(d => d.Tenant).WithMany(p => p.TenantMemberships)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TenantMemberships_Tenants");

            entity.HasOne(d => d.User).WithMany(p => p.TenantMemberships)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TenantMemberships_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C90EA71FB");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E48F8E5FC4").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534479BCCE5").IsUnique();

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<UserBranch>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.TenantId, e.LocationId });

            entity.HasOne(d => d.Location).WithMany(p => p.UserBranches)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserBranches_Locations");

            entity.HasOne(d => d.Tenant).WithMany(p => p.UserBranches)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserBranches_Tenants");

            entity.HasOne(d => d.User).WithMany(p => p.UserBranches)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserBranches_Users");
        });

        modelBuilder.Entity<UserConversationVisibility>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ConversationId });

            entity.HasOne(d => d.Conversation).WithMany(p => p.UserConversationVisibilities)
                .HasForeignKey(d => d.ConversationId)
                .HasConstraintName("FK_UserConversationVisibilities_Conversations");

            entity.HasOne(d => d.User).WithMany(p => p.UserConversationVisibilities)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserConversationVisibilities_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
