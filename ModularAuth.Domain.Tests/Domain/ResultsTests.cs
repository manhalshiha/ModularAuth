using Xunit;
using FluentAssertions;
using ModularAuth.Domain.Results;
using ModularAuth.Domain.Errors;

namespace Platform.Tests.Domain
{
    public class ResultsTests
    {
        [Fact]
        public void Success_ShouldReturnSuccess()
        {
            // Arrange & Act
            var result = Result.Success();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            result.Error.Should().BeNull();
        }

        [Fact]
        public void Failure_ShouldReturnFailureWithError()
        {
            // Arrange
            var error = Error.Validation("VAL001", "Validation failed");

            // Act
            var result = Result.Failure(error);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(error);
        }

        [Fact]
        public void ResultT_Success_ShouldReturnValue()
        {
            var result = Result<int>.Success(42);
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(42);
        }

        [Fact]
        public void ResultT_Failure_ShouldThrow_WhenAccessValue()
        {
            var error = Error.BusinessRule("BR001", "Rule violated");
            var result = Result<int>.Failure(error);

            Action act = () => { var value = result.Value; };
            act.Should().Throw<InvalidOperationException>()
               .WithMessage("Cannot access Value when the result is a failure.");
        }
    }
}