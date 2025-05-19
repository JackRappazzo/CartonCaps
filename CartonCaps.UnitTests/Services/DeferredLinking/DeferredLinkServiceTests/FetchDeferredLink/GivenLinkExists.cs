using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Core.Services.DeferredDeepLinking;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.Core.Composable;
using LeapingGorilla.Testing.NUnit.Attributes;
using NSubstitute;

namespace CartonCaps.UnitTests.Services.DeferredLinking.DeferredLinkServiceTests.FetchDeferredLink
{
    public class GivenLinkExists : WhenTestingResolveLink
    {
        private const int TestDataValue = 55;
        private const string TestDataKey = "test";

        protected string ExpectedDestination = "destination";
        protected Dictionary<string, object> ExpectedMetadata = new Dictionary<string, object>();

        protected override ComposedTest ComposeTest() => TestComposer
            .Given(LinkCodeIsSet)
            .And(StoredLinkHasMetadata)
            .And(LinkClientReturnsLink)
            .When(ResolveLinkIsCalled)
            .Then(ShouldReturnExpectedLink);

        [Given]
        public void StoredLinkHasMetadata()
        {
            //Ensure we are returning the intended metadata and not uninitiatlized data
            ExpectedMetadata.Add(TestDataKey, TestDataValue);
        }


        [Given]
        public void LinkClientReturnsLink()
        {
            DeepLinkClient.FetchDeferredLink(LinkCode, CancellationToken)
                .Returns((true, new DeferredLink()
                {
                    Destination = ExpectedDestination,
                    Data = ExpectedMetadata
                }));
        }

        [Then]
        public void ShouldReturnExpectedLink()
        {
            Assert.That(Result.Destination, Is.EqualTo(ExpectedDestination));

            Assert.That((int)Result.Data[TestDataKey], Is.EqualTo(TestDataValue));

        }
    }
}
