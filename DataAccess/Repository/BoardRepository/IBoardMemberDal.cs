using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.BoardDtos;

namespace DataAccess.Repository.BoardRepository
{
    public interface IBoardMemberDal : IEntityRepository<BoardMember>
    {
        List<BoardMemberDto> GetAllByBoardIdWithUsers(int boardId);
    }
}
