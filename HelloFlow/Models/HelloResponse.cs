namespace HelloFlow.Models;

public class HelloResponse
{
    // [En] Prevents null reference warnings by initializing with an empty string.
    // [Ko] null 참조 경고를 방지하기 위해 빈 문자열로 초기화합니다.
    public string Message { get; set; } = string.Empty; 

    // [En] Records the timestamp when the object was created.
    // [Ko] 객체가 생성된 시점의 시간을 기록합니다.
    public DateTime CreatedAt { get; set; }

    // [En] Stores the location information. Initialized to empty to avoid null issues.
    // [Ko] 위치 정보를 저장합니다. null 문제를 피하기 위해 빈 값으로 초기화합니다.
    public string Location { get; set; } = string.Empty;
}