using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ikvm.runtime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace EmployeeAPITests.Integrations.Controllers
{
    public class EmployeeControllerTests
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public EmployeeControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
    }
}
