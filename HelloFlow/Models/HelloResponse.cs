using System.ComponentModel.DataAnnotations; // [Key] 어트리뷰트 사용을 위해 필수

namespace HelloFlow.Models;

public class HelloResponse
{
    // [En] Primary Key for Database. The [Key] attribute tells EF Core this is the ID.
    // [Ko] 데이터베이스의 기본 키(PK)입니다. [Key]를 붙여야 DB가 식별자로 인식합니다.
    [Key]
    public int Id { get; set; } 

    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string Location { get; set; } = string.Empty;
}