using System;
using System.Collections.Generic;
using System.Text;

namespace QueueIbm
{
    public interface IIbmQueue
    {
        void Write(string menssage);
        string ReadOneMensage();
        List<string> ReadManyMensage();
    }
}
