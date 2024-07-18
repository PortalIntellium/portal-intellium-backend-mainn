using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs.BoardDtos;

namespace Business.Repository.BoardRepository
{
    public interface IBoardService
    {
        IResult Add(Board board);
        IResult Delete(int boardId);
        IResult Update(EditBoardDto board);
        IDataResult<List<BoardDto>> GetAll(int customerId, int userId);
        IDataResult<BoardDto> Get(int boardId);
    }
}
