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
using LicentaAPI.Models;

namespace LicentaAPI.Controllers
{
    public class CANDIDATESController : ApiController
    {
        private noBordersEntities db = new noBordersEntities();

        // GET: api/CANDIDATES
        public IQueryable<CANDIDATES> GetCANDIDATES()
        {
            return db.CANDIDATES;
        }

        // GET: api/CANDIDATES/5
        [ResponseType(typeof(CANDIDATES))]
        public IHttpActionResult GetCANDIDATES(int id)
        {
            CANDIDATES cANDIDATES = db.CANDIDATES.Find(id);
            if (cANDIDATES == null)
            {
                return NotFound();
            }

            return Ok(cANDIDATES);
        }

        // PUT: api/CANDIDATES/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCANDIDATES(int id, CANDIDATES cANDIDATES)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cANDIDATES.id_candidate)
            {
                return BadRequest();
            }

            db.Entry(cANDIDATES).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CANDIDATESExists(id))
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

        // POST: api/CANDIDATES
        [ResponseType(typeof(CANDIDATES))]
        public IHttpActionResult PostCANDIDATES(CANDIDATES cANDIDATES)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CANDIDATES.Add(cANDIDATES);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cANDIDATES.id_candidate }, cANDIDATES);
        }

        // DELETE: api/CANDIDATES/5
        [ResponseType(typeof(CANDIDATES))]
        public IHttpActionResult DeleteCANDIDATES(int id)
        {
            CANDIDATES cANDIDATES = db.CANDIDATES.Find(id);
            if (cANDIDATES == null)
            {
                return NotFound();
            }

            db.CANDIDATES.Remove(cANDIDATES);
            db.SaveChanges();

            return Ok(cANDIDATES);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CANDIDATESExists(int id)
        {
            return db.CANDIDATES.Count(e => e.id_candidate == id) > 0;
        }
    }
}