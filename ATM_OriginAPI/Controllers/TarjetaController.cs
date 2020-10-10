using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly ITarjetaRepository _tarjetaRepository;
        private readonly IMapper _mapper;
        public TarjetaController(ITarjetaRepository tarjetaRepository, IMapper mapper)
        {
            _tarjetaRepository = tarjetaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTarjetas()
        {
            var tarjetas = await _tarjetaRepository.GetTarjetas();
            var tarjetasDto = _mapper.Map<IEnumerable<TarjetaDto>>(tarjetas);
            return Ok(tarjetasDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTarjeta(int id)
        {
            var tarjeta = await _tarjetaRepository.GetTarjeta(id);
            var tarjetaDto = _mapper.Map<TarjetaDto>(tarjeta);
            return Ok(tarjetaDto);
            
        }

        [HttpPost]
        public async Task<IActionResult> Tarjeta(OperacionesDto operacionesDto)
        {
            //var operaciones =  new Operaciones 
            //{
            //    TarjetaId = operacionesDto.TarjetaId,
            //    Balance = operacionesDto.Balance,
            //    CodigoOperacion = operacionesDto.CodigoOperacion,
            //    Fecha = operacionesDto.Fecha,
                
            //};

            var operaciones = _mapper.Map<Operaciones>(operacionesDto);
            await _tarjetaRepository.InsertOperacion(operaciones);
            return Ok(operaciones);
        }
    }
}
