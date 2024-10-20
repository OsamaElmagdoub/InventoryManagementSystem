
using InventoryManagementSystem.Enums;

namespace InventoryManagementSystem.DTOs
{
    public class ResultDTO<T>
    {

        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public ErrorCode ErrorCode { get; set; }

        public static ResultDTO<T> Sucess<T>(T data, string message = "")
        {
            return new ResultDTO<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message,
                ErrorCode = ErrorCode.NoError,
            };
        }

        public static ResultDTO<T> Faliure(ErrorCode errorCode, string message)
        {
            return new ResultDTO<T>
            {
                IsSuccess = false,
                Data = default,
                Message = message,
                ErrorCode = errorCode,
            };
        }
    }
}
