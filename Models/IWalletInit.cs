using System.Collections.Generic;

namespace VendingMachine.Models
{
    interface IWalletInit
    {
        List<Pack> Init();
    }
}
