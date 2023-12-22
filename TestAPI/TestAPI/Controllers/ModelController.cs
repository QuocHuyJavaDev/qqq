using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NoteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        [HttpPost("predict")]
        public IActionResult predictCode(MLModel.ModelInput input)
        {
            var predictionResult = MLModel.Predict(input);

            if (predictionResult.PredictedLabel.Length != 0)
            {
                return Ok(predictionResult.PredictedLabel.ToString());
            }
            else
            {
                return NotFound();
            }
        }
    }
}
