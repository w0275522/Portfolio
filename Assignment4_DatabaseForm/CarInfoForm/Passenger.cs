using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace CarInfoForm
{
    class Passenger:Car
    {
        [Column()]
        public string TrimCode;
    }
}
