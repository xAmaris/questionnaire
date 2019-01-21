using Microsoft.EntityFrameworkCore;
using questionnaire.Core.Domains.Surveys;
using questionnaire.Core.Domains.SurveyTemplates;

namespace questionnaire.Infrastructure.Data
{
    public class QuestionnaireContext : DbContext
    {
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<FieldData> FieldData { get; set; }
        public DbSet<ChoiceOption> ChoiceOptions { get; set; }
        public DbSet<Row> Rows { get; set; }
        public DbSet<SurveyTemplate> SurveyTemplates { get; set; }
        public DbSet<QuestionTemplate> QuestionTemplates { get; set; }
        public DbSet<FieldDataTemplate> FieldDataTemplates { get; set; }
        public DbSet<ChoiceOptionTemplate> ChoiceOptionTemplates { get; set; }
        public DbSet<RowTemplate> RowTemplates { get; set; }

        public QuestionnaireContext(DbContextOptions<QuestionnaireContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Survey>()
                .HasMany(a => a.Questions)
                .WithOne(b => b.Survey)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Question>()
                .HasMany(a => a.FieldData)
                .WithOne(b => b.Question)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<FieldData>()
                .HasMany(a => a.Rows)
                .WithOne(b => b.FieldData)
                .HasForeignKey(s => s.FieldDataId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<FieldData>()
                .HasMany(a => a.ChoiceOptions)
                .WithOne(b => b.FieldData)
                .HasForeignKey(s => s.FieldDataId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SurveyTemplate>()
                .HasMany(a => a.QuestionTemplates)
                .WithOne(b => b.SurveyTemplate)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<QuestionTemplate>()
                .HasMany(a => a.FieldDataTemplates)
                .WithOne(b => b.QuestionTemplate)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<FieldDataTemplate>()
                .HasMany(a => a.RowTemplates)
                .WithOne(b => b.FieldDataTemplate)
                .HasForeignKey(s => s.FieldDataTemplateId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<FieldDataTemplate>()
                .HasMany(a => a.ChoiceOptionTemplates)
                .WithOne(b => b.FieldDataTemplate)
                .HasForeignKey(s => s.FieldDataTemplateId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}