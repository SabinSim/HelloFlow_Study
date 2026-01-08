using HelloFlow.Models;
using HelloFlow.Repositories;

namespace HelloFlow.Services;

public class HelloService
{
    private readonly HelloRepository _repository;

    public HelloService(HelloRepository repository)
    {
        _repository = repository;
    }
    
    // (기존 GetHello 등은 유지...)
    public HelloResponse GetHello(string name)
    {
        // ... (생략)
        var response = new HelloResponse
        {
            Message = $"Hello, {name}!",
            CreatedAt = DateTime.Now,
            Location = "Cazis, Switzerland"
        };
        _repository.Save(response);
        return response;
    }

    // ▼▼▼ [Architect Version 핵심: 입력 값 검증] ▼▼▼
    public List<HelloResponse> FindHelloAdvanced(string keyword, int page, int size)
    {
        // 1. [Sanitization] 페이지 번호가 1보다 작으면 1로 보정
        int safePage = page < 1 ? 1 : page;

        // 2. [Throttling] 한 번에 너무 많은 데이터를 요청하면 50개로 제한 (서버 보호)
        int safeSize = size > 50 ? 50 : size;

        // 3. 안전한 값으로 리포지토리에 위임
        return _repository.SearchAdvanced(keyword, safePage, safeSize);
    }
}