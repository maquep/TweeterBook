using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweeterBook.Contracts.HealthChecks
{
    /// <summary>
    /// Used to describe the Health status of different components of the system
    /// Status: Is healthy or not
    /// Component: The component being tested (DB or another service)
    /// Description: additional information about the status
    /// </summary>
    public class HealthCheck
    {
        public string Status { get; set; }

        public string Componet { get; set; }

        public string Description { get; set; }
    }
}
