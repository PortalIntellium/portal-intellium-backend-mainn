namespace Entities.Concrete
{
    public class Permission
    {
        public long Id { get; set; } //İzin kaydının benzersiz kimliği.
        public long UserId { get; set; } //İzni talep eden kullanıcının kimliği.
        public string PermissionType { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public bool? IsAllowed { get; set; } = false; //Admin onayı için
        public string? Status { get; set; } = "Pending"; //Beklemede olarak ayarlanmış
        public string? DocumentPath { get; set; } // Belge raporu yolu


    }
}

