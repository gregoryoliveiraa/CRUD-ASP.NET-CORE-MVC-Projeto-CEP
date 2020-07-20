using e.mix.Models;
using System.Threading;
using System.Threading.Tasks;

namespace e.mix.Services.Interfaces
{
    public interface IBuscaCEPClient
    {
        Task<BuscaCEPResult> GetAsync(string cep, CancellationToken cancellationToken);
    }
}
