using SOATest.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Http;
using SOATest.Domain;
using SOATest.Service.Responses;

namespace SOATest.WebApiClient.Controllers
{
    public class ReservationController : ApiController
    {
        private ProductService _productService;

        public ReservationController()
        {
            var _messageRepositoryFactory = new LazySingletonMessageRepositoryFactory();
            var _productRepositoryFactory = new LazySingletonProductRepositoryFactory();
            _productService = new ProductService(_messageRepositoryFactory, _productRepositoryFactory);
        }

        //public ReservationController(IProductService productService)
        //{
        //    if (productService == null)
        //    {
        //        throw new ArgumentException("IProductService");
        //    }
        //    _productService = productService;
        //}

        //public HttpResponseMessage Post(ReserveProductRequest reserveProductRequest)
        //{
        //    ServiceResponseBase response = _productService.ReserveProduct(reserveProductRequest);
        //    return Request.BuildResponse(response);
        //}

        public HttpResponseMessage Post()
        {
            ReserveProductRequest reservationRequest = new ReserveProductRequest
            {
                ProductId = "13a35876 - ccf1 - 468a - 88b1 - 0acc04422243",
                ProductQuantity = 10
            };

            ServiceResponseBase response = (ServiceResponseBase)_productService.ReserveProduct(reservationRequest);
            return Request.BuildResponse(response);
        }

        public IEnumerable<string> Get()
        {
            return new string[] {"value1", "value2"};
        }
    }
}