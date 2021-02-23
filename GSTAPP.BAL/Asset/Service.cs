using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.BAL
{
    public class Service
    {
        protected UOW uow = default(UOW);

        public Service()
        {
            uow = new UOW();
        }
    }
}
