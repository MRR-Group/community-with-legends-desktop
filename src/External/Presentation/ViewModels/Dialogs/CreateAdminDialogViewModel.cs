using Domain.Entities;
namespace Presentation.ViewModels.Dialogs;

using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Irihi.Avalonia.Shared.Contracts;

public partial class CreateAdminDialogViewModel: ObservableObject
{
    [ObservableProperty]
    private string? name;
    
    [ObservableProperty] 
    private string? email;
    
    [ObservableProperty] 
    private string? password;

    public CreateAdminDialogViewModel(string? name, string? email, string? password)
    {
        this.name = name;
        this.email = email;
        this.password = password;
    }
}