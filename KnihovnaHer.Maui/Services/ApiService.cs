using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using KnihovnaHer.Dto;

namespace KnihovnaHer.Maui.Services
{
    public class ApiService(HttpClient httpClient) : IApiService
    {
        private readonly HttpClient httpClient = httpClient;

        private readonly JsonSerializerOptions jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };



        // seznam her
        public async Task<List<HraDto>> GetHryAsync()
        {
            
            try
            {
                var response = await httpClient.GetStringAsync($"Hra");
                var result = JsonSerializer.Deserialize<List<HraDto>>(response, jsonOptions);
                return result ?? [];
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Chyba při načítání her: {ex.Message}");
                return [];
            }
        }

        // get 1 hra
        public async Task<HraDto?> GetHra(uint hraId)
        {
            try
            {
                var response = await httpClient.GetStringAsync($"Hra/{hraId}");
                var result = JsonSerializer.Deserialize<HraDto>(response, jsonOptions);
                return result ?? throw new InvalidOperationException("Deserializovaná data jsou null.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Chyba: {ex.Message}");
                return null;
            }
        }

        //přidání hry
        public async Task<HraDto?> AddHraAsync(HraCreateEditDto hraCreateDto)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(hraCreateDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"Hra", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<HraDto>(responseContent, jsonOptions);
                    return result ?? throw new InvalidOperationException("Deserializovaná data jsou null.");
                }
                else
                {
                    Debug.WriteLine($"Chyba při přidávání hry: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Výjimka při přidávání hry: {ex.Message}");
                return null;    
            }
        }

        // update hry
        public async Task<HraDto?> UpdateHraAsync(uint hraId, HraCreateEditDto hraEditDto)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(hraEditDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync($"Hra/{hraId}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<HraDto>(responseContent, jsonOptions);
                    return result ?? throw new InvalidOperationException("Deserializovaná data jsou null.");
                }
                else
                {
                    Debug.WriteLine($"Chyba při aktualizaci hry: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Výjimka při aktualizaci hry: {ex.Message}");
                return null;
            }
        }


        // odstranění hry
        public async Task<HraDto?> DeleteHraAsync(uint hraId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"Hra/{hraId}");
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<HraDto>(responseContent, jsonOptions);
                    return result ?? throw new InvalidOperationException("Deserializovaná data jsou null.");
                }
                else
                {
                    Debug.WriteLine($"Chyba při odstraňování hry: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Výjimka při odstraňování hry: {ex.Message}");
                return null;
            }
        }

        // seznam žánrů
        public async Task<List<ZanrDto>> GetZanryAsync()
        {
            try
            {
                var response = await httpClient.GetStringAsync($"Zanr");
                var result = JsonSerializer.Deserialize<List<ZanrDto>>(response, jsonOptions);
                return result ?? [];
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Chyba při načítání žánrů: {ex.Message}");
                return [];
            }
        }

        // přidat žánr
        public async Task<ZanrDto?> AddZanrAsync(ZanrDto zanrDto)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(zanrDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"Zanr", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ZanrDto>(responseContent, jsonOptions);
                    return result ?? throw new InvalidOperationException("Deserializovaná data jsou null.");
                }
                else
                {
                    Debug.WriteLine($"Chyba při přidávání žánru: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Výjimka při přidávání žánru: {ex.Message}");
                return null;
            }
        }


        // odstranění žánrů
        public async Task<ZanrDto?> DeleteZanr(uint zanrId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"Zanr/{zanrId}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ZanrDto>(responseContent, jsonOptions);
                    return result ?? throw new InvalidOperationException("Deserializovaná data jsou null.");
                }
                else
                {
                    Debug.WriteLine($"Chyba při odstraňování žánru: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Výjimka při odstraňování žánru: {ex.Message}");
                return null;
            }
        }

        // seznam vydavatelů
        public async Task<List<VydavatelDto>> GetVydavateleAsync()
        {
            try
            {
                var response = await httpClient.GetStringAsync($"Vydavatel");
                var result = JsonSerializer.Deserialize<List<VydavatelDto>>(response, jsonOptions);
                return result ?? [];
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Chyba při načítání vydavatelů: {ex.Message}");
                return [];
            }
        }

        // přidání vydavatele
        public async Task<VydavatelDto?> AddVydavatelAsync(VydavatelDto vydavatelDto)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(vydavatelDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"Vydavatel", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<VydavatelDto>(responseContent, jsonOptions);
                    return result ?? throw new InvalidOperationException("Deserializovaná data jsou null.");
                }
                else
                {
                    Debug.WriteLine($"Chyba při přidávání vydavatele: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Výjimka při přidávání vydavatele: {ex.Message}");
                return null;
            }
        }

        // odstranění vydavatele
        public async Task<VydavatelDto?> DeleteVydavatel(uint vydavatelId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"Vydavatel/{vydavatelId}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<VydavatelDto>(responseContent, jsonOptions);
                    return result ?? throw new InvalidOperationException("Deserializovaná data jsou null.");
                }
                else
                {
                    Debug.WriteLine($"Chyba při odstranění vydavatele: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Výjimka při odstranění vydavatele: {ex.Message}");
                return null;
            }
        }

        // seznam uživatelů
        public async Task<List<UzivatelDto>> GetAllUzivatelAsync()
        {
            try 
            {
                var respone = await httpClient.GetAsync("Uzivatel");
                if(respone.IsSuccessStatusCode)
                {
                    var responseContent = await respone.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<List<UzivatelDto>>(responseContent,jsonOptions);
                    return result ?? throw new InvalidOperationException("Deserializovaná data jsou null.");

                }
                else
                {
                    Debug.WriteLine($"Chyba při desirializaci: {respone.StatusCode}");
                    return [];
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Výjímka při získání všech uživatelů: {ex.Message}");
                return [];
            }
        }

        // get 1 uživatele
        public async Task<UzivatelDto?> GetUzivatelAsync(string uzivatelId)
        {
            try
            {
                var response = await httpClient.GetAsync($"Uzivatel/{uzivatelId}");

                if(response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<UzivatelDto>(responseContent, jsonOptions);
                    return result ?? throw new InvalidOperationException("Deserializovaná data jsou null.");
                }
                else
                {
                    Debug.WriteLine($"Chyba při získání 1 uživatele: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Výjímka při získání 1 uživatele: {ex.Message}");
                return null;
            }
        }


        // vytvoření uživatele
        public async Task<UzivatelDto?> AddUzivatelAsync(UzivatelCreateDto uzivatelCreateDto)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(uzivatelCreateDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("Uzivatel", content);

                if(response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<UzivatelDto>(responseContent, jsonOptions);
                    return result ?? throw new InvalidOperationException("Deserializovaná data jsou null.");
                }
                else
                {
                    Debug.WriteLine($"Chyba při vytváření uživatele: {response.StatusCode}");
                    return null;
                }    
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Výjímka při vytváření uživatele: {ex.Message}");
                return null;
            }
        }

        // edit uživatele
        public async Task<UzivatelDto?> UpdateUzivatel(string uzivatelId,UzivatelEditDto uzivatelEditDto)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(uzivatelEditDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"Uzivatel/{uzivatelId}",content);

                if(response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<UzivatelDto>(responseContent);
                    return result ?? throw new InvalidOperationException("Deserializovaná data jsou null.");
                }
                else
                {
                    Debug.WriteLine($"Chyba při editu uživatele: {response.StatusCode}");
                    return null;
                }
            }
            catch( Exception ex)
            {
                Debug.WriteLine($"Výjímka při editu uživatele: {ex.Message}");
                return null;
            }


        }


        // odstranění uživatele
        public async Task<UzivatelDto?> DeleteUzivatelAsync(string uzivatelId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"Uzivatel/{uzivatelId}");

                if(response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<UzivatelDto>(responseContent);
                    return result ?? throw new InvalidOperationException("Deserializovaná data jsou null.");
                }
                else
                {
                    Debug.WriteLine($"Chyba při odstranění uživatele: {response.StatusCode}");
                    return null;
                }
            }
            catch ( Exception ex )
            {
                Debug.WriteLine($"Výjímka při odstranění uživatele: {ex.Message}" );
                return null;
            }
        }



        // přihlášení uživatele
         public async Task<AuthResponseDto?> LoginAsync(AuthDto authDto)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(authDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"Auth", content);


                if(response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<AuthResponseDto>(responseContent,jsonOptions);
                    return result ?? throw new InvalidOperationException("Deserializovaná data jsou null.");
                }
                else
                {
                    Debug.WriteLine($"Chyba při přihlašování: {response.StatusCode}");
                    return null;
                }

            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Chyba při přihlašování: {ex.Message}");
                return null;
            }

        }




        // registrace uživatele
        public async Task<AuthResponseDto?> RegisterAsync(AuthDto authDto)
        {
           try {
            var jsonContent = JsonSerializer.Serialize(authDto);
            var content = new StringContent(jsonContent,Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"Auth/register", content);

               


                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<AuthResponseDto>(responseContent, jsonOptions);
                    return result ?? throw new InvalidOperationException("Deserializovaná data jsou null.");
                }
                else
                {
                    Debug.WriteLine($"Chyba při registraci: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Chyba při registraci: {ex.Message}");
                return null;
            }


        }


     


        // seznam statusu her podle uživatele

        public async Task<List<StatusHryViewDto>> GetStatusHryByUserAsync()
        {
            try
            {
                Debug.WriteLine("GetStatusHryByUserAsync: Začínám získávat uživatelské statusy.");

           
              
                var response = await httpClient.GetAsync("StatusHry");

                Debug.WriteLine($"GetStatusHryByUserAsync: Response status code: {response.StatusCode}");


                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"GetStatusHryByUserAsync: Obsah odpovědi: {content}");

                    var result = JsonSerializer.Deserialize<List<StatusHryViewDto>>(content, jsonOptions);
                    if (result is null)
                    {
                        Debug.WriteLine("GetStatusHryByUserAsync: Deserializovaná data jsou null.");
                        return [];
                    }

                    Debug.WriteLine($"GetStatusHryByUserAsync: Počet statusů načtených: {result.Count}");
                    return result;
                }
                else
                {
                    Debug.WriteLine($"GetStatusHryByUserAsync: Chyba při získávání statusů her: {response.StatusCode}");
                    return [];
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetStatusHryByUserAsync: Výjimka: {ex}");
                return [];
            }
        }



        // vytvořit status hry
        public async Task<StatusHryViewDto?> AddStatusHryAsync(StatusHryCreateDto statusHryCreateDto)
        {
           try
            {
                var jsonContent = JsonSerializer.Serialize(statusHryCreateDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("StatusHry", content);

                if(response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<StatusHryViewDto>(responseContent);
                    return result ?? throw new InvalidOperationException("Deserializovaná data jsou null.");
                }
                else
                {
                    Debug.WriteLine($"Chyby při přidávání statusu her: {response.StatusCode}");
                    return null;
                }

            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Výjímka při přidávání statusu her: {ex}");
                return null;
            }


        }

        // edit statusu hry
        public async Task<StatusHryViewDto?> UpdateStatusHryAsync(uint statusHryId, StatusHryUpdateDto statusHryEditDto)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(statusHryEditDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var respone = await httpClient.PutAsync($"StatusHry/{statusHryId}", content);

                if(respone.IsSuccessStatusCode)
                {
                    var responseContent = await respone.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<StatusHryViewDto>(responseContent, jsonOptions);
                    return result ?? throw new InvalidOperationException("Deserializovaná data jsou null");
                }
                else
                {
                    Debug.WriteLine($"Chyba při updatu statusu hry: {respone.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Výjímka při updatu statusu hry: {ex}");
                return null ;
            }


        }


        // odstranit status hry
        public async Task<StatusHryViewDto?> DeleteStatusHryAsync(uint statusHryId)
        {
            try
            {
                var response =  await httpClient.DeleteAsync($"StatusHry/{statusHryId}");
                
                if(response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<StatusHryViewDto>(responseContent, jsonOptions);
                    return result ?? throw new InvalidOperationException("Deserializovaná data jsou null");
                }
                else
                {
                    Debug.WriteLine($"Chyba při odstranění statusu hry: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Výjímka při odstranění statusu hry: {ex}");
                return null;
            }



        }








    }
}
