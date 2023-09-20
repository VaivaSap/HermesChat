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
    

    public DbSet<GroupConversation> GroupConversations { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<PrivateConversation> PrivateConversations { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
        
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<GroupConversation>().ToTable("GroupConversation");
        builder.Entity<Message>().ToTable("Message");
        builder.Entity<PrivateConversation>().ToTable("PrivateConversation");
        builder.Entity<UserGroup>().ToTable("UserGroup");
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
