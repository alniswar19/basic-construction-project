using BCI.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BCI.Domain.Entities;

public record ConstructionProject : IValidatableObject
{
    public int Id { get; init; }

    [Required]
    [StringLength(200, ErrorMessage = "Project name cannot exceed 200 characters")]
    public string Name { get; init; }

    [Required]
    [StringLength(500, ErrorMessage = "Location name cannot exceed 500 characters")]
    public string Location { get; init; }

    [Required]
    [EnumDataType(typeof(ProjectStage), ErrorMessage = "Invalid Project Stage")]
    public ProjectStage Stage { get; init; }

    [Required]
    public string Category { get; init; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; init; }

    [Required]
    public string Description { get; init; }

    [Required]
    public User Creator { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Stage < ProjectStage.Construction && StartDate < DateTime.UtcNow)
        {
            yield return new ValidationResult("Construction Start Date should be in future date if the stage still Concept, Design, or Pre-\r\nConstruction");
        }
    }
}