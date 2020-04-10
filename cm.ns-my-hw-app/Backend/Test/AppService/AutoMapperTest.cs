using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MGA.Test.AppService
{
    [TestClass]
    public class AutoMapperTest
    {
        

        public void Setup()
        {
            
        }

        [TestMethod]
        public void AutoMapperConfigurationIsValid()
        {
            Setup();

            Mapper.AssertConfigurationIsValid();

        }
    }
}
