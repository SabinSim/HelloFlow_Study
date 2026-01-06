using System.Collections.Concurrent; // [필수] 동시성 컬렉션 도구 상자
using HelloFlow.Models;

namespace HelloFlow.Repositories;

public class HelloRepository
{
    // 1. [변경] List(단순 목록) -> ConcurrentDictionary(키-값 저장소)
    // - Key(string): 손님 이름 (검색용)
    // - Value(HelloResponse): 명함 내용
    // - Concurrent: "동시 접속이 와도 내가 알아서 교통정리 할게."
    private readonly ConcurrentDictionary<string, HelloResponse> _storage = new();

    public void Save(HelloResponse data)
    {
        // 2. [저장] '이름'을 꼬리표(Key)로 달아서 저장합니다.
        // TryAdd: "혹시 이미 같은 이름이 있으면 저장하지 말고, 없으면 저장해라." (안전함)
        // 여기서는 간단하게 덮어쓰기(Key값 접근)로 구현하겠습니다.
        
        // 입력된 이름(name)을 키값으로 사용. 
        // 주의: data.Message에서 이름을 뽑거나, 저장할 때 이름을 따로 받아야 더 완벽하지만,
        // 여기선 개념 이해를 위해 '단순화'해서 그냥 Message 자체를 키로 쓰거나(비추천), 
        // 억지로라도 고유 ID를 만드는 게 맞습니다. 
        
        // 편의상 Key를 "생성 시간"이나 "UUID"로 쓰기도 하지만, 
        // 여기서는 '학습용'으로 이름을 Key로 가정하겠습니다. (실제론 중복 이름 이슈 있음)
        // 일단 단순히 "저장" 기능에 집중하겠습니다.
        
        var key = Guid.NewGuid().ToString(); // 고유한 ID 생성 (주민등록번호 같은 것)
        _storage.TryAdd(key, data);
    }

    public List<HelloResponse> GetAll()
    {
        // 3. [반환] 딕셔너리에 있는 '값(Value)'들만 싹 모아서 리스트로 줍니다.
        return _storage.Values.ToList();
    }
}