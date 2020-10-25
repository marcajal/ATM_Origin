using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATM_Origin.Api.Responses;
using ATM_Origin.Core.DTOs;
using ATM_Origin.Core.Entities;
using ATM_Origin.Core.Interfaces;
using ATM_Origin.Core.RequestFilters;
using ATM_Origin_Infrastucture.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;
using ATM_Origin.Core.CustonEntities;
using Newtonsoft.Json;
using ATM_Origin_Infrastucture.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ATM_Origin.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController : ControllerBase
    {
        private readonly ITarjetaService _tarjetaService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public TarjetaController(ITarjetaService tarjetaService, IMapper mapper, IUriService uriService)
        {
            _tarjetaService = tarjetaService;
            _mapper = mapper;
            _uriService = uriService;
        }

        //1)********************************************************************************************************************
        [HttpGet("number/{number}")]
        public async Task<IActionResult> GetTarjetaByNumber(string number)
        {
            var tarjeta = await _tarjetaService.GetTarjetaByNumber(number);
            var tarjetaDto = _mapper.Map<TarjetaDto>(tarjeta);
            //var response = new ApiResponse<TarjetaDto>(tarjetaDto);
            return Ok(tarjetaDto);
        }
        //2)********************************************************************************************************************
        [HttpPost("security")]
        public async Task<IActionResult> GetTarjetaByPin(TarjetaRequestFilter tarjeta)
        {
            //var tarjeta = _mapper.Map<Tarjeta>(tarjetaDto);
            var response = await _tarjetaService.GetTarjetaByPin(tarjeta);
            //var response2 = new ApiResponse<TarjetaDto>(tarjetaDto);
            return Ok(response);
        }
        //3)********************************************************************************************************************
        [HttpPost]
        public async Task<IActionResult> InsertOperacion(OperacionesDto operacionesDto)
        {
            var operaciones = _mapper.Map<Operaciones>(operacionesDto);
            await _tarjetaService.InsertOperacion(operaciones);
            return Ok(operaciones);
        }
        //4)********************************************************************************************************************
        [HttpPut]
        public async Task<IActionResult> UpdateTarjeta(TarjetaDto tarjetaDto)
        {
            var tarjeta = _mapper.Map<Tarjeta>(tarjetaDto);
            //tarjeta.Id = id;
            var response = await _tarjetaService.UpdateTarjeta(tarjeta);
            //var response = new ApiResponse<bool>(result);
            return Ok(response);
        }



        /// <summary>
        /// Trae todas las tarjetas
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        /// 
        [HttpGet("all")]
        //[HttpGet(Name = nameof(GetTarjetas))]
        public IActionResult GetTarjetas([FromQuery] TarjetaRequestFilter filters)
        {
            var test = nameof(GetTarjetas);
            var tarjetas = _tarjetaService.GetTarjetas(filters);
            var tarjetasDto = _mapper.Map<IEnumerable<Tarjeta>>(tarjetas);
            //var response = new ApiResponse<IEnumerable<TarjetaDto>>(tarjetasDto);

            var metadada = new MetaData
            {
                TotalCount = tarjetas.TotalCount,
                PageSize = tarjetas.PageSize,
                CurrentPage = tarjetas.CurrentPage,
                TotalPages = tarjetas.TotalPages,
                HasNextPage = tarjetas.HasNextPage,
                HasPreviousPage = tarjetas.HasPreviousPage,
                NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetTarjetas))).ToString(),
                PreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetTarjetas))).ToString()
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadada));
            return Ok(tarjetasDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTarjeta(int id)
        {
            var tarjeta = await _tarjetaService.GetTarjeta(id);
            var tarjetaDto = _mapper.Map<TarjetaDto>(tarjeta);
            //var response = new ApiResponse<TarjetaDto>(tarjetaDto);
            return Ok(tarjetaDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tarjetaService.DeleteTarjeta(id);
            return Ok(result);
        }
    }
}
