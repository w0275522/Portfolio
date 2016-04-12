using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace CarInfoForm
{
    class Truck:Car
    {
        [Column()]
        public string Axles;

        [Column()]
        public string Tonnage;
    }
}
