namespace Core.Model
{
    /// <summary>
    /// Contain exception details.
    /// </summary>
    public class ErrorResponseModel
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
    }
}