using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SOATest.Contracts;
using SOATest.Service;
using SOATest.Service.Responses;

namespace SOATest.WebApiClient.Controllers
{
    public class PurchaseController : ApiController
    {
        private readonly IProductService _productService;

        public PurchaseController(IProductService productService)
        {
            if (productService == null) { throw new ArgumentException("IProductService"); }

            _productService = productService;
        }

        public HttpResponseMessage Post(IPurchaseProductRequest purchaseProductRequest)
        {
            purchaseProductRequest.CorrelationId = purchaseProductRequest.ReservationId;
            ServiceResponseBase response =  (ServiceResponseBase) _productService.PurchaseProduct(purchaseProductRequest);

            return Request.BuildResponse(response);
        }

        // GET: api/Default
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //// GET: api/Default/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Default


        //// PUT: api/Default/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Default/5
        //public void Delete(int id)
        //{
        //}
    }
}
