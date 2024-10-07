using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text;
using System.Threading.Tasks;

public class WebhookPayload
{
    public string Event { get; set; }
    public string Data { get; set; }
}

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IDatabase _redisDatabase; // Redis database instance

    public EventsController(HttpClient httpClient, IConnectionMultiplexer redis)
    {
        _httpClient = httpClient;
        _redisDatabase = redis.GetDatabase(); // Initialize Redis database
        
    }

    [HttpPost("trigger")]
    public async Task<IActionResult> TriggerEvent([FromBody] WebhookPayload payload)
    {
        // Save event data to Redis cache before triggering webhook
        var redisKey = $"event:{payload.Event}";
        var jsonPayload = JsonConvert.SerializeObject(payload);
        
        await _redisDatabase.StringSetAsync(redisKey, jsonPayload);

        //get response 

        var result=_redisDatabase.StringGet(redisKey);
        return Ok(result);

    }

    [HttpGet("get-event/{eventName}")]
    public async Task<IActionResult> GetEventFromCache(string eventName)
    {
        // Retrieve event data from Redis cache
        var redisKey = $"event:{eventName}";
        var cachedEvent = await _redisDatabase.StringGetAsync(redisKey);

        if (cachedEvent.HasValue)
        {
            var payload = JsonConvert.DeserializeObject<WebhookPayload>(cachedEvent);
            return Ok(payload);
        }

        return NotFound("Event not found in Redis");
    }
}
