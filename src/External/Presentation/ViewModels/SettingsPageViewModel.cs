using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Application.UseCases;
using Avalonia.Controls;
using Avalonia.SimpleRouter;
using Domain.Entities;
using Echoes;
using Ursa.Themes.Semi;

namespace Presentation.ViewModels;

public partial class SettingsPageViewModel : DataPageViewModel<User>
{
    public SettingsPageViewModel(
        HistoryRouter<ViewModelBase> router,
        LogOutInteractor logOutInteractor
    ) : base(router, logOutInteractor)
    { }

    public string[] Languages => ["English", "Polski"];

    public string Language
    {
        get => CultureInfoName(TranslationProvider.Culture);
        set
        {
            TranslationProvider.SetCulture(CultureInfoByName(value));
        }
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

    protected override async Task RefreshData()
    {
    }
}