using DocumentManagement.Application.Commands;
using DocumentManagement.Application.Queries;
using DocumentManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Handlers
{
    public class GetDocumentListHandler : IRequestHandler<GetDocumentListQuery, List<DocumentDto>>
    {
        private readonly AppDbContext _context;

        public GetDocumentListHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DocumentDto>> Handle(GetDocumentListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Documents.Include(d => d.Details).AsQueryable();

            if (!string.IsNullOrEmpty(request.DocumentNumber))
                query = query.Where(x => x.DocumentNumber.Contains(request.DocumentNumber));
            if (!string.IsNullOrEmpty(request.DocumentType))
                query = query.Where(x => x.DocumentType.Contains(request.DocumentType));
            if (request.DocumentDate.HasValue)
                query = query.Where(x => x.DocumentDate.Date == request.DocumentDate.Value.Date);

            return await query.Select(doc => new DocumentDto
            {
                DocumentNumber = doc.DocumentNumber,
                DocumentDate = doc.DocumentDate,
                DocumentType = doc.DocumentType,
                Description = doc.Description,
                TotalAmount = doc.TotalAmount,
                Details = doc.Details.Select(d => new DocumentDetailDto
                {
                    AccountCode = d.AccountCode,
                    Amount = d.Amount,
                    Description = d.Description,
                    TransactionType = d.TransactionType
                }).ToList()
            }).ToListAsync(cancellationToken);
        }
    }
}
