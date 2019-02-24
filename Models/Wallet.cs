using System;
using System.Collections.Generic;

namespace VendingMachine.Models
{
    /// <summary>
    /// Бумажник. Содержит пачки денежных средств
    /// </summary>
    class Wallet
    {
        private List<Pack> pack = new List<Pack>();

        //public delegate void ChangeSumm(uint sum);

        public event Action<uint> ChangeSum;

        public Wallet()
        {   
        }

        public Wallet(IWalletInit packs)
        {
            this.pack = packs.Init();
        }

        /// <summary>
        /// Добавление пачки денежных средств в бумажник.
        /// </summary>
        /// <param name="value"></param>
        public void Add(Pack value)
        {
            #region Проверки на корректность переданного значения
            if (value == null)
            {
                throw new Exception("Передан пустой объект");
            }

            if (value.Coin == null)
            {
                throw new Exception("Пачка не содержит указатель на денежную единицу");
            }

            if (value.Count <= 0)
            {
                throw new Exception("Количество денежных средств в пачке должно быть больше нуля");
            }
            #endregion

            bool packExist = false;

            foreach (var i in this.pack)
            {
                if (i.Coin.Count == value.Coin.Count)
                {
                    i.Count += value.Count;
                    packExist = true;
                    break;
                }
            }

            if (!packExist)
            {
                // Новая пачка создается для исключения изменений значений пачки, после её добавления в вызывающем коде
                this.pack.Add(new Pack { Coin = value.Coin, Count = value.Count });
            }

            ChangeSum?.Invoke(Sum());

        }

        /// <summary>
        /// Добавление д.с. из бумажника
        /// </summary>
        /// <param name="value"></param>
        public void Add(Wallet value)
        {
            #region Проверки на корректность переданного значения
            if (value == null)
            {
                throw new Exception("Передан пустой объект");
            }

            if (value.Sum() <= 0)
            {
                throw new Exception("Количество денежных средств в бумажнике должно быть больше нуля");
            }

            foreach (var outPack in value.ListPack)
            {
                if (outPack.Count <= 0)
                {
                    throw new Exception("Количество денежных средств в пачке должно быть больше нуля");
                }

                if (outPack.Coin == null)
                {
                    throw new Exception("Пачка не содержит указатель на денежную единицу");
                }
            }
            #endregion

            foreach (var outPack in value.ListPack)
            {
                Add(outPack);
            }

            ChangeSum?.Invoke(Sum());
        }

        /// <summary>
        /// Списать указанную сумму.
        /// </summary>
        /// <param name="value"></param>
        public void GetSum(Wallet value)
        {
            #region Проверки
            if (value == null)
            {
                throw new Exception("Переданное значение не может быть null.");
            }

            if (value.Sum() <= 0)
            {
                throw new Exception("Запрашиваемая на списание сумма должна быть больше нуля.");
            }

            if (value.Sum() > Sum())
            {
                throw new Exception("Запрошенная сумма больше имеющейся.");
            }
            #endregion

            foreach (var inPack in value.ListPack)
            {
                foreach (var thisPack in pack)
                {
                    if (inPack.Coin.Count == thisPack.Coin.Count)
                    {
                        if (thisPack.Count >= inPack.Count)
                        {
                            thisPack.Count -= inPack.Count;
                        }
                        else
                        {
                            throw new Exception("Нет нужного кол-во монет, этого достоинства.");
                        }
                    }
                }
            ChangeSum?.Invoke(Sum());
            }
        }

        /// <summary>
        /// Получить сумму денежных средств. Сдача.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Wallet GetSum(uint value)
        {
            #region Проверки на корректность переданного значения
            if (value == 0)
            {
                throw new Exception("Запрашиваемая сумма должна быть больше нуля");
            }

            if (value > Sum())
            {
                throw new Exception("Недостаточно денежных средств");
            }
            #endregion

            var result = new Wallet();

            foreach (var i in pack)
            {
                if (i.Count > 0)
                {
                    uint div = value / i.Coin.Count;
                    uint mod = value % i.Coin.Count;

                    if (div == 0)
                    {
                        continue;
                    }
                    if (div <= i.Count)
                    {
                        result.Add(new Pack() { Coin = i.Coin, Count = div });
                        div = 0;
                    }
                    else
                    {
                        result.Add(new Pack() { Coin = i.Coin, Count = i.Count });
                        div -= i.Count;
                        div *= i.Coin.Count;
                    }
                    value = div + mod;

                    if (value == 0)
                        break;
                }
            }

            if (value != 0)
            {
                result = null;
            }
            else
            {
                //Списание
                GetSum(result);
                ChangeSum?.Invoke(Sum());
            }

            return result;
        }

        /// <summary>
        /// Забрать все деньги из кошелька.
        /// </summary>
        /// <returns></returns>
        public Wallet GetAllMoney()
        {
            #region Проверки на корректность переданного значения
            if (Sum() <= 0)
            {
                throw new Exception("Бумажник пуст.");
            }
            #endregion

            var result = new Wallet();

            foreach (var i in pack)
            {
                if (i.Count > 0)
                {
                    result.Add(new Pack() { Coin = i.Coin, Count = i.Count });
                    i.Count = 0;
                }  
            }

            ChangeSum?.Invoke(Sum());

            return result;
        }

        /// <summary>
        /// Делает дубль кошелька. Если он не пустой.
        /// </summary>
        /// <returns></returns>
        public Wallet Copy()
        {
            #region Проверки на корректность переданного значения
            if (Sum() <= 0)
            {
                throw new Exception("Бумажник пуст.");
            }
            #endregion

            var result = new Wallet();

            foreach (var i in pack)
            {
                if (i.Count > 0)
                {
                    result.Add(new Pack() { Coin = i.Coin, Count = i.Count });
                }
            }

            return result;
        }

        /// <summary>
        /// Сумма всех денежных средств в бумажнике, в копейках
        /// </summary>
        /// <returns></returns>
        public uint Sum()
        {
            uint sum = 0;

            foreach (var i in pack)
            {
                sum += i.Sum;
            }

            return sum;
        }

        /// <summary>
        /// Выкинуть все монеты из бумажника.
        /// </summary>
        public void Clear()
        {
            if (Sum() <= 0)
            {
                throw new Exception("Бумажник пуст.");
            }

            foreach(var i in pack)
            {
                i.Count = 0;
            }
            ChangeSum?.Invoke(Sum());
        }

        public List<Pack> ListPack
        {
            get => pack;
        }
    }
}
