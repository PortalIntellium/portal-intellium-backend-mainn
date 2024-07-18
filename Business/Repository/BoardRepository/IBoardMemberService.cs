using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs.BoardDtos;

namespace Business.Repository.BoardRepository
{
    public interface IBoardMemberService
    {
        IResult Add(List<BoardMember> boardMembers);
        IResult Delete(int boardMemberId);
        IResult DeleteAll(List<BoardMember> boardMembers);
        IDataResult<List<BoardMember>> GetAllByBoardId(int boardId);
        IDataResult<List<BoardMemberDto>> GetAllByBoardIdWithUsers(int boardId);

    }
}
