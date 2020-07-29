using System;
using System.Collections.Generic;
using System.Text;

namespace Oliver.Domain {
    public abstract class Entity {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
