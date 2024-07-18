using Business.File;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Results.Abstract;
using DataAccess.Repository.TicketAttachmentRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using IResult = Core.Utilities.Results.Abstract.IResult;

namespace Business.Repository.TicketAttachmentRepository
{
    public class TicketAttachmentManager : ITicketAttachmentService
    {
        private readonly ITicketAttachmentDal _ticketAttachmentDal;
        private readonly IFileService _fileService;

        public TicketAttachmentManager(ITicketAttachmentDal ticketAttachmentDal, IFileService fileService)
        {
            _ticketAttachmentDal = ticketAttachmentDal;
            _fileService = fileService;
        }

        public async Task<IResult> Add(List<IFormFile> ticketAttachments, long ticketId)
        {
            foreach (var ticketAttachment in ticketAttachments)
            {
                if (ticketAttachment.Length > 0)
                {
                    var result = await _fileService.Save(ticketAttachment, FileType.TICKET_ATTACHMENT);
                    if (!result.Success) continue;

                    TicketAttachment newTicketAttachment = new()
                    {
                        TicketId = ticketId,
                        Name = result.Data.Name,
                        AttachmentPath = result.Data.FilePath
                    };
                    _ticketAttachmentDal.Add(newTicketAttachment);
                }
            }
            return new SuccessResult("Başarılı");
        }

        public IResult Delete(long ticketAttachmentId)
        {
            var result = _ticketAttachmentDal.Get(t => t.Id.Equals(ticketAttachmentId));
            if (result == null) return new ErrorResult("Silinecek ek bulunamadı");

            _ticketAttachmentDal.Delete(result);
            _fileService.Delete(result.AttachmentPath, FileType.TASK_ATTACHMENT);

            return new SuccessResult();
        }

        public IDataResult<List<TicketAttachment>> GetAllByTicketId(long ticketId)
        {
            var result = _ticketAttachmentDal.GetAll(t => t.TicketId.Equals(ticketId));
            return new SuccessDataResult<List<TicketAttachment>>(result);
        }
    }
}
