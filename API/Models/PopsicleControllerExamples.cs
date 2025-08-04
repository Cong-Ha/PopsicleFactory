using Swashbuckle.AspNetCore.Filters;

namespace API.Models;

public class PopsicleCreateExample : IExamplesProvider<PopsicleCreateModel>
{
    public PopsicleCreateModel GetExamples()
    {
        return new PopsicleCreateModel
        {
            Name = "Strawberry Breeze",
            Type = PopsicleType.Fruit,
            Size = PopsicleSize.Regular,
            Description = "A fruity delight made with organic strawberries.",
            IsOrganic = true,
            IsSugarFree = false,
            Price = 2.99m
        };
    }
}

public class PopsicleReplaceExample : IExamplesProvider<PopsicleReplaceModel>
{
    public PopsicleReplaceModel GetExamples()
    {
        return new PopsicleReplaceModel
        {
            Name = "Mango Cream",
            Type = PopsicleType.Cream,
            Size = PopsicleSize.Jumbo,
            Description = "Rich mango flavor with a creamy texture.",
            IsOrganic = false,
            IsSugarFree = true,
            Price = 3.49m
        };
    }
}

public class PopsicleSearchExample : IExamplesProvider<PopsicleSearchModel>
{
    public PopsicleSearchModel GetExamples()
    {
        return new PopsicleSearchModel
        {
            MinPrice = 1m,
            MaxPrice = 3m
        };
    }
}

public class PopsicleUpdateExample : IExamplesProvider<PopsicleUpdateModel>
{
    public PopsicleUpdateModel GetExamples()
    {
        return new PopsicleUpdateModel
        {
            Name = "Strawberry Hurricane",
            Type = PopsicleType.Fruit,
            Size = PopsicleSize.Regular
        };
    }
}