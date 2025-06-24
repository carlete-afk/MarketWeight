using System.Numerics;

namespace MarketWeight.Core.Persistencia;

public interface IRepoDetalle<T, IS> where IS : IBinaryNumber<IS>
{
    T? Detalle(IS indiceABuscar);
    Task<T?> DetalleAsync(IS indiceABuscar);
}
