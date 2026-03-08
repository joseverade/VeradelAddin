using System;

namespace VeradelAddin.Presentation.AddinRibbon
{
    public interface ICommandObjects : IDisposable
    {
        void SetUserID(int userID);
    }
}
