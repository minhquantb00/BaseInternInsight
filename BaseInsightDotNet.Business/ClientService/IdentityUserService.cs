using BaseInsightDotNet.DataAccess.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.ClientService
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly HttpClient _httpClient;
        private readonly CorrelationIdOptions _options;
        private readonly ICorrelationIdProvider _correlationIdProvider;
        public IdentityUserService(HttpClient httpClient,
            CorrelationIdOptions options,
            ICorrelationIdProvider correlationIdProvider)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _options = options;
            _correlationIdProvider = correlationIdProvider;
            if (_httpClient.DefaultRequestHeaders.Contains(_options.HttpHeaderName))
                _httpClient.DefaultRequestHeaders.Remove(_options.HttpHeaderName);
            _httpClient.DefaultRequestHeaders.Add(_options.HttpHeaderName, _correlationIdProvider.Get());
        }

        public async Task AddUserClaimsAsync(string userId, Dictionary<string, string> claims)
        {
            var uri = $"Users/{userId}/addClaims";
            var response = await _httpClient.PostAsJsonAsync(uri, claims);
            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveUserClaimsAsync(string userId, Dictionary<string, string> claims)
        {
            var uri = $"Users/{userId}/removeClaims";
            var response = await _httpClient.PostAsJsonAsync(uri, claims);
            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveUserClaimsByTypeAsync(string userName, string type)
        {
            var uri = $"Users/{userName}/claims/{type}";
            var response = await _httpClient.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
        }

        public async Task AddUserClaimsByTypeAsync(string userName, string type, List<string> claims)
        {
            var uri = $"Users/{userName}/claims/{type}";
            var response = await _httpClient.PostAsJsonAsync(uri, claims);
            response.EnsureSuccessStatusCode();
        }
        public async Task<List<string>> GetUserClaimsByTypeAsync(string userName, string type)
        {
            var uri = $"Users/{userName}/claims/{type}";
            var response = await _httpClient.GetAsync(uri);
            var result = await response.Content.ReadFromJsonAsync<List<string>>();
            response.EnsureSuccessStatusCode();
            return result;
        }
        public async Task AddUserRolesAsync(string userId, List<string> roles)
        {
            var uri = $"Users/{userId}/addRoles";
            var response = await _httpClient.PostAsJsonAsync(uri, roles);
            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveUserRolesAsync(string userId, List<string> roles)
        {
            var uri = $"Users/{userId}/removeRoles";
            var response = await _httpClient.PostAsJsonAsync(uri, roles);
            response.EnsureSuccessStatusCode();
        }
    }
}
