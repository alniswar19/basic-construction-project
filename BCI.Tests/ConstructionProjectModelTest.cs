using BCI.Domain.Commons;
using BCI.Domain.Entities;
using System.ComponentModel;

namespace BCI.Tests;

public class ConstructionProjectModelTest
{
    [Theory]
    [InlineData("Test Project Name", "Test category", "Test Description", "Test Location", ProjectStage.Concept, "2024-10-10 12:12:12", true)]
    public void ConstructionProejctModelValidation(string name,
        string category,
        string description,
        string location,
        ProjectStage stage,
        DateTime StartDate,
        bool expectedValue)
    {
        var model = new ConstructionProject {
            Name = "name",
            Category = "category",
            CreatorId = 1,
            Description = "description",
            Location = "location",
            Stage = ProjectStage.PreConstruction,
            StartDate = DateTime.Now
        };

        var validationResult = ValidationHelper.ValidateModel(model);

        Assert.Equal(expectedValue, validationResult.Any());
    }
}
