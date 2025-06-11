using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Entities;
using Infrastructure.DTOs;
using Infrastructure.Repositories;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;

namespace Presentation.ViewModels;

public partial class StatisticsPageViewModel : AuthenticatedPageViewModel
{
    private StatisticsRepository _repository;

    [ObservableProperty] 
    private int _posts;
    [ObservableProperty] 
    private int _comments;
    [ObservableProperty] 
    private int _users;
    [ObservableProperty] 
    private int _bannedUsers;
    [ObservableProperty] 
    private int _logs;

    [ObservableProperty] 
    private ISeries[] _usersGrowth;
    [ObservableProperty] 
    private ISeries[] _postsGrowth;
    [ObservableProperty] 
    private ISeries[] _commentsGrowth;
    [ObservableProperty] 
    private ISeries[] _diskUsage;
    [ObservableProperty] 
    private ISeries[] _bannedUsersPercent;

    [ObservableProperty] 
    private bool _isLoaded;

    public Axis[] UserGrowthXAxis { get; set; }
    public Axis[] PostsGrowthXAxis { get; set; }
    public Axis[] CommentsGrowthXAxis { get; set; }

    public StatisticsPageViewModel(HistoryRouter<ViewModelBase> router, StatisticsRepository statisticsRepository, LogOutInteractor logOutInteractor) 
        : base(router, logOutInteractor)
    {
        _repository = statisticsRepository;
        _isLoaded = false;

        _usersGrowth = [];
        _postsGrowth = [];
        _commentsGrowth = [];
        _diskUsage = [];
        _bannedUsersPercent = [];

        RefreshData();
    }

    protected Task RefreshData()
    {
        return SendAction(async () =>
        {
            var data = await _repository.Get();

            LoadSummaryData(data);
            ConfigureUsersGrowth(data);
            ConfigurePostsGrowth(data);
            ConfigureCommentsGrowth(data);
            ConfigureDiskUsage(data);
            ConfigureBannedUsersPercent(data);

            IsLoaded = true;
        });
    }

    private void LoadSummaryData(Statistics data)
    {
        Posts = data.Posts;
        Comments = data.Comments;
        Users = data.Users;
        BannedUsers = data.BannedUsers;
        Logs = data.Logs;
    }
    
    private void ConfigureUsersGrowth(Statistics data)
    {
        ConfigureGrowthSeries(data.UsersGrowth, out var usersGrowth, out var userGrowthXAxis);
        
        UsersGrowth = usersGrowth;
        UserGrowthXAxis = userGrowthXAxis;
    }

    private void ConfigurePostsGrowth(Statistics data)
    {
        ConfigureGrowthSeries(data.PostsGrowth, out var postsGrowth, out var postsGrowthXAxis);
        
        PostsGrowth = postsGrowth;
        PostsGrowthXAxis = postsGrowthXAxis;
    }

    private void ConfigureCommentsGrowth(Statistics data)
    {
        ConfigureGrowthSeries(data.CommentsGrowth, out var commentsGrowth, out var commentsGrowthXAxis);
        
        CommentsGrowth = commentsGrowth;
        CommentsGrowthXAxis = commentsGrowthXAxis;
    }

    private void ConfigureGrowthSeries(
        Dictionary<string, int> growthData,
        out ISeries[] targetSeries,
        out Axis[] targetAxis)
    {
        var ordered = growthData.OrderBy(kv => kv.Key).ToList();

        targetSeries =
        [
            new ColumnSeries<int>
            {
                Values = ordered.Select(kv => kv.Value).ToArray(),
            },
        ];

        targetAxis =
        [
            new Axis
            {
                Labels = ordered.Select(kv => DateTime.Parse(kv.Key).ToString("MM-dd")).ToArray(),
                MinLimit = 0,
            },
        ];
    }

    private void ConfigureDiskUsage(Statistics data)
    {
        DiskUsage =
        [
            new PieSeries<float>
            {
                Values = [ data.DiskUsage ],
                Name = "Disk Usage (%)"
            },
            new PieSeries<float>
            {
                Values = [ 100 - data.DiskUsage ],
                Name = "Free (%)"
            },
        ];
    }

    private void ConfigureBannedUsersPercent(Statistics data)
    {
        var percent = (double)data.BannedUsers / data.Users * 100;

        BannedUsersPercent =
        [
            new PieSeries<double>
            {
                Values = [ percent ],
                Name = "Banned (%)"
            },
            new PieSeries<double>
            {
                Values = [ 100 - percent ],
                Name = "Active (%)"
            },
        ];
    }
}
