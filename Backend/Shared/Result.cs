namespace FormulaOne.Shared
{
    public class Result<T> 
    {
        public T? Value { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }

        private Result(T? Value,bool IsSuccess,string? ErrorMessage)
        {
            this.Value = Value;
            this.IsSuccess = IsSuccess;
            this.ErrorMessage = ErrorMessage;
        }
        public static Result<T> Success(T Value) => new Result<T>(Value,true,default);
        public static Result<T> Error(string ErrorMessage) => new Result<T>(default, false, ErrorMessage);
    }
}
