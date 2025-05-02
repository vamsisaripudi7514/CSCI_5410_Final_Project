namespace AttendanceSystem.Utility
{
    public class ResponseModel<TModel>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public TModel SingleResult { get; set; }
        public IEnumerable<TModel> ListResult { get; set; }
        public object? Data { get; set; }
        public ResponseModel(int StatusCode, string Message)
        {
            this.StatusCode = StatusCode;
            this.Message = Message;
        }
    }
}
