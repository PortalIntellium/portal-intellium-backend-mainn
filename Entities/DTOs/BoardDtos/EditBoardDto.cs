﻿namespace Entities.DTOs.BoardDtos
{
    public class EditBoardDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string AvatarPath { get; set; }
        public bool PrivateToProjectMembers { get; set; }
        public DateTime EndDate { get; set; }
    }
}
