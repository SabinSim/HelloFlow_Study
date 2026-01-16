using HelloFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloFlow.Data;

// [En] The bridge between C# code and the Database.
// [Ko] C# 코드와 실제 데이터베이스 사이를 연결하는 다리(Bridge) 역할을 합니다.
public class AppDbContext : DbContext
{
    // [En] Constructor receives options (like connection string) and passes them to the base class.
    // [Ko] 생성자: 외부(Program.cs)에서 설정한 DB 옵션(연결 정보 등)을 받아서 부모 클래스에 넘깁니다.
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // [En] Represents the 'HelloResponses' table in the database.
    // [Ko] DB 안에 'HelloResponses'라는 이름의 테이블을 생성하겠다는 의미입니다.
    public DbSet<HelloResponse> HelloResponses { get; set; }
}