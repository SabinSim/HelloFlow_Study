using HelloFlow.Services;
using HelloFlow.Repositories;
using HelloFlow.Data; // [Step 3.0] Added
using Microsoft.EntityFrameworkCore; // [Step 3.0] Added

var builder = WebApplication.CreateBuilder(args);
// 1. appsettings.json에서 Azure 주소를 가져옵니다.
var connectionString = builder.Configuration.GetConnectionString("AzureSqlConnection");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// 2. Azure SQL Server용 배관으로 교체합니다.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// ▼▼▼ [Step 3.0: Database Configuration] ▼▼▼

// [En] Configure Entity Framework to use SQLite. "app.db" is the filename.
// [Ko] EF Core가 SQLite를 사용하도록 설정합니다. 데이터는 "app.db" 파일에 저장됩니다.
// AZ-104 Tip: 나중에 이 부분만 "UseSqlServer"로 바꾸면 Azure SQL로 연결됩니다.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// ▼▼▼ [Step 3.0: DI Container Update] ▼▼▼

// [En] CHANGE: Singleton -> Scoped.
// [Ko] 변경: Singleton에서 Scoped로 변경합니다.
// [Reason] DbContext is Scoped. Singleton cannot depend on Scoped services.
// [이유] DbContext가 Scoped(요청마다 생성)이기 때문에, Repository도 수명을 맞춰줘야 에러가 안 납니다.
builder.Services.AddScoped<IHelloRepository, HelloRepository>();

// Service is already Scoped, so keep it as is.
builder.Services.AddScoped<HelloService>();

// ▲▲▲ [Configuration End] ▲▲▲

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();