using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.UserPermissionRepository
{
    public class EfUserPermissionDal : EfEntityRepositoryBase<UserPermission, PortalContext>, IUserPermissionDal
    {
        private readonly PortalContext _context;

        public EfUserPermissionDal(PortalContext context)
        {
            _context = context;
        }

        public UserPermission GetUserPermissionByUserId(long id)
        {
            return _context.UserPermissions.FirstOrDefault(x => x.UserId == id);
        }


    }
}
