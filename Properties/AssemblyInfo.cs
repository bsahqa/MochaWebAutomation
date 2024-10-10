// <copyright file="AssemblyInitialize.cs" company="Xplor Technologies">
// Copyright (c) Xplor Technologies. All rights reserved.
// </copyright>

using NUnit.Framework;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]
[assembly: Parallelizable(ParallelScope.All)]
[assembly: LevelOfParallelismAttribute(4)]

namespace MochaHomeAccounting.Utilities
{
    /// <summary>
    /// Initialize objects at Assembly level.
    /// </summary>
    [TestFixture]
    public class AssemblyInfo
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initialize the Extent Report while initializing the assembly.
        /// </summary>
        /// <param name="testContext">Current Test Context object.</param>
        [OneTimeSetUp]
        public static void InitializeAssembly(TestContext testContext)
        {
            Log.Info("------------------Test execution started------------------");
            ExtentReporting.GetExtentReports();
        }

        /// <summary>
        /// Flush the extent reports object to print all the content to the report files.
        /// </summary>
        [OneTimeTearDown]
        public static void AssemblyCleanUp()
        {
            ExtentReporting.GetExtentReports().Flush();
        }
    }
}