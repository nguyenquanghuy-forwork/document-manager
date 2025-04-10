using DocumentManagement.Application.Commands;
using DocumentManagement.Domain.Entities;
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
    public class UpdateDocumentHandler : IRequestHandler<UpdateDocumentCommand, Unit>
    {
        private readonly AppDbContext _context;

        public UpdateDocumentHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = await _context.Documents
                .Include(d => d.Details)
                .FirstOrDefaultAsync(d => d.DocumentNumber == request.DocumentNumber, cancellationToken);

            if (document == null)
                throw new Exception("Không tìm thấy chứng từ.");

            var sum = request.Details.Sum(x => x.Amount);
            if (sum != request.TotalAmount)
                throw new Exception("Tổng tiền không khớp.");
            if (request.Details.Any(x => x.Amount < 0))
                throw new Exception("Chi tiết có số tiền âm.");

            document.DocumentDate = request.DocumentDate;
            document.DocumentType = request.DocumentType;
            document.Description = request.Description;
            document.TotalAmount = request.TotalAmount;

            _context.DocumentDetails.RemoveRange(document.Details);
            document.Details = request.Details.Select(d => new DocumentDetail
            {
                AccountCode = d.AccountCode,
                Description = d.Description,
                Amount = d.Amount,
                TransactionType = d.TransactionType,
                DocumentNumber = document.DocumentNumber
            }).ToList();

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
