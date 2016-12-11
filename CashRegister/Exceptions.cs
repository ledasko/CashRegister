using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister
{
    [Serializable]
    public class CashRegisterBaseException : Exception { }

    [Serializable]
    public class UsernameAlreadyExists : CashRegisterBaseException { }
}
