using System;
using System.Threading.Tasks;
using SampleProject.Enumerations;
using SampleProject.Interfaces;
using Xamarin.Forms;

namespace SampleProject.Services
{
    public class DialogService : IDialogService
    {
        private Page _dialogPage;

        public void Init(Page dialogPage)
        {
            _dialogPage = dialogPage;
        }

        public async Task ShowMessage(DialogType dialogType, string title, string message, string buttonText, Action hideCallback)
        {
            if (_dialogPage == null)
            {
                return;
            }

            if (dialogType == DialogType.Error)
            {
                await _dialogPage.DisplayAlert($"ERROR: {title}", message, buttonText);
            }
            else
            {
                await _dialogPage.DisplayAlert(title, message, buttonText);
            }

            hideCallback?.Invoke();
        }

        public async Task<bool> ShowMessage(string title, string message, string confirmText, string cancelText, Action<bool> hideCallBack)
        {
            if (_dialogPage == null)
            {
                return false;
            }

            var result = await _dialogPage.DisplayAlert(title, message, confirmText, cancelText);

            hideCallBack?.Invoke(result);

            return result;
        }
    }
}
