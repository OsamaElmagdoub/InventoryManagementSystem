using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace InventoryManagementSystem.Helpers
{
    public static class MapperHelper
    {
        public static IMapper Mapper { get; set; }
        public static TResult MapOne<TResult>(this object source)
        {
            return Mapper.Map<TResult>(source);
        }
        public static IQueryable<TResult> Map<TResult>(this IQueryable<object> source)
        {
            return source.ProjectTo<TResult>(Mapper.ConfigurationProvider);
        }
    }
}
