using System.Globalization;
using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Messaging;
using Echoes;
using Presentation.Messages;

namespace Presentation.ViewModels;

public class SettingsPageViewModel : AuthenticatedPageViewModel
{
    public SettingsPageViewModel(HistoryRouter<ViewModelBase> router, LogOutInteractor logOutInteractor) : base(router, logOutInteractor)
    {
    }
    
    public string[] Languages => ["English", "Polski"];
    
    public string Language
    {
        get => CultureInfoName(TranslationProvider.Culture);
        set
        {
            var culture = CultureInfoByName(value);
            
            TranslationProvider.SetCulture(culture);
            WeakReferenceMessenger.Default.Send(new LanguageChangedMessage(culture));
            UpdateUrsaTranslations();
        }
    }

    private void UpdateUrsaTranslations()
    {
        Avalonia.Application.Current.Resources["STRING_MENU_BRING_TO_FRONT"] = LoadTranslation("ursa_menu_bring_to_front");
        Avalonia.Application.Current.Resources["STRING_MENU_BRING_FORWARD"] = LoadTranslation("ursa_menu_bring_forward");
        Avalonia.Application.Current.Resources["STRING_MENU_SEND_BACKWARD"] = LoadTranslation("ursa_menu_send_backward");
        Avalonia.Application.Current.Resources["STRING_MENU_SEND_TO_BACK"] = LoadTranslation("ursa_menu_send_to_back");
        Avalonia.Application.Current.Resources["STRING_MENU_DIALOG_OK"] = LoadTranslation("ursa_menu_dialog_ok");
        Avalonia.Application.Current.Resources["STRING_MENU_DIALOG_CANCEL"] = LoadTranslation("ursa_menu_dialog_cancel");
        Avalonia.Application.Current.Resources["STRING_MENU_DIALOG_YES"] = LoadTranslation("ursa_menu_dialog_yes");
        Avalonia.Application.Current.Resources["STRING_MENU_DIALOG_NO"] = LoadTranslation("ursa_menu_dialog_no");
        Avalonia.Application.Current.Resources["STRING_MENU_DIALOG_CLOSE"] = LoadTranslation("ursa_menu_dialog_close");
        Avalonia.Application.Current.Resources["STRING_PAGINATION_JUMP_TO"] = LoadTranslation("ursa_pagination_jump_to");
        Avalonia.Application.Current.Resources["STRING_PAGINATION_PAGE"] = LoadTranslation("ursa_pagination_page");
        Avalonia.Application.Current.Resources["STRING_THEME_TOGGLE_DARK"] = LoadTranslation("ursa_theme_toggle_dark");
        Avalonia.Application.Current.Resources["STRING_THEME_TOGGLE_LIGHT"] = LoadTranslation("ursa_theme_toggle_light");
        Avalonia.Application.Current.Resources["STRING_THEME_TOGGLE_SYSTEM"] = LoadTranslation("ursa_theme_toggle_system");
        Avalonia.Application.Current.Resources["STRING_DATE_TIME_CONFIRM"] = LoadTranslation("ursa_date_time_confirm");
        Avalonia.Application.Current.Resources["STRING_DATE_TIME_START_TIME"] = LoadTranslation("ursa_date_time_start_time");
        Avalonia.Application.Current.Resources["STRING_DATE_TIME_END_TIME"] = LoadTranslation("ursa_date_time_end_time");
    }

    private string LoadTranslation(string key)
    {
        return TranslationProvider.ReadTranslation(
            assembly: typeof(Strings).Assembly,
            file: "Translations\\Strings.toml",
            key: key,
            culture: TranslationProvider.Culture
        ) ?? key;
    }

    private CultureInfo CultureInfoByName(string id)
    {
        return id switch
        {
            "Polski" => CultureInfo.GetCultureInfoByIetfLanguageTag("pl-PL"),
            _ => CultureInfo.GetCultureInfoByIetfLanguageTag("en-US"),
        };
    }
    
    private string CultureInfoName(CultureInfo info)
    {
        return info.IetfLanguageTag switch
        {
            "pl-PL" => "Polski",
            _ => "English",
        };
    }
}