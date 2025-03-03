using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Xunit;

namespace Portfolio.UnitTests.Attributes;

public class InlineAutoMoqDataAttribute : CompositeDataAttribute
{
    public InlineAutoMoqDataAttribute(params object[] values)
        : base(new InlineDataAttribute(values), new AutoMoqDataAttribute()) { }
}

file class AutoMoqDataAttribute()
    : AutoDataAttribute(() => new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true }));
