using Cysharp.Web;
using PractiFly.Api.Admin;
using PractiFly.Api.Api.Admin;
using PractiFly.Api.Api.CourseData;
using PractiFly.Api.Api.CourseDetails;
using PractiFly.Api.Api.CourseMaterials;
using PractiFly.Api.Api.CourseThemes;
using PractiFly.Api.Api.Heading;
using PractiFly.Api.Api.HeadingCourse;
using PractiFly.Api.Api.Login;
using PractiFly.Api.Api.MaterialBlocks;
using PractiFly.Api.CourseData;
using PractiFly.Api.CourseDetails;
using PractiFly.Api.CourseMaterials;
using PractiFly.Api.CourseThemes;
using PractiFly.Api.Heading;
using PractiFly.Api.HeadingCourse;
using PractiFly.Api.Login;
using PractiFly.Api.MaterialBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PractiFly.Api.Client;

public class PractiFlyClient : IPractiFlyClient
{
    private readonly HttpClient _httpClient;

    #region Constants
    private const string BasicUrl = "https://localhost:5001/api/";
    private const string JwtDefaultFormat = "Bearer {0}";
    private const string JwtDefaultScheme = "Bearer";
    private const string Authorization = "Authorization";


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

    #region CourseMaterials
    private const string ListsHeadingsCoursesUrl = "course/headings?courseId={0}";
    private const string MaterialIncludingCourseDtoUrl = "course/heading/materials";

    #endregion
    #region MaterialBlocks
    private const string GetListMaterialsUrl = "heading/materials?headingId={0}";
    private const string CreateMaterialBlockUrl = "material";
    private const string EditMaterialBlockUrl = "material/edit";

    #endregion
    #region Heading 
    private const string GetHeadingUrl = "heading";
    private const string CreateHeadingUrl = "heading";
    private const string DeleteHeadingUrl = "heading";
    private const string EditHeadingUrl = "heading/edit";
    #endregion
    #region HeadingCourse
    private const string GetHeadingByBeginHeadUrl = "heading/sub";
    private const string ChangeHeadingInCourseUrl = "heading/include";
    #endregion

    # region CourseThemes
    private const string ListThemesUrl = "course/themes";
    private const string ListMaterialsCourseUrl = "course/materials";
    private const string InformationThemeUrl = "theme";
    private const string CreateThemesUrl = "theme";
    private const string DeleteThemesUrl = "theme";
    private const string UpdateThemesUrl = "theme/edit";
    private const string AddMaterialToThemeUrl = "theme_material";
    private const string DeleteThemeMaterialUrl = "theme_material";
    private const string UpdateThemeMaterialUrl = "theme_material/edit";
    #endregion
    #endregion
    #endregion



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
    public async Task<DetailsMaterialInfoDto?> GetDetailsMaterialAsync(DetailsMaterialDto detailsMaterialInfoDto)
    {
        var query = WebSerializer.ToQueryString(DetailsMaterialUrl, detailsMaterialInfoDto);
        var result = await _httpClient.GetFromJsonAsync<DetailsMaterialInfoDto>(query);

        return result;
    }
    #endregion

    # region CourseMaterials

    public async Task<ListsHeadingsCoursesInfoDto[]> GetListHeadingCourseByIdAsync(int Id)
    {
        string uri = string.Format(ListsHeadingsCoursesUrl, Id.ToString());
        var result = await _httpClient.GetFromJsonAsync<ListsHeadingsCoursesInfoDto[]>(uri)
            ?? throw new NullReferenceException("result");

        return result;
    }

    public async Task<MaterialIncludingCourseInfoDto[]?> GetDetailsMaterialAsync(MaterialIncludingCourseDto materialIncludingCourseDto)
    {
        var query = WebSerializer.ToQueryString(MaterialIncludingCourseDtoUrl, materialIncludingCourseDto);
        var result = await _httpClient.GetFromJsonAsync<MaterialIncludingCourseInfoDto[]>(query);

        return result;
    }

    #endregion

    # region MaterialBlocks
    public async Task<ListMaterialsInfoDto[]> GetListMaterialsByIdAsync(int Id)
    {
        string uri = string.Format(GetListMaterialsUrl, Id.ToString());
        var result = await _httpClient.GetFromJsonAsync<ListMaterialsInfoDto[]>(uri)
            ?? throw new NullReferenceException("result");

        return result;
    }

    public async Task<bool> CreateMaterialAsync(CreateMaterialBlockDto createMaterialsDto)
    {
        return await CreateAsync<CreateMaterialBlockDto, bool>(CreateMaterialBlockUrl, createMaterialsDto);
    }

    public async Task<bool> EditMaterialAsync(EditMaterialBlockDto createMaterialsDto)
    {
        return await CreateAsync<EditMaterialBlockDto, bool>(EditMaterialBlockUrl, createMaterialsDto);
    }
    #endregion

    #region Heading

    public async Task<GetHeadingInfoDto>  GetHeadingByUdcOrHeadIdAsync(GetHeadingDto getHeading)
    {
        var query = WebSerializer.ToQueryString(GetHeadingUrl, getHeading);
        var result = await _httpClient.GetFromJsonAsync<GetHeadingInfoDto>(query)
            ?? throw new NullReferenceException("result");

        return result;
    }
    public async Task<bool> CreateHeadingAsync(CreateHeadingDto createHeadingDto)
    {
        return await CreateAsync<CreateHeadingDto, bool>(CreateHeadingUrl, createHeadingDto);
    }

    public async Task<bool> DeleteHeadingAsync(int id)
    {
        string uri = string.Format(DeleteHeadingUrl, id);
        var response = await _httpClient.DeleteAsync(uri);

        return response.IsSuccessStatusCode;
    }
    public async Task<bool> EditHeadingAsync(EditHeadingDto editHeadingDto)
    {
        return await CreateAsync<EditHeadingDto, bool>(EditHeadingUrl, editHeadingDto);
    }

    #endregion

    #region HeadingCourse
    public async Task<GetHeadingBeginInfoDto[]> GetHeadingByBeginHeadCodeAsync(string code)
    {
        string uri = string.Format(GetHeadingByBeginHeadUrl, code);
        var result = await _httpClient.GetFromJsonAsync<GetHeadingBeginInfoDto[]>(uri)
            ?? throw new NullReferenceException("result");

        return result;
    }

    public async Task<bool> ChangeHeadingInCourseAsync(ChangeHeadingInCourseDto changeHeadingInCourseDto)
    {
        return await CreateAsync<ChangeHeadingInCourseDto, bool>(ChangeHeadingInCourseUrl, changeHeadingInCourseDto);
    }

    #endregion

    #region CourseThemes
    public async Task<ListThemesInfoDto[]> GetListThemesCourseByIdAsync(int id)
    {
        string uri = string.Format(ListThemesUrl, id);
        var result = await _httpClient.GetFromJsonAsync<ListThemesInfoDto[]>(uri)
            ?? throw new NullReferenceException("result");

        return result;
    }
    public async Task<ListMaterialsCourseInfoDto[]> GetListMaterialsCourseByIdAsync(int id)
    {
        string uri = string.Format(ListMaterialsCourseUrl, id);
        var result = await _httpClient.GetFromJsonAsync<ListMaterialsCourseInfoDto[]>(uri)
            ?? throw new NullReferenceException("result");

        return result;
    }

    public async Task<ThemeInformationDto> GetInformationThemeByIdAsync(int themeId)
    {
        string uri = string.Format(InformationThemeUrl, themeId);
        var result = await _httpClient.GetFromJsonAsync<ThemeInformationDto>(uri)
            ?? throw new NullReferenceException("result");

        return result;
    }

    public async Task<bool> CreateThemesOfCourseAsync(CreateThemesOfCourseDto createThemesOfCourseDto)
    {
        return await CreateAsync<CreateThemesOfCourseDto, bool>(CreateThemesUrl, createThemesOfCourseDto);
    }
    public async Task<bool> DeleteThemesAsync(int id)
    {
        string uri = string.Format(DeleteThemesUrl, id);
        var response = await _httpClient.DeleteAsync(uri);

        return response.IsSuccessStatusCode;
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public async Task<bool> UpdateThemesAsync(UpdateThemesDto updateThemesDto)
    {
        return await CreateAsync<UpdateThemesDto, bool>(UpdateThemesUrl, updateThemesDto);
    }

    public async Task<bool> AddMaterialToThemeAsync(AddMaterialToThemeDto addMaterialToTheme)
    {
        return await CreateAsync<AddMaterialToThemeDto, bool>(AddMaterialToThemeUrl, addMaterialToTheme);
    }

    public async Task<bool> DeleteThemeMaterialAsync(int id)
    {
        string uri = string.Format(DeleteThemeMaterialUrl, id);
        var response = await _httpClient.DeleteAsync(uri);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateThemeMaterialAsync(UpdateThemeMaterialDto updateThemeMaterial)
    {
        return await CreateAsync<UpdateThemeMaterialDto, bool>(UpdateThemeMaterialUrl, updateThemeMaterial);
    }
    #endregion
}
