using Business.Repository.CommentReplyRepository.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.CommentReplyRepository;
using Entities.Concrete;
using Entities.DTOs.TicketCommentReplyDtos;

namespace Business.Repository.CommentReplyRepository
{
    public class TicketCommentReplyManager : ITicketCommentReplyService
    {
        private readonly ITicketCommentReplyDal _ticketCommentReplyDal;

        public TicketCommentReplyManager(ITicketCommentReplyDal ticketCommentReplyDal)
        {
            _ticketCommentReplyDal = ticketCommentReplyDal;
        }


        public IResult Add(AddTicketCommentReplyDto addTicketCommentReply)
        {
            TicketCommentReply commentReply = new()
            {
                Content = addTicketCommentReply.Content,
                TicketCommentId = addTicketCommentReply.TicketCommentId,
                UserId = addTicketCommentReply.UserId,
                CreatedAt = DateTime.Now
            };
            _ticketCommentReplyDal.Add(commentReply);
            return new SuccessResult(TicketCommentReplyMessages.AddedCommentReply);
        }

        public IResult Delete(long commentReplyId)
        {
            var reply = _ticketCommentReplyDal.Get(r => r.Id.Equals(commentReplyId));
            if (reply == null) return new ErrorResult(TicketCommentReplyMessages.CommentReplyNotFound);

            _ticketCommentReplyDal.Delete(reply);
            return new SuccessResult(TicketCommentReplyMessages.DeletedCommentReply);
        }

        public IResult DeleteAllByCommentId(long commentId)
        {
            var replies = _ticketCommentReplyDal.GetAll(r => r.TicketCommentId.Equals(commentId));
            if (!replies.Any()) return new ErrorResult();

            foreach (var reply in replies)
            {
                _ticketCommentReplyDal.Delete(reply);
            }
            return new SuccessResult();

        }

        public IResult Update(EditTicketCommentReplyDto editTicketCommentReply)
        {
            var reply = _ticketCommentReplyDal.Get(r => r.Id.Equals(editTicketCommentReply.Id));
            if (reply == null) return new ErrorResult(TicketCommentReplyMessages.CommentReplyNotFound);
            reply.Content = editTicketCommentReply.Content;
            reply.UpdatedAt = DateTime.Now;
            _ticketCommentReplyDal.Update(reply);
            return new SuccessResult(TicketCommentReplyMessages.UpdatedCommentReply);
        }
    }
}
