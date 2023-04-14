using Arduino.Business.Abstract;
using Arduino.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arduino.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatasController : ControllerBase
    {
        private IArduinoService _arduinoService;


        public DatasController(IArduinoService arduinoService)
        {
            _arduinoService=arduinoService;
        }
        /// <summary>
        /// Get All Data
        /// </summary>
        /// <returns> Returns All Data and Status code</returns>
        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            var datas = await _arduinoService.GetDataList();
            return Ok(datas);
        }
        /// <summary>
        /// Get specific Data by using Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return Data and status</returns>

        [HttpGet]
        [Route("[action]")] //api/datas/getdatabyid?id=1 //
        public async Task<IActionResult> GetDataById(int id) // 
        {
            var data = await _arduinoService.GetDataById(id);
            if (data != null)
            {
                return Ok(data);
            }
            return NotFound("Data bulunamadı");
        }
        /// <summary>
        /// Get count of  Datas
        /// </summary>
        /// <returns>Return Data's count and status</returns>
        [HttpGet]
        [Route("[action]")] //api/datas/getdatacount 
        public async Task<IActionResult> GetDataCount() 
        {
            var count = await _arduinoService.GetDataCount();
            
            return Ok(count + "  data bulundu");
            
            
        }

        /// <summary>
        /// Generate a new Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Status code and data</returns>

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateData([FromBody] Data data)
        {
            var createdData = await _arduinoService.CreateData(data);
            return CreatedAtAction("Get", new { id = createdData.Id }, createdData); //201 + DATA
        }

        /// <summary>
        /// updates specific data using id
        /// </summary>
        /// <param name="veri"></param>
        /// <returns>Status code and data</returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateData([FromBody] Data veri)
        {
            if (await _arduinoService.GetDataById(veri.Id) != null)
            {
                return Ok(await _arduinoService.UpdateData(veri));//201 + DATA
            }

            return NotFound("Data Bulunamadı");//404
        }

        /// <summary>
        /// Deletes specific data using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status code and info data</returns>
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteData(int id)
        {
            if (await _arduinoService.GetDataById(id) != null)
            {
                await _arduinoService.DeleteData(id);
                return Ok(id + " nolu veri silindi ");//201 + DATA
            }
            return NotFound("Data Bulunamadı");//404
        }


    }
}
