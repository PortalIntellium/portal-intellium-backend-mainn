using Core.Utilities.Results.Abstract;
using Entities.DTOs.TicketDtos;
using Microsoft.AspNetCore.Http;
using IResult = Core.Utilities.Results.Abstract.IResult;

namespace Business.Repository.TicketRepository
{
    public interface ITicketService
    {
        Task<IResult> Add(AddTicketDto ticket, List<IFormFile>? attachments);
        IResult Update(EditTicketDto addTicket);
        IResult Delete(long id);
        IDataResult<List<GetTicketDto>> GetAllByCustomerId(long customerId);
        IDataResult<List<GetTicketDto>> GetAll();
        IDataResult<List<GetTicketDto>> GetLastTicketsByCustomerId(long customerId, int ticketCount);
        IDataResult<List<GetTicketDto>> GetLastTickets(int ticketCount);
        IDataResult<GetTicketDto> GetById(long id);
        IDataResult<TicketCountDto> GetTicketCount();
        IDataResult<TicketCountDto> GetTicketCountByCustomerId(int customerId);
    }
}
