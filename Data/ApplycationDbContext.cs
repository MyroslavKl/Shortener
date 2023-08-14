using Microsoft.EntityFrameworkCore;
using TaskPr.Models;

namespace TaskPr.Data
{
    public class ApplycationDbContext:DbContext
    {
        public ApplycationDbContext(DbContextOptions<ApplycationDbContext> options) : base(options) { }

        public DbSet<UserModel> PesonalInformations { get; set; }
        public DbSet<UrlModel> UrlsInfo { get; set; }
    }
}
