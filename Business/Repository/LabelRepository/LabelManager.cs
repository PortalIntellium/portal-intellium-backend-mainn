using Business.Repository.LabelRepository.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.LabelRepository;
using Entities.Concrete;

namespace Business.Repository.LabelRepository
{
    public class LabelManager : ILabelService
    {
        private readonly ILabelDal _labelDal;

        public LabelManager(ILabelDal labelDal)
        {
            _labelDal = labelDal;
        }
        public IResult Add(Label label)
        {
            _labelDal.Add(label);
            return new SuccessResult(LabelMessages.AddedLabel);
        }

        public IResult Delete(int labelId)
        {
            var result = _labelDal.Get(p => p.Id.Equals(labelId));
            if (result == null) return new ErrorResult("Silinecek etiket bulunamadı");

            _labelDal.Delete(result);
            return new SuccessResult(LabelMessages.DeletedLabel);
        }

        public IDataResult<List<Label>> GetAll()
        {
            var result = _labelDal.GetAll();
            return new SuccessDataResult<List<Label>>(result);
        }

        public IResult Update(Label label)
        {
            var result = _labelDal.Get(p => p.Id.Equals(label.Id));
            if (result == null) return new ErrorResult("Güncellenecek etiket bulunamadı");

            result.Name = label.Name;
            result.Color = label.Color;
            _labelDal.Update(result);
            return new SuccessResult(LabelMessages.UpdatedLabel);
        }
    }
}
