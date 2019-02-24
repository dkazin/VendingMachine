using System.Collections.Generic;

namespace VendingMachine.Models.Dao
{
    class WalletMachineInit : IWalletInit
    {
        public List<Pack> Init()
        {
            var result = new List<Pack>();

            result.Add(new Pack() { Coin = ListCoins.Coins[0], Count = 100 });
            result.Add(new Pack() { Coin = ListCoins.Coins[1], Count = 100 });
            result.Add(new Pack() { Coin = ListCoins.Coins[2], Count = 100 });
            result.Add(new Pack() { Coin = ListCoins.Coins[3], Count = 100 });

            return result;
        }
    }
}
