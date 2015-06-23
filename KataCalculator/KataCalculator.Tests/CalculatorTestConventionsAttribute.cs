using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit;

namespace KataCalculator.Tests
{
    public class CalculatorTestConventionsAttribute: AutoDataAttribute
    {
        public CalculatorTestConventionsAttribute()
            : base(new Fixture().Customize(new AutoConfiguredMoqCustomization()))
        { 
        }
    }
}
