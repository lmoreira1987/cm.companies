

using MGA.Domain.Interfaces.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

namespace MGA.Test.Service
{
	[TestClass]
	public class ExampleServiceTest
    {
		protected IUserService _userService;

		public void Setup()
		{
			_userService = Substitute.For<IUserService>();
		}

		[TestMethod]
		public void LIMSImporterServiceTest_GenerateReportFile()
		{
			Setup();
			var dtStart = new DateTime(2018, 02, 01);

			Assert.IsTrue(1 == 1);
		}
	}
}
