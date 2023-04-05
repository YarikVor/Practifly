using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PractiFly.WebApi.EntityDb.Users;

namespace PractiFly.WebApi.Db.Configuration.Users;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    
    
    
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User
            {
                Id = 2,
                FirstName = "Yarik",
                LastName = "Vorobyov",
                Email = "yarikhelov@gmail.com",
                Phone = "+380501234567",
                FilePhoto = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                RegistrationDate = new(2021, 1, 1),
                Note = null
            },
            new User
            {
                Id = 3,
                FirstName = "Vadim",
                LastName = "Manchenko",
                Email = "clashstarset2017@gmail.com",
                Phone = "+380501234567",
                FilePhoto = ""
            }
        );
        
        
        
    }
}
