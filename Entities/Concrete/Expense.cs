using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Expense
    {
        public int Id { get; set; }
        public long CustomerId { get; set; }

        public long UserId { get; set; }

        public string ProjectName { get; set; }

        public string Description { get; set; }

        public string ExpenseType { get; set; }
        public decimal ExcludingVatAmount { get; set; }

        public decimal VatRate { get; set; }

        public decimal Vat { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string InvoiceNumber { get; set; }

        public string InvoiceTitle { get; set; }

        public bool IsActive { get; set; }
        public byte[] ImageData { get; set; }

        public bool IsConfirmation  { get; set; }



    }
}
