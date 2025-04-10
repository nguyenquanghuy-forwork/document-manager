using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Commands
{
    public class CreateDocumentCommand : IRequest<string>
    {
        public string DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public string DocumentType { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public List<DocumentDetailDto> Details { get; set; }
    }

    public class DocumentDetailDto
    {
        public string AccountCode { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
    }
}
