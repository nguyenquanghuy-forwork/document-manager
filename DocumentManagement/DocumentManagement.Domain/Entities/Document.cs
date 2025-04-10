using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Domain.Entities
{
    public class Document
    {
        public string DocumentNumber { get; set; } // Số chứng từ
        public DateTime DocumentDate { get; set; } // Ngày chứng từ
        public string DocumentType { get; set; }   // Loại chứng từ
        public string Description { get; set; }    // Diễn giải
        public decimal TotalAmount { get; set; }   // Tổng tiền

        public ICollection<DocumentDetail> Details { get; set; }
    }
}
