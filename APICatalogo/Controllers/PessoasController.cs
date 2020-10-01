using API_Crud.DTOs;
using API_Crud.Repository;
using API_Crud.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace API_Crud.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[Controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public PessoasController(IUnitOfWork contexto, IMapper mapper)
        {
            _uof = contexto;
            _mapper = mapper;
        }

        [HttpGet("PessoasPorEscolaridade")]
        public ActionResult<IEnumerable<PessoaDTO>> GetPessoasPorEscolaridade()
        {
            var pessoas = _uof.PessoaRepository.GetPessoasPorEscolaridade().ToList();
            var PessoasDto = _mapper.Map<List<PessoaDTO>>(pessoas);

            return PessoasDto;
        }

        /// <summary>
        /// Exibe uma relação dos pessoas
        /// </summary>
        /// <returns>Retorna uma lista de objetos Pessoa</returns>
        // api/pessoas
        [HttpGet]
        public ActionResult<IEnumerable<PessoaDTO>> Get()
        {
            var pessoas = _uof.PessoaRepository.Get().ToList();

            var PessoasDto = _mapper.Map<List<PessoaDTO>>(pessoas);
            return PessoasDto;
        }

        /// <summary>
        /// Obtem um pessoa pelo seu identificador pessoaId
        /// </summary>
        /// <param name="id">Código do pessoa</param>
        /// <returns>Um objeto Pessoa</returns>
        // api/pessoas/1
        [HttpGet("{id}", Name = "GetPessoa")]
        public ActionResult<PessoaDTO> Get(int id)
        {
            var pessoa = _uof.PessoaRepository.GetById(p => p.ID == id);

            if (pessoa == null)
            {
                return NotFound();
            }

            var pessoaDto = _mapper.Map<PessoaDTO>(pessoa);
            return pessoaDto;
        }

        //  api/pessoas
        [HttpPost]
        public ActionResult Post([FromBody]PessoaDTO pessoaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var pessoa = _mapper.Map<Pessoa>(pessoaDto);

            _uof.PessoaRepository.Add(pessoa);
            _uof.Commit();

            var pessoaDTO = _mapper.Map<PessoaDTO>(pessoa);

            return new CreatedAtRouteResult("GetPessoa",
                new { id = pessoa.ID }, pessoaDTO);
        }

       /// <summary>
       /// Atualiza um pessoa pelo id
       /// </summary>
       /// <param name="id"></param>
       /// <param name="pessoaDto"></param>
       /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] PessoaDTO pessoaDto)
        {
            if (id != pessoaDto.ID)
            {
                return BadRequest();
            }

            var pessoa = _mapper.Map<Pessoa>(pessoaDto);

            _uof.PessoaRepository.Update(pessoa);
            _uof.Commit();
            return Ok();
        }

        //  api/pessoas/1
        [HttpDelete("{id}")]
        public ActionResult<PessoaDTO> Delete(int id)
        {
            var pessoa = _uof.PessoaRepository.GetById(p => p.ID == id);

            if (pessoa == null)
            {
                return NotFound();
            }
    
            _uof.PessoaRepository.Delete(pessoa);
            _uof.Commit();

            var pessoaDto = _mapper.Map<PessoaDTO>(pessoa);

            return pessoaDto;
        }
    }
}