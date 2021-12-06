using System;

namespace Lab3.Warehouse.Core {
    public abstract class Entity {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
