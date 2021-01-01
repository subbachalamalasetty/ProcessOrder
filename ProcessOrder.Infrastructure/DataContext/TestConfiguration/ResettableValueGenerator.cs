using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Threading;

namespace ProcessOrder.Infrastructure.DataContext.TestConfiguration
{
    public class ResettableValueGenerator : ValueGenerator<int>
    {
        private int _current;

        public override bool GeneratesTemporaryValues => false;

        public override int Next(EntityEntry entry)
            => Interlocked.Increment(ref _current);

        public void Reset() => _current = 0;
    }
}