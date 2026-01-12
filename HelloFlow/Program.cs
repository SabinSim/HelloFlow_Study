using HelloFlow.Services;
using HelloFlow.Repositories;

var builder = WebApplication.CreateBuilder(args);

// [En] Add services to the container (DI Setup).
// [Ko] 컨테이너에 서비스들을 등록합니다 (의존성 주입 설정).
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ▼▼▼ [Dependency Injection Configuration] ▼▼▼

// [En] Mapping Interface to Implementation.
// [Ko] 인터페이스와 구현체를 매핑(연결)합니다.
// [En] Meaning: "When any class asks for IHelloRepository, provide the HelloRepository instance."
// [Ko] 의미: "누군가 IHelloRepository를 달라고 하면, HelloRepository(구현체)를 줘라."
builder.Services.AddSingleton<IHelloRepository, HelloRepository>();

// [En] Register HelloService. It will automatically receive the repository above.
// [Ko] HelloService를 등록합니다. 위에서 등록한 리포지토리를 자동으로 주입받게 됩니다.
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