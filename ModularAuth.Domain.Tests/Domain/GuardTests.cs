using Xunit;
using ModularAuth.Domain.Guards;
using ModularAuth.Domain.Results;

namespace Platform.Tests.Domain
{
    public class GuardTests
    {
        [Fact]
        public void EnsureNotNullOrEmpty_ShouldFail_WhenNull()
        {
            var result = Guard.EnsureNotNullOrEmpty(null, "VAL001", "Value cannot be null");
            Assert.True(result.IsFailure);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void EnsureNotNullOrEmpty_ShouldFail_WhenEmpty()
        {
            var result = Guard.EnsureNotNullOrEmpty("", "VAL002", "Value cannot be empty");
            Assert.True(result.IsFailure);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void EnsureNotNullOrEmpty_ShouldSucceed_WhenValid()
        {
            var result = Guard.EnsureNotNullOrEmpty("Valid", "VAL003", "Valid input");
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
        }
    }
}