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
using DataAccessPF;

namespace AppWebApiPF.Controllers
{
    public class pdefec01Controller : ApiController
    {        
        private bdtexEntities db = new bdtexEntities();


        // GET: api/pdefec01
        public IQueryable<pdefec01> Getpdefec01()
        {
            return db.pdefec01;
        }

        // GET: api/pdefec01/5
        [ResponseType(typeof(pdefec01))]
        public IHttpActionResult Getpdefec01(string id)
        {
            pdefec01 pdefec01 = db.pdefec01.Find(id);
            if (pdefec01 == null)
            {
                return NotFound();
            }

            return Ok(pdefec01);
        }

        // PUT: api/pdefec01/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpdefec01(string id, pdefec01 pdefec01)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pdefec01.careas)
            {
                return BadRequest();
            }

            db.Entry(pdefec01).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!pdefec01Exists(id))
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

        // POST: api/pdefec01
        [ResponseType(typeof(pdefec01))]
        public IHttpActionResult Postpdefec01(pdefec01 pdefec01)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.pdefec01.Add(pdefec01);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (pdefec01Exists(pdefec01.careas))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pdefec01.careas }, pdefec01);
        }

        // DELETE: api/pdefec01/5
        [ResponseType(typeof(pdefec01))]
        public IHttpActionResult Deletepdefec01(string id)
        {
            pdefec01 pdefec01 = db.pdefec01.Find(id);
            if (pdefec01 == null)
            {
                return NotFound();
            }

            db.pdefec01.Remove(pdefec01);
            db.SaveChanges();

            return Ok(pdefec01);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool pdefec01Exists(string id)
        {
            return db.pdefec01.Count(e => e.careas == id) > 0;
        }


    }
}