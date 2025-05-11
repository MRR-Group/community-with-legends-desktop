using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;


namespace Presentation.ViewModels;

public partial class RegisterPageViewModel : AuthPageViewModel
{
    private RegisterInteractor _registerInteractor;
    
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    
    public RegisterPageViewModel(HistoryRouter<ViewModelBase> router, RegisterInteractor registerInteractor) : base(router)
    {
        _registerInteractor = registerInteractor;

        Email = "";
        Name = "";
        Password = "";
        ConfirmPassword = "";
    }

    [RelayCommand]
    private void Register()
    {
        SendForm(async () => {
            await _registerInteractor.Register(Name, Email, Password, ConfirmPassword);
            _router.GoTo<LoginPageViewModel>();
        });
    }
    
    [RelayCommand]
    private void GoToLoginPage()
    {
        _router.GoTo<LoginPageViewModel>();
    }
}