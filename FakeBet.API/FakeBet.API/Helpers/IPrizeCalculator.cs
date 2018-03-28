using System.Threading.Tasks;
using FakeBet.API.Models;

namespace FakeBet.API.Helpers
{
    public interface IPrizeCalculator
    {
        Task CalculateAsync(Match match, Team winner);
    }
}