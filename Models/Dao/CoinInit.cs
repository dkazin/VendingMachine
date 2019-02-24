using System.Collections.Generic;

namespace VendingMachine.Models.Dao
{
    class CoinInit : ICoinsInit
    {
        public List<Coin> Init()
        {
            var result = new List<Coin>();

            // д.с. добавляются в порядке уменьшения их стоимости.
            
            result.Add(new Coin("Рублей", "Руб.", 10 * 100));
            result.Add(new Coin("Рублей", "Руб.", 5 * 100));
            result.Add(new Coin("Рубля", "Руб.", 2 * 100));
            result.Add(new Coin("Рубль", "Руб.", 1 * 100));

            return result;
        }
    }
}
