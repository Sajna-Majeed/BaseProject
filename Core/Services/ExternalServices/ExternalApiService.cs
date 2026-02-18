using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Security;
using Core.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Services.ExternalServices
{
    public class ExternalApiService:IExternalApiService
    {
      

        private readonly HttpClient _httpClient;
        private readonly ILogger<ExternalApiService> _logger;

        public ExternalApiService(HttpClient httpClient,
                                  ILogger<ExternalApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ExternalApiResponse<T>> GetAsync<T>(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);

                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("External GET failed: {Status} {Content}",
                        response.StatusCode, content);

                    return ExternalApiResponse<T>.Fail(
                        $"External API error: {response.StatusCode}",
                        (int)response.StatusCode);
                }

                var data = JsonSerializer.Deserialize<T>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return ExternalApiResponse<T>.Ok(data!, (int)response.StatusCode);
            }
            catch (TaskCanceledException)
            {
                _logger.LogError("External GET timeout");
                return ExternalApiResponse<T>.Fail("Request timeout", 408);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "External GET exception");
                return ExternalApiResponse<T>.Fail("External API call failed", 500);
            }
        }

        public async Task<ExternalApiResponse<T>> PostAsync<T>(string url, object payload)
        {
            try
            {
                var json = JsonSerializer.Serialize(payload);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(url, content);

                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("External POST failed: {Status} {Content}",
                        response.StatusCode, result);

                    return ExternalApiResponse<T>.Fail(
                        $"External API error: {response.StatusCode}",
                        (int)response.StatusCode);
                }

                var data = JsonSerializer.Deserialize<T>(result,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return ExternalApiResponse<T>.Ok(data!, (int)response.StatusCode);
            }
            catch (TaskCanceledException)
            {
                _logger.LogError("External POST timeout");
                return ExternalApiResponse<T>.Fail("Request timeout", 408);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "External POST exception");
                return ExternalApiResponse<T>.Fail("External API call failed", 500);
            }
        }

    }
}
