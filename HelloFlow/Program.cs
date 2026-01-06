using HelloFlow.Services;       // 주방장(Service) 위치
using HelloFlow.Repositories;   // 창고지기(Repository) 위치

var builder = WebApplication.CreateBuilder(args);

// 1. [기본 설정] 컨트롤러(카운터) 기능을 켭니다.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Swagger 관련
builder.Services.AddSwaggerGen();           // Swagger 관련

// ▼▼▼ [핵심: 직원 등록] ▼▼▼

// 2. 창고지기(Repository) 등록 -> [Singleton]
// 의미: "서버가 켜져 있는 동안, 이 창고지기는 딱 한 명만 존재한다."
// 이유: 그래야 방명록(List)이 초기화되지 않고 계속 유지됩니다.
builder.Services.AddSingleton<HelloRepository>();

// 3. 주방장(Service) 등록 -> [Scoped]
// 의미: "손님 한 명이 올 때마다 새로 고용해서 쓰고, 가면 퇴근시킨다."
// 이유: 주방장은 이제 데이터를 안 가지고 있으니, 매번 새로 만들어도 상관없습니다.
// (Singleton인 창고지기한테 물건만 잘 맡기면 되니까요!)
builder.Services.AddScoped<HelloService>();

// ▲▲▲ [등록 끝] ▲▲▲

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); // 카운터 직원들을 자리에 배치합니다.

app.Run(); // 가게 문을 엽니다.