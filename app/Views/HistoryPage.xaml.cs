using NutritionApp.ViewModels;

namespace NutritionApp.Views;

public partial class HistoryPage : ContentPage
{
    private readonly HistoryViewModel _viewModel;
    public HistoryPage(HistoryViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // ����������� ������ ������, ���� ������� ��� �������
        _viewModel.LoadHistoryCommand.Execute(null);
    }
}