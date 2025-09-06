namespace Production.Domain.Entities
{
    public class PolisherAssignment
    {
        public Guid Id { get; private set; }
        public Guid PolisherId { get; private set; }
        public string PolisherName { get; private set; } // snapshot
        public DateTime CreatedDate { get; private set; }
        public string CreatedBy { get; private set; }

        private readonly List<PolisherAssignmentItem> _items = new();
        public IReadOnlyCollection<PolisherAssignmentItem> Items => _items.AsReadOnly();

        private PolisherAssignment() { }

        public PolisherAssignment(Guid polisherId, string polisherName, string createdBy)
        {
            Id = Guid.NewGuid();
            PolisherId = polisherId;
            PolisherName = polisherName;
            CreatedBy = createdBy;
            CreatedDate = DateTime.UtcNow;
        }

        public void AddItem(
            Guid productId,
            Guid assignmentId,
            string productCode,
            string productName,
            Guid bagTypeId,
            string bagTypeName,
            decimal bagWeight,
            int dozens,
            decimal totalWeight,
            decimal productAvgWeight,
            decimal toleranceDiff)
        {
            var item = new PolisherAssignmentItem(
                productId,
                assignmentId,
                productCode,
                productName,
                bagTypeId,
                bagTypeName,
                bagWeight,
                dozens,
                totalWeight,
                productAvgWeight,
                toleranceDiff);

            _items.Add(item);
        }
    }
}

