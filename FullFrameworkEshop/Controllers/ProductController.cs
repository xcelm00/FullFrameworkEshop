using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using FullFrameworkEshop.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace FullFrameworkEshop.Controllers
{


    public class ProductController : ApiController
    {
        // GET api/<controller>
        //public dynamic Get()
        //{
        //    var db = ApplicationDbContext.Create();

        //    var list = db.Products.Include(x=>x.ProductType)
        //        .Select(x=> new {x.Name, x.ProductType.ProductTypeName});

        //    return list;
        //}

        public IHttpActionResult Get()
        {
            var db = ApplicationDbContext.Create();

            var list =  db.Products.Include(x => x.ProductType)
                .Select(x => new { x.Name, x.ProductType.ProductTypeName });

             return Json(list);
        }

        // GET api/<controller>/5
        public dynamic Get(int id)
        {
            var db = ApplicationDbContext.Create();

            var list = db.Products.Include(x => x.ProductType)
                .Select(x => new { x.Name, x.ProductType.ProductTypeName, x.Id }).Where(x=>x.Id == id);

            return list;
        }


        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}