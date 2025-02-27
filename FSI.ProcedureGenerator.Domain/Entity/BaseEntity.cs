using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.ProcedureGenerator.Domain.Entity
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public long CreatedId { get; set; }
        public long UpdateId { get; set; }
        public bool IsActive { get; set; }

    }
}
