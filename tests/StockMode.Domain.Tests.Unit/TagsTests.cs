using FluentAssertions;
using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Tags;

namespace StockMode.Domain.Tests.Unit
{
    public class TagsTests
    {
        [Theory]
        [MemberData(nameof(ValidTagData))]
        public void CreateTag_ShouldCreateTag_WhenParametersAreValid(PlainTag tag)
        {
            // Act
            var createdTag = new Tag(tag.Name, tag.Color);
            Action act = () => new Tag(tag.Name, tag.Color);

            // Assert
            act.Should().NotThrow<Exception>();
            createdTag.Should().NotBeNull();
            createdTag.Should().Be(createdTag.Name);
            createdTag.Should().Be(createdTag.Color);
        }

        [Theory]
        [MemberData(nameof(InvalidTagData))]
        public void CreateTag_ShouldThrowDomainException_WhenParametersAreInvalid(PlainTag tag, string expectedMessage)
        {
            // Act
            Action act = () => new Tag(tag.Name, tag.Color);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage(expectedMessage);
        }

        [Theory]
        [MemberData(nameof(ValidTagData))]
        public void Updatetag_ShouldUdpateTagDetails_WhenParametersAreValid(PlainTag tag)
        {
            // Arrange
            var existingTag = new Tag("OldName", "#000000");

            // Act
            Action act = () => existingTag.UpdateDetails(tag.Name, tag.Color);

            // Assert
            act.Should().NotThrow<Exception>();
            existingTag.Should().Be(existingTag.Name);
            existingTag.Should().Be(existingTag.Color);
        }

        [Theory]
        [MemberData(nameof(InvalidTagData))]
        public void UpdateTag_ShouldThrowDomainException_WhenParametersAreInvalid(PlainTag tag, string expectedMessage)
        {
            // Arrange
            var existingTag = new Tag("OldName", "#000000");
            // Act
            Action act = () => existingTag.UpdateDetails(tag.Name, tag.Color);
            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage(expectedMessage);
        }

        public static IEnumerable<object[]> ValidTagData =>
            new List<object[]>
            {
                new object[] { new PlainTag("Important", null) },
                new object[] { new PlainTag(null, "#FF5733") },
                new object[] { new PlainTag("Urgent", "#FF0000") },
                new object[] { new PlainTag("  Note  ", "  #00FF00  ") }
            };
        public static IEnumerable<object[]> InvalidTagData =>
            new List<object[]>
            {
                new object[] { new PlainTag(null, null), "Tag must have at least a name or a color."},
                new object[] { new PlainTag("", null ), "Tag must have at least a name or a color."},
                new object[] {new PlainTag( null, ""), "Tag must have at least a name or a color." },
                new object[] { new PlainTag("   ", "   " ), "Tag must have at least a name or a color."}
            };
    }

    public class PlainTag
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public PlainTag(string? name, string? color)
        {
            Name = name ?? "";
            Color = color ?? "";
        }

    }
}
