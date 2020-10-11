using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATM_Origin.Api.Responses;
using ATM_Origin.Core.DTOs;
using ATM_Origin.Core.Entities;
using ATM_Origin.Core.Interfaces;
using ATM_Origin_Infrastucture.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATM_Origin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController : ControllerBase
    {
        private readonly ITarjetaService _tarjetaService;
        private readonly IMapper _mapper;
        public TarjetaController(ITarjetaService tarjetaService, IMapper mapper)
        {
            _tarjetaService = tarjetaService;
            _mapper = mapper;
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
        [HttpPost]
        public async Task<IActionResult> GetTarjetaByPin(TarjetaDto tarjetaDto)
        {
            var tarjeta = _mapper.Map<Tarjeta>(tarjetaDto);
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
        public async Task<IActionResult> UpdateTarjeta(int id, TarjetaDto tarjetaDto)
        {
            var tarjeta = _mapper.Map<Tarjeta>(tarjetaDto);
            tarjeta.Id = id;
            var result = await _tarjetaService.UpdateTarjeta(tarjeta);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }




        [HttpGet]
        public async Task<IActionResult> GetTarjetas()
        {
            var tarjetas = await _tarjetaService.GetTarjetas();
            var tarjetasDto = _mapper.Map<IEnumerable<TarjetaDto>>(tarjetas);
            var response = new ApiResponse<IEnumerable<TarjetaDto>>(tarjetasDto);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTarjeta(int id)
        {
            var tarjeta = await _tarjetaService.GetTarjeta(id);
            var tarjetaDto = _mapper.Map<TarjetaDto>(tarjeta);
            var response = new ApiResponse<TarjetaDto>(tarjetaDto);
            return Ok(response);   
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tarjetaService.DeleteTarjeta(id);
            return Ok(result);
        }
    }
}
