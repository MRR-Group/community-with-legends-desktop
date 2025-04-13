using System;
using System.Threading.Tasks;
using Application.Exceptions;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Exceptions;
using Infrastructure.Exceptions;
using Presentation.Events;
using Presentation.Utils;

namespace Presentation.ViewModels;

public partial class AuthPageViewModel : FormPageViewModel
{
    public AuthPageViewModel(HistoryRouter<ViewModelBase> router) : base(router) { }

    protected override async Task SendForm(Func<Task> sendAction)
    {
        try
        {
            Processing = true;
            _exceptions.Clear();

            await sendAction.Invoke();
        }
        catch (PasswordsDoNotMatchException e)
        {
            _exceptions.Add("password", e.Message);
        }
        catch (InvalidPasswordException e)
        {
            _exceptions.Add("password", e.Message);
        }
        catch (InvalidEmailException e)
        {
            _exceptions.Add("email", e.Message);
        }
        catch (FormValidationException e)
        {
            _exceptions.AddRange(e.Fields);
        }
        finally
        {
            UpdateFormEvents();
            Processing = false;
        }
    }
}