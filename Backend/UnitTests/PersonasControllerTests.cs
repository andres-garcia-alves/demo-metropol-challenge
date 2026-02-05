using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Backend.Controllers;
using Backend.BusinessLogic.Interfaces;
using Backend.DTOs;

namespace UnitTests
{
    [TestClass]
    public class PersonasControllerTests
    {
        private Mock<IPersonaLogic>? _mockPersonaLogic;
        private Mock<ILogger<PersonasController>>? _mockLogger;
        private PersonasController? _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockPersonaLogic = new Mock<IPersonaLogic>();
            _mockLogger = new Mock<ILogger<PersonasController>>();
            _controller = new PersonasController(_mockPersonaLogic.Object, _mockLogger.Object);
        }

        /// <summary>
        /// Verificar que el método GetAll devuelva status 200 (OK) y una lista de personas.
        /// </summary>
        [TestMethod]
        public async Task GetAll_ShouldReturnOk_WithListOfPersonas()
        {
            // Arrange
            var personasDto = new List<PersonaDTO> { new PersonaDTO { Nombre = "Juan" } };
            _mockPersonaLogic!.Setup(l => l.GetAll()).ReturnsAsync(personasDto);

            // Act
            var result = await _controller!.GetAll();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(personasDto, okResult!.Value);
        }

        /// <summary>
        /// Verificar que el método Post devuelva status 200 (OK).
        /// </summary>
        [TestMethod]
        public async Task Post_ShouldReturnOk_WhenDataIsValid()
        {
            // Arrange
            var personaDto = new PersonaDTO { Nombre = "Juan", DNI = "12345678" };
            _mockPersonaLogic!.Setup(l => l.Create(personaDto)).ReturnsAsync(1);

            // Act
            var result = await _controller!.Post(personaDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        /// <summary>
        /// Verificar que el método Post devuelva status 400 (BAD REQUEST) cuando hay un error de validación.
        /// </summary>
        [TestMethod]
        public async Task Post_ShouldReturnBadRequest_WhenValidationFails()
        {
            // Arrange
            var personaDto = new PersonaDTO { Nombre = "" };
            var failures = new List<ValidationFailure> { new ValidationFailure("Nombre", "Requerido") };
            _mockPersonaLogic!.Setup(l => l.Create(personaDto)).ThrowsAsync(new ValidationException(failures));

            // Act
            var result = await _controller!.Post(personaDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
    }
}
