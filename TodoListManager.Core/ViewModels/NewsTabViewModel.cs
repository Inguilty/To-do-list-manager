using System.Linq;
using System.Threading.Tasks;
using Foundation;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using SafariServices;
using TodoListManager.Core.Models;
using TodoListManager.Core.Services;
using UIKit;


namespace TodoListManager.Core.ViewModels
{
    public class NewsTabViewModel : BaseViewModel<UserModel>
    {
        public NewsTabViewModel(IMvxNavigationService navigationService, IJsonService json)
            : base(navigationService)
        {
            Title = "News";
            _jsonService = json;
            News = new MvxObservableCollection<NewsModel>();
        }

        #region Fields and Properties
        private UserModel _user;
        private readonly IJsonService _jsonService;
        private MvxObservableCollection<NewsModel> _news;
        public MvxObservableCollection<NewsModel> News
        {
            get => _news;
            set
            {
                _news = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public override void Prepare(UserModel parameter)
        {
            _user = parameter;
        }

        public override async Task Initialize()
        {
            var newsList = await _jsonService.GetFeedsAsync();
            foreach (var item in newsList.ToList())
            {
                News.Add(new NewsModel()
                {
                    Title = item.Title,
                    Description = item.Description,
                    PublicationDate = item.PublicationDate,
                    Picture = item.Picture,
                    Link = item.Link
                });
            }
            await base.Initialize();
        }


        public async void GoToSourceCommand(NewsModel _news)
        {
            var url = new NSUrl(_news.Link);
            var browser = new SFSafariViewController(url/*, true*/);
             var window = UIApplication.SharedApplication.KeyWindow;
            var controller = window.RootViewController;
            await controller.PresentViewControllerAsync(browser, true);
        }
    }
}
