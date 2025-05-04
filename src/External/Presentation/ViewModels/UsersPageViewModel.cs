using System.Collections.ObjectModel;
using Avalonia.SimpleRouter;
using Domain.Entities;
using Domain.ValueObjects;

namespace Presentation.ViewModels;

public partial class UsersPageViewModel : ViewModelBase
{
    public ObservableCollection<User> Users { get; private set;  }

    public UsersPageViewModel(HistoryRouter<ViewModelBase> router) : base(router)
    {
        Users = [];

        for (uint i = 0; i < 10; i++)
        {
            Users.Add(new User(i, $"User {i}", $"{i}.png", new Email($"user{i}@gmail.com"), new Roles([]), new Permissions([]), new Date(1746347764)));
        }
    }
}