using AutoMapper;
using BusinessLogic;
using BusinessLogic.Interfaces;
using DataTransferModel.Oprema;
using DataTransferModel.Zaposleni;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvidentiranjeOpremeZaFakultet.Controllers
{
    [Route("api/oprema")]
    [ApiController]
    public class OpremaController : ControllerBase
    {
        //row version nije koriscen sada jer nije bilo use case za neki update
        //ali je dodat kao property i u bazi, a tu su i sve potrebne metode za taj use case
        //pa ako se bude implementiralo potrebne su minimalne izmene
        public OpremaController(IOpremaLogic opremaLogic, IMapper mapper)
        {
            OpremaLogic = opremaLogic;
            Mapper = mapper;
        }

        public IOpremaLogic OpremaLogic { get; }
        public IMapper Mapper { get; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetOpremaDTO>>> GetAll()
        {
            try
            {
                var oprema = await OpremaLogic.GetAll();

                return Ok(Mapper.Map<List<GetOpremaDTO>>(oprema));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetOpremaDTO>> GetById([FromRoute] Guid id)
        {
            try
            {
                var oprema = await OpremaLogic.GetById(id);

                if (oprema == null)
                    return NotFound();

                return Ok(Mapper.Map<Oprema, GetOpremaDTO>(oprema));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("sortperkolicina")]
        public async Task<ActionResult<IEnumerable<GetOpremaDTO>>> GetSortedPerKolicina([FromQuery] bool asc)
        {
            try
            {
                var oprema = await OpremaLogic.GetSortedPerKolicina(asc);

                return Ok(Mapper.Map<List<GetOpremaDTO>>(oprema));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("sortpernaziv")]
        public async Task<ActionResult<IEnumerable<GetOpremaDTO>>> GetSortedPerNaziv()
        {
            try
            {
                var oprema = await OpremaLogic.GetSortedPerNaziv();

                return Ok(Mapper.Map<List<GetOpremaDTO>>(oprema));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CreateOpremaDTO entityDTO)
        {
            try
            {
                var entity = Mapper.Map<CreateOpremaDTO, Oprema>(entityDTO);
                var result = await OpremaLogic.Add(entity);

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
        [HttpPost("rashodovanje")]
        public async Task<ActionResult> Rashoduj([FromBody] RashodujOpremaDTO entityDTO)
        {
            try
            {
                var entity = Mapper.Map<RashodujOpremaDTO, Oprema>(entityDTO);
                var result = await OpremaLogic.Rashoduj(entity);

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
        [HttpPost("otpisivanje")]
        public async Task<ActionResult> Otpisi([FromBody] OtpisiOpremaDTO entityDTO)
        {
            try
            {
                var entity = Mapper.Map<OtpisiOpremaDTO, Oprema>(entityDTO);
                var result = await OpremaLogic.Otpisi(entity);

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
        public async Task<ActionResult> Update([FromBody] UpdateOpremaDTO entityDTO)
        {
            try
            {
                var entity = Mapper.Map<UpdateOpremaDTO, Oprema>(entityDTO);
                var result = await OpremaLogic.Update(entity);

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
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await OpremaLogic.Delete(id);

                return Ok();
            }
            catch (DbUpdateException)
            {
                return BadRequest("Ne moze se obrisati oprema dok postoji zaduzivanje ili tip opreme koji koristi tu opremu");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
