using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.BAL
{
    public class MiscellaneousService : Service, IService<MiscellaneousMaster, ReturnModel<MiscellaneousMaster>>
    {
        public ReturnModel<MiscellaneousMaster> Delete(MiscellaneousMaster model)
        {
            throw new NotImplementedException();
        }

        public ReturnModel<MiscellaneousMaster> GetAll()
        {
            throw new NotImplementedException();
        }

        public ReturnModel<MiscellaneousMaster> GetSingle(MiscellaneousMaster model)
        {
            throw new NotImplementedException();
        }

        public ReturnModel<MiscellaneousMaster> Insert(MiscellaneousMaster model)
        {
            throw new NotImplementedException();
        }

        public ReturnModel<MiscellaneousMaster> Update(MiscellaneousMaster model)
        {
            throw new NotImplementedException();
        }

        public ReturnModel<MiscellaneousMaster> GetAllUsingMiscellaneousType(MiscellaneousMaster model)
        {
            return new ReturnModel<MiscellaneousMaster>()
            {
                code = "0",
                data = uow.MiscellaneousMaster.GetAll(o=>o.MiscellaneousType == model.MiscellaneousType && o.Disabled == false).Select(p=>new MiscellaneousMaster()
                {
                    MiscellaneousType = p.MiscellaneousType,
                    MiscellaneousKey = p.MiscellaneousKey,
                    MiscellaneousValue = p.MiscellaneousValue
                })
            };
        }
        public ReturnModel<MiscellaneousMaster> GetAllState()
        {
            return new ReturnModel<MiscellaneousMaster>()
            {
                code = "0",
                data = uow.MiscellaneousMaster.GetAll(o => o.MiscellaneousType == "state" && o.Disabled == false).OrderBy(u=>u.MiscellaneousKey).Select(p => new MiscellaneousMaster()
                {
                    MiscellaneousType = p.MiscellaneousType,
                    MiscellaneousKey = p.MiscellaneousKey,
                    MiscellaneousValue = p.MiscellaneousValue
                })
            };
        }
    }
}
