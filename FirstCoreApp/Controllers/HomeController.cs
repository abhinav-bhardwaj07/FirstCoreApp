using FirstCoreApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstCoreApp.Controllers
{
    [Route("home")]
    public class HomeController: Controller
    {
        private IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [Route("index/{id}")]
        public ViewResult Index(int id)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            model.employee = _employeeRepository.GetEmployee(id);
            model.Title = "Index page";

            return View(model);
        }

        [Route("Details")]
        public ViewResult GetEmployeeData()
        {
            var model = _employeeRepository.GetAll();
            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create( Employee emp)
        {
            if(ModelState.IsValid)
            {
                 Employee newEmp = _employeeRepository.Add(emp);
                return RedirectToAction("Details");
            }
            return View();
            
        }

        [HttpGet]
        [Route("Edit")]
        public IActionResult Edit (int id)
        {
            var empData = _employeeRepository.GetEmployee(id);
            return View(empData);
        }

        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit( Employee emp)
        {
            if (ModelState.IsValid)
            {
                 Employee newEmp = _employeeRepository.Update(emp);
                return RedirectToAction("Index", new {id = emp.Id});
            }
            return View();

        }

        public IActionResult Delete(int id)
        {
            _employeeRepository.Delete(id);
            return RedirectToAction("Details");

        }


    }
}

// Pass data from controller -> view :
// View data, view bag, strongly typed views, view models
//CRUD => Create, Read, Update, Delete
