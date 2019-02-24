using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Models.Dao
{
    class WalletBuyerInit : IWalletInit
    {
        public List<Pack> Init()
        {
            var result = new List<Pack>();
            
            result.Add(new Pack() { Coin = ListCoins.Coins[0], Count = 15 });
            result.Add(new Pack() { Coin = ListCoins.Coins[1], Count = 20 });
            result.Add(new Pack() { Coin = ListCoins.Coins[2], Count = 30 });
            result.Add(new Pack() { Coin = ListCoins.Coins[3], Count = 10 });

            return result;
        }
    }
}
