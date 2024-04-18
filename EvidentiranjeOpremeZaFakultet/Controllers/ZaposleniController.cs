using AutoMapper;
using BusinessLogic.Interfaces;
using DataTransferModel.Zaposleni;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvidentiranjeOpremeZaFakultet.Controllers
{
    [Route("api/zaposleni")]
    [ApiController]
    public class ZaposleniController : ControllerBase
    {
        //nisu sve rute iskoriscene ali su neke napravljene ako se u buducnosti budu 
        //dodavali novi slucajevi koriscenja kao naprimer logovanje i slicno, to vazi
        //za sve kontrolere
        public IZaposleniLogic ZaposleniLogic { get; }
        public IMapper Mapper { get; }

        public ZaposleniController(IZaposleniLogic zaposleniLogic, IMapper mapper)
        {
            this.ZaposleniLogic = zaposleniLogic;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetZaposleniDTO>>> GetAll() 
        {
            try
            {
                var zaposleni = await ZaposleniLogic.GetAll();

                return Ok(Mapper.Map<List<GetZaposleniDTO>>(zaposleni));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetZaposleniDTO>> GetById([FromRoute]string id)
        {
            try
            {
                var zaposleni = await ZaposleniLogic.GetById(id);

                if (zaposleni == null)
                    return NotFound();

                return Ok(Mapper.Map<Zaposleni, GetZaposleniDTO>(zaposleni));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
