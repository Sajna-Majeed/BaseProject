using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Security;
using Microsoft.AspNetCore.Http;
using System.Data;
using static System.Net.WebRequestMethods;

namespace Core.Services
{


    public class InvoiceService : IInvoiceService
    {
        private readonly IExternalApiService _externalApi;
        private string url = "invoice/document-status/" + Guid.NewGuid();
        public InvoiceService(IExternalApiService externalApi)
        {
            _externalApi = externalApi;
        }

        public async Task<object?> GetInvoiceTraking()
        {
            // use relative URL instead of full URL
             

        var result = await _externalApi.GetAsync<object>(url);

            if (!result.Success)
                return result.ErrorMessage ?? "External API failed";

            return result;
        }

        public async Task<string> SetInvoiceTraking()
        {
            var payload = new
            {
                name = "test"
            };

            var response = await _externalApi.PostAsync<object>(
                "v1/some-endpoint",   // relative URL
                payload);

            if (!response.Success)
                return response.ErrorMessage ?? "External API POST failed";

            return "Success";
        }
    }


}
