using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Authentication;
using Business.File;
using Business.Helpers;
using Business.Helpers.DebitHelpers;
using Business.Repository.BoardRepository;
using Business.Repository.CommentReplyRepository;
using Business.Repository.CustomerRepository;
using Business.Repository.CustomRepository;
using Business.Repository.DaysWorkRepository;
using Business.Repository.DebitRepository;
using Business.Repository.ExpenseRepository;
using Business.Repository.ForgotPasswordRepository;
using Business.Repository.HolidayRepository;
using Business.Repository.LabelRepository;
using Business.Repository.LeaveDeducationRepository;
using Business.Repository.MailRepository;
using Business.Repository.MailTemplatesRepository;
using Business.Repository.OperationClaimRepository;
using Business.Repository.PermissionRepository;
using Business.Repository.ProjectRepository;
using Business.Repository.ProjectTeamMemberRepository;
using Business.Repository.ProjectTeamRepository;
using Business.Repository.ProjectTypeRepository;
using Business.Repository.RolesForUsersRepository;
using Business.Repository.TaskListRepository;
using Business.Repository.TaskRepository;
using Business.Repository.TicketAttachmentRepository;
using Business.Repository.TicketCommentRepository;
using Business.Repository.TicketRepository;
using Business.Repository.UserCertificateDetailRepository;
using Business.Repository.UserCustomerRepository;
using Business.Repository.UserEducationDetailRepository;
using Business.Repository.UserFamilyDetailRepository;
using Business.Repository.UserJobDetailsRepository;
using Business.Repository.UserJobExperienceRepository;
using Business.Repository.UserLanguageDetailRepository;
using Business.Repository.UserOperationClaimRepository;
using Business.Repository.UserPermissionRepository;
using Business.Repository.UserProfileDetailRepository;
using Business.Repository.UserRepository;
using Business.Repository.UserRoleRepository;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Repository.BoardRepository;
using DataAccess.Repository.CommentReplyRepository;
using DataAccess.Repository.CustomerRepository;
using DataAccess.Repository.CustomRepository;
using DataAccess.Repository.DaysWorkRepository;
using DataAccess.Repository.DebitRepository;
using DataAccess.Repository.ExpenseRepository;
using DataAccess.Repository.ForgotPasswordRepository;
using DataAccess.Repository.HolidayRepository;
using DataAccess.Repository.LabelRepository;
using DataAccess.Repository.LeaveDeducationRepository;
using DataAccess.Repository.MailRepository;
using DataAccess.Repository.OperationClaimRepository;
using DataAccess.Repository.PermissionRepository;
using DataAccess.Repository.ProjectRepository;
using DataAccess.Repository.ProjectTeamMemberRepository;
using DataAccess.Repository.ProjectTeamRepository;
using DataAccess.Repository.ProjectTypeRepository;
using DataAccess.Repository.RolesForUsersRepository;
using DataAccess.Repository.TaskListRepository;
using DataAccess.Repository.TaskRepository;
using DataAccess.Repository.TicketAttachmentRepository;
using DataAccess.Repository.TicketCommentRepository;
using DataAccess.Repository.TicketRepository;
using DataAccess.Repository.UserCertificateDetailRepository;
using DataAccess.Repository.UserCustomerRepository;
using DataAccess.Repository.UserEducationDetailRepository;
using DataAccess.Repository.UserFamilyDetailRepository;
using DataAccess.Repository.UserJobDetailRepository;
using DataAccess.Repository.UserJobExperienceRepository;
using DataAccess.Repository.UserLanguageDetailRepository;
using DataAccess.Repository.UserOperationClaimRepository;
using DataAccess.Repository.UserPermissionRepository;
using DataAccess.Repository.UserProfileDetailRepository;
using DataAccess.Repository.UserRepository;
using DataAccess.Repository.UserRoleRepository;

namespace Business.DependencyResolves.AutoFac
{
    public class AutoFacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TokenHandler>().As<ITokenHandler>();

            builder.RegisterType<FileManager>().As<IFileService>();

            builder.RegisterType<GetDebitPDFPath>();
            builder.RegisterType<PermissionHelpers>();

            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<DebitManager>().As<IDebitService>();
            builder.RegisterType<EfDebitDal>().As<IDebitDal>();

            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();

            builder.RegisterType<MailParameterManager>().As<IMailParameterService>();
            builder.RegisterType<EfMailParameterDal>().As<IMailParameterDal>();

            builder.RegisterType<MailManager>().As<IMailService>();
            builder.RegisterType<EfMailDal>().As<IMailDal>();

            builder.RegisterType<MailTemplatesManager>().As<IMailTemplatesService>();
            builder.RegisterType<EfMailTemplatesDal>().As<IMailTemplatesDal>();

            builder.RegisterType<ProjectManager>().As<IProjectService>();
            builder.RegisterType<EfProjectDal>().As<IProjectDal>();

            builder.RegisterType<ProjectTypeManager>().As<IProjectTypeService>();
            builder.RegisterType<EfProjectTypeDal>().As<IProjectTypeDal>();

            builder.RegisterType<ProjectTeamManager>().As<IProjectTeamService>();
            builder.RegisterType<EfProjectTeamDal>().As<IProjectTeamDal>();

            builder.RegisterType<ProjectTeamMemberManager>().As<IProjectTeamMemberService>();
            builder.RegisterType<EfProjectTeamMemberDal>().As<IProjectTeamMemberDal>();

            builder.RegisterType<TicketAttachmentManager>().As<ITicketAttachmentService>();
            builder.RegisterType<EfTicketAttachmentDal>().As<ITicketAttachmentDal>();

            builder.RegisterType<TicketManager>().As<ITicketService>();
            builder.RegisterType<EfTicketDal>().As<ITicketDal>();

            builder.RegisterType<TicketCommentManager>().As<ITicketCommentService>();
            builder.RegisterType<EfTicketCommentDal>().As<ITicketCommentDal>();

            builder.RegisterType<TicketCommentReplyManager>().As<ITicketCommentReplyService>();
            builder.RegisterType<EfTicketCommentReplyDal>().As<ITicketCommentReplyDal>();

            builder.RegisterType<CustomerManager>().As<ICustomerService>();
            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>();

            builder.RegisterType<UserCustomerManager>().As<IUserCustomerService>();
            builder.RegisterType<EfUserCustomerDal>().As<IUserCustomerDal>();

            builder.RegisterType<ForgotPasswordManager>().As<IForgotPasswordService>();
            builder.RegisterType<EfForgotPasswordDal>().As<IForgotPasswordDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();

            builder.RegisterType<EfBoardDal>().As<IBoardDal>();
            builder.RegisterType<BoardManager>().As<IBoardService>();

            builder.RegisterType<EfBoardCategoryDal>().As<IBoardCategoryDal>();
            builder.RegisterType<BoardCategoryManager>().As<IBoardCategoryService>();

            builder.RegisterType<EfBoardMemberDal>().As<IBoardMemberDal>();
            builder.RegisterType<BoardMemberManager>().As<IBoardMemberService>();


            builder.RegisterType<EfLabelDal>().As<ILabelDal>();
            builder.RegisterType<LabelManager>().As<ILabelService>();

            builder.RegisterType<EfTaskListDal>().As<ITaskListDal>();
            builder.RegisterType<TaskListManager>().As<ITaskListService>();

            builder.RegisterType<EfTaskDal>().As<ITaskDal>();
            builder.RegisterType<TaskManager>().As<ITaskService>();

            builder.RegisterType<EfTaskCommentDal>().As<ITaskCommentDal>();
            builder.RegisterType<TaskCommentManager>().As<ITaskCommentService>();

            builder.RegisterType<EfTaskAttachmentDal>().As<ITaskAttachmentDal>();
            builder.RegisterType<TaskAttachmentManager>().As<ITaskAttachmentService>();

            builder.RegisterType<EfTaskLabelDal>().As<ITaskLabelDal>();
            builder.RegisterType<TaskLabelManager>().As<ITaskLabelService>();

            builder.RegisterType<EfTaskMemberDal>().As<ITaskMemberDal>();
            builder.RegisterType<TaskMemberManager>().As<ITaskMemberService>();

            builder.RegisterType<EfTaskTodoListDal>().As<ITaskTodoListDal>();
            builder.RegisterType<TaskTodoListManager>().As<ITaskTodoListService>();

            builder.RegisterType<EfTaskTodoDal>().As<ITaskTodoDal>();
            builder.RegisterType<TaskTodoManager>().As<ITaskTodoService>();

            builder.RegisterType<EfPermissionDal>().As<IPermissionDal>();
            builder.RegisterType<PermissionManager>().As<IPermissionService>();

            builder.RegisterType<EfUserPermissionDal>().As<IUserPermissionDal>();
            builder.RegisterType<UserPermissionManager>().As<IUserPermissionService>();

            builder.RegisterType<EfHolidayDal>().As<IHolidayDal>();
            builder.RegisterType<HolidayManager>().As<IHolidayService>();

            builder.RegisterType<EfLeaveDeducationDal>().As<ILeaveDeducationDal>();
            builder.RegisterType<LeaveDeducationManager>().As<ILeaveDeducationService>();

            builder.RegisterType<EfCustomDal>().As<ICustomDal>();
            builder.RegisterType<CustomManager>().As<ICustomService>();

            builder.RegisterType<EfDaysWorkDal>().As<IDaysWorkDal>();
            builder.RegisterType<DaysWorkManager>().As<IDaysWorkService>();

            builder.RegisterType<EfUserJobDetailDal>().As<IUserJobDetailDal>();
            builder.RegisterType<UserJobDetailManager>().As<IUserJobDetailService>();

            builder.RegisterType<EfUserJobExperienceDal>().As<IUserJobExperienceDal>();
            builder.RegisterType<UserJobExperienceManager>().As<IUserJobExperienceService>();

            builder.RegisterType<EfUserProfileDetailDal>().As<IUserProfileDetailDal>();
            builder.RegisterType<UserProfileDetailsManager>().As<IUserProfileDetailService>();

            builder.RegisterType<EfUserLanguageDetailDal>().As<IUserLanguageDetailDal>();
            builder.RegisterType<UserLanguageDetailManager>().As<IUserLanguageDetailService>();


            builder.RegisterType<EfUserFamilyDetailDal>().As<IUserFamilyDetailDal>();
            builder.RegisterType<UserFamilyDetailService>().As<IUserFamilyDetailService>();


            builder.RegisterType<EfUserEducationDetailDal>().As<IUserEducationDetailDal>();
            builder.RegisterType<UserEducationDetailManager>().As<IUserEducationDetailService>();


            builder.RegisterType<EfUserCertificateDetailDal>().As<IUserCertificateDetailDal>();
            builder.RegisterType<UserCertificateDetailManager>().As<IUserCertificateDetailService>();



            builder.RegisterType<EfUserRoleDal>().As<IUserRoleDal>();
            builder.RegisterType<UserRoleManager>().As<IUserRoleService>();

            builder.RegisterType<EfRolesForUsersDal>().As<IRolesForUsersDal>();
            builder.RegisterType<RolesForUsersManager>().As<IRolesForUsersService>();

            builder.RegisterType<EfExpenseDal>().As<IExpenseDal>();
            builder.RegisterType<ExpenseManager>().As<IExpenseService>();



            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();

        }
    }
}
