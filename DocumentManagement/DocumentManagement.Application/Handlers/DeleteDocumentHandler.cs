using DocumentManagement.Application.Commands;
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
    public class DeleteDocumentHandler : IRequestHandler<DeleteDocumentCommand, Unit>
    {
        private readonly AppDbContext _context;

        public DeleteDocumentHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            var doc = await _context.Documents
                .Include(d => d.Details)
                .FirstOrDefaultAsync(d => d.DocumentNumber == request.DocumentNumber, cancellationToken);

            if (doc == null)
                throw new Exception("Không tìm thấy chứng từ.");

            _context.Documents.Remove(doc);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
