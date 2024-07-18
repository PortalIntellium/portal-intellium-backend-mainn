using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Debit
    {
        public int Id { get; set; }
        public long ReceiverUserId { get; set; } // Teslim Alan Personel
        public long DeliveredUserId { get; set; } // Teslim Eden Personel
        public string Laptop { get; set; }
        public string SerialNumber { get; set; }
        public string BarcodeNumber { get; set; }
        public string Model { get; set; }
        public string CPU { get; set; }
        public string RAM { get; set; }
        public string Storage { get; set; }
        public string Mouse { get; set; }
        public string? ComputerBag { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string? Status { get; set; } = "Teslim Edildi";
        public string? PdfPath { get; set; }
    }
}
