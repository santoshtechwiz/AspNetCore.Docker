using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/discount-types")]
public class DiscountTypesController : ControllerBase
{
    private readonly AppDbContext _context;

    public DiscountTypesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscountType([FromBody] DiscountType discountType)
    {
        _context.DiscountTypes.Add(discountType);
        await _context.SaveChangesAsync();
        return Ok(discountType);
    }

    [HttpGet]
    public IActionResult GetDiscountTypes()
    {
        var discountTypes = _context.DiscountTypes.ToList();
        return Ok(discountTypes);
    }
}
