using HelloFlow.Models;
using HelloFlow.Repositories; // [준비물] 창고지기 연락처

namespace HelloFlow.Services;

public class HelloService
{
    // 1. 이제 List를 직접 안 가집니다. 대신 Repository를 가집니다.
    private readonly HelloRepository _repository;
    public readonly string InstanceId = Guid.NewGuid().ToString().Substring(0, 5);

    // 2. [계약] Service가 생성될 때 Repository가 필요합니다.
    public HelloService(HelloRepository repository)
    {
        _repository = repository;
    }

    public HelloResponse GetHello(string name)
    {
        // 3. 요리(로직)는 여전히 주방장이 합니다.
        var response = new HelloResponse
        {
            Message = $"Hello, {name}! (Repository Version)", // 표시를 위해 문구 살짝 변경
            CreatedAt = DateTime.Now,
            Location = "Cazis, Switzerland"
        };
        Console.WriteLine($"[Service ID]: {InstanceId} (new) | [Repo ID]: {_repository.InstanceId} (old)");
        // 4. [저장 위임] "어이 창고지기, 이거 좀 넣어둬."
        _repository.Save(response);

        return response;
    }
    // [추가] 단순 연결 역할
    public List<HelloResponse> FindHello(string keyword)
    {
        // 비즈니스 로직: "검색어가 없으면 빈 리스트를 줄까, 전체를 줄까?"
        // 여기선 "검색어가 비어있으면 전체 리스트 반환"으로 처리해 봅시다.
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return _repository.GetAll();
        }

        return _repository.Search(keyword);
    }

    public List<HelloResponse> GetHistory()
    {
        // 5. [조회 위임] "어이 창고지기, 명단 좀 줘봐."
        return _repository.GetAll();
    }
}