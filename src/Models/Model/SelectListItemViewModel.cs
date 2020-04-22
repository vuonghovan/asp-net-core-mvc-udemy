using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class SelectListItemViewModel : SelectListItem
    {
        public long Id { get; set; }
    }
}
