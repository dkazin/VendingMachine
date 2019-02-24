using System.Collections.ObjectModel;
using VendingMachine.Models.Dao;

namespace VendingMachine.Models
{
    class Machine
    {
        public Wallet walletMachine;
        public Wallet walletDeposit;
        public ShopWindow shopWindow;
        public ObservableCollection<Product> cart = new ObservableCollection<Product>();

        public Machine()
        {
            walletMachine = new Wallet(new WalletMachineInit());
            walletDeposit = new Wallet();
            shopWindow = new ShopWindow(new ShowWindowInit());
            walletDeposit.ChangeSum += shopWindow.ChangeStatusProduct;
        }

        /// <summary>
        /// Совершить покупку
        /// </summary>
        /// <param name="itemProduct"></param>
        public void Buy(int numberProduct)
        {
            if (numberProduct >= 0 && shopWindow.ListProduct[numberProduct].IsEnabled)
            {
                Wallet reserveDeposit = walletDeposit.Copy();   // Сохранить депозит  на случай отката(нет монет для сдачи)
                walletMachine.Add(walletDeposit.GetAllMoney()); // Забрать деньги из депозита в автомат
                uint sumChange = reserveDeposit.Sum() - shopWindow.ListProduct[numberProduct].Cost; // Вычисление суммы сдачи.
                if (sumChange > 0)  // Если сдача требуетя
                {
                    Wallet change = walletMachine.GetSum(sumChange);    // Попытка получить сдачу
                    if (change != null) // Если сдачу получили
                    {
                        walletDeposit.Add(change);  // Зачислить сдачу на депозит
                        shopWindow.ListProduct[numberProduct].DecCount();   // Уменьшить кол-во товара на 1
                        cart.Add(shopWindow.ListProduct[numberProduct]);  // Добавить товар в корзину.
                    }
                    else //Если не удалось получить сдачу
                    {
                        walletDeposit.Add(reserveDeposit);  // Вернуть д.с. на депозит
                        return;
                    }
                }
                else    // Если сдача не требуется
                {
                    shopWindow.ListProduct[numberProduct].DecCount();   // Уменьшить кол-во товара на 1.
                    cart.Add(shopWindow.ListProduct[numberProduct]);  // Добавить товар в корзину.
                }
            }
        }
    }
}
