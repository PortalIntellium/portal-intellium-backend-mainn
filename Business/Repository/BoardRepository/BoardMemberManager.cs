using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.BoardRepository;
using Entities.Concrete;
using Entities.DTOs.BoardDtos;
using Microsoft.IdentityModel.Tokens;

namespace Business.Repository.BoardRepository
{
    public class BoardMemberManager : IBoardMemberService
    {
        private readonly IBoardMemberDal _boardMemberDal;

        public BoardMemberManager(IBoardMemberDal boardMemberDal)
        {
            _boardMemberDal = boardMemberDal;
        }

        public IResult Add(List<BoardMember> boardMembers)
        {
            if (boardMembers.IsNullOrEmpty()) return new ErrorResult("Panoya atanacak kullanıcılar bulunamadı");
            foreach (var boardMember in boardMembers)
            {
                _boardMemberDal.Add(boardMember);
            }
            return new SuccessResult("Panoya kullanıcılar atandı.");
        }

        public IResult Delete(int boardMemberId)
        {
            var result = _boardMemberDal.Get(p => p.Id.Equals(boardMemberId));
            if (result == null) return new ErrorResult("Silmek için panoya atanmış kullanıcı bulunamadı");
            _boardMemberDal.Delete(result);
            return new SuccessResult("Panoya atanmış kullanıcı kaldırıldı");
        }

        public IResult DeleteAll(List<BoardMember> boardMembers)
        {
            if (boardMembers.IsNullOrEmpty()) return new ErrorResult("Silmek için panoya atanmış kullanıcılar bulunamadı");
            foreach (var boardMember in boardMembers)
            {
                Delete(boardMember.Id);
            }
            return new SuccessResult();
        }

        public IDataResult<List<BoardMember>> GetAllByBoardId(int boardId)
        {
            var result = _boardMemberDal.GetAll(p => p.BoardId.Equals(boardId));
            return new SuccessDataResult<List<BoardMember>>(result);
        }

        public IDataResult<List<BoardMemberDto>> GetAllByBoardIdWithUsers(int boardId)
        {
            var result = _boardMemberDal.GetAllByBoardIdWithUsers(boardId);
            return new SuccessDataResult<List<BoardMemberDto>>(result);
        }
    }
}
