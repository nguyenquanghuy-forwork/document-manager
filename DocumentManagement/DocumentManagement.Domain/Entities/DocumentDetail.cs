using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Domain.Entities
{
    public class DocumentDetail
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; } // Khóa ngoại
        public string AccountCode { get; set; }    // Mã tài khoản
        public string Description { get; set; }    // Diễn giải
        public decimal Amount { get; set; }        // Số tiền
        public string TransactionType { get; set; } // Loại giao dịch

        public Document Document { get; set; }
    }
}
