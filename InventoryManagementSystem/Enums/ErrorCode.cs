namespace InventoryManagementSystem.Enums
{
    public enum ErrorCode
    {
        NoError,

        UnKnown = 1,
        //101-200 User
        PasswordsDontMatch = 101,
        UserNameAlreadyExist,
        EmailAlreadyExist,
        WrongPasswordOrEmail,
        EmailIsNotFound,
        UserNameIsNotFound,
        WrongOtp,
        UnableToSendEmail,


    }
}
