using HelloFlow.Data;
using HelloFlow.Models;

namespace HelloFlow.Repositories;

public class HelloRepository : IHelloRepository
{
    // [En] Now depends on AppDbContext instead of a Dictionary.
    // [Ko] 이제 딕셔너리 대신 DB 관리자(Context)에 의존합니다.
    private readonly AppDbContext _context;

    public HelloRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Save(HelloResponse data)
    {
        // [En] Tracks the entity to be added. Not saved to DB yet.
        // [Ko] 데이터를 추가하겠다고 '표시'만 합니다. 아직 DB에 안 들어갔습니다.
        _context.HelloResponses.Add(data);
        
        // [En] Commits changes to the database. Generates INSERT SQL.
        // [Ko] 변경 사항을 확정합니다. 이때 실제로 INSERT SQL이 날아갑니다. (필수!)
        _context.SaveChanges();
    }

    public List<HelloResponse> GetAll()
    {
        // [En] SELECT * FROM HelloResponses
        // [Ko] 테이블의 모든 데이터를 가져옵니다.
        return _context.HelloResponses.ToList();
    }

    public List<HelloResponse> SearchAdvanced(string keyword, int pageNumber, int pageSize)
    {
        // [En] Prepare Query (Deferred Execution)
        // [Ko] 쿼리 준비 (지연 실행)
        var query = _context.HelloResponses.AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            // [En] Translates to SQL: WHERE Message LIKE '%keyword%'
            // [Ko] SQL의 LIKE 검색으로 자동 번역됩니다.
            query = query.Where(x => x.Message.Contains(keyword));
        }

        return query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList(); // [En] Execute Query / [Ko] 여기서 DB 조회 실행
    }
}