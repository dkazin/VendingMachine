using System;

namespace VendingMachine.Models
{
    /// <summary>
    /// Денежная единица
    /// </summary>
    class Coin
    {
        private uint count;

        /// <summary>
        /// Название д.с.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Краткое название д.с.
        /// </summary>
        public string ShortName { get; private set; }

        /// <summary>
        /// Цена(номинал) одной денежной еденицы в копейках
        /// </summary>
        public uint Count
        {
            get => count;
            private set
            {
                if (value == 0)
                {
                    throw new Exception("Значение свойства Count должно быть больше нуля.");
                }
                else
                {
                    count = value;
                }
            }
        }

        public Coin(string name, string shortName, uint count)
        {
            Name = name;
            ShortName = shortName;
            Count = count;
        }
    }
}
