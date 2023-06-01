using CRMBackend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRMBackend.Data.Models
{
    public class RMbackendContext : IdentityDbContext<ApplicationUser>
    {
        public RMbackendContext(DbContextOptions<RMbackendContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Contacts>().HasOne(x => x.District).WithMany(x => x.Cities).OnDelete(DeleteBehavior.NoAction);
            /*modelBuilder.Entity<States>()
            .HasOne(b => b.Contact)
            .WithOne(i => i.State).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Districts>()
            .HasOne(b => b.Contact)
            .WithOne(i => i.District).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Cities>()
            .HasOne(b => b.Contact)
            .WithOne(i => i.City).OnDelete(DeleteBehavior.NoAction);*/

            modelBuilder.Entity<ApplicationUser>()
       .HasOne(a => a.Client)
       .WithOne(b => b.ApplicationUser)
       .HasForeignKey<Clients>(b => b.ApplicationUserId);

            // contactevent table
            modelBuilder.Entity<ContactEvents>().HasKey(t => new { t.ContactsId, t.EventsId });
            modelBuilder.Entity<ContactEvents>()
                        .HasOne(t => t.Contacts)
                        .WithMany(t => t.ContactEvent)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ContactEvents>()
                        .HasOne(t => t.Events)
                        .WithMany(t => t.ContactEvent)
                        .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ContactEvents>().HasOne(x => x.Contacts).WithMany(x => x.ContactEvent).OnDelete(DeleteBehavior.NoAction);

            // contactgroup table
            modelBuilder.Entity<ContactGroups>().HasKey(t => new { t.ContactId, t.GroupId });
            modelBuilder.Entity<ContactGroups>()
                        .HasOne(t => t.Contact)
                        .WithMany(t => t.ContactGroup)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ContactGroups>()
                        .HasOne(t => t.Group)
                        .WithMany(t => t.ContactGroup)
                        .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ContactGroups>().HasOne(x => x.Contact).WithMany(x => x.ContactGroup).OnDelete(DeleteBehavior.NoAction);

            // state
            modelBuilder.Entity<Contacts>().HasOne(x => x.State).WithMany(x => x.Contacts).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Contacts>().HasOne(x => x.District).WithMany(x => x.Contacts).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Contacts>().HasOne(x => x.City).WithMany(x => x.Contacts).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<States>().HasMany(x => x.Districts).WithOne(x => x.State).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Districts>().HasMany(x => x.Cities).WithOne(x => x.District).OnDelete(DeleteBehavior.NoAction);



            // contact table
            /*modelBuilder.Entity<Contacts>()
                           .HasIndex(x => x.StateId).IsUnique().IsClustered(true);
            modelBuilder.Entity<Contacts>()
                           .HasIndex(x => x.DistrictId).IsUnique().IsClustered(true);
            modelBuilder.Entity<Contacts>()
                           .HasIndex(x => x.CityId).IsUnique().IsClustered(true);*/

            /*modelBuilder.Entity<Contacts>()
                           .HasOne<Districts>()
                           .WithOne()
                           .HasForeignKey<Contacts>(b => b.DistrictId);
            modelBuilder.Entity<Contacts>()
                           .HasOne<Cities>()
                           .WithOne()
                           .HasForeignKey<Contacts>(b => b.CityId);*/


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Reminders> Reminders { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<UserSMSs> UserSMSs { get; set; }
        public DbSet<RecipientsPhoneNos> RecipientsPhoneNos { get; set; }
        public DbSet<UserEmails> UserEmails { get; set; }
        public DbSet<RecipientsEmails> RecipientsEmails { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<SubCategories> SubCategories { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<ContactEvents> ContactEvents { get; set; }
        public DbSet<ContactGroups> ContactGroups { get; set; }
        public DbSet<UserPhotos> UserPhotos { get; set; }
        public DbSet<UserNotes> UserNotes { get; set; }
        public DbSet<UserDocuments> UserDocuments { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<Districts> Districts { get; set; }
        public DbSet<Cities> Cities { get; set; }

    }
}
