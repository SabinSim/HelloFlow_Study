using HelloFlow.Models;
using HelloFlow.Repositories;

namespace HelloFlow.Services;

public class HelloService
{
    // [En] Dependency: Depends on the Interface, NOT the concrete class (HelloRepository).
    // [Ko] 의존성: 구체적인 클래스(HelloRepository)가 아니라 인터페이스에 의존합니다.
    // [En] Why? This allows swapping the repository (e.g., to SQL) without changing this code.
    // [Ko] 이유? 이렇게 해야 나중에 코드를 안 고치고 저장소(예: SQL)를 교체할 수 있습니다.
    private readonly IHelloRepository _repository;

    // [En] Constructor Injection: Someone (Program.cs) must provide an implementation of IHelloRepository.
    // [Ko] 생성자 주입: 누군가(Program.cs)가 IHelloRepository를 구현한 객체를 넣어줘야 합니다.
    public HelloService(IHelloRepository repository)
    {
        _repository = repository;
    }

    public HelloResponse GetHello(string name)
    {
        var response = new HelloResponse
        {
            Message = $"Hello, {name}!",
            CreatedAt = DateTime.Now,
            Location = "Cazis, Switzerland"
        };

        // [En] Calls Save via the interface. The Service doesn't know "how" it's saved.
        // [Ko] 인터페이스를 통해 Save를 호출합니다. 서비스는 "어떻게" 저장되는지 모릅니다.
        _repository.Save(response);
        
        return response;
    }

    public List<HelloResponse> FindHelloAdvanced(string keyword, int page, int size)
    {
        // [En] Input Validation (Defensive Programming).
        // [Ko] 입력값 검증 (방어적 프로그래밍).
        int safePage = page < 1 ? 1 : page;
        int safeSize = size > 50 ? 50 : size;

        // [En] Delegates the search task to the repository.
        // [Ko] 검색 작업을 리포지토리에게 위임합니다.
        return _repository.SearchAdvanced(keyword, safePage, safeSize);
    }
}