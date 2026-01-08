using System.Collections.Concurrent;
using HelloFlow.Models;

namespace HelloFlow.Repositories;

public class HelloRepository
{
    // Thread-Safe한 저장소
    private readonly ConcurrentDictionary<string, HelloResponse> _storage = new();
    
    // 1. 저장 (기존 기능)
    public void Save(HelloResponse data)
    {
        var key = Guid.NewGuid().ToString();
        _storage.TryAdd(key, data);
    }
    
    // 2. 전체 조회 (기존 기능)
    public List<HelloResponse> GetAll()
    {
        return _storage.Values.ToList();
    }

    // ▼▼▼ [여기가 누락되었습니다!] ▼▼▼
    // 3. 고급 검색 (SearchAdvanced)
    public List<HelloResponse> SearchAdvanced(string keyword, int pageNumber, int pageSize)
    {
        var query = _storage.Values.AsEnumerable(); 

        // 검색어 필터링 (대소문자 무시)
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(x => x.Message.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        // 정렬 및 페이징
        return query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }
}