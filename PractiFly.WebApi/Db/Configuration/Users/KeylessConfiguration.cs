using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PractiFly.WebApi.EntityDb.Users;

namespace PractiFly.WebApi.Db.Configuration.Users;


public class KeylessConfiguration<T> : IEntityTypeConfiguration<T> where T : class
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasNoKey();
    }
}

