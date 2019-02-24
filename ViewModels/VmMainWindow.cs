using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using VendingMachine.Models;
using VendingMachine.Models.Dao;

namespace VendingMachine.ViewModels
{
    class VmMainWindow : INotifyPropertyChanged
    {
        private MainWindow window;
        private Machine machine;
        private Buyer buyer;
        
        /// <summary>
        /// В конструкторе задаётся количество д.с. в кошельках и ассортимент товара
        /// </summary>
        /// <param name="mainWindow"></param>
        public VmMainWindow(MainWindow mainWindow)
        {
            var listCoins = new ListCoins(new CoinInit());
            machine = new Machine();
            buyer = new Buyer();

            window = mainWindow;
        }

        /// <summary>
        /// Кошелёк автомата
        /// </summary>
        public List<Pack> MachineWallet
        {
            get => machine.walletMachine.ListPack;
        }

        /// <summary>
        /// Сумма Д.с в автомате
        /// </summary>
        public uint MachineMoney
        {
            get => machine.walletMachine.Sum();
        }

        /// <summary>
        /// Кошелёк клиента
        /// </summary>
        public List<Pack> ClientWallet
        {
            get
            {
                return buyer.wallet.ListPack;
            }
        }

        /// <summary>
        /// Сумма Д.с в кошельке клиента
        /// </summary>
        public uint ClientMoney
        {
            get => buyer.wallet.Sum();
        }

        /// <summary>
        /// Сумма д.с.внесённых клиентом
        /// </summary>
        public uint Deposit
        {
            get => machine.walletDeposit.Sum();
        }

        /// <summary>
        /// Короткое наименование д.с.
        /// </summary>
        public string ShortNameCoin
        {
            get => ListCoins.Coins[0].ShortName;
        }

        /// <summary>
        /// Активна кнопка получение сдачи или нет
        /// </summary>
        public bool isActivGetDeposit
        {
            get => machine.walletDeposit.Sum() > 0;
        }

        /// <summary>
        /// Действие при нажатии на монету в кошельке клиента
        /// </summary>
        public int SelectedClientCoin
        {
            set
            {
                if (value >= 0)
                {
                    if (buyer.wallet.ListPack[value].Count > 0)
                    {
                        machine.walletDeposit.Add(new Pack() { Coin = ListCoins.Coins[value], Count = 1 });
                        buyer.wallet.ListPack[value].Dec();

                        //foreach (var i in shopWindow)
                        //{
                        //    if (i.Cost > Deposit)
                        //    {
                        //        i.Disabled();
                        //    }
                        //    else
                        //    {
                        //        i.Enabled();
                        //    }
                        //}

                        UpdateWindow();
                    }
                }
            }
        }

        /// <summary>
        /// Перевести деньги с депозита в кошелёк клиента.
        /// </summary>
        public void GetDeposit()
        {
            buyer.wallet.Add(machine.walletDeposit.GetAllMoney());

            //foreach (var i in shopWindow)
            //{
            //    i.Disabled();
            //}

            UpdateWindow();
        }

        /// <summary>
        /// Действие при нажатии кнопки "Забрать сдачу".
        /// </summary>
        public ExecCommand DelegateGetDeposit
        {
            get
            {
                return new ExecCommand(GetDeposit);
            }
        }

        /// <summary>
        /// Действие при выборе товара
        /// </summary>
        public int SelectedProduct
        {
            set
            {
                if (value >= 0)
                {
                    machine.Buy(value);
                    UpdateWindow();
                }
            }
        }

        /// <summary>
        /// Список с товарами
        /// </summary>
        public ObservableCollection<Product> ShopWindow
        {
            get => machine.shopWindow.ListProduct;
        }

        /// <summary>
        /// История покупок
        /// </summary>
        public ObservableCollection<Product> Cart
        {
            get => machine.cart;
        }

        private void UpdateWindow()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Deposit"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ClientMoney"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MachineMoney"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("isActivGetDeposit"));

            var a = window.userMoney.ItemsSource;
            window.userMoney.SelectedIndex = -1;
            window.userMoney.ItemsSource = null;
            window.userMoney.ItemsSource = a;

            a = window.machineMoney.ItemsSource;
            window.machineMoney.ItemsSource = null;
            window.machineMoney.ItemsSource = a;


            a = window.produsts.ItemsSource;
            window.produsts.SelectedIndex = -1;
            window.produsts.ItemsSource = null;
            window.produsts.ItemsSource = a;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}