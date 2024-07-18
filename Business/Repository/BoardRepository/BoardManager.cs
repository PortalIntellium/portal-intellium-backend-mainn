using Business.Repository.TaskListRepository;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.BoardRepository;
using Entities.Concrete;
using Entities.DTOs.BoardDtos;
using Microsoft.IdentityModel.Tokens;

namespace Business.Repository.BoardRepository
{
    public class BoardManager : IBoardService
    {
        private readonly IBoardDal _boardDal;
        private readonly ITaskListService _taskListService;
        private readonly IBoardMemberService _boardMemberService;

        public BoardManager(IBoardDal boardDal, ITaskListService taskListService, IBoardMemberService boardMemberService)
        {
            _boardDal = boardDal;
            _taskListService = taskListService;
            _boardMemberService = boardMemberService;
        }

        public IResult Add(Board board)
        {
            _boardDal.Add(board);
            return new SuccessResult("Pano oluşturuldu.");
        }

        public IResult Delete(int boardId)
        {
            var removedBoard = _boardDal.Get(p => p.Id.Equals(boardId));

            if (removedBoard == null) return new ErrorResult("Silinecek pano bulunamadı");
            
            var removedTaskLists = _taskListService.GetAllByBoardId(boardId).Data;
            var removedBoardMembers = _boardMemberService.GetAllByBoardId(boardId).Data;

            if (removedTaskLists != null)
            {
                _taskListService.DeleteAll(removedTaskLists);
            }
            if (removedBoardMembers != null)
            {
                _boardMemberService.DeleteAll(removedBoardMembers);
            }

            _boardDal.Delete(removedBoard);
            return new SuccessResult("Pano silindi.");
        }

        public IDataResult<BoardDto> Get(int boardId)
        {
            var result = _boardDal.GetBoard(boardId);
            return (result != null) ? new SuccessDataResult<BoardDto>(result) : new ErrorDataResult<BoardDto>("Pano bulunamadı");
        }

        public IDataResult<List<BoardDto>> GetAll(int customerId, int userId)
        {
            var result = _boardDal.GetAllBoards(customerId, userId);
            return (!result.IsNullOrEmpty()) ? new SuccessDataResult<List<BoardDto>>(result) : new ErrorDataResult<List<BoardDto>>("Bu şirkete bağlı pano yok");
        }

        public IResult Update(EditBoardDto board)
        {
            var updatedBoard = _boardDal.Get(p => p.Id.Equals(board.Id));
            if(updatedBoard == null) return new ErrorResult("Güncellenecek pano bulunamadı");

            updatedBoard.Name = board.Name;
            updatedBoard.CategoryId = board.CategoryId;
            updatedBoard.PrivateToProjectMembers = board.PrivateToProjectMembers;
            updatedBoard.AvatarPath = board.AvatarPath;
            updatedBoard.EndDate = board.EndDate;
            _boardDal.Update(updatedBoard);
            return new SuccessResult("Pano güncellendi.");
        }
    }
}
