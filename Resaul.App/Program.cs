using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Resaul.App.Domain.Models;
using Resaul.App.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PersistenceContext>(e => e.UseInMemoryDatabase(new Guid().ToString()));
var identity = builder.Services.AddIdentityApiEndpoints<UserModel>(e =>
{
    e.SignIn.RequireConfirmedEmail = true;

    e.Password.RequiredUniqueChars = 0;
    e.Password.RequiredLength = 6;

    e.Password.RequireNonAlphanumeric = false;
    e.Password.RequireUppercase = false;
    e.Password.RequireDigit = false;
});


identity.AddPasswordlessLoginProvider();
identity.AddEntityFrameworkStores<PersistenceContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
