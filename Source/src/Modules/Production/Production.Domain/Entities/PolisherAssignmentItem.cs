using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Production.Domain.Entities
{
    public class PolisherAssignmentItem
    {
        public Guid Id { get; private set; }
        public Guid AssignmentId { get; private set; } // FK
        public PolisherAssignment Assignment { get; private set; } = default!; // nav
        public Guid ProductId { get; private set; }
        public string ProductCode { get; private set; }
        public string ProductName { get; private set; }

        public Guid BagTypeId { get; private set; }
        public string BagTypeName { get; private set; }
        public decimal BagWeight { get; private set; }

        public int Dozens { get; private set; }
        public decimal TotalWeight { get; private set; }
        public decimal NetWeight { get; private set; }
        public decimal AvgWeight { get; private set; }
        public decimal ProductAvgWeight { get; private set; }
        public decimal ToleranceDiff { get; private set; }

        private PolisherAssignmentItem() { }
        public PolisherAssignmentItem(
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
            Id = Guid.NewGuid();
            AssignmentId = assignmentId;
            ProductId = productId;
            ProductCode = productCode;
            ProductName = productName;
            BagTypeId = bagTypeId;
            BagTypeName = bagTypeName;
            BagWeight = bagWeight;
            Dozens = dozens;
            TotalWeight = totalWeight;
            ProductAvgWeight = productAvgWeight;

            CalculateAndValidate(toleranceDiff);
        }
        private void CalculateAndValidate(decimal toleranceDiff)
        {
            NetWeight = TotalWeight - BagWeight;
            AvgWeight = Dozens > 0 ? NetWeight / Dozens : 0;
            ToleranceDiff = Math.Round(AvgWeight - ProductAvgWeight, 3, MidpointRounding.AwayFromZero);

            decimal toleranceLimit = ProductAvgWeight * 0.02m;
            if (toleranceDiff != ToleranceDiff)
            {
                throw new InvalidOperationException(
                    $"Tolerance mismatched. Expected ±{ToleranceDiff:N3}, but found {toleranceDiff:N3}");
            }
            if (Math.Abs(ToleranceDiff) > toleranceLimit)
            {
                throw new InvalidOperationException(
                    $"Tolerance exceeded. Allowed ±{toleranceLimit:N2}, but found {ToleranceDiff:N2}");
            }
        }
    }
}