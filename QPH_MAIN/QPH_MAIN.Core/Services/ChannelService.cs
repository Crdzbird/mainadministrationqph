using Microsoft.Extensions.Options;
using OrderByExtensions;
using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Exceptions;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Core.QueryFilters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Services
{
    public class ChannelService : IChannelService
    {
        private readonly IUnitOfWorkChannel _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public ChannelService(IUnitOfWorkChannel unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<Channel> GetChannel(int id) => await _unitOfWork.ChannelRepository.GetById(id);

        public PagedList<Channel> GetChannels(ChannelQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var Channel = _unitOfWork.ChannelRepository.GetAll();
            if(filters.filter != null)
            {
                Channel = Channel.Where(x => x.Ambiente.ToLower().Contains(filters.filter.ToLower()));
                Channel = Channel.Where(x => x.Bodega.ToLower().Contains(filters.filter.ToLower()));
                Channel = Channel.Where(x => x.CategoriaCliente.ToLower().Contains(filters.filter.ToLower()));
                Channel = Channel.Where(x => x.CodigoProducto.ToLower().Contains(filters.filter.ToLower()));
                Channel = Channel.Where(x => x.CuentaContable.ToLower().Contains(filters.filter.ToLower()));
                Channel = Channel.Where(x => x.DocumentoElectronico.ToLower().Contains(filters.filter.ToLower()));
                Channel = Channel.Where(x => x.FormaPago.ToLower().Contains(filters.filter.ToLower()));
                Channel = Channel.Where(x => x.Enlace.ToLower().Contains(filters.filter.ToLower()));
                Channel = Channel.Where(x => x.GrupoCredito.ToLower().Contains(filters.filter.ToLower()));
                Channel = Channel.Where(x => x.Iva.ToString().ToLower().Contains(filters.filter.ToLower()));
                Channel = Channel.Where(x => x.LimiteCredito.ToLower().Contains(filters.filter.ToLower()));
                Channel = Channel.Where(x => x.ListaPrecioContado.ToLower().Contains(filters.filter.ToLower()));
                Channel = Channel.Where(x => x.ListaPrecioCredito.ToLower().Contains(filters.filter.ToLower()));
                Channel = Channel.Where(x => x.NombreProducto.ToLower().Contains(filters.filter.ToLower()));
                Channel = Channel.Where(x => x.PuntoEmision.ToLower().Contains(filters.filter.ToLower()));
                Channel = Channel.Where(x => x.Relacionado.ToLower().Contains(filters.filter.ToLower()));
                Channel = Channel.Where(x => x.Segmento.ToLower().Contains(filters.filter.ToLower()));
                Channel = Channel.Where(x => x.Status.ToLower().Contains(filters.filter.ToLower()));
                Channel = Channel.Where(x => x.Uge.ToLower().Contains(filters.filter.ToLower()));
                Channel = Channel.Where(x => x.VendorSeccion.ToLower().Contains(filters.filter.ToLower()));
            }
            if (filters.Ambiente != null)
            {
                Channel = Channel.Where(x => x.Ambiente.ToLower().Contains(filters.Ambiente.ToLower()));
            }
            if (filters.Bodega != null)
            {
                Channel = Channel.Where(x => x.Bodega.ToLower().Contains(filters.Bodega.ToLower()));
            }
            if (filters.Enlace != null)
            {
                Channel = Channel.Where(x => x.Enlace.ToLower().Contains(filters.Enlace.ToLower()));
            }
            if (filters.CategoriaCliente != null)
            {
                Channel = Channel.Where(x => x.CategoriaCliente.ToLower().Contains(filters.CategoriaCliente.ToLower()));
            }
            if (filters.CodigoProducto != null)
            {
                Channel = Channel.Where(x => x.CodigoProducto.ToLower().Contains(filters.CodigoProducto.ToLower()));
            }
            if (filters.CuentaContable != null)
            {
                Channel = Channel.Where(x => x.CuentaContable.ToLower().Contains(filters.CuentaContable.ToLower()));
            }
            if (filters.DocumentoElectronico != null)
            {
                Channel = Channel.Where(x => x.DocumentoElectronico.ToLower().Contains(filters.DocumentoElectronico.ToLower()));
            }
            if (filters.FormaPago != null)
            {
                Channel = Channel.Where(x => x.FormaPago.ToLower().Contains(filters.FormaPago.ToLower()));
            }
            if (filters.GrupoCredito != null)
            {
                Channel = Channel.Where(x => x.GrupoCredito.ToLower().Contains(filters.GrupoCredito.ToLower()));
            }
            if (filters.Iva.ToString() != null)
            {
                Channel = Channel.Where(x => x.Iva.ToString().ToLower().Contains(filters.Iva.ToString().ToLower()));
            }
            if (filters.LimiteCredito != null)
            {
                Channel = Channel.Where(x => x.LimiteCredito.ToLower().Contains(filters.LimiteCredito.ToLower()));
            }
            if (filters.ListaPrecioContado != null)
            {
                Channel = Channel.Where(x => x.ListaPrecioContado.ToLower().Contains(filters.ListaPrecioContado.ToLower()));
            }
            if (filters.ListaPrecioCredito != null)
            {
                Channel = Channel.Where(x => x.ListaPrecioCredito.ToLower().Contains(filters.ListaPrecioCredito.ToLower()));
            }
            if (filters.NombreProducto != null)
            {
                Channel = Channel.Where(x => x.NombreProducto.ToLower().Contains(filters.NombreProducto.ToLower()));
            }
            if (filters.PuntoEmision != null)
            {
                Channel = Channel.Where(x => x.PuntoEmision.ToLower().Contains(filters.PuntoEmision.ToLower()));
            }
            if (filters.Segmento != null)
            {
                Channel = Channel.Where(x => x.Segmento.ToLower().Contains(filters.Segmento.ToLower()));
            }
            if (filters.Status != null)
            {
                Channel = Channel.Where(x => x.Status.ToLower().Contains(filters.Status.ToLower()));
            }
            if (filters.Uge != null)
            {
                Channel = Channel.Where(x => x.Uge.ToLower().Contains(filters.Uge.ToLower()));
            }
            if (filters.VendorSeccion != null)
            {
                Channel = Channel.Where(x => x.VendorSeccion.ToLower().Contains(filters.VendorSeccion.ToLower()));
            }
            if (filters.orderedBy != null && filters.orderedBy.Count() > 0)
            {
                foreach (var sortM in filters.orderedBy)
                {
                    Channel = Channel.OrderBy(sortM.PairAsSqlExpression);
                }
            }
            var pagedPosts = PagedList<Channel>.Create(Channel, filters.PageNumber, filters.PageSize);
            return pagedPosts;
        }

        public async Task InsertChannel(Channel Channel)
        {
            await _unitOfWork.ChannelRepository.Add(Channel);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateChannel(Channel Channel)
        {
            var existingChannel = await _unitOfWork.ChannelRepository.GetById(Channel.Id);
            existingChannel.VendorSeccion = Channel.VendorSeccion;
            existingChannel.Uge = Channel.Uge;
            existingChannel.Status = Channel.Status;
            existingChannel.Segmento = Channel.Segmento;
            existingChannel.Relacionado = Channel.Relacionado;
            existingChannel.PuntoEmision = Channel.PuntoEmision;
            existingChannel.NombreProducto = Channel.NombreProducto;
            existingChannel.ListaPrecioCredito = Channel.ListaPrecioCredito;
            existingChannel.ListaPrecioContado = Channel.ListaPrecioContado;
            existingChannel.LimiteCredito = Channel.LimiteCredito;
            existingChannel.Iva = Channel.Iva;
            existingChannel.GrupoCredito = Channel.GrupoCredito;
            existingChannel.FormaPago = Channel.FormaPago;
            existingChannel.Fecha = Channel.Fecha;
            existingChannel.DocumentoElectronico = Channel.DocumentoElectronico;
            existingChannel.CuentaContable = Channel.CuentaContable;
            existingChannel.CodigoProducto = Channel.CodigoProducto;
            existingChannel.CategoriaCliente = Channel.CategoriaCliente;
            existingChannel.Bodega = Channel.Bodega;
            existingChannel.Ambiente = Channel.Ambiente;
            _unitOfWork.ChannelRepository.Update(existingChannel);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteChannel(int id)
        {
            await _unitOfWork.ChannelRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}