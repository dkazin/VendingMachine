using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Models.Dao;

namespace VendingMachine.Models
{
    /// <summary>
    /// Клиент
    /// </summary>
    class Buyer
    {
        public Wallet wallet;

        public Buyer()
        {
            wallet = new Wallet(new WalletBuyerInit());
        }
    }
}
