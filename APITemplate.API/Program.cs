using APITemplate.API.Middleware;
using APITemplate.Business.Abstract;
using APITemplate.Business.Concrete;
using APITemplate.Business.Logger;
using APITemplate.DataAccess.Abstract.DataManagement;
using APITemplate.DataAccess.Concrete.Context;
using APITemplate.DataAccess.Concrete.DataManagement;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.IO.Compression;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


//Response Compression
builder.Services.AddResponseCompression(options =>
{
	options.EnableForHttps = true;
	//options.Providers.Add<BrotliCompressionProvider>(); bu defaultta var güçlü ama eski teknolojiyi kapsamýyor. (daha az boyut döner)
	//options.Providers.Add<GzipCompressionProvider>(); bu her tarayýcýda çalýþýyor fakat brotli kadar güçlü sýkýþtýrma yapamýyor. (yine az boyut döner fakat brotliye göre fazla boyutlu döner)
});

// sýkýþtýrma levellerýný ayarlama
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
	// options.Level = CompressionLevel.Fastest; Default hali bu
	// response boyutu
	// Optimal > Fastest > SmallestSize
});

builder.Services.AddEndpointsApiExplorer();

//swagger'a token kontrolü ekleme
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "JwtTokenWithIdentity", Version = "v1", Description = "JwtTokenWithIdentity test app" });
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
							{
								Reference = new OpenApiReference
								{
									Type = ReferenceType.SecurityScheme,
									Id = "Bearer"
								}
							},
							new string[] {}
					}
				});
});

Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Debug()
	.WriteTo.Console()
	.WriteTo.File("logs/myLog-.txt", rollingInterval: RollingInterval.Day)
	.CreateLogger();

builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<APITemplateContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<Lazy<IUnitOfWork>>(provider => new Lazy<IUnitOfWork>(() => provider.GetRequiredService<IUnitOfWork>())); //Lazy kullaným
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IUserRoleService, UserRoleManager>();
builder.Services.AddScoped<IRoleService, RoleManager>();


builder.Services.AddControllers(options =>
{
    // Global olarak filtreyi ekle
    options.Filters.Add<LogActivityActionFilter>();
});

builder.Services.AddFluentValidationAutoValidation();

//sisteme token kontrolü
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.IncludeErrorDetails = true;
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["JWT:Issuer"], // Tokeni oluþturan tarafin adresi
		ValidAudience = builder.Configuration["JWT:Audiance"], // Tokenin kullanilacagi hedef adres
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Token"]))// Gizli anahtar
	};
});
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

//Response Compression
app.UseResponseCompression();

app.UseHttpsRedirection();

app.UseGlobalExceptionMiddleware();
app.UseCors(options => { options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });

app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
