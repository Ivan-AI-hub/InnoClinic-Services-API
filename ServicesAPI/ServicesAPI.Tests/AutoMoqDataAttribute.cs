using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoFixture;

namespace ServicesAPI.Tests
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}
