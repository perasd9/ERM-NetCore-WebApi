using AutoMapper;
using BusinessLogic;
using BusinessLogic.Interfaces;
using DataTransferModel.Oprema;
using DataTransferModel.Zaduzivanje;
using DataTransferModel.Zaposleni;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvidentiranjeOpremeZaFakultet.Controllers
{
    [Route("api/zaduzivanje")]
    [ApiController]
    public class ZaduzivanjeController : ControllerBase
    {
        public ZaduzivanjeController(IZaduzivanjeLogic zaduzivanjeLogic, IMapper mapper)
        {
            ZaduzivanjeLogic = zaduzivanjeLogic;
            Mapper = mapper;
        }

        public IZaduzivanjeLogic ZaduzivanjeLogic { get; }
        public IMapper Mapper { get; }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<GetZaduzivanjeDTO>>> GetAll()
        //{
        //    try
        //    {
        //        var zaduzivanja = await ZaduzivanjeLogic.GetAll();

        //        return Ok(Mapper.Map<List<GetZaduzivanjeDTO>>(zaduzivanja));
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet]
        public async Task<ActionResult<PaginatedListZaduzivanja>> GetPaginatedList([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            try
            {
                var zaduzivanja = await ZaduzivanjeLogic.GetPaginatedList(pageIndex, pageSize);

                if(zaduzivanja != null)
                    zaduzivanja.Items = Mapper.Map<List<Zaduzivanje>>(zaduzivanja.Items);
                else
                    return BadRequest();


                return Ok(zaduzivanja);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetZaduzivanjeDTO>> GetById([FromRoute] int id)
        {
            try
            {
                var zaduzivanje = await ZaduzivanjeLogic.GetById(id);

                if (zaduzivanje == null)
                    return NotFound();

                return Ok(Mapper.Map<Zaduzivanje, GetZaduzivanjeDTO>(zaduzivanje));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("groupzaposleni")]
        public async Task<ActionResult<IEnumerable<GetZaduzivanjeDTO>>> GetPerZaposleni()
        {
            try
            {
                var zaduzivanja = await ZaduzivanjeLogic.GetPerZaposleni();

                return Ok(Mapper.Map<List<GroupPerZaposleniDTO>>(zaduzivanja));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("groupkabinet")]
        public async Task<ActionResult<IEnumerable<GetZaduzivanjeDTO>>> GetPerKabinet()
        {
            try
            {
                var zaduzivanja = await ZaduzivanjeLogic.GetPerKabinet();

                return Ok(Mapper.Map<List<GroupPerKabinetDTO>>(zaduzivanja));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody]List<CreateZaduzivanjeDTO> entitiesDTO)
        {
            try
            {
                var entities = Mapper.Map<List<Zaduzivanje>>(entitiesDTO);
                var result = await ZaduzivanjeLogic.AddRange(entities);

                if (result != null)
                {
                    return BadRequest(result[0]);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateZaduzivanjeDTO entityDTO)
        {
            try
            {
                var entity = Mapper.Map<UpdateZaduzivanjeDTO, Zaduzivanje>(entityDTO);
                var result = await ZaduzivanjeLogic.Update(entity);

                if (result != null)
                {
                    return BadRequest(result[0]);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("razduzi")]
        public async Task<ActionResult> Razduzi([FromBody] RazduzivanjeDTO entityDTO)
        {
            try
            {
                var entity = Mapper.Map<RazduzivanjeDTO, Zaduzivanje>(entityDTO);
                var result = await ZaduzivanjeLogic.Razduzi(entity);

                if (result != null)
                {
                    return BadRequest(result[0]);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await ZaduzivanjeLogic.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
