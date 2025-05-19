using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartonCaps.Core.Services.DeferredDeepLinking
{
    public class DeferredLink
    {
        public string Destination { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }
}
