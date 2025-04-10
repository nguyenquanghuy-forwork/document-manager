using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Commands
{
    public class DeleteDocumentCommand : IRequest<Unit>
    {
        public string DocumentNumber { get; set; }
    }
}
