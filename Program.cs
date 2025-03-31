using TasksApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure the AntiForgery options
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-XSRF-TOKEN";
    options.SuppressXFrameOptionsHeader = false;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<TaskService>();

// Enable CORS
builder.Services.AddCors(options =>
{
    // Define a policy that allows all origins, headers, and methods
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// Enable CORS middleware
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();