using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Automasipp.Desktop
{
    public class PageManager:DependencyObject,IPageManager
    {
        private List<IPage> pages;


        public static readonly DependencyProperty CurrentPageProperty = DependencyProperty.Register("CurrentPage", typeof(IPage), typeof(PageManager), new PropertyMetadata(null));
        public IPage CurrentPage
        {
            get { return (IPage)GetValue(CurrentPageProperty); }
            private set { SetValue(CurrentPageProperty, value); }
        }
        IPage IPageManager.CurrentPage
        {
            get => CurrentPage;
        }


        public PageManager() 
        {
            this.pages = new List<IPage>();    
        }

        public async Task OpenPageAsync(IPage Page)
        {
            Page.PageManager=this;
            pages.Add(Page);
            CurrentPage= Page;
            await Page.LoadAsync();
        }

        public T? GetPage<T>() where T : IPage
        {
            return pages.OfType<T>().FirstOrDefault();
        }

    }

}
