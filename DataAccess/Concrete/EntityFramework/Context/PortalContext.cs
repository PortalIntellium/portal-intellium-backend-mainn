using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class PortalContext : DbContext
    {
        //public PortalContext() // Parametresiz yapılandırıcı ekle
        //{
        //}


        public PortalContext(DbContextOptions<PortalContext> options)
            : base(options)
        {
        }

        public PortalContext() // Parametresiz yapılandırıcı ekle
        {
        }

        //public PortalContext(DbContextOptions<PortalContext> options): base(options)
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var dbConnection = builder.Build().GetSection("ConnectionStrings:DevConnectionStrings").Value;

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            optionsBuilder.UseNpgsql(dbConnection);
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=142536;Database=portaldbnew;");
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<UserCustomer> UserCustomers { get; set; }
        public DbSet<MailParameters> MailParameters { get; set; }
        public DbSet<MailTemplates> MailTemplates { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<ProjectTeam> ProjectTeams { get; set; }
        public DbSet<ProjectTeamMember> ProjectTeamMembers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketComment> TicketComments { get; set; }
        public DbSet<TicketCommentReply> TicketCommentReplies { get; set; }
        public DbSet<TicketAttachment> TicketAttachments { get; set; }
        public DbSet<ForgotPassword> ForgotPasswords { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<BoardMember> BoardMembers { get; set; }
        public DbSet<BoardCategory> BoardCategories { get; set; }
        public DbSet<Entities.Concrete.Task> Tasks { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }
        public DbSet<TaskMember> TaskMembers { get; set; }
        public DbSet<TaskLabel> TaskLabels { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }
        public DbSet<TaskAttachment> TaskAttachments { get; set; }
        public DbSet<TaskTodoList> TaskTodoLists { get; set; }
        public DbSet<TaskTodo> TaskTodos { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Debit> Debits { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<LeaveDeducation> LeaveDeducations { get; set; }
        public DbSet<Custom> Customs { get; set; }
        public DbSet<DaysWork> DaysWorks { get; set; }
        public DbSet<UserJobDetail> UserJobDetails { get; set; }
        public DbSet<UserJobExperience> UserJobExperiences { get; set; }
        public DbSet<UserProfileDetails> UserProfileDetails { get; set; }
        public DbSet<UserCertificateDetail> UserCertificateDetails { get; set; }
        public DbSet<UserEducationDetail> UserEducationDetails { get; set; }
        public DbSet<UserFamilyDetail> UserFamilyDetails { get; set; }
        public DbSet<UserLanguageDetail> UserLanguageDetails { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolesForUsers> RolesForUsers { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}
