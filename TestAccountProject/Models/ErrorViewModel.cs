using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;   

namespace TestAccountProject.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

}
