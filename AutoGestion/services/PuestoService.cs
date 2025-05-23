﻿using AutoGestion.interfaces;
using AutoGestion.interfaces.IEmpleado;
using AutoGestion.interfaces.IPuesto;
using AutoGestion.Models;
using AutoGestion.Utils;

namespace AutoGestion.services
{
    public class PuestoService : IPuestoService
    {
        private readonly IPuestoRepository _puestoRepository;
        private readonly IAsignaciones _AsinacionesService;

        public PuestoService(IPuestoRepository puestoRepository, IAsignaciones asignacionesService)
        {
            _puestoRepository = puestoRepository;
            _AsinacionesService = asignacionesService;
        }

        public async Task<IEnumerable<PuestoDto>> GetPuestos()
        {

            var puestos = await _puestoRepository.GetPuestos();
            if (puestos == null)
            {
                throw new KeyNotFoundException("Lista de puestos Vacia.");
            }
            var puestosDto = puestos.Select(p => new PuestoDto
            {
                Id = p.Id!,
                Nombre = p.Nombre,
                Activo = p.Activo,            
                Descripcion = p.Descripcion
            });

            return puestosDto;
        }
        public async Task<PuestoDto?> GetPuestosById(string id)
        {
            var puesto = await _puestoRepository.GetPuestosById(id);

            if (puesto == null)
            {
                throw new KeyNotFoundException("Puesto no encontrado.");
            }

            var PuestoDto = new PuestoDto
            {
                Id = puesto.Id!,
                Nombre = puesto.Nombre,
                Descripcion = puesto.Descripcion,
                Activo = puesto.Activo
            };

            return PuestoDto;
        }




        public async Task<IEnumerable<PuestoDto>> GetPuestosActivos()
        {
            var puestos = await _puestoRepository.GetPuestosActivos();
            if (puestos == null)
            {
                throw new KeyNotFoundException("Lista de puestos Vacia.");
            }
            var puestosDto = puestos.Select(e => new PuestoDto
            {
                Id = e.Id!,
                Nombre = e.Nombre,
                Activo = e.Activo,
                Descripcion = e.Descripcion

            });
            return puestosDto;
        }

        public async Task<PuestoDto> PostPuestos(Puesto puesto)
        {
            var token = _AsinacionesService.GetTokenFromHeader();
            puesto.Id = _AsinacionesService.GenerateNewId();
            puesto.Created_at = _AsinacionesService.GetCurrentDateTime();
            puesto.Updated_at = _AsinacionesService.GetCurrentDateTime();
            puesto.Adicionado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            puesto.Modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            await _puestoRepository.PostPuestos(puesto);
            return new PuestoDto
            {
                Id = puesto.Id,
                Nombre = puesto.Nombre,
                Descripcion = puesto.Descripcion,
                Activo = puesto.Activo,
            };

        }

        public async Task<PuestoDto> PutPuestos(string id, Puesto puesto)
        {
            var puestoFound = await _puestoRepository.GetPuestosById(id);

            if (puestoFound == null)
            {
                throw new KeyNotFoundException("Puesto no encontrado.");
            }
            var token = _AsinacionesService.GetTokenFromHeader();
            puestoFound.ActualizarPropiedades(puesto);
            puestoFound.Activo = puesto.Activo;
            puestoFound.Updated_at = _AsinacionesService.GetCurrentDateTime();
            puestoFound.Modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";

            await _puestoRepository.PutPuestos(id, puestoFound);
            return new PuestoDto
            {
                Id = puestoFound.Id!,
                Nombre = puestoFound.Nombre,
                Activo = puestoFound.Activo,
                Descripcion = puestoFound.Descripcion

            };
        }

    }
}
