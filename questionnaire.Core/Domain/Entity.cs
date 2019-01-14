using System;

namespace questionnaire.Core.Domain {
    public abstract class Entity {
        public Guid Id { get; protected set; }

        protected Entity () {
            Id = Guid.NewGuid ();
        }
    }
}