using System;

namespace MGA.CrossCutting.Provider.Interface
{
    public interface IDateTimeProvider
    {
        DateTime GetNow();
        DateTime GetUtcNow();
    }
}
