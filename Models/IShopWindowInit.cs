using System.Collections.ObjectModel;

namespace VendingMachine.Models
{
    interface IShopWindowInit
    {
        ObservableCollection<Product> Init();
    }
}
