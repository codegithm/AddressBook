using AddressBook.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Data
{
    public class AddressBookContext : IdentityDbContext<User>
    {
        public AddressBookContext(DbContextOptions<AddressBookContext> options) :base(options){}

        DbSet<User> Users { get; set; }
        public DbSet<UserLoginRecord> UserLoginRecords { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<DataExportLog> DataExportLogs { get; set; }
        public DbSet<ApiUsageLog> ApiUsageLogs { get; set; }
        public DbSet<DataImportLog> DataImportLogs { get; set; }
    }
}
