using Microsoft.AspNetCore.Mvc;
using HelloFlow.Services;

namespace HelloFlow.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloController : ControllerBase
{
    private readonly HelloService _service;

    public HelloController(HelloService service)
    {
        _service = service;
    }

    // ▼▼▼ 1. [복구됨] 데이터 넣는 곳 (SayHello) ▼▼▼
    // 이 메서드가 있어야 Swagger에서 데이터를 추가할 수 있습니다.
    [HttpGet]
    public IActionResult SayHello(string name)
    {
        var result = _service.GetHello(name);
        return Ok(result);
    }

    // ▼▼▼ 2. [기존 유지] 검색하는 곳 (SearchHello) ▼▼▼
    [HttpGet("search")]
    public IActionResult SearchHello(
        [FromQuery] string? name,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10
    )
    {
        var results = _service.FindHelloAdvanced(name ?? "", page, size);
        return Ok(results);
    }
}