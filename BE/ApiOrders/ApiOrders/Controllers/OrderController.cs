using OrdersManager.Domain;
using OrdersManager.Domain.DTOs;
using OrdersManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
 

namespace OrdersManager.Api.Controllers
{
  //  [Authorize]
    [RoutePrefix("api")]
    public class OrderController : ApiController
    {

        private readonly IOrderService orderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        /// <param name="orderService">The device service.</param>
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        /// <summary>
        /// Gets the specified page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>     
         [Route("Order/PostGetOrders")]
        public IHttpActionResult PostGetOrders ([FromBody]BaseCriteriaDTO criteria)
        {
            if (criteria == null)
                return BadRequest();
            PagedListDTO<OrderDTO> result = orderService.GetOrders(criteria);

            return Ok(result);
        }

        [Route("Order/PostGetOrdersDetails")]
        public IHttpActionResult PostGetOrdersDetails([FromBody]BaseCriteriaDTO criteria)
        {
            if (criteria == null)
                return BadRequest();
            PagedListDTO<OrderDetailDTO> result = orderService.GetOrdersDetails(criteria);

            return Ok(result);
        }

        /// <summary>
        /// Upload Image
        /// </summary>
        /// <returns></returns>
        [Route("Order/PostOrdersData")]
        public async Task<HttpResponseMessage> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            #warning review  this line
            string root = HttpContext.Current == null
                        ? System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "ApiOrders\\App_Data")
                        : HttpContext.Current.Server.MapPath("ApiOrders\\App_Data");



                //HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server file path: " + file.LocalFileName);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }



        [Route("Order/PostEditOrderDetail")]
        public IHttpActionResult PostEditOrder([FromBody]OrderDetailDTO orderDetailDTO)
        {
            if (orderDetailDTO == null)
                return BadRequest();

            orderService.EditOrderDetail(orderDetailDTO);

            return Ok();
        }

    }
}
