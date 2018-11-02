using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoListManager.Core.Models;

namespace TodoListManager.Core.Services
{
    public interface IFilePickerService
    {
        Task<PickedFileModel> UploadImageAsync(int userId);
        Task<PickedFileModel> NewUserUploadImageAsync();
    }
}
