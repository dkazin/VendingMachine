using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Models
{
    /// <summary>
    /// Список доступных номиналов денежных средств
    /// </summary>
    class ListCoins
    {
        public static List<Coin> Coins { get; private set; }

        /// <summary>
        /// Конструктор с инициализаций.
        /// </summary>
        public ListCoins(ICoinsInit listCoins)
        {
            Coins = listCoins.Init();
        }
    }
}
