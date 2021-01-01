using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Linq;

namespace ProcessOrder.Infrastructure.DataContext.TestConfiguration
{
    public static class DbContextExtensions
    {
        public static void ResetValueGenerators(this Microsoft.EntityFrameworkCore.DbContext context)
        {
            var cache = context.GetService<IValueGeneratorCache>();

            foreach (var keyProperty in context.Model.GetEntityTypes()
                .Where(w => !w.IsQueryType)
                .Select(e => e.FindPrimaryKey().Properties[0])
                .Where(p => p.ClrType == typeof(int)
                            && p.ValueGenerated == ValueGenerated.OnAdd))
            {
                var generator = (ResettableValueGenerator)cache.GetOrAdd(
                    keyProperty,
                    keyProperty.DeclaringEntityType,
                    (p, e) => new ResettableValueGenerator());

                generator.Reset();
            }
        }

        public static void SetAllValueGeneratorsAsResettable(this Microsoft.EntityFrameworkCore.DbContext context)
        {
            var cache = context.GetService<IValueGeneratorCache>();

            foreach (var keyProperty in context.Model.GetEntityTypes()
                .Where(w => !w.IsQueryType)
                .Select(e => e.FindPrimaryKey().Properties[0])
                .Where(p => p != null && p.ClrType == typeof(int)
                                      && p.ValueGenerated == ValueGenerated.OnAdd))
            {
                cache.GetOrAdd(
                    keyProperty,
                    keyProperty.DeclaringEntityType,
                    (p, e) => new ResettableValueGenerator());
            }
        }
    }
}
