using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Models
{
    /// <summary>
    /// Пачка денежных средств одного достоинства
    /// </summary>
    class Pack
    {
        /// <summary>
        /// Номинал денежных средств. В копейках.
        /// </summary>
        public Coin Coin { get; set; }
        
        /// <summary>
        /// Количество
        /// </summary>
        public uint Count { get; set; }
        
        /// <summary>
        /// Сумма денежных средств в пачке
        /// </summary>
        public uint Sum
        {
            get => Coin.Count * Count;
        }

        /// <summary>
        /// Добавить денежную единицу
        /// </summary>
        public void Inc()
        {
            Count++;
        }

        /// <summary>
        /// Взять денежную единицу
        /// </summary>
        public void Dec()
        {
            if (Count > 0)
            {
                Count--;
            }
            else
            {
                throw new Exception("Пачка пуста.");
            }
        }

        /// <summary>
        /// Добавить количество(кол-во) денежных единиц
        /// </summary>
        /// <param name="value">Кол-во штук</param>
        public void Add(uint value)
        {
            if (value > 0)
            {
                Count += value;
            }
            else
            {
                throw new Exception("Значение должно быть больше нуля.");
            }
        }

        /// <summary>
        /// Взять количество(штук) денежных средств из пачки. 
        /// </summary>
        /// <param name="value">Кол-во штук</param>
        public Pack Get(uint value)
        {
            Pack result;

            if (Count >= value)
            {
                result = new Pack() {Coin = this.Coin, Count = value };
                Count -= value;
            }
            else
            {
                throw new Exception("Недостаточно денежных средств");
            }

            return result;
        }

        /// <summary>
        /// Взять определённую сумму из пачки.
        /// </summary>
        /// <param name="value">Сумма в копейках</param>
        /// <returns></returns>
        public Pack GetSum(uint value)
        {
            throw new Exception("Метод GetSum не реализован.");

            #region Проверки на корректность переданного значения
            if (value > Count)
            {
                throw new Exception("Недостаточно денежных средств");
            }

            if ((value % Coin.Count) != 0)
            {
                throw new Exception("Номинал монет в пачке, не позволяет получить запрошенную сумму");
            }
            #endregion
        }

        /// <summary>
        /// Взять все денежные средства из пачки
        /// </summary>
        /// <returns></returns>
        public Pack GetAll()
        {
            Pack result;

            if (Count > 0)
            {
                result = new Pack { Coin = this.Coin, Count = this.Count };
                Count = 0;
            }
            else
            {
                throw new Exception("Пачка пуста.");
            }
            
            return result;
        }

        /// <summary>
        /// Название денежной единицы.
        /// </summary>
        public string Name
        {
            get => Coin.Name;
        }

        /// <summary>
        /// Краткое название денежной единицы.
        /// </summary>
        public string ShortName
        {
            get => Coin.ShortName;
        }

        public override string ToString()
        {
            return $"{Coin.Count / 100} {Coin.Name} x {Count} ";
        }
    }
}
