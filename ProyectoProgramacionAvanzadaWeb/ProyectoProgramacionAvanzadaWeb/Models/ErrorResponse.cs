namespace ProyectoProgramacionAvanzadaWeb.Models
{
    public class ErrorResponse
    {
        public Dictionary<string, ErrorDetail> Errors { get; set; }
    }

    public class ErrorDetail
    {
        public object RawValue { get; set; }
        public object AttemptedValue { get; set; }
        public List<ErrorItem> Errors { get; set; }
    }

    public class ErrorItem
    {
        public object Exception { get; set; }
        public string ErrorMessage { get; set; }
    }
}
