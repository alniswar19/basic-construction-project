using BCI.Domain.Constants;
using BCI.Domain.Enums;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace BCI.Domain.QueryFilters;

public record BaseFilter
{
    [DefaultValue(QueryConstant.PAGE_SIZE)]
    public virtual int? PageSize { get; init; }

    [DefaultValue(1)]
    public int? PageNumber { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SortOrder SortOrder { get; init; } = SortOrder.Descending;

    public string? OrderBy { get; init; } = "createTs";
}
