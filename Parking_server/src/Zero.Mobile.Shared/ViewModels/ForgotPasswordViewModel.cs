﻿using System.Threading.Tasks;
using System.Windows.Input;
using Abp.MultiTenancy;
using Acr.UserDialogs;
using Zero.ApiClient;
using Zero.Authorization.Accounts;
using Zero.Authorization.Accounts.Dto;
using Zero.Commands;
using Zero.Core.Threading;
using Zero.Localization;
using Zero.ViewModels.Base;
using Zero.Views;

namespace Zero.ViewModels
{
    public class ForgotPasswordViewModel : XamarinViewModel
    {
        public ICommand SendForgotPasswordCommand => HttpRequestCommand.Create(SendForgotPasswordAsync);

        private readonly IAccountAppService _accountAppService;
        private bool _isForgotPasswordEnabled;

        public ForgotPasswordViewModel(IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }

        private string _emailAddress;
        public string EmailAddress
        {
            get => _emailAddress;
            set
            {
                _emailAddress = value;
                SetEmailActivationButtonEnabled();
                RaisePropertyChanged(() => EmailAddress);
            }
        }

        public bool IsForgotPasswordEnabled
        {
            get => _isForgotPasswordEnabled;
            set
            {
                _isForgotPasswordEnabled = value;
                RaisePropertyChanged(() => IsForgotPasswordEnabled);
            }
        }

        public void SetEmailActivationButtonEnabled()
        {
            IsForgotPasswordEnabled = !string.IsNullOrWhiteSpace(EmailAddress);
        }

        private async Task SendForgotPasswordAsync()
        {
            await SetBusyAsync(async () =>
            {
                await WebRequestExecuter.Execute(
                    async () =>
                    await _accountAppService.SendPasswordResetCode(new SendPasswordResetCodeInput { EmailAddress = EmailAddress }),
                    PasswordResetMailSentAsync
                );
            });
        }

        private async Task PasswordResetMailSentAsync()
        {
            await UserDialogs.Instance.AlertAsync(L.Localize("PasswordResetMailSentMessage"), L.Localize("MailSent"), L.Localize("Ok"));

            await NavigationService.SetMainPage<LoginView>(clearNavigationHistory: true);
        }
    }
}
