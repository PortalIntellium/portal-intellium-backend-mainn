using Business.Repository.CommentReplyRepository;
using Business.Repository.TicketCommentRepository.Constants;
using Business.Repository.TicketCommentRepository.Validation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.TicketCommentRepository;
using Entities.Concrete;
using Entities.DTOs.TicketCommentDtos;

namespace Business.Repository.TicketCommentRepository
{
    public class TicketCommentManager : ITicketCommentService
    {
        private readonly ITicketCommentDal _ticketCommentDal;
        private readonly ITicketCommentReplyService _ticketCommentReplyService;

        public TicketCommentManager(ITicketCommentDal ticketCommentDal, ITicketCommentReplyService ticketCommentReplyService)
        {
            _ticketCommentDal = ticketCommentDal;
            _ticketCommentReplyService = ticketCommentReplyService;
        }

        [ValidationAspect(typeof(TicketCommentValidator))]
        public IResult Add(AddTicketCommentDto addTicketComment)
        {
            TicketComment ticketComment = new()
            {
                Content = addTicketComment.Content,
                TicketId = addTicketComment.TicketId,
                UserId = addTicketComment.UserId,
                CreatedAt = DateTime.Now,
            };
            _ticketCommentDal.Add(ticketComment);
            return new SuccessResult(TicketCommentMessages.AddedTicketComment);
        }

        public IResult Delete(long ticketCommentId)
        {
            var ticketComment = _ticketCommentDal.Get(t => t.Id.Equals(ticketCommentId));
            if (ticketComment == null) { return new ErrorResult(TicketCommentMessages.TicketCommentNotFound); }

            _ticketCommentReplyService.DeleteAllByCommentId(ticketCommentId);

            _ticketCommentDal.Delete(ticketComment);
            return new SuccessResult(TicketCommentMessages.DeletedTicketComment);
        }

        public IResult DeleteAllByTicketId(long ticketId)
        {
            var comments = _ticketCommentDal.GetAll(c => c.TicketId.Equals(ticketId));
            if (!comments.Any()) return new ErrorResult();

            foreach (var comment in comments)
            {
                Delete(comment.Id);
            }
            return new SuccessResult();
        }

        public IDataResult<List<GetTicketCommentDto>> GetAllByTicketId(long ticketId)
        {
            var comments = _ticketCommentDal.GetAllByTicketId(ticketId);
            return new SuccessDataResult<List<GetTicketCommentDto>>(comments, TicketCommentMessages.TicketCommentListed);
        }

        public IResult Update(EditTicketCommentDto editTicketComment)
        {
            var comment = _ticketCommentDal.Get(c => c.Id.Equals(editTicketComment.Id));
            if (comment == null) return new ErrorResult(TicketCommentMessages.TicketCommentNotFound);
            comment.Content = editTicketComment.Content;
            comment.UpdatedAt = DateTime.Now;
            _ticketCommentDal.Update(comment);
            return new SuccessResult(TicketCommentMessages.UpdatedTicketComment);
        }
    }
}
