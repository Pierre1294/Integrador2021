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
    public class paudit01Controller : ApiController
    {
        private bdtexEntities db = new bdtexEntities();

        // GET: api/paudit01
        public IQueryable<paudit01> Getpaudit01()
        {
            return db.paudit01;
        }

        // GET: api/paudit01/5
        [ResponseType(typeof(paudit01))]
        public IHttpActionResult Getpaudit01(string id)
        {
            paudit01 paudit01 = db.paudit01.Find(id);
            if (paudit01 == null)
            {
                return NotFound();
            }

            return Ok(paudit01);
        }

        // PUT: api/paudit01/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpaudit01(string id, paudit01 paudit01)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paudit01.careas)
            {
                return BadRequest();
            }

            db.Entry(paudit01).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!paudit01Exists(id))
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

        // POST: api/paudit01
        [ResponseType(typeof(paudit01))]
        public IHttpActionResult Postpaudit01(paudit01 paudit01)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.paudit01.Add(paudit01);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (paudit01Exists(paudit01.careas))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = paudit01.careas }, paudit01);
        }

        // DELETE: api/paudit01/5
        [ResponseType(typeof(paudit01))]
        public IHttpActionResult Deletepaudit01(string id)
        {
            paudit01 paudit01 = db.paudit01.Find(id);
            if (paudit01 == null)
            {
                return NotFound();
            }

            db.paudit01.Remove(paudit01);
            db.SaveChanges();

            return Ok(paudit01);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool paudit01Exists(string id)
        {
            return db.paudit01.Count(e => e.careas == id) > 0;
        }
    }
}