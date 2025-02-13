using ResultTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Automasipp.Desktop
{
    public abstract class Page : DependencyObject, IPage
    {


        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(PageState), typeof(Page), new PropertyMetadata(PageState.Undefined));
        public PageState State
        {
            get { return (PageState)GetValue(StateProperty); }
            private set { SetValue(StateProperty, value); }
        }



        public static readonly DependencyProperty ErrorMessageProperty = DependencyProperty.Register("ErrorMessage", typeof(string), typeof(Page), new PropertyMetadata(null));
        public string? ErrorMessage
        {
            get { return (string?)GetValue(ErrorMessageProperty); }
            set { SetValue(ErrorMessageProperty, value); }
        }




        private IPageManager? pageManager;
        protected IPageManager? PageManager
        {
            get => pageManager;
        }

        IPageManager? IPage.PageManager
        {
            get => pageManager;
            set => this.pageManager=value;
        }


        public abstract string Name
        {
            get;
        }

        public Page()
        {
        }

        protected abstract Task OnLoadAsync();


        public async Task<IResult<bool>> LoadAsync()
        {
            State = PageState.Loading;
            try
            {
                await OnLoadAsync();
                State = PageState.Loaded;
                return Result.Success<bool>(true);
            }
            catch (Exception ex)
            {
                State = PageState.Error;
                ErrorMessage=ex.Message;
                return Result.Fail<bool>(ex);
            }
            
        }
        public void Dispose()
        {
            OnDispose();
        }

        protected virtual void OnDispose()
        {
        }
    }
}
