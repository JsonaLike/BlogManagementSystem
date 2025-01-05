namespace MT.Innovation.Shared.Infrastructure;

/// <summary>
/// Represent the base criteria parameters that are used in Searching methods.
/// </summary>
[Serializable]
public class SearchCriteriaBase 
{
    /// <summary>
    /// The size of page that is intended to be retrieved.
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// The number of page to be retrieved.
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// The property name to sort with
    /// </summary>
    public string SortProperty { get; set; } = "Id";

    /// <summary>
    /// The sort direction (Ascending, Descending)
    /// </summary>


}


