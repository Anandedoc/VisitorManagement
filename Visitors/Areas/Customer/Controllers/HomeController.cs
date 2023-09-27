namespace Visitors.Areas.Customer.Controllers
{
    [Area("Customer")]
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        //[Authorize(Roles = nameof(UserRoleTypeValues.Admin))]
        [Authorize]
        public IActionResult Index()
        {
            var details = _unitOfWork.VisitorDetails.GetAll();
            var total = 0;
            var completed = 0;
            var pending = 0;
            var cancelled = 0;
            var todayDate = DateTime.Now.ToString("yyyy-MM-dd");

            if (!User.IsInRole(nameof(UserRoleTypeValues.User)))
            {
                total = details.Where(x => x.Date == todayDate).Count();
                pending = details.Where(x => x.Date == todayDate).Where(x => x.VisitingStatus == VisitingStatus.Pending).Count();
                completed = details.Where(x => x.Date == todayDate).Where(x => x.VisitingStatus == VisitingStatus.Completed).Count();
                cancelled = details.Where(x => x.Date == todayDate).Where(x => x.VisitingStatus == VisitingStatus.Cancelled).Count();
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                total = details.Where(x => x.Date == todayDate).Where(x => x.ToUserId == claims.Value).Count();
                pending = details.Where(x => x.Date == todayDate).Where(x => x.ToUserId == claims.Value).Where(x => x.VisitingStatus == VisitingStatus.Pending).Count();
                completed = details.Where(x => x.Date == todayDate).Where(x => x.ToUserId == claims.Value).Where(x => x.VisitingStatus == VisitingStatus.Completed).Count();
                cancelled = details.Where(x => x.Date == todayDate).Where(x => x.ToUserId == claims.Value).Where(x => x.VisitingStatus == VisitingStatus.Cancelled).Count();

            }

            var countDetails = new DashBoardCount()
            {
                TotalAppointments = total,
                TotalPending = pending,
                TotalCanceled = cancelled,
                TotalCompleted = completed
            };

            return View(countDetails);
        }

        public IActionResult History()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region API CALLS
        [HttpGet]

        public IActionResult GetAll(string status)
        {
            IEnumerable<VisitorDetails> visitorDetails;

            var todayDate = DateTime.Now.ToString("yyyy-MM-dd");


            if (!User.IsInRole(nameof(UserRoleTypeValues.User)))
            {
                visitorDetails = _unitOfWork.VisitorDetails.GetAll(includeProperties: "User").Where(x => x.Date == todayDate).OrderByDescending(x => x.AppointMentTime);
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                visitorDetails = _unitOfWork.VisitorDetails.GetAll(x => x.ToUserId == claim.Value, includeProperties: "User").Where(x => x.Date == todayDate).OrderBy(x => x.AppointMentTime);
            }

            switch (status)
            {
                case "pending":
                    visitorDetails = visitorDetails.Where(x => x.VisitingStatus == VisitingStatus.Pending);
                    break;
                case "completed":
                    visitorDetails = visitorDetails.Where(x => x.VisitingStatus == VisitingStatus.Completed);
                    break;
                case "cancelled":
                    visitorDetails = visitorDetails.Where(x => x.VisitingStatus == VisitingStatus.Cancelled);
                    break;
                default:
                    break;

            }

            List<object> newVisitorDetails = new List<object>();

            foreach (VisitorDetails detail in visitorDetails)
            {
                var departmentName = _unitOfWork.Departments.GetFirstOrDefault(x => x.Id == detail.User.DepartmentId).Name;

                var temp = new
                {
                    id = detail.Id,
                    name = detail.Name,
                    department = departmentName ?? "",
                    phone = detail.Phone,
                    toUserName = detail.User?.Name,
                    email = detail.Email,
                    time = detail.AppointMentTime?.ToString("hh:mm:ss tt"),
                    inTime = detail.InTime?.ToString("hh:mm:ss tt"),
                    outTime = detail.OutTime?.ToString("hh:mm:ss tt"),

                };
                newVisitorDetails.Add(temp);

            }

            return Json(new { data = newVisitorDetails });
        }

        [HttpGet]
        public IActionResult GetAllHistory(string status)
        {
            IEnumerable<VisitorDetails> visitorDetails;

            if (!User.IsInRole(nameof(UserRoleTypeValues.User)))
            {
                visitorDetails = _unitOfWork.VisitorDetails.GetAll(includeProperties: "User");


                switch (status)
                {
                    case "pending":
                        visitorDetails = visitorDetails.Where(x => x.VisitingStatus == VisitingStatus.Pending);
                        break;
                    case "completed":
                        visitorDetails = visitorDetails.Where(x => x.VisitingStatus == VisitingStatus.Completed);
                        break;
                    case "cancelled":
                        visitorDetails = visitorDetails.Where(x => x.VisitingStatus == VisitingStatus.Cancelled);
                        break;
                    default:
                        break;

                }

            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                visitorDetails = _unitOfWork.VisitorDetails.GetAll(x => x.ToUserId == claim.Value, includeProperties: "User");

                switch (status)
                {
                    case "pending":
                        visitorDetails = visitorDetails.Where(x => x.ToUserId == claim.Value).Where(x => x.VisitingStatus == VisitingStatus.Pending);
                        break;
                    case "completed":
                        visitorDetails = visitorDetails.Where(x => x.ToUserId == claim.Value).Where(x => x.VisitingStatus == VisitingStatus.Completed);
                        break;
                    case "cancelled":
                        visitorDetails = visitorDetails.Where(x => x.ToUserId == claim.Value).Where(x => x.VisitingStatus == VisitingStatus.Cancelled);
                        break;
                    default:
                        visitorDetails = visitorDetails.Where(x => x.ToUserId == claim.Value);
                        break;

                }
            }

            List<object> newVisitorDetails = new List<object>();

            foreach (VisitorDetails detail in visitorDetails)
            {
                var departmentName = _unitOfWork.Departments.GetFirstOrDefault(x => x.Id == detail.User.DepartmentId)?.Name;

                var temp = new
                {
                    id = detail.Id,
                    name = detail.Name,
                    department = departmentName ?? "",
                    phone = detail.Phone,
                    toUserName = detail.User?.Name,
                    email = detail.Email,
                    time = detail.AppointMentTime?.ToString("hh:mm:ss tt"),
                    inTime = detail.InTime?.ToString("hh:mm:ss tt"),
                    outTime = detail.OutTime?.ToString("hh:mm:ss tt"),
                };
                newVisitorDetails.Add(temp);

            }
            return Json(new { data = newVisitorDetails });


        }

        #endregion
    }
}