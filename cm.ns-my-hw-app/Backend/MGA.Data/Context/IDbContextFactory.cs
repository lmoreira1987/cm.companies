using System;
using System.Collections.Generic;
using System.Text;

namespace MGA.Data.Context
{
    public interface IDbContextFactory
    {
        MGAContext GetMainContext();
    }
}
