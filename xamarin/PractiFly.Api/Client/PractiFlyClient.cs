using PractiFly.Api.Api.Admin;
using PractiFly.Api.Api.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PractiFly.Api.Client;

public class PractiFlyClient 
{
    #region Constants
    private const string BasicUrl = "https://localhost:5001/api/";
    private const string JwtDefaultFormat = "Bearer {0}";
    private const string JwtDefaultScheme = "Bearer";
    private const string Authorization = "Authorization";

    #endregion

    #region ConstantsUrl
    #region User
    private const string LoginUrl = "user/login";
    private const string RefreshTokenUrl = "user/refresh-token";
    private const string DeleteSelfUrl = "user/delete";
    #endregion
    #region Admin
    //https://localhost:5001/api/admin/user?userId=17
    private const string AdminGetUserByIdUrl = "admin/user?userId={0}";
    
    #endregion
    #endregion

    private readonly HttpClient _httpClient;
    

    public PractiFlyClient(string token)
    {
        //TODO: Checking validation token 
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(BasicUrl);
        _httpClient.DefaultRequestHeaders.Add(Authorization, string.Format(JwtDefaultFormat,token));
    }

    public async Task<LoginResponseDto> GetLoginResponseAsync(LoginRequestDto loginRequestDto)
    {
        var response = await _httpClient.PostAsJsonAsync(LoginUrl, loginRequestDto);
        //TODO: Add throw helper
        //var str = await response.Content.ReadAsStringAsync();
        var result =  await response.Content.ReadFromJsonAsync<LoginResponseDto>() 
            ?? throw new NullReferenceException("result");
        //ArgumentNullException.ThrowIfNull(result);
        return result;
    }
    public async Task<string> GetRefreshTokenAsync()
    {
        var result = await _httpClient.GetStringAsync(RefreshTokenUrl) 
            ?? throw new NullReferenceException("result"); 
        //TODO: Add throw helper
       
        return result;
    }

    public void UpdateToken(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization
            = new AuthenticationHeaderValue(JwtDefaultScheme, token);
    }

    public void DeleteToken()
    {
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<bool> DeleteSelfAsync()
    {
        var response = await _httpClient.GetAsync(DeleteSelfUrl);
        var result = response.IsSuccessStatusCode;

        return result;

    }
    //TODO: Maybe add registration

    public async Task<UserAdminInfoDto?> GetUserByIdAsAdminAsync(int id)
    {
        string uri = string.Format(AdminGetUserByIdUrl, id);
        var result = await _httpClient.GetFromJsonAsync<UserAdminInfoDto>(uri);
        
        return result;
    }
    
}
