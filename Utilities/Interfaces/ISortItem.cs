using static Utilities.Enums.EnumTypes;

namespace Utilities.Interfaces
{
    public interface ISortItem<T>
    {
        List<T> DoSort(List<T> entities, SortOrder sortOrder);
    }
}
