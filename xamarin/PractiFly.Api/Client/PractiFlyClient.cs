using Cysharp.Web;
using PractiFly.Api.Admin;
using PractiFly.Api.Api.Admin;
using PractiFly.Api.Api.CourseData;
using PractiFly.Api.Api.CourseDetails;
using PractiFly.Api.Api.Login;
using PractiFly.Api.CourseData;
using PractiFly.Api.CourseDetails;
using PractiFly.Api.Login;
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
    private const string AdminGetUserByIdUrl = "admin/user?userId={0}";
    private const string AdminDeleteUserByIdUrl = "admin/user?userId={0}";
    private const string AdminCreateUserByIdUrl = "admin/user";
    private const string AdminUpdateUserByIdUrl = "admin/user/edit";
    private const string AdminFilterUserUrl = "admin/user/filter";
    #endregion
    #region CourseData
    private const string CourseDataItemAllUrl = "course/all?ownerId={0}";
    private const string CourseInfoDataUrl = "course?courseId={0}";
    private const string CreateCourseUrl = "course";
    private const string DeleteCourseUrl = "course";
    private const string UpdateCourseUrl = "course/edit";
    #endregion 

    #region CourseDetails
    private const string DetailsMaterialUrl = "theme/material";
    
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
    //TODO: Maybe add registration


    #region BasicMethods
    private async Task<TOutput> CreateAsync<TInput, TOutput>(string url, TInput dto)
    {
        var response = await _httpClient.PostAsJsonAsync(url, dto);
        var result = await response.Content.ReadFromJsonAsync<TOutput>()
            ?? throw new NullReferenceException("result");
        return result;
    }
    #endregion

    #region Login
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
    #endregion
   

    #region Admin
    public async Task<UserAdminInfoDto?> GetUserByIdAsAdminAsync(int id)
    {
        string uri = string.Format(AdminGetUserByIdUrl, id);
        var result = await _httpClient.GetFromJsonAsync<UserAdminInfoDto>(uri);
        
        return result;
    }

    public async Task<bool> DeleteUserByIdAsAdminAsync(int id)
    {
        string uri = string.Format(AdminDeleteUserByIdUrl, id);
        var response = await _httpClient.DeleteAsync(uri);

        return response.IsSuccessStatusCode;
    }

    public async Task<AdminUserInfoDto> CreateUserByAdminAsync(UserCreateInfoDto userInfoDto)
    {
        //AdminCreateUserByIdUrl
        
        return await CreateAsync<UserCreateInfoDto, AdminUserInfoDto>(AdminCreateUserByIdUrl, userInfoDto); ;
    }

    public async Task<AdminUserInfoDto> UpdateUserByAdminAsync(UserUpdateInfoDto userInfoDto)
    {
        return await CreateAsync<UserUpdateInfoDto, AdminUserInfoDto>(AdminUpdateUserByIdUrl, userInfoDto);
    }

    
    public async Task<UserItemInfoDto[]> GetFilterUserAsync(UserFilterInfoDto userFilterInfoDto)
    {
        var query = WebSerializer.ToQueryString(AdminFilterUserUrl,userFilterInfoDto);

        var result = await _httpClient.GetFromJsonAsync<UserItemInfoDto[]>(query) 
            ??  throw new NullReferenceException("result");

        return result;
    }
    #endregion

    //CourseData
    #region CourseData
    public async Task<CourseItemInfoDto[]> GetAllCourseAsync(int? ownerId)
    {
        string uri = string.Format(CourseDataItemAllUrl, ownerId.ToString());
        var result = await _httpClient.GetFromJsonAsync<CourseItemInfoDto[]>(uri)
            ?? throw new NullReferenceException("result");

        return result;
    }

    public async Task<CourseFullInfoDto> GetCourseById(int courseId)
    {
        string uri = string.Format(CourseInfoDataUrl, courseId.ToString());
        var result = await _httpClient.GetFromJsonAsync<CourseFullInfoDto>(uri)
            ?? throw new NullReferenceException("result");

        return result;
    }

    public async Task<CreateCourseInfoDto> CreateCourseAsync(CreateCourseDto createCourseDto)
    {
        return await CreateAsync<CreateCourseDto, CreateCourseInfoDto>(CreateCourseUrl, createCourseDto);
    }

    public async Task<bool> DeleteCourseAsync(int id)
    {
        string uri = string.Format(DeleteCourseUrl, id);
        var response = await _httpClient.DeleteAsync(uri);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateCourseAsync(UpdateCourseDto updateCourse)
    {
       
        var response = await _httpClient.PostAsJsonAsync(UpdateCourseUrl, updateCourse);

        return response.IsSuccessStatusCode;
    }

    #endregion

    #region CourseDetails 
    public async Task<DetailsMaterialDto?> DetailsMaterialAsync(DetailsMaterialInfoDto detailsMaterialInfoDto)
    {
        var query = WebSerializer.ToQueryString(DetailsMaterialUrl, detailsMaterialInfoDto);
        var result = await _httpClient.GetFromJsonAsync<DetailsMaterialDto>(query);

        return result;
    }
    #endregion


}
