namespace Visitors.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class DepartmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var objCategories = _unitOfWork.Departments.GetAll();
            return View(objCategories);
        }
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Departments.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Department Created Successfully";
                return Redirect("Index");
            }
            return View(obj);

        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var departmentToBeEdit = _unitOfWork.Departments.GetFirstOrDefault(x => x.Id == id);

            if (departmentToBeEdit == null)
                return NotFound();

            return View(departmentToBeEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Departments.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Department Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var departmentToBeDelete = _unitOfWork.Departments.GetFirstOrDefault(x => x.Id == id);

            if (departmentToBeDelete == null)
                return NotFound();

            return View(departmentToBeDelete);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {
            var departmentToBeDelete = _unitOfWork.Departments.GetFirstOrDefault(x => x.Id == id);

            if (departmentToBeDelete == null)
            {
                return NotFound();
            }
            _unitOfWork.Departments.Remove(departmentToBeDelete);
            _unitOfWork.Save();
            TempData["success"] = "Department Deleted Successfully";

            return RedirectToAction("Index");


        }

    }

}
