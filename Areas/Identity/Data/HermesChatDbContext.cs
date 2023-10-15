using HermesChat_TeamA.Areas.Identity.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HermesChat_TeamA.Areas.Identity.Data;

public class HermesChatDbContext : IdentityDbContext<User>
{
    public HermesChatDbContext(DbContextOptions<HermesChatDbContext> options)
        : base(options)
    { }

    public DbSet<User> Users { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ConnectionLog> ConnectionLogs { get; set; }
    public DbSet<ConversationUser> ConversationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.Entity<User>().HasMany(c => c.ConversationUser).WithOne(k => k.User);
        //builder.Entity<ConversationUser>().HasOne(c => c.Conversation).WithMany(t => ConversationUsers);
        builder.Entity<Conversation>().ToTable("Conversation");
        builder.Entity<ConversationUser>().ToTable("ConversationUser");
        builder.Entity<Message>().ToTable("Message");
        builder.Entity<ConnectionLog>().ToTable("ConnectionLog");
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
