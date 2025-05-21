using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.Core.Composable;
using LeapingGorilla.Testing.NUnit.Attributes;
using NSubstitute;
using static System.Net.WebRequestMethods;

namespace CartonCaps.UnitTests.Services.DeferredLinking.MockDeepLinkClientTests.CreateTests
{
    public class GivenHappyPath : WhenTestingCreate
    {

        protected string GeneratedShortCode = "ABC123DE";

        protected override ComposedTest ComposeTest() => TestComposer
            .Given(DestinationIsSet)
            .And(MetadataIsSet)
            .And(CodeGeneratorCreatesCode)
            .When(CreateIsCalled)
            .Then(ShouldReturnExpectedUrl);


        [Given]
        public void DestinationIsSet()
        {
            Destination = "test-destination";
        }
        [Given]
        public void MetadataIsSet()
        {
            Metadata = new Dictionary<string, object>();
            Metadata.Add("ShouldBeNumber55", 55);
        }

        [Given]
        public void CodeGeneratorCreatesCode()
        {
            ShortCodeGenerator.GenerateShortCode(8).Returns(GeneratedShortCode);
        }
        
        [Then]
        public void ShouldReturnExpectedUrl()
        {
            var expectedUrl = string.Format("https://cartoncaps.link/{0}", GeneratedShortCode);
            Assert.That(Result, Is.EqualTo(expectedUrl));
        }
    }
}
