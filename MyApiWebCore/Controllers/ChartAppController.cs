using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiWebCore.Repositories.IRepository;

namespace MyApiWebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartAppController : ControllerBase
    {
        private IChartAppRepository chartAppRepository;

        public ChartAppController(IChartAppRepository chartAppRepository) {
        this.chartAppRepository = chartAppRepository;
        }
        [HttpGet]
        public async Task<IActionResult> ChartAppProduct(int year)
        {
            try
            {
                return  Ok(await chartAppRepository.GetProductChartDataByMonth(year));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
