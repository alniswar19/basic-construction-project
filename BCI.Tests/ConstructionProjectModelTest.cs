using BCI.Domain.Commons;
using BCI.Domain.Entities;
using BCI.Domain.Enums;
using Pose;
using Faker;
using Moq;


namespace BCI.Tests;

public class ConstructionProjectModelTest
{
    [Theory]
    [InlineData("Test Project Name", "Test category", "Test Description", "Test Location", 0, "2024-10-10 12:12:12", false)]
    public void ConstructionProejctModelValidation(string name,
        string category,
        string description,
        string location,
        int stage,
        DateTime StartDate,
        bool expectedValue)
    {
        var model = new ConstructionProject
        {
            Id = RandomNumber.Next(1, 999999),
            Name = name,
            Category = category,
            Creator = new User()
            {
                Id = Guid.NewGuid(),
            },
            Description = description,
            Location = location,
            Stage = (ProjectStage)stage,
            StartDate = StartDate
        };

        var validationResult = ValidationHelper.ValidateModel(model);

        Assert.Equal(expectedValue, validationResult.Any());
    }

    [Theory]
    [InlineData("2024-06-24", "2024-06-23", ProjectStage.Concept, true)]
    [InlineData("2024-06-24", "2024-06-23", ProjectStage.DesignAndImplementation, true)]
    [InlineData("2024-06-24", "2024-06-23", ProjectStage.PreConstruction, true)]
    [InlineData("2024-06-24", "2024-06-23", ProjectStage.Construction, false)]

    public void ConstructionStartDateShouldBeInTheFutureIfStageNotInConstruction(DateTime now, DateTime startDate, ProjectStage stage, bool expectedValue)
    {
        Shim dateTimeShim = Shim.Replace(() => DateTime.UtcNow).With(() => now);

        var model = new ConstructionProject
        {
            Id = RandomNumber.Next(1, 999999),
            Name = Name.FullName(),
            Category = Lorem.Sentence(10),
            Creator = new User()
            {
                Id = Guid.NewGuid(),
            },
            Description = Lorem.Paragraph(200),
            Location = Lorem.Sentence(20),
            Stage = stage,
            StartDate = startDate
        };


        var validationResult = ValidationHelper.ValidateModel(model);

        Assert.Equal(expectedValue, validationResult.Any());
    }

    [Theory]
    [InlineData("Test Project Name", "Test category", "Test Description", "Test Location", 0, "2024-10-10 12:12:12", false)]
    [InlineData("Test Project Name", "Test category", "Test Description", "Test Location", 0, null, true)]
    [InlineData("Test Project Name", "Test category", "Test Description", "Test Location", 9, "2024-10-10 12:12:12", true)]
    [InlineData("Test Project Name", "Test category", "Test Description", null, 0, "2024-10-10 12:12:12", true)]
    [InlineData("Test Project Name", "Test category", null, "Test Location", 0, "2024-10-10 12:12:12", true)]
    [InlineData("Test Project Name", null, "Test Description", "Test Location", 0, "2024-10-10 12:12:12", true)]
    [InlineData(null, "Test category", "Test Description", "Test Location", 0, "2024-10-10 12:12:12", true)]

    public void ConstructionProjectModelAllRequired(string name,
        string category,
        string description,
        string location,
        int stage,
        DateTime StartDate,
        bool expectedValue)
    {
        var model = new ConstructionProject
        {
            Id = RandomNumber.Next(1, 999999),
            Name = name,
            Category = category,
            Creator = new User()
            {
                Id = Guid.NewGuid(),
            },
            Description = description,
            Location = location,
            Stage = (ProjectStage)stage,
            StartDate = StartDate
        };

        var validationResult = ValidationHelper.ValidateModel(model);

        Assert.Equal(expectedValue, validationResult.Any());
    }
}
