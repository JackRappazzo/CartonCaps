using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeapingGorilla.Testing.Core.Attributes;

namespace CartonCaps.IntegrationTests.Api.Controllers.UserControllerTests.ReferralCodeTests
{
    public abstract class WhenTestingGetReferralCode : WhenTestingUserController
    {
        protected override string RequestUrl => "api/users/referralCode";

    }
}
