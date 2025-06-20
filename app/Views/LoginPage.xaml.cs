using NutritionApp.Services;

namespace NutritionApp.Views;

public partial class LoginPage : ContentPage
{
    private readonly ApiService _apiService;

    public LoginPage(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(UsernameEntry.Text) || string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            ErrorLabel.Text = "���� �����, �������� �� ����.";
            return;
        }

        LoadingIndicator.IsRunning = true;
        ErrorLabel.Text = "";

        try
        {
            var response = await _apiService.LoginUserAsync(new { username = UsernameEntry.Text, passwordHash = PasswordEntry.Text });

            if (response != null && response.UserId > 0)
            {
                Preferences.Set("userId", response.UserId);
                // ������� �� ���� � ��������� (TabBar)
                await Shell.Current.GoToAsync("//MainApp");
            }
            else
            {
                ErrorLabel.Text = "������������ ���� ��� ������.";
            }
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = $"�� ������� ����������� �� �������: {ex.Message}";
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
        }
    }

    async void OnGoToRegisterButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}