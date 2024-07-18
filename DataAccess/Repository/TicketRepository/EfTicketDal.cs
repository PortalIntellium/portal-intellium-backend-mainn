using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.ProjectDtos;
using Entities.DTOs.TicketDtos;

namespace DataAccess.Repository.TicketRepository
{
    public class EfTicketDal : EfEntityRepositoryBase<Ticket, PortalContext>, ITicketDal
    {
        public List<GetTicketDto> GetAllAsDto()
        {
            using var context = new PortalContext();
            var result = (from ticket in context.Tickets
                          select new GetTicketDto
                          {
                              Id = ticket.Id,
                              Name = ticket.Name,
                              Description = ticket.Description,
                              Status = ticket.Status,
                              CreationDate = ticket.CreationDate,
                              ResolutionDate = ticket.ResolutionDate,
                              RequestType = ticket.RequestType,
                              AssignedUser = context.Users.Where(u => u.Id.Equals(ticket.AssignedUserId)).Select(user => new TicketUserDto
                              {
                                  Id = user.Id,
                                  Name = user.Name,
                                  ImageUrl = user.ImageUrl,
                                  IsActive = user.IsActive
                              }).SingleOrDefault(),
                              CreatorUser = context.Users.Where(u => u.Id.Equals(ticket.CreatorUserId)).Select(user => new TicketUserDto
                              {
                                  Id = user.Id,
                                  Name = user.Name,
                                  ImageUrl = user.ImageUrl,
                                  IsActive = user.IsActive
                              }).Single(),
                              Customer = context.Customers.Where(c => c.CustomerId.Equals(ticket.CustomerId)).Select(customer => new CustomerDto
                              {
                                  CustomerId = customer.CustomerId,
                                  CustomerName = customer.CustomerName
                              }).Single(),
                              Project = context.Projects.Where(p => p.Id.Equals(ticket.ProjectId)).Select(project => new ProjectForTicketDto
                              {
                                  Id = project.Id,
                                  ProjectName = project.ProjectName,
                              }).Single()
                          }).ToList();

            return result;
        }

        public List<GetTicketDto> GetAllByCustomerId(long customerId)
        {
            using var context = new PortalContext();
            var result = (from ticket in context.Tickets
                          where ticket.CustomerId == customerId
                          select new GetTicketDto
                          {
                              Id = ticket.Id,
                              Name = ticket.Name,
                              Description = ticket.Description,
                              Status = ticket.Status,
                              CreationDate = ticket.CreationDate,
                              ResolutionDate = ticket.ResolutionDate,
                              RequestType = ticket.RequestType,
                              AssignedUser = context.Users.Where(u => u.Id.Equals(ticket.AssignedUserId)).Select(user => new TicketUserDto
                              {
                                  Id = user.Id,
                                  Name = user.Name,
                                  ImageUrl = user.ImageUrl,
                                  IsActive = user.IsActive
                              }).SingleOrDefault(),
                              CreatorUser = context.Users.Where(u => u.Id.Equals(ticket.CreatorUserId)).Select(user => new TicketUserDto
                              {
                                  Id = user.Id,
                                  Name = user.Name,
                                  ImageUrl = user.ImageUrl,
                                  IsActive = user.IsActive
                              }).Single(),
                              Customer = context.Customers.Where(c => c.CustomerId.Equals(ticket.CustomerId)).Select(customer => new CustomerDto
                              {
                                  CustomerId = customer.CustomerId,
                                  CustomerName = customer.CustomerName
                              }).Single(),
                              Project = context.Projects.Where(p => p.Id.Equals(ticket.ProjectId)).Select(project => new ProjectForTicketDto
                              {
                                  Id = project.Id,
                                  ProjectName = project.ProjectName,
                              }).Single()
                          }).ToList();

            return result;
        }

        public GetTicketDto GetById(long id)
        {
            using var context = new PortalContext();
            var result = (from ticket in context.Tickets
                          where ticket.Id == id
                          select new GetTicketDto
                          {
                              Id = ticket.Id,
                              Name = ticket.Name,
                              Description = ticket.Description,
                              Status = ticket.Status,
                              CreationDate = ticket.CreationDate,
                              ResolutionDate = ticket.ResolutionDate,
                              RequestType = ticket.RequestType,
                              AssignedUser = context.Users.Where(u => u.Id.Equals(ticket.AssignedUserId)).Select(user => new TicketUserDto
                              {
                                  Id = user.Id,
                                  Name = user.Name,
                                  ImageUrl = user.ImageUrl,
                                  IsActive = user.IsActive
                              }).SingleOrDefault(),
                              CreatorUser = context.Users.Where(u => u.Id.Equals(ticket.CreatorUserId)).Select(user => new TicketUserDto
                              {
                                  Id = user.Id,
                                  Name = user.Name,
                                  ImageUrl = user.ImageUrl,
                                  IsActive = user.IsActive
                              }).Single(),
                              Customer = context.Customers.Where(c => c.CustomerId.Equals(ticket.CustomerId)).Select(customer => new CustomerDto
                              {
                                  CustomerId = customer.CustomerId,
                                  CustomerName = customer.CustomerName
                              }).Single(),
                              Project = context.Projects.Where(p => p.Id.Equals(ticket.ProjectId)).Select(project => new ProjectForTicketDto
                              {
                                  Id = project.Id,
                                  ProjectName = project.ProjectName,
                              }).Single()
                          }).SingleOrDefault();

            return result;
        }

        public List<GetTicketDto> GetLastTickets(int ticketCount)
        {
            using var context = new PortalContext();
            var result = (from ticket in context.Tickets
                          orderby ticket.CreationDate descending
                          select new GetTicketDto
                          {
                              Id = ticket.Id,
                              Name = ticket.Name,
                              Description = ticket.Description,
                              Status = ticket.Status,
                              CreationDate = ticket.CreationDate,
                              ResolutionDate = ticket.ResolutionDate,
                              RequestType = ticket.RequestType,
                              AssignedUser = context.Users.Where(u => u.Id.Equals(ticket.AssignedUserId)).Select(user => new TicketUserDto
                              {
                                  Id = user.Id,
                                  Name = user.Name,
                                  ImageUrl = user.ImageUrl,
                                  IsActive = user.IsActive
                              }).SingleOrDefault(),
                              CreatorUser = context.Users.Where(u => u.Id.Equals(ticket.CreatorUserId)).Select(user => new TicketUserDto
                              {
                                  Id = user.Id,
                                  Name = user.Name,
                                  ImageUrl = user.ImageUrl,
                                  IsActive = user.IsActive
                              }).Single(),
                              Customer = context.Customers.Where(c => c.CustomerId.Equals(ticket.CustomerId)).Select(customer => new CustomerDto
                              {
                                  CustomerId = customer.CustomerId,
                                  CustomerName = customer.CustomerName
                              }).Single(),
                              Project = context.Projects.Where(p => p.Id.Equals(ticket.ProjectId)).Select(project => new ProjectForTicketDto
                              {
                                  Id = project.Id,
                                  ProjectName = project.ProjectName,
                              }).Single()
                          }).Take(ticketCount).ToList();

            return result;
        }

        public List<GetTicketDto> GetLastTicketsByCustomerId(long customerId, int ticketCount)
        {
            using var context = new PortalContext();
            var result = (from ticket in context.Tickets
                          where ticket.CustomerId == customerId
                          orderby ticket.CreationDate descending
                          select new GetTicketDto
                          {
                              Id = ticket.Id,
                              Name = ticket.Name,
                              Description = ticket.Description,
                              Status = ticket.Status,
                              CreationDate = ticket.CreationDate,
                              ResolutionDate = ticket.ResolutionDate,
                              RequestType = ticket.RequestType,
                              AssignedUser = context.Users.Where(u => u.Id.Equals(ticket.AssignedUserId)).Select(user => new TicketUserDto
                              {
                                  Id = user.Id,
                                  Name = user.Name,
                                  ImageUrl = user.ImageUrl,
                                  IsActive = user.IsActive
                              }).SingleOrDefault(),
                              CreatorUser = context.Users.Where(u => u.Id.Equals(ticket.CreatorUserId)).Select(user => new TicketUserDto
                              {
                                  Id = user.Id,
                                  Name = user.Name,
                                  ImageUrl = user.ImageUrl,
                                  IsActive = user.IsActive
                              }).Single(),
                              Customer = context.Customers.Where(c => c.CustomerId.Equals(ticket.CustomerId)).Select(customer => new CustomerDto
                              {
                                  CustomerId = customer.CustomerId,
                                  CustomerName = customer.CustomerName
                              }).Single(),
                              Project = context.Projects.Where(p => p.Id.Equals(ticket.ProjectId)).Select(project => new ProjectForTicketDto
                              {
                                  Id = project.Id,
                                  ProjectName = project.ProjectName,
                              }).Single()
                          }).Take(ticketCount).ToList();

            return result;
        }

        public TicketCountDto GetTicketCount()
        {
            using var context = new PortalContext();
            var tickets = context.Tickets;
            var result = new TicketCountDto
            {
                TotalCount = tickets.Count(),
                NewRequestCount = tickets.Where(t => t.Status.Equals(0)).Count(),
                InProgressCount = tickets.Where(t => t.Status.Equals(1)).Count(),
                CompletedCount = tickets.Where(t => t.Status.Equals(2)).Count(),
            };
            return result;
        }

        public TicketCountDto GetTicketCountByCustomerId(int customerId)
        {
            using var context = new PortalContext();
            var tickets = context.Tickets.Where(t => t.CustomerId.Equals(customerId));
            var result = new TicketCountDto
            {
                TotalCount = tickets.Count(),
                NewRequestCount = tickets.Where(t => t.Status.Equals(0)).Count(),
                InProgressCount = tickets.Where(t => t.Status.Equals(1)).Count(),
                CompletedCount = tickets.Where(t => t.Status.Equals(2)).Count(),
            };
            return result;
        }
    }
}
