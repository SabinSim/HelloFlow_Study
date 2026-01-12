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

    // [En] API Endpoint to create data.
    // [Ko] 데이터를 생성하는 API 엔드포인트입니다.
    [HttpGet]
    public IActionResult SayHello(string name)
    {
        var result = _service.GetHello(name);
        return Ok(result);
    }

    // [En] API Endpoint for advanced search using Query Parameters.
    // [Ko] 쿼리 파라미터를 사용하는 고급 검색용 API 엔드포인트입니다.
    // Example: GET /api/hello/search?name=sabin&page=1
    [HttpGet("search")]
    public IActionResult SearchHello(
        [FromQuery] string? name,     // [En] Optional search term / [Ko] 검색어 (없을 수 있음)
        [FromQuery] int page = 1,     // [En] Default page is 1 / [Ko] 기본 1페이지
        [FromQuery] int size = 10     // [En] Default size is 10 / [Ko] 기본 10개
    )
    {
        // [En] Handle null name by converting it to empty string using '??'.
        // [Ko] '??' 연산자를 써서 이름이 null이면 빈 문자열로 처리합니다.
        var results = _service.FindHelloAdvanced(name ?? "", page, size);
        return Ok(results);
    }
}