using System;
using System.Windows.Input;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Binding.Views;
using TodoListManager.Core.Models;
using UIKit;

namespace ToDoListManagerAI.iOS.Views.Cells
{
    public partial class NewsTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("NewsTableViewCell");
        public static readonly UINib Nib;

        static NewsTableViewCell()
        {
            Nib = UINib.FromName("NewsTableViewCell", NSBundle.MainBundle);
        }

        public ICommand SetNewsImageCommand => new MvxCommand<UIImage>(SetNewsImage);
        public void SetNewsImage(UIImage img)
        {
            imageNews.Image = img;
        }

        protected NewsTableViewCell(IntPtr handle) : base(handle)
        {        
            this.DelayBind(()=>
            {
                var set = this.CreateBindingSet<NewsTableViewCell, NewsModel>();
                set.Bind(lblTitle).To(vm => vm.Title);              
                set.Bind(lblDescription).To(vm => vm.Description);
                set.Bind(lblPublishingDate).To(vm => vm.PublicationDate);                
                set.Apply();           
            });      
        }
    }
}
