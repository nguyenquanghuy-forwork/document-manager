using DocumentManagement.Application.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Queries
{
    public class GetDocumentListQuery : IRequest<List<DocumentDto>>
    {
        public string? DocumentNumber { get; set; }
        public string? DocumentType { get; set; }
        public DateTime? DocumentDate { get; set; }
    }

    public class DocumentDto
    {
        public string DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public string DocumentType { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public List<DocumentDetailDto> Details { get; set; }
    }
}
