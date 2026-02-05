using AutoMapper;
using Backend.Controllers;
using Backend.DataAccess.Interfaces;
using Backend.DTOs;
using Backend.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class PersonasControllerTests
    {
        private Mock<IPersonaRepository>? _mockRepository;
        private Mock<IMapper>? _mockMapper;
        private Mock<IValidator<Persona>>? _mockValidator;
        private Mock<ILogger<PersonasController>>? _mockLogger;
        private PersonasController? _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IPersonaRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockValidator = new Mock<IValidator<Persona>>();
            _mockLogger = new Mock<ILogger<PersonasController>>();
            _controller = new PersonasController(_mockRepository.Object, _mockMapper.Object, _mockValidator.Object, _mockLogger.Object);
        }

        /// <summary>
        /// Verificar que el método Get devuelva status 200 (OK) y una lista de personas.
        /// </summary>
        [TestMethod]
        public async Task Get_ShouldReturnOk_WithListOfPersonas()
        {
            // Arrange
            var personas = new List<Persona> { new Persona { Nombre = "Juan" } };
            var personasDto = new List<PersonaDTO> { new PersonaDTO { Nombre = "Juan" } };
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(personas);
            _mockMapper.Setup(m => m.Map<IEnumerable<PersonaDTO>>(personas)).Returns(personasDto);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(personasDto, okResult.Value);
        }

        /// <summary>
        /// Verificar que el método Post devuelva status 200 (OK).
        /// </summary>
        [TestMethod]
        public async Task Post_ShouldReturnOk_WhenDataIsValid()
        {
            // Arrange
            var personaDto = new PersonaDTO { Nombre = "Juan", DNI = "12345678" };
            var persona = new Persona { Nombre = "Juan", DNI = "12345678" };
            
            _mockMapper.Setup(m => m.Map<Persona>(personaDto)).Returns(persona);
            _mockValidator.Setup(v => v.ValidateAsync(persona, default)).ReturnsAsync(new ValidationResult());
            _mockRepository.Setup(r => r.AddAsync(persona)).ReturnsAsync(1);

            // Act
            var result = await _controller.Post(personaDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        /// <summary>
        /// Verificar que el método Post devuelva status 400 (BAD REQUEST).
        /// </summary>
        [TestMethod]
        public async Task Post_ShouldReturnBadRequest_WhenValidationFails()
        {
            // Arrange
            var personaDto = new PersonaDTO { Nombre = "" };
            var persona = new Persona { Nombre = "" };
            var failures = new List<ValidationFailure> { new ValidationFailure("Nombre", "Requerido") };
            
            _mockMapper.Setup(m => m.Map<Persona>(personaDto)).Returns(persona);
            _mockValidator.Setup(v => v.ValidateAsync(persona, default)).ReturnsAsync(new ValidationResult(failures));

            // Act
            var result = await _controller.Post(personaDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
    }
}
