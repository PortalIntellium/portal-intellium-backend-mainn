using Business.Repository.TicketAttachmentRepository;
using Business.Repository.TicketCommentRepository;
using Business.Repository.TicketRepository.Constants;
using Business.Repository.TicketRepository.Validation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.TicketRepository;
using Entities.Concrete;
using Entities.DTOs.TicketDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using IResult = Core.Utilities.Results.Abstract.IResult;

namespace Business.Repository.TicketRepository
{
    public class TicketManager : ITicketService
    {
        private readonly ITicketDal _ticketDal;
        private readonly ITicketCommentService _ticketCommentService;
        private readonly ITicketAttachmentService _ticketAttachmentService;

        public TicketManager(ITicketDal ticketDal, ITicketCommentService ticketCommentService, ITicketAttachmentService ticketAttachmentService)
        {
            _ticketDal = ticketDal;
            _ticketCommentService = ticketCommentService;
            _ticketAttachmentService = ticketAttachmentService;
        }

        //[SecuredOperation("admin,teamlead,ticket.add")]//yetki kontrolü
        [ValidationAspect(typeof(TicketValidator))]
        public async Task<IResult> Add(AddTicketDto addTicket, List<IFormFile>? attachments)
        {
            Ticket ticket = new()
            {
                Name = addTicket.Name,
                Description = addTicket.Description,
                ProjectId = addTicket.ProjectId,
                CustomerId = addTicket.CustomerId,
                CreatorUserId = addTicket.CreatorUserId,
                CreationDate = DateTime.Now,
                Status = 0
            };
            _ticketDal.Add(ticket);

            if (!attachments.IsNullOrEmpty())
            {
                await _ticketAttachmentService.Add(attachments!, ticket.Id);
            }
            return new SuccessResult(TicketMessages.AddedTicket);
        }
        //[SecuredOperation("admin,teamlead,ticket.add")]// authorization
        public IResult Update(EditTicketDto ticket)
        {
            var updatedTicket = _ticketDal.Get(t => t.Id.Equals(ticket.Id));
            updatedTicket.AssignedUserId = ticket.AssignedUserId;
            updatedTicket.RequestType = ticket.RequestType;
            updatedTicket.Status = ticket.Status;

            // will be revised, enum will be used
            if (ticket.Status == 2)
            {
                updatedTicket.ResolutionDate = DateTime.Now;
            }

            _ticketDal.Update(updatedTicket);
            return new SuccessResult(TicketMessages.UpdatedTicket);
        }
        public IResult Delete(long id)
        {
            var result = _ticketDal.Get(p => p.Id == id);
            if (result == null)
            {
                return new ErrorResult(TicketMessages.TicketNotFound);
            }

            _ticketCommentService.DeleteAllByTicketId(id);

            _ticketDal.Delete(result);
            return new SuccessResult(TicketMessages.DeletedTicket);
        }
        public IDataResult<GetTicketDto> GetById(long id)
        {
            var ticket = _ticketDal.GetById(id);
            if (ticket == null) return new ErrorDataResult<GetTicketDto>(TicketMessages.TicketNotFound);

            return new SuccessDataResult<GetTicketDto>(ticket, TicketMessages.TicketListed);
        }

        public IDataResult<List<GetTicketDto>> GetAllByCustomerId(long customerId)
        {
            var tickets = _ticketDal.GetAllByCustomerId(customerId);
            return new SuccessDataResult<List<GetTicketDto>>(tickets, TicketMessages.TicketListed);
        }

        public IDataResult<List<GetTicketDto>> GetAll()
        {
            var tickets = _ticketDal.GetAllAsDto();
            return new SuccessDataResult<List<GetTicketDto>>(tickets, TicketMessages.TicketListed);
        }

        public IDataResult<List<GetTicketDto>> GetLastTicketsByCustomerId(long customerId, int ticketCount)
        {
            var tickets = _ticketDal.GetLastTicketsByCustomerId(customerId, ticketCount);
            return new SuccessDataResult<List<GetTicketDto>>(tickets, TicketMessages.TicketListed);
        }

        public IDataResult<List<GetTicketDto>> GetLastTickets(int ticketCount)
        {
            var tickets = _ticketDal.GetLastTickets(ticketCount);
            return new SuccessDataResult<List<GetTicketDto>>(tickets, TicketMessages.TicketListed);
        }

        public IDataResult<TicketCountDto> GetTicketCount()
        {
            var count = _ticketDal.GetTicketCount();
            return new SuccessDataResult<TicketCountDto>(count);
        }

        public IDataResult<TicketCountDto> GetTicketCountByCustomerId(int customerId)
        {
            var count = _ticketDal.GetTicketCountByCustomerId(customerId);
            return new SuccessDataResult<TicketCountDto>(count);
        }
    }
}
