using ServiceStack.OrmLite;
using System.Data;

namespace ServicesAPI.Tests
{
    public static class GenericTableExtensions
    {
        static object ExecWithAlias<T>(string table, Func<object> fn)
        {
            var modelDef = typeof(T).GetModelMetadata();
            lock (modelDef)
            {
                var hold = modelDef.Alias;
                try
                {
                    modelDef.Alias = table;
                    return fn();
                }
                finally
                {
                    modelDef.Alias = hold;
                }
            }
        }

        public static void CreateTableIfNotExists<T>(this IDbConnection db, string table)
        {
            ExecWithAlias<T>(table, () => { db.CreateTableIfNotExists<T>(); return null; });
        }
    }
}
