﻿namespace Application.Interfaces.Services.EmailSenderService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
