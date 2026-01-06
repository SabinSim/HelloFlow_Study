using Microsoft.AspNetCore.Mvc; // 1. 웹(Web)과 관련된 도구들을 가져옵니다.
using HelloFlow.Services;       // 2. 일을 시킬 '주방(Services)'의 위치를 파악합니다.
using HelloFlow.Models;         // 3. 데이터를 담을 '그릇(Models)'의 규격을 가져옵니다.

namespace HelloFlow.Controllers; // 4. 이 직원의 소속 부서는 'Controllers'입니다.

[ApiController] // 5. [신분증] 저는 일반 클래스가 아니라 'API 처리 담당자'입니다.
[Route("api/[controller]")] // 6. [창구 번호] 저를 찾으려면 주소창에 '/api/hello'라고 치세요.
public class HelloController : ControllerBase // 7. 저는 'ControllerBase'라는 표준 규정을 준수합니다.
{
    // 8. [파트너] 제가 일을 하려면 'HelloService'라는 전문가가 반드시 필요합니다.
    private readonly HelloService _service;

    // 9. [고용 계약서(생성자)] 
    // 시스템(Program.cs)이 저를 생성할 때, 반드시 HelloService를 제 손에 쥐여줘야 합니다. (의존성 주입)
    public HelloController(HelloService service)
    {
        _service = service; // 10. 넘겨받은 전문가를 제 전용 변수에 저장해두고 계속 씁니다.
    }

    [HttpGet] // 11. [업무 시간] 'GET(조회)' 요청이 들어오면 제가 처리합니다.
    // 12. [입력] 손님은 반드시 'name'이라는 글자를 줘야 합니다. (예: ?name=Sabin)
    public IActionResult SayHello(string name) 
    {
        // 13. [위임] 저는 요리 안 합니다. 주방장(_service)에게 이름을 던져주고 결과(명함)를 받아옵니다.
        // 이때 'result'는 단순 글자가 아니라 'HelloResponse'라는 객체입니다.
        var result = _service.GetHello(name); 
        
        // 14. [응답] 주방장이 준 고급진 결과물을 '200 OK' 상자에 담아 손님에게 줍니다.
        // C#이 자동으로 JSON 형식 {"message": "...", ...} 으로 변환해서 내보냅니다.
        return Ok(result); 
    }

    [HttpGet("history")] // [새 창구] 주소창에 '/api/hello/history'라고 치면 여기로 연결됩니다.
    public IActionResult GetHistory()
    {
        var history = _service.GetHistory();
        return Ok(history);
    }
}