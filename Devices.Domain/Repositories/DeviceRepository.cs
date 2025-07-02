using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devices.Domain.Data;
using Devices.Domain.Models;
using Microsoft.Extensions.Options;

namespace Devices.Domain.Repositories
{
    public class DeviceRepository : BaseRepository<Device>
    {
        public DeviceRepository(IOptions<DatabaseSettings> options) : base(options)
        {
            
        }
        
    }
}
