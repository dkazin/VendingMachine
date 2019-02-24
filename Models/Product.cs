using System;

namespace VendingMachine.Models
{
    /// <summary>
    /// Информация о товаре.
    /// </summary>
    class Product
    {
        public string Title { get; private set; }
        public uint Cost { get; private set; }
        public string Image { get; private set; }
        public ulong Count { get; private set; }
        public string CoinShortName { get; private set; }
        private bool isEnabled;

        public bool IsEnabled
        {
            get => (Count > 0 && isEnabled);
        }

        public Product(string title, uint cost, ulong count, string image = "\uE8B9", bool isEnabled = false)
        {
            Title = title;
            Cost = cost;
            Count = count;
            Image = image;
            this.isEnabled = isEnabled;
        }

        /// <summary>
        /// Уменьшить на 1 кол-во товара
        /// </summary>
        public void DecCount()
        {
            if (Count > 0)
            {
                Count--;
            }
            else
            {
                throw new Exception("Товар отсутствует");
            }
        }

        /// <summary>
        /// Снять признак недоступности
        /// </summary>
        public void Enabled()
        {
            isEnabled = true;
        }

        /// <summary>
        /// Установить признак запрета
        /// </summary>
        public void Disabled()
        {
            isEnabled = false;
        }
    }
}
