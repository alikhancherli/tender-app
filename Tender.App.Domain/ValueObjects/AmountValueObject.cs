using Tender.App.Domain.Exceptions;
using Tender.App.Domain.Shared;

namespace Tender.App.Domain.ValueObjects
{
    public class AmountValueObject : BaseValueObject
    {
        public long Value { get; private set; }

        public AmountValueObject(long value)
        {
            if(value < 0) throw new ValueOutOfRangeException(nameof(value));
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
