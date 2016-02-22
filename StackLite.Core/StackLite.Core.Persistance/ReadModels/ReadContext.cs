using Microsoft.Data.Entity;

namespace StackLite.Core.Persistance.ReadModels
{
    public class ReadContext: DbContext
    {
        public DbSet<QuestionData> Questions { get; set; }
        public DbSet<AnswerData> Answers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionData>().HasKey(q => q.Id);
            modelBuilder.Entity<AnswerData>().HasKey(a => a.Id);
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source=.;Initial Catalog=StackLiteReporting;Integrated Security=True;");
        }
    }
}