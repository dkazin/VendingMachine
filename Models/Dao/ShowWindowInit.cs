using System.Collections.ObjectModel;

namespace VendingMachine.Models.Dao
{
    class ShowWindowInit : IShopWindowInit
    {
        public ObservableCollection<Product> Init()
        {
            var result = new ObservableCollection<Product>();

            result.Add(new Product(title: "Чай", cost: 13 * 100, count: 10));
            result.Add(new Product(title: "Кофе", cost: 18 * 100, count: 20));
            result.Add(new Product(title: "Кофе с молоком", cost: 21 * 100, count: 20));
            result.Add(new Product(title: "Сок апельсиновый", cost: 35 * 100, count: 15));
            result.Add(new Product(title: "Вода", cost: 3 * 100, count: 10));

            return result;
        }
    }
}
