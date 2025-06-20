using NutritionApp.Services;

namespace NutritionApp.Views;

public partial class RegisterPage : ContentPage
{
    private readonly ApiService _apiService;

    public RegisterPage(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(UsernameEntry.Text) || string.IsNullOrWhiteSpace(PasswordEntry.Text) || PasswordEntry.Text.Length < 8 ||
            !double.TryParse(HeightEntry.Text, out double height) || !double.TryParse(WeightEntry.Text, out double weight) || !int.TryParse(AgeEntry.Text, out int age) ||
            GoalPicker.SelectedItem == null || ActivityPicker.SelectedItem == null)
        {
            ErrorLabel.Text = "���� �����, �������� �� ���� ��������.";
            return;
        }

        LoadingIndicator.IsRunning = true;
        ErrorLabel.Text = "";

        try
        {
            var response = await _apiService.RegisterUserAsync(new
            {
                username = UsernameEntry.Text,
                passwordHash = PasswordEntry.Text,
                height,
                weight,
                age,
                goal = GoalPicker.SelectedItem.ToString(),
                activityLevel = ActivityPicker.SelectedItem.ToString()
            });

            if (response != null && response.Message.Contains("successfully"))
            {
                await DisplayAlert("����", "�� ������ ��������������! ����� ������ �����.", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                ErrorLabel.Text = response?.Message ?? "������� ���������.";
            }
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = $"�������: {ex.Message}";
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
        }
    }
}