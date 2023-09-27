using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Visitors.ViewModel
{
    public class VisitorDetailsVM
    {
        public VisitorDetails VisitorDetails { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> VisitingStatusList { get; set; }
    }
}
