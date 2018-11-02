using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;

namespace TodoListManager.Core.Models
{
    public class NewsModel 
    {
        public string Title { get; set; }
        public string PublicationDate { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Picture { get; set; }
    }

}
