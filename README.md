# Introduction 
This project is a sample selenium automation project built over Selenium & C# as the primary tool, aimed at providing individuals intending to learn test automation a sample project for team members to try their hands on. Logging & Reporting capabilities have already been integrated within the framework using Log4Net & Extent Report nuget packages respectively. 
The framework supports execution of tests in parallel, in order to achieve quicker execution, currently the framework has been set to execute 4 threads of test execution in parallel, this value is configurable within the AssemblyInfo.cs file.
Note: The framework currently supports execution on Chrome Browser, support for additional browsers can easily be integrated into the framework & can be used as a config level parameter to support execution across multiple browsers.

# Getting Started
Inorder to execute the project:
## Installation & Setup process
1. Install .NET Core 6 on your system.
2. Clone the code repository on to your local system/VM.

## Software Dependencies
.NET Core 6 (LTS)
Visual Studio (Community, Professional or Community either of the three can be used)
Git
Chrome Browser

## Latest Releases
V 1.0 Initial Framework setup & sample tests for practicing automation exercises. Test Execution is currently supported only via Chrome Browser.

# Build and Test
Once the necessary software dependencies & the code is cloned locally, it is pretty much ready to go. 
1. Simply build the solution from the Solution Explorer or the Build Menu option.
2. Open Test Explorer window.
3. Select one or more tests that you intend to execute & click Run test button to execute the tests.

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)