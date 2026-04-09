using ConnectPro.SharedKernel;
using Messaging.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Infrastructure.Services
{
    public class UserExistenceChecker : IUserExistenceChecker
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserExistenceChecker(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> ExistsAsync(Guid userId, CancellationToken ct = default)
        {
            var client = _httpClientFactory.CreateClient("IdentityBC");
            var response = await client.GetAsync($"/api/users/{userId}/exists", ct);
            return response.IsSuccessStatusCode;
        }
    }
}
