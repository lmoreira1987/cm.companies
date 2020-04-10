using MGA.CrossCutting.Provider.Interface;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace MGA.Test.Service
{
    public class ExampleAppServiceTest
    {

        protected IDateTimeProvider _dateTimeProvider;

        public void Setup() {
            _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        }

        //[Test]
    }
}
