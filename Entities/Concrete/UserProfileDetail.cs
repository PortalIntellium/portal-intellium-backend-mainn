namespace Entities.Concrete
{
    public class UserProfileDetails
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PreferredName { get; set; }//Tercih Edilen Ad
        public DateTime? BirthDate { get; set; } //User Tablosundan Alýnacak

        public string BirthPlace { get; set; } //Doðum Yeri
        public int? Age { get; set; } //Hesaplanacak
        public string Sex { get; set; } //Cinsiyet
        public string MilitaryCase { get; set; } //Askerlik Durumu
        public DateTime? MilitaryDate { get; set; }
        public string BankAccountNo { get; set; }
        public string BankName { get; set; }
        public string IBANNo { get; set; }

        public string Condition { get; set; } //Medeni Durumu
        public string HandicappedState { get; set; } //Engelli Durumu

        public string TC { get; set; }
        public string Nationality { get; set; } //Uyruk
        public string BloodType { get; set; } //Kan Grubu

        public string Country { get; set; } // Ülke
        public string Province { get; set; }// Ýl
        public string District { get; set; } //ilçe
        public string PostCode { get; set; } //Posta Kodu
        public string Adress { get; set; } //Ev Adresi


        public string TelNo { get; set; }
        public string? HomePhone { get; set; } //ev telefonu numarasý
        public string? Interphone { get; set; } //dahili tel no 
        public string? OtherEmail { get; set; } //diðer mail adresi

        public string? Office { get; set; }
        public string GithubUrl { get; set; }
        public string LinkedInUrl { get; set; }


    }
}
