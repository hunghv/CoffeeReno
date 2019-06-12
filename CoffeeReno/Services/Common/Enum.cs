using System.ComponentModel.DataAnnotations;

namespace Services.Common
{
    public enum SortOrder
    {
        [Display(Name = "asc")]
        Asc,
        [Display(Name = "desc")]
        Desc,
        [Display(Name = "none")]
        None
    }
}
