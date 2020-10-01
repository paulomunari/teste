using API_Crud.DTOs;
using API_Crud.Repository;
using API_Crud.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Crud.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[Controller]")]
    [ApiController]
    public class EscolaridadesController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EscolaridadesController(IUnitOfWork contexto,IMapper mapper,
            ILogger<EscolaridadesController> logger)
        {
            _context = contexto;
            _mapper = mapper;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("teste")]
        public string GetTeste()
        {
            return $"EscolaridadesController - {DateTime.Now.ToLongDateString().ToString()}";
        }

        /// <summary>
        /// Obtém os pessoas relacionados para cada escolaridade
        /// </summary>
        /// <returns>Objetos Escolaridade e respectivo Objetos Pessoas</returns>
        [HttpGet("pessoas")]
        public ActionResult<IEnumerable<EscolaridadeDTO>> GetEscolaridadesPessoas()
        {
            _logger.LogInformation("================GET api/escolaridades/pessoas ======================");

            var escolaridades = _context.EscolaridadeRepository.GetEscolaridadesPessoas().ToList();
            var escolaridadesDto = _mapper.Map<List<EscolaridadeDTO>>(escolaridades);
            return escolaridadesDto;
        }

        /// <summary>
        /// Retorna uma coleção de objetos Escolaridade
        /// </summary>
        /// <returns>Lista de Escolaridades</returns>
        [HttpGet]
        public ActionResult<IEnumerable<EscolaridadeDTO>> Get()
        {

            _logger.LogInformation("================GET api/escolaridades ======================");

            var escolaridades = _context.EscolaridadeRepository.Get().ToList();
            var escolaridadesDto = _mapper.Map<List<EscolaridadeDTO>>(escolaridades);
            return escolaridadesDto;
        }

        [HttpGet("paginacao")]
        public ActionResult<IEnumerable<EscolaridadeDTO>> GetPaginacao(int pag=1, int reg=5)
        {
            if (reg > 99)
                reg = 5;

            var escolaridades = _context.EscolaridadeRepository
                .LocalizaPagina<Escolaridade>(pag,reg)
                .ToList();

            var totalDeRegistros = _context.EscolaridadeRepository.GetTotalRegistros();
            var numeroPaginas = ((int)Math.Ceiling((double)totalDeRegistros / reg));

            Response.Headers["X-Total-Registros"] = totalDeRegistros.ToString();
            Response.Headers["X-Numero-Paginas"] = numeroPaginas.ToString();

            var escolaridadesDto = _mapper.Map<List<EscolaridadeDTO>>(escolaridades);
            return escolaridadesDto;
        }

        /// <summary>
        /// Obtem uma Escolaridade pelo seu Id
        /// </summary>
        /// <param name="id">codigo do escolaridade</param>
        /// <returns>Objetos Escolaridade</returns>
        [HttpGet("{id}", Name = "ObterEscolaridade")]
        //[EnableCors("PermitirApiRequest")]
        [ProducesResponseType(typeof(PessoaDTO),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EscolaridadeDTO> Get(int id)
        {
            var escolaridade = _context.EscolaridadeRepository.GetById(p => p.EscolaridadeId == id);

            _logger.LogInformation($"===============GET api/escolaridades/id = {id} ======================");

            if (escolaridade == null)
            {
                _logger.LogInformation($"===============GET api/escolaridades/id = {id} NOT FOUND ========");
                return NotFound();
            }
            var escolaridadeDto = _mapper.Map<EscolaridadeDTO>(escolaridade);
            return escolaridadeDto;
        }

        /// <summary>
        /// Inclui uma nova escolaridade
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST api/escolaridades
        ///     {
        ///        "escolaridadeId": 1,
        ///        "nome": "escolaridade1",
        ///        "imagemUrl": "http://teste.net/1.jpg"
        ///     }
        /// </remarks>
        /// <param name="escolaridadeDto">objeto Escolaridade</param>
        /// <returns>O objeto Escolaridade incluida</returns>
        /// <remarks>Retorna um objeto Escolaridade incluído</remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody]EscolaridadeDTO escolaridadeDto)
        {
            var escolaridade = _mapper.Map<Escolaridade>(escolaridadeDto);

            _context.EscolaridadeRepository.Add(escolaridade);
            _context.Commit();

            var escolaridadeDTO = _mapper.Map<EscolaridadeDTO>(escolaridade);

            return new CreatedAtRouteResult("ObterEscolaridade",
                new { id = escolaridade.EscolaridadeId }, escolaridadeDTO);
        }

        [HttpPut("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Put))]
        public ActionResult Put(int id, [FromBody] EscolaridadeDTO escolaridadeDto)
        {
            if (id != escolaridadeDto.EscolaridadeId)
            {
                return BadRequest();
            }

            var escolaridade = _mapper.Map<Escolaridade>(escolaridadeDto);

            _context.EscolaridadeRepository.Update(escolaridade);
            _context.Commit();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<EscolaridadeDTO> Delete(int id)
        {
            var escolaridade = _context.EscolaridadeRepository.GetById(p => p.EscolaridadeId == id);

            if (escolaridade == null)
            {
                return NotFound();
            }
            _context.EscolaridadeRepository.Delete(escolaridade);
            _context.Commit();

            var escolaridadeDto = _mapper.Map<EscolaridadeDTO>(escolaridade);

            return escolaridadeDto;
        }
    }
}

