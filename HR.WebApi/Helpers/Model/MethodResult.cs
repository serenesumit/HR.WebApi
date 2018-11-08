using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.WebApi.Helpers.Model
{
    public class MethodResult<T>
    {
        public string ErrorMessage { get; set; }

        public bool Failed { get; set; }

        public T Result { get; set; }
    }
}
