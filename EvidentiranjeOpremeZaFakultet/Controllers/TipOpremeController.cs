using AutoMapper;
using BusinessLogic;
using BusinessLogic.Interfaces;
using DataTransferModel.Tip_opreme;
using DataTransferModel.Zaposleni;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvidentiranjeOpremeZaFakultet.Controllers
{
    [Route("api/tipopreme")]
    [ApiController]
    public class TipOpremeController : ControllerBase
    {
        public TipOpremeController(ITipOpremeLogic tipOpremeLogic, IMapper mapper)
        {
            TipOpremeLogic = tipOpremeLogic;
            Mapper = mapper;
        }

        public ITipOpremeLogic TipOpremeLogic { get; }
        public IMapper Mapper { get; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTipOpremeDTO>>> GetAll()
        {
            try
            {
                var tipoviOpreme = await TipOpremeLogic.GetAll();

                return Ok(Mapper.Map<List<GetTipOpremeDTO>>(tipoviOpreme));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("subtypes")]
        public async Task<ActionResult<IEnumerable<GetTipOpremeDTO>>> GetAllSubtypes([FromQuery] int subtype)
        {
            try
            {
                var tipoviOpreme = await TipOpremeLogic.GetAllSubtypes(subtype);

                return Ok(Mapper.Map<List<GetTipOpremeDTO>>(tipoviOpreme));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetTipOpremeDTO>> GetById([FromRoute]int id)
        {
            try
            {
                var tipOpreme = await TipOpremeLogic.GetById(id);

                if (tipOpreme == null)
                    return NotFound();

                return Ok(Mapper.Map<TipOpreme, GetTipOpremeDTO>(tipOpreme));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
