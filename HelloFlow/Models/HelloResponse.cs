namespace HelloFlow.Models;

public class HelloResponse
{
    // 이곳에는 string 만 쓸수 있고  이름표는 Message이다. 
    // { get; set; }  :  내가 입력할 수 도 있고 읽을수도 있음
    public string Message { get; set; } = string.Empty;    
    // 날짜와 시간만 들어간다.
    public DateTime CreatedAt { get; set; }    
    // 여기엔 위치(Location)가 들어간다.
    
    public string Location { get; set; } = string.Empty;    
}