using AutoMapper;
using BusinessLogic;
using BusinessLogic.Interfaces;
using DataTransferModel.Kabinet;
using DataTransferModel.Tip_opreme;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvidentiranjeOpremeZaFakultet.Controllers
{
    [Route("api/kabinet")]
    [ApiController]
    public class KabinetController : ControllerBase
    {
        public KabinetController(IKabinetLogic kabinetLogic, IMapper mapper)
        {
            KabinetLogic = kabinetLogic;
            Mapper = mapper;
        }

        public IKabinetLogic KabinetLogic { get; }
        public IMapper Mapper { get; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetKabinetDTO>>> GetAll()
        {
            try
            {
                var kabineti = await KabinetLogic.GetAll();

                return Ok(Mapper.Map<List<GetKabinetDTO>>(kabineti));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetKabinetDTO>> GetById([FromRoute]int id)
        {
            try
            {
                var kabinet = await KabinetLogic.GetById(id);
                if(kabinet == null)
                {
                    return NotFound();
                }

                return Ok(Mapper.Map<Kabinet, GetKabinetDTO>(kabinet));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
