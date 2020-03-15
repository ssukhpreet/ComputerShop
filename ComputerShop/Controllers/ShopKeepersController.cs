using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ComputerShop.Models;

namespace ComputerShop.Controllers
{
    public class ShopKeepersController : ApiController
    {
        private ComputerShopEntities db = new ComputerShopEntities();

        // GET: api/ShopKeepers
        public IQueryable<ShopKeeper> GetShopKeepers()
        {
            return db.ShopKeepers;
        }

        // GET: api/ShopKeepers/5
        [ResponseType(typeof(ShopKeeper))]
        public IHttpActionResult GetShopKeeper(int id)
        {
            ShopKeeper shopKeeper = db.ShopKeepers.Find(id);
            if (shopKeeper == null)
            {
                return NotFound();
            }

            return Ok(shopKeeper);
        }

        // PUT: api/ShopKeepers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShopKeeper(int id, ShopKeeper shopKeeper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shopKeeper.ID)
            {
                return BadRequest();
            }

            db.Entry(shopKeeper).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopKeeperExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ShopKeepers
        [ResponseType(typeof(ShopKeeper))]
        public IHttpActionResult PostShopKeeper(ShopKeeper shopKeeper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShopKeepers.Add(shopKeeper);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shopKeeper.ID }, shopKeeper);
        }

        // DELETE: api/ShopKeepers/5
        [ResponseType(typeof(ShopKeeper))]
        public IHttpActionResult DeleteShopKeeper(int id)
        {
            ShopKeeper shopKeeper = db.ShopKeepers.Find(id);
            if (shopKeeper == null)
            {
                return NotFound();
            }

            db.ShopKeepers.Remove(shopKeeper);
            db.SaveChanges();

            return Ok(shopKeeper);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShopKeeperExists(int id)
        {
            return db.ShopKeepers.Count(e => e.ID == id) > 0;
        }
    }
}