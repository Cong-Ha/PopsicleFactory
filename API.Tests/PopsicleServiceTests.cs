using API.Models;
using API.Repository.Interfaces;
using API.Services;
using Moq;

namespace API.Tests;

[TestFixture]
public class PopsicleServiceTests
{
    private Mock<IPopsicleRepository> _mockRepo;
    private PopsicleService _service;
    
    [SetUp]
    public void Setup()
    {
        _mockRepo = new Mock<IPopsicleRepository>();
        _service = new PopsicleService(_mockRepo.Object);
    }

    [Test]
    public async Task GetPopsicleAsync_WithValidId_ReturnsPopsicle()
    {
        //arrange
        int popsicleId = 1;
        var popsicle = new Popsicle
        {
            Id = popsicleId,
            Name = "Strawberry",
            Type = PopsicleType.Fruit,
            Size = PopsicleSize.Regular,
            Description = "Strawberry popsicle",
            IsOrganic = true,
            IsSugarFree = false,
            Price = 2.99m
        };
        _mockRepo.Setup(repo => repo.GetPopsicleAsync(popsicleId))
            .ReturnsAsync(popsicle);
        
        //act
        var result = await _service.GetPopsicleAsync(popsicleId);
        
        //assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(popsicleId));
        Assert.That(result.Name, Is.EqualTo(popsicle.Name));
    }

    [Test]
    public async Task GetPopsicleAsync_WithInvalidId_ThrowsKeyNotFoundException()
    {
        //arrange
        int invalidId = 999;
        _mockRepo.Setup(repo => repo.GetPopsicleAsync(invalidId))
            .ReturnsAsync((Popsicle?)null);

        //act && assert
        var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _service.GetPopsicleAsync(invalidId));
        Assert.That(ex.Message, Does.Contain($"Popsicle with id {invalidId} was not found"));
    }

    [Test]
    public async Task CreatePopsicleAsync_WithValidModel_ReturnsPopsicle()
    {
        //arrange
        var createModel = new PopsicleCreateModel
        {
            Name = "Test popsicle",
            Type = PopsicleType.Fruit,
            Size = PopsicleSize.Regular,
            Description = "Test popsicle",
            IsOrganic = true,
            IsSugarFree = false,
            Price = 2.991m //will be rounded
        };

        var savedEntity = new Popsicle
        {
            Id = 1,
            Name = createModel.Name,
            Type = createModel.Type,
            Size = createModel.Size,
            Description = createModel.Description,
            IsOrganic = createModel.IsOrganic,
            IsSugarFree = createModel.IsSugarFree,
            Price = 2.99m //rounded value
        };
        _mockRepo.Setup((repo) => repo.CreatePopsicleAsync(createModel)).ReturnsAsync(savedEntity);
        
        //act
        var result = await _service.CreatePopsicleAsync(createModel);
        
        //assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("Test popsicle"));
        Assert.That(result.Price, Is.EqualTo(2.99m));
    }

    [Test]
    public async Task UpdatePopsicleAsync_WithNullModel_ThrowsNullArgumentException()
    {
        //arrange
        PopsicleCreateModel? model = null;
        
        //act
        var ex = Assert.ThrowsAsync<ArgumentNullException>(() => _service.CreatePopsicleAsync(model));
        
        //assert
        Assert.That(ex.ParamName, Is.EqualTo("model"));

    }

    [Test]
    public async Task ReplacePopsicle_WithValidId_UpdatesAndReturnsViewModel()
    {
        //arrange
        var id = 1;
        var model = new PopsicleReplaceModel
        {
            Name = "Updated Name",
            Type = PopsicleType.Fruit,
            Size = PopsicleSize.Regular,
            Description = "Updated description",
            IsOrganic = true,
            IsSugarFree = false,
            Price = 2.99m
        };

        var existingEntity = new Popsicle
        {
            Id = id,
            Name = "Old Name",
            Type = PopsicleType.Cream,
            Size = PopsicleSize.Jumbo,
            Description = "Old Description",
            IsOrganic = false,
            IsSugarFree = true,
            Price = 10.99m
        };

        var updatedEntity = new Popsicle
        {
            Id = id,
            Name = model.Name,
            Type = model.Type,
            Size = model.Size,
            Description = model.Description,
            IsOrganic = model.IsOrganic,
            IsSugarFree = model.IsSugarFree,
            Price = model.Price
        };
        
        _mockRepo.Setup(r => r.GetPopsicleAsync(id)).ReturnsAsync(existingEntity);
        _mockRepo.Setup(r => r.ReplacePopsicleAsync(model, existingEntity)).ReturnsAsync(updatedEntity);

        //act
        var result = await _service.ReplacePopsicle(id, model);

        //assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<PopsicleViewModel>());
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.Name, Is.EqualTo(model.Name));
            Assert.That(result.Type, Is.EqualTo(model.Type));
            Assert.That(result.Size, Is.EqualTo(model.Size));
            Assert.That(result.Description, Is.EqualTo(model.Description));
            Assert.That(result.IsOrganic, Is.EqualTo(model.IsOrganic));
            Assert.That(result.IsSugarFree, Is.EqualTo(model.IsSugarFree));
            Assert.That(result.Price, Is.EqualTo(model.Price));
        });
    }
    
    [Test]
    public async Task ReplacePopsicle_InvalidId_ThrowsKeyNotFoundException()
    {
        // Arrange
        var id = 999;
        var model = new PopsicleReplaceModel();
        
        _mockRepo.Setup(r => r.GetPopsicleAsync(id)).ReturnsAsync((Popsicle?)null);

        // Act & Assert
        var ex = Assert.ThrowsAsync<KeyNotFoundException>(() => _service.ReplacePopsicle(id, model));
        Assert.That(ex!.Message, Does.Contain($"id {id}"));
    }

    [Test]
    public async Task DeletePopsicle_WithValidId_CallsRepositoryDelete()
    {
        //arrange
        int popsicleId = 1;
        var popsicle = new Popsicle
        {
            Id = popsicleId,
            Name = "Test popsicle",
            Type = PopsicleType.Cream,
            Size = PopsicleSize.Regular,
            Description = "Test for Delete",
            IsOrganic = true,
            IsSugarFree = false,
            Price = 2.99m
        };
        _mockRepo.Setup(r => r.GetPopsicleAsync(popsicleId)).ReturnsAsync(popsicle);
        _mockRepo.Setup(r => r.DeletePopsicleAsync(popsicle)).Returns(Task.CompletedTask);
        
        //act
        await _service.DeletePopsicleAsync(popsicleId);

        //assert
        _mockRepo.Verify(r => r.GetPopsicleAsync(popsicleId));
        _mockRepo.Verify(r => r.DeletePopsicleAsync(popsicle), Times.Once);
    }

    [Test]
    public async Task DeletePopsicle_WithInvalidId_ThrowsKeyNotFoundException()
    {
        //arrange
        int popsicleId = 999;
        _mockRepo.Setup(r => r.GetPopsicleAsync(popsicleId)).ReturnsAsync((Popsicle?)null);
        
        //act and assert
        var ex = Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeletePopsicleAsync(popsicleId));
        Assert.That(ex!.Message, Does.Contain($"id {popsicleId}"));
        
    }

    [Test]
    public async Task UpdatePopsicle_WithValidId_UpdatesAndReturnsViewModel()
    {
        //arrange
        int popsicleId = 1;
        var requestBodyModel = new PopsicleUpdateModel
        {
            Name = "Updated Name",
            Type = PopsicleType.Cream,
            Description = "Updated Description",
            Price = 10.99m
        };

        var entity = new Popsicle
        {
            Id = popsicleId,
            Name = "Old Name",
            Type = PopsicleType.Fruit,
            Size = PopsicleSize.Regular,
            Description = "Old Description",
            IsOrganic = false,
            IsSugarFree = false,
            Price = 2.99m
        };

        var patchedEntity = new Popsicle
        {
            Id = popsicleId,
            Name = requestBodyModel.Name,
            Type = (PopsicleType)requestBodyModel.Type,
            Size = entity.Size,
            Description = requestBodyModel.Description,
            IsOrganic = entity.IsOrganic,
            IsSugarFree = entity.IsSugarFree,
            Price = (decimal)requestBodyModel.Price,
            
        };
        _mockRepo.Setup(r => r.GetPopsicleAsync(popsicleId)).ReturnsAsync(entity);
        _mockRepo.Setup(r => r.UpdatePopsicleAsync(requestBodyModel, entity)).ReturnsAsync(patchedEntity);

        //act
        var result = await _service.UpdatePopsicleAsync(popsicleId, requestBodyModel);

        //assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<PopsicleViewModel>());
            
            //updated values
            Assert.That(result.Name, Is.EqualTo("Updated Name"));
            Assert.That(result.Type, Is.EqualTo(PopsicleType.Cream));
            Assert.That(result.Description, Is.EqualTo("Updated Description"));
            Assert.That(result.Price, Is.EqualTo(10.99m));
            
            //values that should not have been updated
            Assert.That(result.IsOrganic, Is.EqualTo(entity.IsOrganic));
            Assert.That(result.IsSugarFree, Is.EqualTo(entity.IsSugarFree));
            Assert.That(result.Size, Is.EqualTo(entity.Size));
        });
    }

    [Test]
    public async Task UpdatePopsicle_WithInvalidId_ThrowsKeyNotFoundException()
    {
        // Arrange
        var id = 999;
        var model = new PopsicleUpdateModel();
        
        _mockRepo.Setup(r => r.GetPopsicleAsync(id)).ReturnsAsync((Popsicle?)null);

        // Act & Assert
        var ex = Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdatePopsicleAsync(id, model));
        Assert.That(ex!.Message, Does.Contain($"id {id}"));
    }

    [Test]
    public async Task SearchPopsicle_WithQueryModel_ReturnsViewModelList()
    {
        //arrange
        var query = new PopsicleSearchModel
        {
            Name = null,
            Type = null,
            Size = null,
            IsOrganic = true,
            IsSugarFree = null,
            MinPrice = 1.00m,
            MaxPrice = 5.00m
        };

        var popsicleData = new List<Popsicle>
        {
            new Popsicle
            {
                Name = "Berry Blast",
                Type = PopsicleType.Fruit,
                Size = PopsicleSize.Regular,
                IsOrganic = false,
                IsSugarFree = false,
                Price = 8.99m
            },
            new Popsicle
            {
                Name = "Super Vanilla",
                Type = PopsicleType.Cream,
                Size = PopsicleSize.Jumbo,
                IsOrganic = true,
                IsSugarFree = false,
                Price = 3.99m
            },
            new Popsicle
            {
                Name = "Organic Orange Juice",
                Type = PopsicleType.Juice,
                Size = PopsicleSize.Regular,
                IsOrganic = true,
                IsSugarFree = false,
                Price = 4.99m
                
            }
        };
        
        _mockRepo.Setup(r => r.GetPopsiclesAsync(query)).ReturnsAsync(popsicleData.Where(p =>
                        p.IsOrganic == query.IsOrganic &&
                        p.Price >= query.MinPrice &&
                        p.Price <= query.MaxPrice));
        
        //act
        var result = await _service.SearchPopsicleAsync(query);

        //assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<PopsicleViewModel>>());
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result.All(p => p.Price is >= 1.00m and <= 5.00m));
        Assert.That(result.Any(p => p.Name == "Super Vanilla"));
        Assert.That(result.Any(p => p.Name == "Organic Orange Juice"));
    }

    [Test]
    public async Task SearchPopsicle_WithEmptyRequest_ThrowsArgumentException()
    {
        //arrange
        var query = new PopsicleSearchModel
        {
            Name = null,
            Type = null,
            Size = null,
            IsOrganic = null,
            IsSugarFree = null,
            MinPrice = null,
            MaxPrice = null
        };
        
        //act
        var ex = Assert.ThrowsAsync<ArgumentException>(() => _service.SearchPopsicleAsync(query));
        
        //assert
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex!.Message, Does.Contain($"At least one property for popsicle must be provided for search."));
    }
}