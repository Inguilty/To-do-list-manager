using System.Threading.Tasks;

namespace TodoListManager.Core.Services
{
    public interface IDialogHandle
    {
         Task Close();
    }
}
