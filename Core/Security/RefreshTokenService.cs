using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security
{
    public class RefreshTokenService
    {
        public string Generate()
            => Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        public DateTime Expiry()
            => DateTime.UtcNow.AddDays(7);
    }


}
