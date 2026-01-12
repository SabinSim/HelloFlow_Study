using HelloFlow.Models;

namespace HelloFlow.Repositories;

// [En] Interface: A contract that defines "what" must be done, not "how".
// [Ko] 인터페이스: "어떻게"가 아니라 "무엇을" 해야 하는지 정의하는 계약서입니다.
public interface IHelloRepository
{
    // [En] Contract: Implementing class MUST have a Save method.
    // [Ko] 계약조건: 이 인터페이스를 쓰는 클래스는 반드시 Save 메서드를 가지고 있어야 합니다.
    void Save(HelloResponse data);

    // [En] Contract: Must provide a method to get all data.
    // [Ko] 계약조건: 모든 데이터를 조회하는 메서드를 제공해야 합니다.
    List<HelloResponse> GetAll();

    // [En] Contract: Must provide advanced search functionality.
    // [Ko] 계약조건: 고급 검색(페이징 등) 기능을 제공해야 합니다.
    List<HelloResponse> SearchAdvanced(string keyword, int pageNumber, int pageSize);
}