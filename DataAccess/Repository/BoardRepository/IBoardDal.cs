using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.BoardDtos;

namespace DataAccess.Repository.BoardRepository
{
    public interface IBoardDal : IEntityRepository<Board>
    {
        List<BoardDto> GetAllBoards(int customerId, int userId);
        BoardDto GetBoard(int boardId);
    }
}
