using DocumentManagement.Application.Commands;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Handlers
{
    public class CreateDocumentHandler : IRequestHandler<CreateDocumentCommand, string>
    {
        private readonly AppDbContext _context;

        public CreateDocumentHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var sumDetails = request.Details.Sum(d => d.Amount);
            if (sumDetails != request.TotalAmount)
                throw new Exception("Tổng tiền chi tiết không khớp với tổng tiền chứng từ.");
            if (request.Details.Any(d => d.Amount < 0))
                throw new Exception("Số tiền không được âm.");

            var document = new Document
            {
                DocumentNumber = request.DocumentNumber,
                DocumentDate = request.DocumentDate,
                DocumentType = request.DocumentType,
                Description = request.Description,
                TotalAmount = request.TotalAmount,
                Details = request.Details.Select(d => new DocumentDetail
                {
                    AccountCode = d.AccountCode,
                    Description = d.Description,
                    Amount = d.Amount,
                    TransactionType = d.TransactionType
                }).ToList()
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync(cancellationToken);

            return document.DocumentNumber;
        }
    }
}
