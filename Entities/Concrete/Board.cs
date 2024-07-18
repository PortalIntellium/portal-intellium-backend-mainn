using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Board
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CategoryId { get; set; }
        public int CreatedUserId { get; set; }
        public string Name { get; set; }
        public string AvatarPath { get; set; }
        public bool PrivateToProjectMembers { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}
