namespace EMS.Shared.DTOs
{
    public class ResultDto<T>
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ResultDto<T> Ok(T data, string message = "") => new() { Success = true, Message = message, Data = data };
        public static ResultDto<T> Fail(string message) => new() { Success = false, Message = message, Data = default };

    }
}
