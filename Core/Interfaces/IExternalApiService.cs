using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IExternalApiService
    {
        Task<ExternalApiResponse<T>> GetAsync<T>(string url);
        Task<ExternalApiResponse<T>> PostAsync<T>(string url, object payload);
    }

}
