using System;
using System.Collections.Generic;
using System.Text;

namespace cms_api.entities.entities
{
    public class ExceptionStack
    {
        public bool IsSuccess { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorModule { get; set; }
    }
}
