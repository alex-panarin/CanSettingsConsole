using System;
using System.Collections.Generic;
using System.Text;

namespace CanSettingsConsole.ViewModel
{
    public interface IConnectionViewModel
    {
        bool Open();
        void Close();
    }
}
