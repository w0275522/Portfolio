using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Data.Linq;


namespace CarInfoForm
{
    class Db : DataContext
    {
        public Table<Car> cars;
        public Db() : base(@"Data Source=(localdb)\Projects;Initial Catalog=Cars;Integrated Security=True")
        {

        }
    }
}
