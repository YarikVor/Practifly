using PractiFly.Api.Api.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PractiFly.Api.Client
{
    public class PractiFlyClient 
    {
        #region Constants
        private const string BasicUrl = "https://localhost:5001/api/";
        private const string JwtDefaultFormat = "Bearer {0}";
        private const string Authorization = "Authorization";
        #endregion

        #region ConstantsUrl
        private const string LoginUrl = "user/login";
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
            var str = await response.Content.ReadAsStringAsync();
            var result =  await response.Content.ReadFromJsonAsync<LoginResponseDto>() 
                ?? throw new NullReferenceException("result");
            //ArgumentNullException.ThrowIfNull(result);
            return result;
        }
    }
}
