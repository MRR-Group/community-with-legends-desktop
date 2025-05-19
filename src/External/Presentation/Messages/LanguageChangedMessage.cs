using System.Globalization;
using CommunityToolkit.Mvvm.Messaging.Messages;
namespace Presentation.Messages;

public class LanguageChangedMessage : ValueChangedMessage<CultureInfo>
{
    public LanguageChangedMessage(CultureInfo culture) : base(culture) { }
}
