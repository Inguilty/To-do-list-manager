
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Views;
using TodoListManager.Core.Models;
using TodoListManager.Core.ViewModels;
using ToDoListManagerAI.iOS.Services;
using ToDoListManagerAI.iOS.Views.Cells;
using UIKit;

namespace ToDoListManagerAI.iOS.Views.Tabs
{
    public partial class NewsTabView : MvxViewController<NewsTabViewModel>
    {
        private UIRefreshControl _refreshControl;
        bool _useRefreshControl = false;
        private CustomSimpleTableViewSource _source;
        private readonly ProgressDialogHandle _loading;
        public NewsTabView() : base(nameof(NewsTabView), null)
        {
            _loading = new ProgressDialogHandle("", "Loading data..");
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _loading.BeginAnimation();
            InitializeNews();
            Title = ViewModel.Title;

            _source = new CustomSimpleTableViewSource(tableNews, NewsTableViewCell.Key, NewsTableViewCell.Key);

            var set = this.CreateBindingSet<NewsTabView, NewsTabViewModel>();
            set.Bind(_source).To(vm => vm.News);
            set.Apply();

            tableNews.Source = _source;
            tableNews.ContentInset = UIEdgeInsets.FromString("20.0, 20.0, 20.0, 20.0");
            tableNews.RowHeight = 215;
            tableNews.ReloadData();

            
            _source.SelectedItemChanged += ActionSheetButtonsTouchUpInside;

            Refresh();

            AddRefreshControl();
            Add(tableNews);
            tableNews.Add(_refreshControl);
        }

        protected void ActionSheetButtonsTouchUpInside(object sender, EventArgs e)
        {
            var source = sender as MvxSimpleTableViewSource;
            var currCell = source.SelectedItem as NewsModel;
            ViewModel.GoToSourceCommand(currCell);
            tableNews.ReloadData();
        }

        private void Refresh()
        {
            // only activate the refresh control if the feature is available  
            if (_useRefreshControl)
                _refreshControl.BeginRefreshing();

            if (_useRefreshControl)
                _refreshControl.EndRefreshing();

            tableNews.ReloadData();

        }

        // This method will add the UIRefreshControl to the table view if  
        // it is available, ie, we are running on iOS 6+  
        void AddRefreshControl()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(6, 0))
            {
                // the refresh control is available, let's add it  
                _refreshControl = new UIRefreshControl();
                _refreshControl.ValueChanged += (sender, e) =>
                {
                    tableNews.Source = _source;
                    tableNews.ContentInset = UIEdgeInsets.FromString("20.0, 20.0, 20.0, 20.0");
                    tableNews.RowHeight = 215;
                    Refresh();
                    _useRefreshControl = false;
                };
                
                _useRefreshControl = true;
            }
        }

        private async void InitializeNews()
        {
            await ViewModel.Initialize();
            await _loading.Close();
        }
    }
    public class CustomSimpleTableViewSource : MvxSimpleTableViewSource
    {
        public CustomSimpleTableViewSource(UITableView table, NSString firstKey, NSString secondKey)
            : base(table, firstKey, secondKey)
        {

        }

        public override void WillDisplay(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
        {
            var newsData = GetItemAt(indexPath);
            NewsModel model = (NewsModel)newsData;
            Foundation.NSData data = NSData.FromUrl(new NSUrl(model.Picture));
            ((NewsTableViewCell)cell).SetNewsImageCommand.Execute(new UIImage(data));
        }

    }
}