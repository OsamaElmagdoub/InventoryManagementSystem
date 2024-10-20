﻿

using InventoryManagementSystem.Enums;

namespace InventoryManagementSystem.Exceptions
{
    public class BusinessException:Exception
    {
        public ErrorCode ErrorCode { get; set; }

        public BusinessException(ErrorCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
