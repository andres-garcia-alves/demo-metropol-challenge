using Microsoft.VisualStudio.TestTools.UnitTesting;
using Backend.BusinessLogic.Validators;
using Backend.Entities;

namespace UnitTests
{
    [TestClass]
    public class PersonaValidatorTests
    {
        public required PersonaValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _validator = new PersonaValidator();
        }

        [TestMethod]
        public void Validator_ShouldHaveError_WhenNombreIsEmpty()
        {
            var persona = new Persona { Nombre = "", Apellido = "Pérez", DNI = "12345678", Sexo = "M", Mail = "test@test.com" };
            var result = _validator.Validate(persona);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Any(e => e.PropertyName == "Nombre"));
        }

        [TestMethod]
        public void Validator_ShouldHaveError_WhenDNIIsNotNumeric()
        {
            var persona = new Persona { Nombre = "Juan", Apellido = "Pérez", DNI = "12345ABC", Sexo = "M", Mail = "test@test.com" };
            var result = _validator.Validate(persona);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Any(e => e.PropertyName == "DNI"));
        }

        [TestMethod]
        public void Validator_ShouldHaveError_WhenMailIsInvalid()
        {
            var persona = new Persona { Nombre = "Juan", Apellido = "Pérez", DNI = "12345678", Sexo = "M", Mail = "formato-invalido" };
            var result = _validator.Validate(persona);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Any(e => e.PropertyName == "Mail"));
        }

        [TestMethod]
        public void Validator_ShouldBeValid_WhenAllDataIsCorrect()
        {
            var persona = new Persona { Nombre = "Juan", Apellido = "Pérez", DNI = "12345678", Sexo = "Masculino", Mail = "juan.perez@ejemplo.com" };
            var result = _validator.Validate(persona);
            Assert.IsTrue(result.IsValid);
        }
    }
}
