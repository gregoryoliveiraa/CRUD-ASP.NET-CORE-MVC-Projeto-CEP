using e.mix.Models;
using e.mix.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace e.mix.Services
{
    public class BuscaCepClient : IBuscaCEPClient
    {
        private readonly HttpClient _httpClient;

        public BuscaCepClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BuscaCEPResult> GetAsync(string cep, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"/ws/{cep}/json", cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<BuscaCEPResult>(cancellationToken).ConfigureAwait(false);
        }
    }
}