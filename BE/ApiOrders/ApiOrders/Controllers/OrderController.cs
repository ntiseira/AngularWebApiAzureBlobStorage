using OrdersManager.Common;
using OrdersManager.Domain;
using OrdersManager.Domain.DTOs;
using OrdersManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

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
            
            string root = ManageDirectory.GetDirectory();
                            
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                //Get name of file
                string nameOfFile = provider.FileData[0].Headers.ContentDisposition.FileName;

                //Parse if it's JSON
                if (StringHelper.ValidateJSON(nameOfFile))
                { 
                   
                    using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(provider.FileData[0].Headers.ContentDisposition.FileName)))
                    {
                        // Deserialization from JSON  
                        DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(string));
                        nameOfFile = (string)deserializer.ReadObject(ms);                 
                    }
                }

                await orderService.UploadImageContainer(provider.FileData[0].LocalFileName, nameOfFile);

               
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
