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

public class PractiFlyClient 
{
    private readonly HttpClient _httpClient;

    public PractiFlyClient(string token)
    {
        //TODO: Checking validation token 
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(BasicUrl);
        _httpClient.DefaultRequestHeaders.Add(Authorization, string.Format(JwtDefaultFormat, token));
    }

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
    private const string CourseInfoDataUrl = "course/info?courseId={0}";
    private const string CreateCourseUrl = "course";
    private const string DeleteCourseUrl = "course?courseId={0}";
    private const string UpdateCourseUrl = "course/edit";
    private const string CourseUsersUrl = "course/users?courseId={0}";
    private const string CourseOwnerUrl = "course/owner?courseId={0}";
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
    private const string AllMaterialUrl = "material/all";

    #endregion
    #region Heading 
    private const string GetHeadingUrl = "heading?headingId={0}";
    private const string CreateHeadingUrl = "heading";
    private const string DeleteHeadingUrl = "heading?headingId={0}";
    private const string EditHeadingUrl = "heading/edit";
    #endregion
    #region HeadingCourse
    //
    private const string GetHeadingByBeginHeadUrl = "heading/sub?headingId={0}";
    private const string GetHeadingCourseUrl = "course/heading/sub?courseId={0}&beginCode={1}";
    private const string GetHeadingByCourseUrl = "course/heading/sub?courseId={0}";
    private const string ChangeHeadingInCourseUrl = "course/heading/include";
    #endregion

    # region CourseThemes
    private const string ListThemesUrl = "course/themes?courseId={0}";
    private const string ListMaterialsCourseUrl = "course/materials?courseId={0}";
    private const string InformationThemeUrl = "theme?themeId={0}";
    private const string CreateThemesUrl = "theme";
    private const string DeleteThemesUrl = "theme?themeId={0}";
    private const string UpdateThemesUrl = "theme/edit";
    private const string AddMaterialToThemeUrl = "theme_material";
    private const string DeleteThemeMaterialUrl = "theme_material";
    private const string UpdateThemeMaterialUrl = "theme_material/edit";
    #endregion
    #endregion
    #endregion


    #region BasicMethods
   
    private async Task<bool> CreateUpdateAsync<TInput>(string url, TInput dto)
    {
        var response = await _httpClient.PostAsJsonAsync(url, dto);
        return response.IsSuccessStatusCode;
    }
 
    private async Task<TOutput?> GetAsync<TInput, TOutput>(string url, TInput dto)
    {
        string uri = string.Format(url, dto);
        var result = await _httpClient.GetFromJsonAsync<TOutput?>(uri);
        return result;
    }
    private async Task<bool> DeleteAsync(string url, int id)
    {
        string uri = string.Format(url, id);
        var response = await _httpClient.DeleteAsync(uri);

        return response.IsSuccessStatusCode;
    }
    #region 
    private async Task<TOutput> CreateAsync<TInput, TOutput>(string url, TInput dto)
    {
        var response = await _httpClient.PostAsJsonAsync(url, dto);
        var result = await response.Content.ReadFromJsonAsync<TOutput>()
            ?? throw new NullReferenceException("result");
        return result;
    }
    #endregion
    #endregion

    #region Login
    //LoginResponseDto
    public async Task<bool> GetLoginResponseAsync(LoginRequestDto loginRequestDto)
    {
        return await CreateUpdateAsync(LoginUrl, loginRequestDto);
    }
    public async Task<LoginResponseDto?> GetLoginUsersDataAsync(LoginRequestDto loginRequestDto)
    {
        var response = await _httpClient.PostAsJsonAsync(LoginUrl, loginRequestDto);
        var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
        return result;
    }
    public async Task<string> GetRefreshTokenAsync()
    {
        var result = await _httpClient.GetStringAsync(RefreshTokenUrl) 
            ?? throw new NullReferenceException("result"); 
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
    public async Task<AdminUserInfoDto?> GetUserByIdAsAdminAsync(int id)
    {
        return await GetAsync<int, AdminUserInfoDto>(AdminGetUserByIdUrl, id);
    }

    public async Task<bool> DeleteUserByIdAsAdminAsync(int id)
    {
        return await DeleteAsync(AdminDeleteUserByIdUrl,id);
    }

    public async Task<bool> CreateUserByAdminAsync(UserCreateInfoDto userInfoDto)
    {
        //AdminCreateUserByIdUrl
        return await CreateUpdateAsync(AdminCreateUserByIdUrl, userInfoDto);
        
    }

    public async Task<bool> UpdateUserByAdminAsync(UserUpdateInfoDto userInfoDto)
    {

        return await CreateUpdateAsync(AdminUpdateUserByIdUrl, userInfoDto);

    }
    public async Task<UserItemInfoDto[]> GetFilterUserAsync(UserFilterInfoDto userFilterInfoDto)
    {
        var query = WebSerializer.ToQueryString(AdminFilterUserUrl,userFilterInfoDto);

        var result = await _httpClient.GetFromJsonAsync<UserItemInfoDto[]>(query)
            ?? throw new NullReferenceException("result");
        return result;
    }
    #endregion

    #region CourseData
    public async Task<CourseItemInfoDto[]?> GetAllCourseAsync(int? ownerId)
    {

        return await GetAsync<string, CourseItemInfoDto[]>(CourseDataItemAllUrl, ownerId.ToString());

    }

    public async Task<CourseFullInfoDto> GetCourseById(int courseId)
    {
        string uri = string.Format(CourseInfoDataUrl, courseId.ToString());
        var result = await _httpClient.GetFromJsonAsync<CourseFullInfoDto>(uri)
            ?? throw new NullReferenceException("result");

        return result;
    }

    public async Task<bool> CreateCourseAsync(CreateCourseDto createCourseDto)
    {
        return await CreateUpdateAsync(CreateCourseUrl, createCourseDto);
    }

    public async Task<bool> DeleteCourseAsync(int id)
    {
        return await DeleteAsync(DeleteCourseUrl, id);
    }

    public async Task<bool> UpdateCourseAsync(UpdateCourseDto updateCourse)
    {
        return await CreateUpdateAsync(UpdateCourseUrl, updateCourse);
    }
    //CourseUsersUrl
    public async Task<UserItemInfoDto[]?> GetUserCourseAsync(int Id)
    {
        return await GetAsync<int, UserItemInfoDto[]>(CourseUsersUrl, Id);
    }
    public async Task<OwnerInfoDto?> GetOwnerCourseAsync(int Id)
    {
      
        return await GetAsync<int, OwnerInfoDto>(CourseOwnerUrl, Id);
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

    public async Task<ListsHeadingsCoursesInfoDto[]?> GetListHeadingCourseByIdAsync(int id)
    {
        return await GetAsync<int, ListsHeadingsCoursesInfoDto[]>(ListsHeadingsCoursesUrl, id);
    }

    public async Task<MaterialIncludingCourseInfoDto[]?> GetDetailsMaterialAsync(MaterialIncludingCourseDto materialIncludingCourseDto)
    {
        var query = WebSerializer.ToQueryString(MaterialIncludingCourseDtoUrl, materialIncludingCourseDto);
        var result = await _httpClient.GetFromJsonAsync<MaterialIncludingCourseInfoDto[]>(query);

        return result;
    }

    #endregion

    # region MaterialBlocks
    public async Task<ListMaterialsInfoDto[]?> GetListMaterialsByIdAsync(int id)
    {
        return await GetAsync<int, ListMaterialsInfoDto[]>(GetListMaterialsUrl, id);
    }

    public async Task<bool> CreateMaterialAsync(CreateMaterialBlockDto createMaterialsDto)
    {
        return await CreateUpdateAsync(CreateMaterialBlockUrl, createMaterialsDto);
    }

    public async Task<bool> EditMaterialAsync(EditMaterialBlockDto editMaterialsDto)
    {
        return await CreateUpdateAsync(EditMaterialBlockUrl, editMaterialsDto);
    }

    public async Task<ListAllMaterialInfoDto[]?> GetAllListMaterialsAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<ListAllMaterialInfoDto[]>(AllMaterialUrl)
            ?? throw new NullReferenceException("result");
        return result;
    }
    #endregion

    #region Heading

    public async Task<GetHeadingInfoDto?> GetHeadingByHeadIdAsync(int headingId)
    {
        return await GetAsync<int, GetHeadingInfoDto>(GetHeadingUrl, headingId);
    }
    public async Task<bool> CreateHeadingAsync(CreateHeadingDto createHeadingDto)
    {
        var response = await _httpClient.PostAsJsonAsync(CreateHeadingUrl, createHeadingDto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteHeadingAsync(int id)
    {
        return await DeleteAsync(DeleteHeadingUrl, id);
    }
    public async Task<bool> EditHeadingAsync(EditHeadingDto editHeadingDto)
    {
        var response = await _httpClient.PostAsJsonAsync(EditHeadingUrl, editHeadingDto);
        return response.IsSuccessStatusCode;
    }

    #endregion

    #region HeadingCourse

    public async Task<GetHeadingBeginInfoDto[]> GetHeadingByBeginHeadCodeAsync(int? headingId)
    {
        string uri = string.Format(GetHeadingByBeginHeadUrl, headingId);
        var result = await _httpClient.GetFromJsonAsync<GetHeadingBeginInfoDto[]>(uri)
            ?? throw new NullReferenceException("result");

        return result;
    }
    
    //public async Task<GetHeadingBeginInfoDto[]> GetHeadingCourseAsync(HeadingsInCourseDto heading)
    //{
    //    var query = WebSerializer.ToQueryString(GetHeadingCourseUrl, heading);

    //    var result = await _httpClient.GetFromJsonAsync<GetHeadingBeginInfoDto[]>(query)
    //        ?? throw new NullReferenceException("result");

    //    return result;
    //}
    public async Task<GetHeadingBeginInfoDto[]?> GetHeadingCourseAsync(int heading)
    {
        return await GetAsync<int, GetHeadingBeginInfoDto[]>(GetHeadingByCourseUrl, heading);
    }
    public async Task<GetHeadingBeginInfoDto[]?> GetHeadingCourseAsync(int courseId, string beginCode)
    {
        object[] mas = { courseId, beginCode };
        var uri = string.Format(GetHeadingCourseUrl, mas);

        var result = await _httpClient.GetFromJsonAsync<GetHeadingBeginInfoDto[]>(uri);
        return result;
    }

    public async Task<bool> ChangeHeadingInCourseAsync(ChangeHeadingInCourseDto changeHeadingInCourseDto)
    {
        return await CreateUpdateAsync(ChangeHeadingInCourseUrl, changeHeadingInCourseDto);
    }

    #endregion

    #region CourseThemes
    public async Task<ListThemesInfoDto[]?> GetListThemesCourseByIdAsync(int id)
    {
        
        return await GetAsync<int, ListThemesInfoDto[]>(ListThemesUrl, id);
    }
    public async Task<ListMaterialsCourseInfoDto[]?> GetListMaterialsCourseByIdAsync(int id)
    {
        return await GetAsync<int, ListMaterialsCourseInfoDto[]>(ListMaterialsCourseUrl, id);
    }

    public async Task<ThemeInformationDto?> GetInformationThemeByIdAsync(int themeId)
    {
        return await GetAsync<int, ThemeInformationDto>(InformationThemeUrl, themeId);
    }

    public async Task<bool> CreateThemesOfCourseAsync(CreateThemesOfCourseDto createThemesOfCourseDto)
    {
        return await CreateUpdateAsync(CreateThemesUrl, createThemesOfCourseDto);
    }
    public async Task<bool> DeleteThemesAsync(int id)
    {
        return await DeleteAsync(DeleteThemesUrl, id);
    }

    public async Task<bool> UpdateThemesAsync(UpdateThemesDto updateThemesDto)
    {
        return await CreateUpdateAsync(UpdateThemesUrl, updateThemesDto);
    }

    public async Task<bool> AddMaterialToThemeAsync(AddMaterialToThemeDto addMaterialToTheme)
    {
        return await CreateUpdateAsync(AddMaterialToThemeUrl, addMaterialToTheme);
    }

    public async Task<bool> DeleteThemeMaterialAsync(int id)
    {
        return await DeleteAsync(DeleteThemeMaterialUrl, id);
    }

    public async Task<bool> UpdateThemeMaterialAsync(UpdateThemeMaterialDto updateThemeMaterial)
    {
        return await CreateUpdateAsync(UpdateThemeMaterialUrl, updateThemeMaterial);

    }
    #endregion
}
