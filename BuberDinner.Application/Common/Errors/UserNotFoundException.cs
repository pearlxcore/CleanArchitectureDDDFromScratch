using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Common.Errors
{
    public class UserNotFoundException : Exception, IExceptionService
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
        public string Message => "User with email given is not exists.";
    }
}
