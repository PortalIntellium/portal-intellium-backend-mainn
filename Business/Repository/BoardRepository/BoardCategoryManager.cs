using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.BoardRepository;
using Entities.Concrete;

namespace Business.Repository.BoardRepository
{
    public class BoardCategoryManager : IBoardCategoryService
    {
        private readonly IBoardCategoryDal _boardCategoryDal;

        public BoardCategoryManager(IBoardCategoryDal boardCategoryDal)
        {
            _boardCategoryDal = boardCategoryDal;
        }


        public IResult Add(BoardCategory boardCategory)
        {
            _boardCategoryDal.Add(boardCategory);
            return new SuccessResult("Kategori Eklendi");
        }

        public IResult Delete(int boardCategoryId)
        {
            var result = _boardCategoryDal.Get(p => p.Id.Equals(boardCategoryId));
            if (result == null) return new ErrorResult("Silinecek kategori bulunamadı");

            _boardCategoryDal.Delete(result);
            return new SuccessResult("Kategori silindi");
        }

        public IDataResult<List<BoardCategory>> GetAll()
        {
            var result = _boardCategoryDal.GetAll();
            return new SuccessDataResult<List<BoardCategory>>(result, "Listelendi");
        }

        public IResult Update(BoardCategory boardCategory)
        {
            var result = _boardCategoryDal.Get(p => p.Id.Equals(boardCategory.Id));
            if (result == null) return new ErrorResult("Güncellenecek kategori bulunamadı");

            result.Name = boardCategory.Name;
            _boardCategoryDal.Update(result);
            return new SuccessResult("Güncelleme Başarılı");


        }
    }
}
