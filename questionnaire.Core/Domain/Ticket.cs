using System;

namespace questionnaire.Core.Domain {
    public class Ticket : Entity {
        public Guid EventId { get; protected set; }
        public int Seating { get; protected set; }
        public decimal Price { get; protected set; }
        public Guid? UserId { get; protected set; }
        public string UserName { get; protected set; }
        public DateTime? PurchasedAt { get; protected set; }
        public bool Purchased => PurchasedAt.HasValue;

        protected Ticket () {

        }
        public Ticket (Event @event, int seating, decimal price) {
            EventId = @event.Id;
            Seating = seating;
            Price = price;
        }
    }
}