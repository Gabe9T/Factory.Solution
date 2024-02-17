using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Factory.Controllers
{
    public class MachinesController : Controller
    {
        private readonly FactoryContext _db;

        public MachinesController(FactoryContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View(_db.Machines.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Machine machine)
        {
            _db.Machines.Add(machine);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var thisMachine = _db.Machines
                .Include(machine => machine.JoinEntities)
                .ThenInclude(join => join.Engineer)
                .FirstOrDefault(machine => machine.MachineId == id);
            return View(thisMachine);
        }

        public ActionResult Edit(int id)
        {
            var thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
            return View(thisMachine);
        }

        [HttpPost]
        public ActionResult Edit(Machine machine)
        {
            _db.Entry(machine).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
            return View(thisMachine);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
            _db.Machines.Remove(thisMachine);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        
[HttpGet]
public ActionResult AddMachine(int id)
{
    var engineer = _db.Engineers.FirstOrDefault(e => e.EngineerId == id);
    if (engineer == null)
    {
        return NotFound();
    }

    ViewBag.EngineerId = engineer.EngineerId;
    ViewBag.Machines = _db.Machines.ToList();
    return View(engineer);
}

[HttpPost]
public ActionResult AddEngineer(int id, int engineerId)
{
    var machine = _db.Machines.Include(m => m.JoinEntities).FirstOrDefault(m => m.MachineId == id);
    var engineer = _db.Engineers.FirstOrDefault(e => e.EngineerId == engineerId);

    if (machine != null && engineer != null)
    {
        if (machine.JoinEntities.Any(j => j.EngineerId == engineerId))
        {
            return RedirectToAction("Details", new { id = id });
        }

        var joinEntity = new MachineEngineer { MachineId = id, EngineerId = engineerId };
        _db.MachineEngineers.Add(joinEntity);
        _db.SaveChanges();
    }

    return RedirectToAction("Details", new { id = id });
}

    }
}