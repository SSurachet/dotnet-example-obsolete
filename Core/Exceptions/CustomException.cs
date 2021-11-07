using System;

namespace Core.Exceptions
{
    public interface ICustomException
    {
        string Code { get; }
        string Title { get; }
        string Message { get; }
    }

    public class CustomException : Exception, ICustomException
    {
        public string Code { get; set; }
        public string Title { get; set; }

        public CustomException(string message, string title, string code = null) : base(message)
        {
            this.Code = code;
            this.Title = title;
        }
    }
}