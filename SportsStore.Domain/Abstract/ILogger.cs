using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Store.WebUI.Infrastructure
{
    public interface ILogger
    {
        void Info(string message);
    }
}
