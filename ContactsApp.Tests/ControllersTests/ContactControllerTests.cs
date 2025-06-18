using ContactsApp.Api.Controllers;
using ContactsApp.Api.DTOs;
using ContactsApp.Api.Models;
using ContactsApp.Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ContactsApp.Tests.ControllersTests;

public class ContactControllerTests
{
    private Mock<IContactService> _mockService;

    private ContactsController _controller;

    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<IContactService>();
        _controller = new ContactsController(_mockService.Object);
    }

    [Test]
    public async Task GetAllAsync_ShouldReturnAllContacts()
    {
       // Arrange
       var expected = new List<Contact>
       {
           new() { Id = 1, FullName = "Betty James", Email = "BettyJames@gmail.com", Phone = "0843975834" },
           new(){Id = 2, FullName = "Edi Jack", Email = "Edi@gmail.com", Phone = "08284975834"}
       };

       _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(expected);
       
       // Act
       var result = await _controller.GetAllAsync();
       
       // Assert
       var OkResult = result.Result as OkObjectResult;
       OkResult.Should().NotBeNull();
       OkResult.Value.Should().BeEquivalentTo((expected));
    }

    [Test]
    public async Task GetContactByIdAsync_ShouldReturnContact_WhenExists()
    {
        // Arrange
        var contact = new Contact
        {
            Id = 1, FullName = "Alice Ineza", Email = "aliceineza@gmail.com", Phone = "0789437548"
        };
        _mockService.Setup(s => s.GetContactByIdAsync(1)).ReturnsAsync(contact);
        
        // Act
        var result = await _controller.GetContactByIdAsync(1);
        
        // Assert
        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.Value.Should().BeEquivalentTo(contact);
    }

    [Test]
    public async Task GetContactByIdAsync_ShouldReturnNotFound_WhenContactDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.GetContactByIdAsync(4)).ReturnsAsync((Contact?)null);
        
        // Act
        var result = await _controller.GetContactByIdAsync(4);
        
        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Test]
    public async Task CreateContactAsync_ShouldReturnSuccess_WhenValid()
    {
        //Arrange
        _controller.ModelState.AddModelError("Email", "Required");
        var dto = new CreateContactDto { FullName = "Jossy" };
        
        //Act
        var result = await _controller.CreateContactAsync(dto);
        
        //Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>();

    }

    [Test]
    public async Task UpdateContactAsync_ShouldReturnSuccess_WhenValid()
    {
        // Arrange
        var dto = new UpdateContactDto { FullName = "Jack Jill", Phone = "098345673", Email = "jack@gmail.com" };
        
        // Arrange
        var result = await _controller.UpdateContactAsync(1, dto);
        
        // Assert
        var okResult = result.Result as OkObjectResult;
        okResult!.Value.Should().Be("Contact updated successfully");
        _mockService.Verify(s=> s.UpdateContactAsync(1, dto), Times.Once);
        
    }
    [Test]
    public async Task UpdateContactAsync_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("FullName", "Required");
        var dto = new UpdateContactDto { FullName = "" };

        // Act
        var result = await _controller.UpdateContactAsync(1, dto);

        // Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Test]
    public async Task DeleteAsync_ShouldReturnSuccess()
    {
        // Act
        var result = await _controller.DeleteAsync(1);
        
        //Assert
        var okResult = result.Result as OkObjectResult;
        okResult!.Value.Should().Be("Contact deleted successfully");
        _mockService.Verify(s=> s.DeleteAsync(1), Times.Once);
    }
   
    

}