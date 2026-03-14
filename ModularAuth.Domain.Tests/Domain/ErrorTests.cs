using Xunit;
using ModularAuth.Domain.Errors;

namespace Platform.Tests.Domain
{
    public class ErrorTests
    {
        [Fact]
        public void CreateValidationError_ShouldSetCorrectTypeAndCode()
        {
            var error = Error.Validation("VAL001", "Validation error");

            Assert.Equal(ErrorType.Validation, error.Type);
            Assert.Equal("VAL001", error.Code);
            Assert.Equal("Validation error", error.Description);
        }

        [Fact]
        public void CreateBusinessRuleError_ShouldSetCorrectType()
        {
            var error = Error.BusinessRule("BR001", "Business rule violated");

            Assert.Equal(ErrorType.BusinessRule, error.Type);
            Assert.Equal("BR001", error.Code);
            Assert.Equal("Business rule violated", error.Description);
        }
    }
}