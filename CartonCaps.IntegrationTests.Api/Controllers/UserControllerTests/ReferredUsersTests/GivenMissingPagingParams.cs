using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.Core.Composable;
using LeapingGorilla.Testing.NUnit.Attributes;
using NSubstitute;

namespace CartonCaps.IntegrationTests.Api.Controllers.UserControllerTests.ReferredUsersTests
{
    public class GivenMissingPagingParams : WhenTestingGetReferredUsers
    {
        protected override ComposedTest ComposeTest() => TestComposer
            .Given(ApplicationIsRunning)
            .And(RequestDoesNotHavePaging)
            .And(ReferredUserServiceReturnsReferrals)
            .When(GetIsCalled)
            .Then(ShouldReturnOk)
            .And(ShouldReturnReferredUsers);


        [Given]
        public void RequestDoesNotHavePaging()
        {
            //Omit pageStart and numberPerPage
            RequestUrl = "api/users/referredUsers";
        }

        [Given]
        public void ReferredUserServiceReturnsReferrals()
        {
            ExpectedReferrals = GetMockReferredUsers();

            ReferredUserService
                .GetUserReferralsById(
                    Arg.Any<Guid>(),
                    skip: 0, //Default 
                    take: 10, //Default
                    Arg.Any<CancellationToken>())
                .Returns((2, ExpectedReferrals));
        }
    }
}
