
using API_HM.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace API_HM.DAL
{
    public class APIContext: DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<NomenArticleLevel> nomenArticleLevels { get; set; }

        public DbSet<NomenUserRole> NomenUserRoles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

}