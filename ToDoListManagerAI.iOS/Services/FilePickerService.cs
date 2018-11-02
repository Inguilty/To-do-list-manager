using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using TodoListManager.Core.Models;
using TodoListManager.Core.Services;
using UIKit;

namespace ToDoListManagerAI.iOS.Services
{
    public class FilePickerService : IFilePickerService
    {
        private readonly IDbService _dbService;
        public FilePickerService(IDbService dbService)
        {
            _dbService = dbService;
        }

        public Task<PickedFileModel> UploadImageAsync(int userId)
        {
            var ctrl = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var source = new TaskCompletionSource<PickedFileModel>();

            var imagePicker = new UIImagePickerController();
            imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
            imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(imagePicker.SourceType);

            imagePicker.FinishedPickingMedia += (sender, e) =>
            {
                bool isImage = false;
                switch (e.Info[UIImagePickerController.MediaType].ToString())
                {
                    case "public.image":
                        isImage = true;
                        break;
                    case "public.video":
                        break;
                }

                var referenceUrl = e.Info[new NSString("UIImagePickerControllerReferenceURL")] as NSUrl;
                if (referenceUrl != null)
                    Console.WriteLine("Url:" + referenceUrl.ToString());

                if (isImage)
                {
                    if (e.Info[UIImagePickerController.OriginalImage] is UIImage originalImage)
                    {
                        originalImage = GetUserSquaredImage(originalImage);

                        using (var imgData = originalImage.AsJPEG().AsStream())
                        {
                            var memStream = new MemoryStream();
                            imgData.CopyTo(memStream);
                            byte[] bytes = memStream.ToArray();

                            var user = _dbService.GetItem<UserModel>(userId);
                            if (user != null)
                            {
                                user.photo = bytes;
                                _dbService.SaveItem(user);
                            }

                            source.SetResult(new PickedFileModel
                            {
                                Name = originalImage.AccessibilityLabel,
                                ImageBytes = bytes
                            });
                        }
                    }

                }
                else
                {
                    var mediaUrl = e.Info[UIImagePickerController.MediaURL] as NSUrl;
                    if (mediaUrl != null)
                    {
                        Console.WriteLine(mediaUrl.ToString());
                    }
                }
                imagePicker.DismissViewController(true, null);
            };
            imagePicker.Canceled += (sender, e) =>
            {
                source.SetResult(new PickedFileModel());
                imagePicker.DismissViewController(true, null);
            };

            ctrl.PresentModalViewController(imagePicker, true);
            return source.Task;
        }

        public Task<PickedFileModel> NewUserUploadImageAsync()
        {
            var ctrl = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var source = new TaskCompletionSource<PickedFileModel>();

            var imagePicker = new UIImagePickerController
            {
                SourceType = UIImagePickerControllerSourceType.PhotoLibrary
            };
            imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(imagePicker.SourceType);

            imagePicker.FinishedPickingMedia += (sender, e) =>
            {
                bool isImage = false;
                switch (e.Info[UIImagePickerController.MediaType].ToString())
                {
                    case "public.image":
                        isImage = true;
                        break;
                    case "public.video":
                        break;
                }

                var referenceUrl = e.Info[new NSString("UIImagePickerControllerReferenceURL")] as NSUrl;
                if (referenceUrl != null)
                    Console.WriteLine("Url:" + referenceUrl.ToString());

                if (isImage)
                {
                    if (e.Info[UIImagePickerController.OriginalImage] is UIImage originalImage)
                    {
                        originalImage = GetUserSquaredImage(originalImage);

                        using (var imgData = originalImage.AsJPEG().AsStream())
                        {
                            var memStream = new MemoryStream();
                            imgData.CopyTo(memStream);
                            byte[] bytes = memStream.ToArray();

                            source.SetResult(new PickedFileModel
                            {
                                Name = originalImage.AccessibilityLabel,
                                ImageBytes = bytes
                            });
                        }
                    }

                }
                else
                {
                    var mediaUrl = e.Info[UIImagePickerController.MediaURL] as NSUrl;
                    if (mediaUrl != null)
                    {
                        Console.WriteLine(mediaUrl.ToString());
                    }
                }
                imagePicker.DismissViewController(true, null);
            };
            imagePicker.Canceled += (sender, e) =>
            {
                source.SetResult(new PickedFileModel());
                imagePicker.DismissViewController(true, null);
            };

            ctrl.PresentModalViewController(imagePicker, true);
            return source.Task;
        }

        private UIImage GetUserSquaredImage(UIImage originalImage)
        {
            if (originalImage == null)
            {
                return null;
            }

            var orientation = originalImage.Orientation;

            //originalImage = Rotate(originalImage, orientation);

            CGSize inputSize = originalImage.Size;
            nfloat outputLength = (nfloat)Math.Min(inputSize.Width, inputSize.Height);
            CGSize outputSize = new CGSize(outputLength, outputLength);
            CGRect outputRect = new CGRect(inputSize.Width / 2.0 - outputSize.Width / 2.0,
                inputSize.Height / 2.0 - outputSize.Height / 2.0,
                outputSize.Width,
                outputSize.Height);
            UIImage outImage = null;
            using (CGImage cr = originalImage.CGImage.WithImageInRect(outputRect))
            {
                outImage = UIImage.FromImage(cr);
            }

            outImage = outImage?.Scale(new CGSize(outputLength, outputLength));

            if (outImage != null)
            {
                return outImage;
            }

            return originalImage;
        }

    }
}