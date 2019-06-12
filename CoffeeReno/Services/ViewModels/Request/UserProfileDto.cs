using System;
using System.Collections.Generic;
using System.Text;

namespace Services.ViewModels.Request
{
    public class UserProfileDto : PagingRequest
    {
    }
    public class UserProfileModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}
