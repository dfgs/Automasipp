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
        private List<Page> pages;


        public static readonly DependencyProperty CurrentPageProperty = DependencyProperty.Register("CurrentPage", typeof(Page), typeof(PageManager), new PropertyMetadata(null));
        public Page CurrentPage
        {
            get { return (Page)GetValue(CurrentPageProperty); }
            private set { SetValue(CurrentPageProperty, value); }
        }
        IPage IPageManager.CurrentPage
        {
            get => CurrentPage;
        }


        public PageManager() 
        {
            this.pages = new List<Page>();    
        }

        public async Task OpenPageAsync(Page Page)
        {
            pages.Add(Page);
            CurrentPage= Page;
            await Page.LoadAsync();
        }
    


    }

}
