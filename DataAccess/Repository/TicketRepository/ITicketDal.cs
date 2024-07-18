using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.TicketDtos;

namespace DataAccess.Repository.TicketRepository
{
    public interface ITicketDal : IEntityRepository<Ticket>
    {
        List<GetTicketDto> GetAllByCustomerId(long customerId);
        List<GetTicketDto> GetAllAsDto();
        List<GetTicketDto> GetLastTicketsByCustomerId(long customerId, int ticketCount);
        List<GetTicketDto> GetLastTickets(int ticketCount);
        GetTicketDto GetById(long id);
        TicketCountDto GetTicketCount();
        TicketCountDto GetTicketCountByCustomerId(int customerId);


    }
}
