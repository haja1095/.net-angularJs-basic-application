using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.BAL
{
    public interface IService<T1, T2>
    {
        T2 GetSingle(T1 model);
        T2 GetAll();
        T2 Insert(T1 model);
        T2 Update(T1 model);
        T2 Delete(T1 model);
    }
}
