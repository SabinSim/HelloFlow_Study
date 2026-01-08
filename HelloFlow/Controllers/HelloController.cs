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

    // (기존 SayHello 등은 유지...)

    // ▼▼▼ [Architect Version 핵심: 검색 API] ▼▼▼
    // GET /api/hello/search?name=검색어&page=1&size=10
    [HttpGet("search")]
    public IActionResult SearchHello(
        [FromQuery] string? name,     // 검색어 (없을 수도 있음 -> nullable)
        [FromQuery] int page = 1,     // 안 보내면 기본 1페이지
        [FromQuery] int size = 10     // 안 보내면 기본 10개
    )
    {
        // 서비스에게 처리를 위임
        var results = _service.FindHelloAdvanced(name ?? "", page, size);
        
        return Ok(results);
    }
}