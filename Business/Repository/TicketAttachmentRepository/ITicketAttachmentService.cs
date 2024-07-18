using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using IResult = Core.Utilities.Results.Abstract.IResult;

namespace Business.Repository.TicketAttachmentRepository
{
    public interface ITicketAttachmentService
    {
        IDataResult<List<TicketAttachment>> GetAllByTicketId(long ticketId);
        Task<IResult> Add(List<IFormFile> ticketAttachments, long ticketId);
        IResult Delete(long ticketAttachmentId);
    }
}
