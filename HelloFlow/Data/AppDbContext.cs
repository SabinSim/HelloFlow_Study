using HelloFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloFlow.Data;

// DbContext: C# 코드와 실제 DB 사이를 연결하는 '다리(Bridge)'이자 '관리자'입니다.
public class AppDbContext : DbContext
{
    // 생성자: 외부(Program.cs)에서 DB 설정(옵션)을 받아서 부모(base)에게 넘깁니다.
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DbSet: "이 데이터를 DB 테이블로 만들어라"라는 명령입니다.
    // 테이블 이름은 'HelloResponses'가 됩니다.
    public DbSet<HelloResponse> HelloResponses { get; set; }
}