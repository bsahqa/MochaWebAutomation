/*using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]
[assembly: Parallelizable(ParallelScope.All)]

namespace MochaHomeAccounting.Utilities.AsssemblyInitialize
{
    [TestFixture]
    public class AssemblyInitialize
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [OneTimeSetUp]
        public static void InitializeAssembly(TestContext testContext)
        {
            log.Info("------------------Test execution started------------------");
            ExtentReporting.GetExtentReports();
        }


        [OneTimeTearDown]
        public static void AssemblyCleanUp()
        {
            ExtentReporting.GetExtentReports().Flush();
        }

    }
}
*/