﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Api.Controllers.Messages;
using CartonCaps.Persistence.Models;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.Core.Composable;
using LeapingGorilla.Testing.NUnit.Attributes;
using Newtonsoft.Json;
using NSubstitute;

namespace CartonCaps.IntegrationTests.Api.Controllers.UserControllerTests.ReferredUsersTests
{
    public class GivenHappyPath : WhenTestingGetReferredUsers
    {

        int PageStart = 0;
        int NumberPerPage = 10;
       

        protected override ComposedTest ComposeTest() => TestComposer
            .Given(ApplicationIsRunning)
            .And(RequestUrlIsSet)
            .And(ReferredUserServiceReturnsReferrals)
            .When(GetIsCalled)
            .Then(ShouldReturnOk)
            .And(ShouldReturnReferredUsers);


        [Given]
        public void RequestUrlIsSet()
        {
            RequestUrl = $"api/users/referredUsers?pageStart={PageStart}&numberPerPage={NumberPerPage}";
        }

        [Given]
        public void ReferredUserServiceReturnsReferrals()
        {
            ExpectedReferrals = GetMockReferredUsers();

            ReferredUserService
                .GetUserReferralsById(
                    Arg.Any<Guid>(),
                    PageStart,
                    NumberPerPage,
                    Arg.Any<CancellationToken>())
                .Returns((2, ExpectedReferrals));
        }
    }
}
