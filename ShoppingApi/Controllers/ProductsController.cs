﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApi.Models;
using ShoppingApi.Models.Products;
using ShoppingApi.Services;

namespace ShoppingApi.Controllers
{
    public class ProductsController : ControllerBase
    {
        private readonly ILookupProducts _productLookup;

        public ProductsController(ILookupProducts productLookup)
        {
            _productLookup = productLookup;
        }

        [HttpGet("/products/{id:int}")]
        public async Task<ActionResult> GetProductsById(int Id)
        {
            GetProductDetailsResponse response = await _productLookup.GetProductById(Id);
            return this.Maybe(response);
        }
    }
}
