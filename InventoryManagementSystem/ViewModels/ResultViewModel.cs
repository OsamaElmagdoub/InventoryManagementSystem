
using InventoryManagementSystem.Enums;

namespace InventoryManagementSystem.ViewModels
{
    public class ResultViewModel<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public ErrorCode ErrorCode { get; set; }


        public static ResultViewModel<T> Sucess<T>(T data, string message = "")
        {
            return new ResultViewModel<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message,
                ErrorCode = ErrorCode.NoError,
            };
        }

        public static ResultViewModel<T> Faliure(ErrorCode errorCode, string message)
        {
            return new ResultViewModel<T>
            {
                IsSuccess = false,
                Data = default,
                Message = message,
                ErrorCode = errorCode,
            };
        }
    }
}
