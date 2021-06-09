using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VechiclesInformation.Integrations
{
    public interface ISyncService
    {
        void SyncVehicleStatus();
    }
}
