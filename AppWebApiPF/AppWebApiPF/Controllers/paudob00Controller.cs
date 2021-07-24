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
    public class paudob00Controller : ApiController
    {
        private bdtexEntities db = new bdtexEntities();

        // GET: api/paudob00
        public IQueryable<paudob00> Getpaudob00()
        {
            return db.paudob00;
        }

        // GET: api/paudob00/5
        [ResponseType(typeof(paudob00))]
        public IHttpActionResult Getpaudob00(int id)
        {
            paudob00 paudob00 = db.paudob00.Find(id);
            if (paudob00 == null)
            {
                return NotFound();
            }

            return Ok(paudob00);
        }

        // PUT: api/paudob00/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpaudob00(int id, paudob00 paudob00)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paudob00.idobse)
            {
                return BadRequest();
            }

            db.Entry(paudob00).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!paudob00Exists(id))
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

        // POST: api/paudob00
        [ResponseType(typeof(paudob00))]
        public IHttpActionResult Postpaudob00(paudob00 paudob00)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.paudob00.Add(paudob00);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = paudob00.idobse }, paudob00);
        }

        // DELETE: api/paudob00/5
        [ResponseType(typeof(paudob00))]
        public IHttpActionResult Deletepaudob00(int id)
        {
            paudob00 paudob00 = db.paudob00.Find(id);
            if (paudob00 == null)
            {
                return NotFound();
            }

            db.paudob00.Remove(paudob00);
            db.SaveChanges();

            return Ok(paudob00);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool paudob00Exists(int id)
        {
            return db.paudob00.Count(e => e.idobse == id) > 0;
        }
    }
}