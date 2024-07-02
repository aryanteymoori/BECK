﻿using _01_BECKQuery.Contract.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestArrivalsViewComponent:ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public LatestArrivalsViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public IViewComponentResult Invoke()
        {
            return View(_productQuery.GetProducts());
        }
    }
}
