using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Models
{
    class ShopWindow
    {
        private ObservableCollection<Product> listProduct;

        public ObservableCollection<Product> ListProduct
        {
            get => listProduct;
        }

        public ShopWindow(IShopWindowInit ShopWindowInit)
        {
            this.listProduct = ShopWindowInit.Init();
        }

        public ShopWindow()
        {
            this.listProduct = new ObservableCollection<Product>();
        }

        public void ChangeStatusProduct(uint sum)
        {
            foreach (var i in listProduct)
            {
                if (sum < i.Cost)
                {
                    i.Disabled();
                }
                else
                {
                    i.Enabled();
                }
            }
        }
    }
}
