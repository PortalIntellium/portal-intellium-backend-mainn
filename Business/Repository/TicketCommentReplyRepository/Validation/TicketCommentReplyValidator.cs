using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.CommentReplyRepository.Validation
{
    public class TicketCommentReplyValidator:AbstractValidator<TicketCommentReply>
    {
        public TicketCommentReplyValidator()
        {
            RuleFor(p => p.Content).NotEmpty().WithMessage("İçerik boş olamaz!");
        }
    }
}
