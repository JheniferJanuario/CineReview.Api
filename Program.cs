using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using CineReview.Api.Data;
using CineReview.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona controladores e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura o EF Core com SQLite
builder.Services.AddDbContext<CineReviewContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["AppSettings:JwtKey"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Injeção de dependências
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMidiaService, MidiaService>();
builder.Services.AddScoped<IApiExternaService, ApiExternaService>();
builder.Services.AddHttpClient();

// Cria o app
var app = builder.Build();

// Executa migrações e popula o banco com dados iniciais
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CineReviewContext>();
    context.Database.Migrate();
    SeedData.Initialize(context);
}

// Configura o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // ✅ esta linha estava cortada no seu código

app.Run();
