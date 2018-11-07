using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListManager.Core.Models;

namespace TodoListManager.Core.Services
{
    public interface IJsonService
    {
        Task<IEnumerable<NewsModel>> GetFeedsAsync();
    }
}
