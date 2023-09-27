namespace Visitors.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class VisitorDetailsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public VisitorDetailsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Upsert(int id)
        {

            var statusList = Enum.GetValues(typeof(VisitingStatus));


            VisitorDetailsVM visitorDetailsVM = new()
            {
                VisitorDetails = new(),
                //UserList = _unitOfWork.User.GetAll().Select(
                //    x => new SelectListItem
                //    {
                //        Text = x.Name,
                //        Value = x.Id.ToString()
                //    }),
                VisitingStatusList = statusList
                .Cast<VisitingStatus>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                })
                .ToList()

            };
            if (id == 0 || id == null)
            {
                return View(visitorDetailsVM);
            }
            else
            {
                visitorDetailsVM.VisitorDetails = _unitOfWork.VisitorDetails.GetFirstOrDefault(x => x.Id == id);
                return View(visitorDetailsVM);
            }

        }

        public IActionResult Details(int id)
        {


            VisitorDetails visitorDetails = _unitOfWork.VisitorDetails.GetFirstOrDefault(x => x.Id == id, includeProperties: "User");
            return View(visitorDetails);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(VisitorDetailsVM obj)
        {
            var success = false;

            if (User.IsInRole(nameof(UserRoleTypeValues.User)) && ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                obj.VisitorDetails.ToUserId = claims.Value;

                if (obj.VisitorDetails.Id == 0)
                {
                    _unitOfWork.VisitorDetails.Add(obj.VisitorDetails);
                    TempData["success"] = "Appointment Created Successfully";

                }
                else
                {
                    _unitOfWork.VisitorDetails.UpdateStatus(obj.VisitorDetails);
                    TempData["success"] = "Appointment Updated Successfully";
                }
                success = true;

            }
            else if (User.IsInRole(nameof(UserRoleTypeValues.GateAdmin)))
            {

                _unitOfWork.VisitorDetails.UpdateTime(obj.VisitorDetails);

                TempData["success"] = "Appointment Updated Successfully";
                success = true;

            }

            if (success)
            {
                _unitOfWork.Save();
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
            return View(obj);

        }

    }
}
