using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Factory.Controllers
{
    public class EngineersController : Controller
    {
         private readonly FactoryContext _db;

    public EngineersController(FactoryContext db)
    {
        _db = db;
    }

        public ActionResult Index()
        {
            return View(_db.Engineers.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Engineer engineer)
        {
            _db.Engineers.Add(engineer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var thisEngineer = _db.Engineers
                .Include(engineer => engineer.JoinEntities)
                .ThenInclude(join => join.Machine)
                .FirstOrDefault(engineer => engineer.EngineerId == id);
            return View(thisEngineer);
        }

        public ActionResult Edit(int id)
        {
            var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
            return View(thisEngineer);
        }

        [HttpPost]
        public ActionResult Edit(Engineer engineer)
        {
            _db.Entry(engineer).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
            return View(thisEngineer);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
            _db.Engineers.Remove(thisEngineer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

[HttpGet]
public ActionResult AddEngineer(int id)
{
    var machine = _db.Machines.FirstOrDefault(m => m.MachineId == id);
    if (machine == null)
    {
        return NotFound();
    }

    ViewBag.MachineId = machine.MachineId;
    ViewBag.Engineers = _db.Engineers.ToList();
    return View(machine);
}

[HttpPost]
public ActionResult AddMachine(int id, int machineId)
{
    var engineer = _db.Engineers.Include(e => e.JoinEntities).FirstOrDefault(e => e.EngineerId == id);
    var machine = _db.Machines.FirstOrDefault(m => m.MachineId == machineId);
    
    if (engineer != null && machine != null)
    {
        if (engineer.JoinEntities.Any(j => j.MachineId == machineId))
        {
            return RedirectToAction("Details", new { id = id });
        }

        var joinEntity = new MachineEngineer { EngineerId = id, MachineId = machineId };
        _db.MachineEngineers.Add(joinEntity);
        _db.SaveChanges();
    }

    return RedirectToAction("Details", new { id = id });
}
    }
}