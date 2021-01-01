using ProcessOrder.Infrastructure.DataContext;
using ProcessOrder.Infrastructure.DataContext.TestConfiguration;
using System.Collections.Generic;

namespace ProcessOrder.Tests
{
    internal static class TestContextHelper
    {
        public static ProcessOrderDbContext GetContext()
        {
            var processOrderContextFactory = new ProcessOrderContextFactoryForTest();
            var writableContext = processOrderContextFactory.CreateWriteableContext();
            return writableContext;
        }

        public static ProcessOrderDbContext GetCleanContext()
        {
            CleanDatabase();
            var processOrderContextFactory = new ProcessOrderContextFactoryForTest();
            var writableContext = processOrderContextFactory.CreateWriteableContext();
            return writableContext;
        }

        public static void CleanDatabase()
        {
            var processOrderContextFactory = new ProcessOrderContextFactoryForTest();
            var writableContext = processOrderContextFactory.CreateWriteableContext();

            writableContext.ResetValueGenerators();
            writableContext.Database.EnsureDeleted();
        }

        public static void AddEntity<T>(T entity) where T : class
        {
            var context = GetContext();
            context.Set<T>().AddRange(entity);
            context.SaveChanges();
        }

        public static void AddEntity<T>(ProcessOrderDbContext context, T entity) where T : class
        {
            context.Set<T>().AddRange(entity);
            context.SaveChanges();
        }

        public static void AddEntities<T>(List<T> entities) where T : class
        {
            var context = GetContext();
            context.Set<T>().AddRange(entities);
            context.SaveChanges();
        }

        public static void AddEntities<T>(ProcessOrderDbContext context, List<T> entities) where T : class
        {
            context.Set<T>().AddRange(entities);
            context.SaveChanges();
        }
    }
}

