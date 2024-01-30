using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Resaul.App.Domain.Models;

namespace Resaul.App.Persistence;

public class PersistenceContext(DbContextOptions<PersistenceContext> options) : IdentityDbContext<UserModel>(options)
{
}
