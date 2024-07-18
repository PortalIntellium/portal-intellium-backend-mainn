using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.BoardDtos;

namespace DataAccess.Repository.BoardRepository
{
    public class EfBoardDal : EfEntityRepositoryBase<Board, PortalContext>, IBoardDal
    {
        public List<BoardDto> GetAllBoards(int customerId, int userId)
        {
            using (var context = new PortalContext())
            {
                var filteredBoards = (from board in context.Boards
                                      where board.CustomerId == customerId
                                         && (board.PrivateToProjectMembers == false
                                             || context.BoardMembers.Any(boardMember => boardMember.UserId == userId)
                                             || board.CreatedUserId == userId)
                                      select board).ToList();

                var result = filteredBoards.Select(board => new BoardDto
                {
                    Id = board.Id,
                    CustomerId = board.CustomerId,
                    Category = context.BoardCategories.FirstOrDefault(category => category.Id == board.CategoryId),
                    Name = board.Name,
                    AvatarPath = board.AvatarPath,
                    PrivateToProjectMembers = board.PrivateToProjectMembers,
                    StartDate = board.StartDate,
                    EndDate = board.EndDate,
                    CreatedUser = context.Users
                    .Where(user => user.Id == userId)
                    .Select(user => new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        ImageUrl = user.ImageUrl,
                        Name = user.Name,
                        AddetAt = user.AddetAt,
                        BirthDate = user.BirthDate,
                        Language = user.Language,
                        IsActive = user.IsActive,
                        Customer = context.UserCustomers.Where(uc => uc.UserId.Equals(user.Id))
                                 .Join(context.Customers, uc => uc.CustomerId, customer => customer.CustomerId, (uc, customer) => new CustomerDto
                                 {
                                     CustomerId = customer.CustomerId,
                                     CustomerName = customer.CustomerName,
                                 }).SingleOrDefault(),
                        UserRole = context.RolesForUsers.Where(ru => ru.UserId.Equals(user.Id))
                                 .Join(context.UserRoles, ru => ru.RoleId, role => role.Id, (ru, role) => new UserRole
                                 {
                                     Id = role.Id,
                                     RoleName = role.RoleName,
                                 }).SingleOrDefault(),
                    }).FirstOrDefault()!,
                    BoardMembers = (from boardMember in context.BoardMembers
                                    join user in context.Users on boardMember.UserId equals user.Id
                                    where boardMember.BoardId == board.Id
                                    select new BoardMemberDto
                                    {
                                        Id = boardMember.Id,
                                        UserId = boardMember.UserId,
                                        Email = user.Email,
                                        ImageUrl = user.ImageUrl,
                                        Name = user.Name
                                    }).ToList()
                }).ToList();

                return result;
            }
        }

        public BoardDto GetBoard(int boardId)
        {
            using (var context = new PortalContext())
            {

                var result = context.Boards
                .Where(board => board.Id == boardId)
                .Select(board => new BoardDto
                {
                    Id = board.Id,
                    CustomerId = board.CustomerId,
                    Category = context.BoardCategories.FirstOrDefault(category => category.Id == board.CategoryId),
                    Name = board.Name,
                    AvatarPath = board.AvatarPath,
                    PrivateToProjectMembers = board.PrivateToProjectMembers,
                    StartDate = board.StartDate,
                    EndDate = board.EndDate,
                    CreatedUser = context.Users
                        .Where(user => user.Id == board.CreatedUserId)
                        .Select(user => new UserDto
                        {
                            Id = user.Id,
                            Email = user.Email,
                            ImageUrl = user.ImageUrl,
                            Name = user.Name,
                            AddetAt = user.AddetAt,
                            BirthDate = user.BirthDate,
                            Language = user.Language,
                            IsActive = user.IsActive,
                            Customer = context.UserCustomers.Where(uc => uc.UserId.Equals(user.Id))
                                 .Join(context.Customers, uc => uc.CustomerId, customer => customer.CustomerId, (uc, customer) => new CustomerDto
                                 {
                                     CustomerId = customer.CustomerId,
                                     CustomerName = customer.CustomerName,
                                 }).SingleOrDefault(),
                            UserRole = context.RolesForUsers.Where(ru => ru.UserId.Equals(user.Id))
                                 .Join(context.UserRoles, ru => ru.RoleId, role => role.Id, (ru, role) => new UserRole
                                 {
                                     Id = role.Id,
                                     RoleName = role.RoleName,
                                 }).SingleOrDefault(),
                        })
                        .FirstOrDefault()!,
                    BoardMembers = (from boardMember in context.BoardMembers
                                    join user in context.Users on boardMember.UserId equals user.Id
                                    where boardMember.BoardId == board.Id
                                    select new BoardMemberDto
                                    {
                                        Id = boardMember.Id,
                                        UserId = boardMember.UserId,
                                        Email = user.Email,
                                        ImageUrl = user.ImageUrl,
                                        Name = user.Name
                                    }).ToList()

            })
                .SingleOrDefault();

                return result;
            }
        }
    }
}
