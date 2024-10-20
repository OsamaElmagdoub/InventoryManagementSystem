using FluentEmail.Core;
using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Enums;

namespace InventoryManagementSystem.Helpers
{
    public interface IFluentEmailService
    {
        Task<ResultDTO<bool>> SendEmailAsync(SendEmailDto payload);
    }

    public class FluentEmailService : IFluentEmailService
    {
        private readonly IFluentEmail _fluentEmail;

        public FluentEmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task<ResultDTO<bool>> SendEmailAsync(SendEmailDto payload)
        {
            var sendEmailResponse = await
                           _fluentEmail.To(payload.To)
                                       .Subject(payload.Subject)
                                       .Body(payload.Body)
                                       .SendAsync();

            if (!sendEmailResponse.Successful)
                return ResultDTO<bool>.Faliure(ErrorCode.UnableToSendEmail, "Sorry we are able to send email at the moment");

            return ResultDTO<bool>.Sucess(true, "Email sent successfully");
        }
    }
}
