using Microsoft.AspNetCore.Components;

namespace SparcWeek2025.Services
{
    public class AuthenticationService
    {
        private readonly NavigationManager _navigationManager;

        public AuthenticationService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            // Mock login logic
            if (username == "admin" && password == "password")
            {
                // Redirect to dashboard after successful login
                _navigationManager.NavigateTo("/dashboard");
                return true;
            }
            return false;
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            return true;
        }

        public async Task LogoutAsync()
        {
            // Mock logout logic
            _navigationManager.NavigateTo("/login");
        }
    }
}

