using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using DauGiaTrucTuyen.DataBinding;

namespace DauGiaTrucTuyen.HubRealTime
{
    public class ProductHub : Hub
    {
        ProductService productService = new ProductService();
        public void Hello()
        {
            Clients.All.hello("xin chao");
        }

        public void ListProduct()
        {
            Clients.All.GetListProduct(productService.GetListProductForPageClient());
        }

        public void ListProductFromCategory(string categoryId)
        {
            Clients.All.GetListProducFromCategory(productService.GetListProductFromCategory(categoryId));
        }

        public void AuctionProduct(decimal price, string transactionId, string sessionUserId)
        {
            var result = productService.AuctionProduct(price, transactionId, sessionUserId);
            if (result == true)
                Clients.All.GetListProduct(result, price);
        }
    }
}