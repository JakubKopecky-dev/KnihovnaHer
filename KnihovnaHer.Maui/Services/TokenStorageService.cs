using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KnihovnaHer.Maui.Services
{
    public class TokenStorageService :ITokenStorageService
    {
        private const string TokenKey = "auth_token";

        // Uložení tokenu
        public  async Task SaveTokenAsync(string token)
        {
            try
            {
                await SecureStorage.Default.SetAsync(TokenKey, token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Chyba při ukládání tokenu: {ex.Message}");
            }
        }

        // Načtení tokenu
        public  async Task<string?> GetTokenAsync()
        {
            try
            {
                return await SecureStorage.Default.GetAsync(TokenKey);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Chyba při načítání tokenu: {ex.Message}");
                return null; // V případě chyby
            }
        }

        // Smazání tokenu
        public  void DeleteToken()
        {
            try
            {
                SecureStorage.Default.Remove(TokenKey);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Chyba při mazání tokenu: {ex.Message}");
            }
        }

        // Kontrola, zda existuje token
        public  async Task<bool> HasTokenAsync()
        {
            var token = await GetTokenAsync();
            return !string.IsNullOrEmpty(token);
        }



        public async Task<bool> IsTokenValidAsync()
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrWhiteSpace(token))
                return false;

            try
            {
                var parts = token.Split('.');
                if (parts.Length != 3)
                    return false;

                var payload = parts[1];
                var jsonBytes = Convert.FromBase64String(PadBase64(payload));
                var payloadJson = JsonDocument.Parse(jsonBytes);

                if (!payloadJson.RootElement.TryGetProperty("exp", out var expElement))
                    return false;

                var expUnix = expElement.GetInt64();
                var expTime = DateTimeOffset.FromUnixTimeSeconds(expUnix);

                return expTime > DateTimeOffset.UtcNow;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[TokenStorageService] Chyba při kontrole expirace tokenu: {ex.Message}");
                return false;
            }
        }

        private string PadBase64(string base64)
        {
            return base64.PadRight(base64.Length + (4 - base64.Length % 4) % 4, '=');
        }


        // jestli je přihlášeny uživatel admin
        public async Task<bool> IsAdminAsync()
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrWhiteSpace(token))
                return false;

            try
            {
                var payload = token.Split('.')[1];
                var jsonBytes = Convert.FromBase64String(PadBase64(payload));
                using var jsonDoc = JsonDocument.Parse(jsonBytes);

                var root = jsonDoc.RootElement;

                // Microsoft role claim
                const string roleClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

                if (root.TryGetProperty(roleClaim, out var roleElement))
                {
                    if (roleElement.ValueKind == JsonValueKind.String)
                    {
                        return roleElement.GetString()?.Equals("Admin", StringComparison.OrdinalIgnoreCase) == true;
                    }
                    else if (roleElement.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var role in roleElement.EnumerateArray())
                        {
                            if (role.GetString()?.Equals("Admin", StringComparison.OrdinalIgnoreCase) == true)
                                return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[TokenStorageService] Chyba při čtení role claimu: {ex.Message}");
                return false;
            }
        }





    }

}
