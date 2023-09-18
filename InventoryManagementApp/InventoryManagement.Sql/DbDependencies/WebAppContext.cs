using InventoryManagement.Core.Acls;
using InventoryManagement.Sql.Entities;
using InventoryManagement.Sql.Entities.Base;
using InventoryManagement.Sql.Entities.Configurations;
using InventoryManagement.Sql.Entities.Enrols;
using InventoryManagement.Sql.Entities.Identities;
using InventoryManagement.Sql.Entities.Logs;
using InventoryManagement.Sql.Entities.Settings;
using InventoryManagement.Sql.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Reflection;
using static InventoryManagement.Sql.Entities.Identities.IdentityModel;

namespace InventoryManagement.Sql.DbDependencies
{
    public class WebAppContext : IdentityDbContext<User,
        Role, long,
        UserClaim,
        UserRole,
        UserLogin,
        RoleClaim,
        UserToken>
    {
        public const string DefaultSchemaName = "dbo";
        public string Schema { get; set; }

        public readonly ISignInHelper SignInHelper;

        public WebAppContext(DbContextOptions<WebAppContext> options, ISignInHelper signInHelper) : base(options)
        {
            SignInHelper = signInHelper;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.LogTo(message => WriteSqlQueryLog(message));
            optionsBuilder.UseLoggerFactory(_myLoggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema(Schema);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.RelationConvetion();
            builder.DecimalConvention();
            builder.DateTimeConvention();

            builder.Seed();
        }

        public static readonly Microsoft.Extensions.Logging.LoggerFactory _myLoggerFactory = new LoggerFactory(new[] {
            new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
                //new ConsoleLoggerProvider((_, __) => true, true)
        });

        #region acl entitites
        //public DbSet<IdentityRole<long>> AspNetRoles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<RoleMenuMapper> RoleMenuMappers { get; set; }
        #endregion

        #region settings
        public DbSet<Setting> Settings { get; set; }
        #endregion

        #region logs
        public DbSet<SmsLog> SmsLogs { get; set; }
        public DbSet<EmailLog> EmailLogs { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<RequestLog> RequestLogs { get; set; }


        #endregion



        #region config
        public DbSet<Country> Countries { get; set; }
        public DbSet<State>States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }


        #endregion
                

        #region Enroles

        public DbSet<Company> Companies { get; set; }  
        public DbSet<Currency>  Currencies { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<OpenQuantity> OpenQuantities { get; set; }
        public DbSet<WareHouse> WareHouses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Return> Returns { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<PurchaseDetails> PurchaseDetails { get; set; }
        public DbSet<Damage> Damages { get; set; }

        #endregion



        #region Audit
        private void Audit()
        {
            long userId = 0;
            var now = DateTimeOffset.UtcNow;

            if (SignInHelper.IsAuthenticated)
                userId = (long)SignInHelper.UserId;

            foreach (var entry in base
                .ChangeTracker.Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Added
                         || e.State == EntityState.Modified))
            {
                if (entry.State != EntityState.Added)
                {
                    entry.Entity.UpdatedDateUtc ??= now;
                    entry.Entity.UpdatedBy ??= userId;
                }
                else
                {
                    entry.Entity.CreatedBy = entry.Entity.CreatedBy != 0 ? entry.Entity.CreatedBy : userId;
                    entry.Entity.CreatedDateUtc = entry.Entity.CreatedDateUtc == DateTimeOffset.MinValue ? now : entry.Entity.CreatedDateUtc;
                }
            }
        }

        private void AuditTrail()
        {
            long userId = 0;

            if (SignInHelper.IsAuthenticated)
                userId = (long)SignInHelper.UserId;

            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is BaseEntity
                    || entry.Entity is AuditLog
                    || entry.State == EntityState.Detached
                    || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = userId;
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAuditLog());
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AuditTrail();
            Audit();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            AuditTrail();
            Audit();

            return base.SaveChanges();
        }
        #endregion
        public DbSet<PurchasesMaster> PurchasesMasters { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        private void WriteSqlQueryLog(string query, StoreType storeType = StoreType.Output)
        {
            if (storeType == StoreType.Output)
                Debug.WriteLine(query);
            else if (storeType == StoreType.Db)
            {
                // store in db
            }
            else if (storeType == StoreType.File)
            {
                // store & append in file
                //new StreamWriter("mylog.txt", append: true);
            }

            //using (WebAppContext context = new WebAppContext())
            //{
            //    context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            //}

            //IQueryable queryable = from x in Blogs
            //                       where x.Id == 1
            //                       select x;

            //var sqlEf5 = ((System.Data.Objects.ObjectQuery)queryable).ToTraceString();
            //var sqlEf6 = ((System.Data.Entity.Core.Objects.ObjectQuery)queryable).ToTraceString();
            //var sql = queryable.ToString();

            // https://docs.microsoft.com/en-us/ef/ef6/fundamentals/logging-and-interception?redirectedfrom=MSDN
            // https://stackoverflow.com/questions/1412863/how-do-i-view-the-sql-generated-by-the-entity-framework
        }



        #region private
        private void RevertChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity != null).ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;  //Revert changes made to deleted entity.
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
        #endregion
    }

    public enum StoreType
    {
        Db,
        File,
        Output
    }

}
