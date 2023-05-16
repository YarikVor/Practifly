using PractiFly.Api.Api.Login;
using PractiFly.Api.Client;
using PractiFly.Api.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiFly.Api
{
    public class PractiFlyClientWrapper
    {
        private readonly PractiFlyClient _client ;

        public PractiFlyClientWrapper(PractiFlyClient client)
        {
            _client = client;
        }

        public async Task<UserInfoDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
           var result = await _client.GetLoginResponseAsync(loginRequestDto);
            if (result == null)
            {
                throw new Exception("Login is invalid");
            }
            _client.UpdateToken(result.Token);

            return result.User;
        }
    }
}
