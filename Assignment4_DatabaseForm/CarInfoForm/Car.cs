using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace CarInfoForm
{
    [Table]
    [InheritanceMapping(Code = "P", Type = typeof(Passenger))]
    [InheritanceMapping(Code = "T", Type = typeof(Truck))]
    [InheritanceMapping(Code = "C", Type = typeof(Car), IsDefault = true)]

    class Car
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int id;
        [Column]
        public string Model;
        [Column]
        public string Make;
        [Column]
        public string Year;
        [Column(IsDiscriminator = true)]
        public string type;
        [Column]
        public string VIN;
    }
}
