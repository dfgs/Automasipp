using Automasipp.Desktop.Pages;
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

        public static readonly DependencyProperty RefreshCommandProperty = DependencyProperty.Register("RefreshCommand", typeof(PageCommand), typeof(Page), new PropertyMetadata(null));
        public PageCommand RefreshCommand
        {
            get { return (PageCommand)GetValue(RefreshCommandProperty); }
            set { SetValue(RefreshCommandProperty, value); }
        }
        public static readonly DependencyProperty CloseCommandProperty = DependencyProperty.Register("CloseCommand", typeof(PageCommand), typeof(Page), new PropertyMetadata(null));
        public PageCommand CloseCommand
        {
            get { return (PageCommand)GetValue(CloseCommandProperty); }
            set { SetValue(CloseCommandProperty, value); }
        }
        public static readonly DependencyProperty FocusCommandProperty = DependencyProperty.Register("FocusCommand", typeof(PageCommand), typeof(Page), new PropertyMetadata(null));
        public PageCommand FocusCommand
        {
            get { return (PageCommand)GetValue(FocusCommandProperty); }
            set { SetValue(FocusCommandProperty, value); }
        }


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
            this.RefreshCommand = new PageCommand(this, (parameter) => (State==PageState.Loaded), (parameter) => LoadAsync());
            this.CloseCommand = new PageCommand(this, (parameter) => (State == PageState.Loaded) && OnCanClose(parameter), (parameter) => CloseCommandExecutedAsync());
            this.FocusCommand = new PageCommand(this, (parameter) => true, (parameter) => FocusCommandExecutedAsync());

        }

        protected abstract Task<bool> OnLoadAsync();


        public async Task<IResult<bool>> LoadAsync()
        {
            ErrorMessage= null;
            return await RunAsync(OnLoadAsync());
        }

        public virtual async Task<IResult<bool>> CloseAsync()
        {
            return await Task.FromResult(Result.Success(true));
        }

        public async Task<IResult<T>> RunAsync<T>(Task<T> Func)
        {
            State = PageState.Loading;
            try
            {
                T val=await Func;
                State = PageState.Loaded;
                return Result.Success<T>(val);
            }
            catch (Exception ex)
            {
                State = PageState.Error;
                ErrorMessage = ex.Message;
                return Result.Fail<T>(ex);
            }
        }
        public async Task<IResult<bool>> RunAsync(Task Action)
        {
            State = PageState.Loading;
            try
            {
                await Action;
                State = PageState.Loaded;
                return Result.Success<bool>(true);
            }
            catch (Exception ex)
            {
                State = PageState.Error;
                ErrorMessage = ex.Message;
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


        protected virtual bool OnCanClose(object? parameter)
        {
            return true;
        }
        

        private async Task CloseCommandExecutedAsync()
        {
            if (PageManager == null) return;
            await PageManager.ClosePageAsync();
        }
        private async Task FocusCommandExecutedAsync()
        {
            if (PageManager == null) return;
            while(PageManager.CurrentPage!=this)
            {
                await PageManager.ClosePageAsync();
            }
            
        }


    }
}
