using ATM_Origin.Core.CustonEntities;
using ATM_Origin.Core.Entities;
using ATM_Origin.Core.Exceptions;
using ATM_Origin.Core.Interfaces;
using ATM_Origin.Core.RequestFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Origin.Core.Services
{
   
    public class TarjetaService : ITarjetaService
    {
        //VALIDATE
        private readonly ITarjetaRepository _tarjetaRepository;
        private readonly PaginationOptions _paginationOptions;
        //private readonly IUnitOfWork _unitOfWork;
        //private readonly IRepository<Tarjeta> _tarjetaRepositoryGeneric;
        //public TarjetaService(ITarjetaRepository tarjetaRepository, IRepository<Tarjeta> tarjetaRepositoryGeneric, IUnitOfWork unitOfWork)
        public TarjetaService(ITarjetaRepository tarjetaRepository, IOptions<PaginationOptions> options)
        {
            _tarjetaRepository = tarjetaRepository;
            //_tarjetaRepositoryGeneric = tarjetaRepositoryGeneric;
            //_unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public async Task<Tarjeta> GetTarjetaByNumber(string number)
        {
            return await _tarjetaRepository.GetTarjetaByNumber(number);
            //return await _unitOfWork.tarjetaRepository.GetById(1);

        }


        public async Task<Tarjeta> GetTarjetaByPin(TarjetaRequestFilter tarjeta)
        {
            var response = await _tarjetaRepository.GetTarjetaByPin(tarjeta);
            //if(response == null)
            //{
            //    throw new BusinessException("Ingreso Incorrecto de PIN"); 
            //}

            return response;
        }

        public async Task InsertOperacion(Operaciones operaciones)
        {
            operaciones.Fecha = DateTime.Now;
            await _tarjetaRepository.InsertOperacion(operaciones);
        }

        public async Task<bool> UpdateTarjeta(Tarjeta tarjeta)
        {
            return await _tarjetaRepository.UpdateTarjeta(tarjeta);
        }

        public async Task<Tarjeta> GetTarjeta(int id)
        {
            return await _tarjetaRepository.GetTarjeta(id);
        }
        public PagedList<Tarjeta> GetTarjetas(TarjetaRequestFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DeafultPageNumber : filters.PageNumber;
            filters.PageSize= filters.PageSize == 0 ? _paginationOptions.DeafultPageSize: filters.PageSize;
            var response = _tarjetaRepository.GetTarjetas();
            //response = response.Where(t => t.FechaVto.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
            var pagedTarjetas = PagedList<Tarjeta>.Create(response, filters.PageNumber, filters.PageSize);
            return pagedTarjetas;
        }
        public async Task<bool> DeleteTarjeta(int id)
        {
            return await _tarjetaRepository.DeleteTarjeta(id);
        }

    }
}
