using _0_Framework.Infrastructure;
using CommentManagement.Application;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Configuration.Permission;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommentManagement.Configuration
{
    public class CommentManagementBootstraper
    {
        public static void Configure(IServiceCollection services,string connectionString)
        {
            services.AddTransient<ICommentApplication, CommentApplication>();
            services.AddTransient<ICommentRepository, CommentRepository>();

            services.AddTransient<IPermissionExposer, CommentPermissionExposer>();

            services.AddDbContext<CommentContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
