using Microsoft.AspNetCore.Mvc;

namespace Automasipp.Backend.Controllers
{
    public class InternalServerError: ContentResult
    {
        public InternalServerError(string Message) 
        {
            this.StatusCode = 500;
            this.Content = Message;
            this.ContentType = "text/plain";
        }
    }
}
