using System.Collections.Concurrent;
using HelloFlow.Models;

namespace HelloFlow.Repositories;

// [En] Implements IHelloRepository. The compiler ensures all interface methods exist here.
// [Ko] IHelloRepository를 구현합니다. 컴파일러는 인터페이스의 모든 메서드가 여기 있는지 감시합니다.
public class HelloRepository : IHelloRepository
{
    // [En] Thread-safe storage to simulate a database in memory.
    // [Ko] 메모리 상에서 데이터베이스를 흉내 내기 위한 스레드 안전 저장소입니다.
    private readonly ConcurrentDictionary<string, HelloResponse> _storage = new();

    // [En] Implements the Save method defined in the interface.
    // [Ko] 인터페이스에 정의된 Save 메서드를 실제로 구현합니다.
    public void Save(HelloResponse data)
    {
        // [En] Generates a unique Key (simulating a Primary Key in DB).
        // [Ko] 고유 키를 생성합니다 (DB의 Primary Key 역할).
        var key = Guid.NewGuid().ToString();
        
        // [En] Saves data to the dictionary.
        // [Ko] 딕셔너리에 데이터를 저장합니다.
        _storage.TryAdd(key, data);
    }

    // [En] Implements the GetAll method.
    // [Ko] GetAll 메서드를 구현합니다.
    public List<HelloResponse> GetAll()
    {
        return _storage.Values.ToList();
    }

    // [En] Implements advanced search logic using LINQ.
    // [Ko] LINQ를 사용하여 고급 검색 로직을 구현합니다.
    public List<HelloResponse> SearchAdvanced(string keyword, int pageNumber, int pageSize)
    {
        // [En] Prepares the query without executing it immediately (Deferred Execution).
        // [Ko] 즉시 실행하지 않고 쿼리 계획만 준비합니다 (지연 실행).
        var query = _storage.Values.AsEnumerable(); 

        // [En] Filters by keyword if it's not empty (Case-insensitive).
        // [Ko] 검색어가 비어있지 않다면 필터링합니다 (대소문자 무시).
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(x => x.Message.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        // [En] Orders by date, skips previous pages, and takes only the page size.
        // [Ko] 날짜순 정렬 후, 이전 페이지는 건너뛰고(Skip), 정해진 개수만 가져옵니다(Take).
        return query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList(); // [En] Executes the query now. / [Ko] 이제 쿼리를 실행합니다.
    }
}