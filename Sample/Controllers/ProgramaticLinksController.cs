﻿using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.LinkHeader;

namespace LinkHeaderSample.Controllers
{
    /// <summary>
    /// Demonstrates the usage of <see cref="HttpMessageExtensions.AddLink"/> and  <see cref="HttpActionResultExtensions.WithLink"/>.
    /// </summary>
    [RoutePrefix("programatic-links")]
    public class ProgramaticLinksController : ApiController
    {
        /// <summary>
        /// Entry point with relative links to child elements.
        /// </summary>
        [HttpGet, Route("")]
        public IHttpActionResult Overview()
        {
            return StatusCode(HttpStatusCode.NoContent)
                .WithRouteLink("Products", rel: "products");
                // -or
                //.WithLink("products", rel: "products");
        }

        /// <summary>
        /// Collection with a template link for the child elements.
        /// </summary>
        [HttpGet, Route("products", Name = "Products")]
        [ResponseType(typeof(IEnumerable<int>))]
        public HttpResponseMessage Products()
        {
            var productIds = new[] {1, 2, 3};
            var response = Request.CreateResponse(HttpStatusCode.OK, productIds);

            foreach (var productId in productIds)
            {
                response.AddRouteLink("Product", new {id = productId},
                    rel: "product",
                    title: "Product #" + productId);
                // -or-
                //response.AddLink("products/" + productId,
                //    rel: "product",
                //    title: "Product #" + productId);
            }
            return response;
        }

        /// <summary>
        /// Collection element with relative link to child element.
        /// </summary>
        [HttpGet, Route("products/{id}", Name = "Product")]
        public string Product(int id)
        {
            return "Product #" + id;
        }
    }
}