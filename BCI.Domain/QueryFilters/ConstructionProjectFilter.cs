using BCI.Domain.Enums;
using System.ComponentModel;

namespace BCI.Domain.QueryFilters;

public record ConstructionProjectFilter : BaseFilter
{
    [DefaultValue(null)]
    public ProjectStage? ProjectStage { get; set; }
}
