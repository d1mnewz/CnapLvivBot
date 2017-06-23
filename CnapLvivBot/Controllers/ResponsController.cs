using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;

namespace CnapLvivBot.Controllers
{
    public class ResponsController : Controller
    {
        private сnapEntities _db = new сnapEntities();

        // GET: Respons
        public async Task<ActionResult> Index()
        {
            var responses = _db.Responses.Include(r => r.Intent);
            return View(await responses.ToListAsync());
        }

        // GET: Respons/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respons respons = await _db.Responses.FindAsync(id);
            if (respons == null)
            {
                return HttpNotFound();
            }
            return View(respons);
        }

        // GET: Respons/Create/
        public ActionResult Create(int id = 0)
        {
            if (id != 0)
                ViewBag.intentId = new SelectList(_db.Intents, "Id", "Content", id);
            else
            {
                var intents = _db.Intents.ToArray();
                ViewBag.intentId = new SelectList(_db.Intents, "Id", "Content", intents.Length);
            }
            return View();
        }

        // POST: Respons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Content,IntentId")] Respons respons)
        {
            if (ModelState.IsValid)
            {
                _db.Responses.Add(respons);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IntentId = new SelectList(_db.Intents, "Id", "Content", respons.IntentId);
            return View(respons);
        }

        // GET: Respons/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respons respons = await _db.Responses.FindAsync(id);
            if (respons == null)
            {
                return HttpNotFound();
            }
            ViewBag.IntentId = new SelectList(_db.Intents, "Id", "Content", respons.IntentId);
            return View(respons);
        }

        // POST: Respons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Content,IntentId")] Respons respons)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(respons).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IntentId = new SelectList(_db.Intents, "Id", "Content", respons.IntentId);
            return View(respons);
        }

        // GET: Respons/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respons respons = await _db.Responses.FindAsync(id);
            if (respons == null)
            {
                return HttpNotFound();
            }
            return View(respons);
        }

        // POST: Respons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Respons respons = await _db.Responses.FindAsync(id);
            _db.Responses.Remove(respons);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
