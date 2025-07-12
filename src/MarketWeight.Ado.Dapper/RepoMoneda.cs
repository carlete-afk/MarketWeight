using System.Data;
using Dapper;
using MarketWeight.Core;
using MarketWeight.Core.Persistencia;
using System.Data.Common;

namespace MarketWeight.Ado.Dapper;

public class RepoMoneda : RepoGenerico, IRepoMoneda
{
    public RepoMoneda(IDbConnection conexion) : base(conexion) { }

    // -----------------------------------------------------------
    //      AltaMoneda
    // -----------------------------------------------------------

    public void Alta(Moneda moneda)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@xprecio", moneda.Precio);
        parametros.Add("@xcantidad", moneda.Cantidad);
        parametros.Add("@xnombre", moneda.Nombre);

        try
        {
            Conexion.Execute("AltaCriptoMoneda", parametros);
        }
        catch (DbException e)
        {
            //DuplicateKeyEntry   
            if (e.ErrorCode == 1062)
            {
                throw new ConstraintException($"La moneda {moneda.Nombre} ya ha sido ingresada.");
            }
            throw;
        }
    }

    public async Task AltaAsync(Moneda moneda)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@xprecio", moneda.Precio);
        parametros.Add("@xcantidad", moneda.Cantidad);
        parametros.Add("@xnombre", moneda.Nombre);

        try
        {
            await Conexion.ExecuteAsync("AltaCriptoMoneda", parametros);
        }
        catch (DbException e)
        {
            //DuplicateKeyEntry   
            if (e.ErrorCode == 1062)
            {
                throw new ConstraintException($"La moneda {moneda.Nombre} ya ha sido ingresada.");
            }
            throw;
        }
    }

    // -----------------------------------------------------------
    //      DetalleMoneda 
    // -----------------------------------------------------------

    public Moneda? Detalle(uint indiceABuscar)
    {
        var consulta = $"SELECT * FROM Moneda WHERE idMoneda = {indiceABuscar}";
        var monedas = Conexion.QueryFirstOrDefault<Moneda>(consulta);
        return monedas;
    }

    public async Task<Moneda?> DetalleAsync(uint indiceABuscar)
    {
        var consulta = $"SELECT * FROM Moneda WHERE idMoneda = {indiceABuscar}";
        var monedas = await Conexion.QueryFirstOrDefaultAsync<Moneda>(consulta);
        return monedas;
    }

    // -----------------------------------------------------------
    //      ObtenerMonedas
    // -----------------------------------------------------------

    public IEnumerable<Moneda> Obtener()
    {
        var consulta = "SELECT * FROM Moneda";
        var monedas = Conexion.Query<Moneda>(consulta);
        return monedas;
    }

    public async Task<IEnumerable<Moneda>> ObtenerAsync()
    {
        var consulta = "SELECT * FROM Moneda";
        var monedas = await Conexion.QueryAsync<Moneda>(consulta);
        return monedas;
    }

    // -----------------------------------------------------------
    //      ObtenerConCondicion 
    // -----------------------------------------------------------

    public IEnumerable<Moneda> ObtenerConCondicion(string condicion)
    {
        var consulta = $"SELECT * FROM Moneda WHERE {condicion}";
        var monedas = Conexion.Query<Moneda>(consulta);
        return monedas;
    }

    public async Task<IEnumerable<Moneda>> ObtenerConCondicionAsync(string condicion)
    {
        var consulta = $"SELECT * FROM Moneda WHERE {condicion}";
        var monedas = await Conexion.QueryAsync<Moneda>(consulta);
        return monedas;
    }

    // -----------------------------------------------------------
    //      ActualizarMoneda
    // -----------------------------------------------------------

    public void Actualizar(Moneda moneda, uint id)
    {
        var consulta = "UPDATE Moneda SET precio = @precio, cantidad = @cantidad, nombre = @nombre WHERE idMoneda = @id;";
        var parametros = new DynamicParameters();
        parametros.Add("@precio", moneda.Precio);
        parametros.Add("@cantidad", moneda.Cantidad);
        parametros.Add("@nombre", moneda.Nombre);
        parametros.Add("@id", id);

        Conexion.Execute(consulta, parametros);
    }

    public async Task ActualizarAsync(Moneda moneda, uint id)
    {
        var consulta = "UPDATE Moneda SET precio = @precio, cantidad = @cantidad, nombre = @nombre WHERE idMoneda = @id;";
        var parametros = new DynamicParameters();
        parametros.Add("@precio", moneda.Precio);
        parametros.Add("@cantidad", moneda.Cantidad);
        parametros.Add("@nombre", moneda.Nombre);
        parametros.Add("@id", id);

        await Conexion.ExecuteAsync(consulta, parametros);
    }

    // -----------------------------------------------------------
    //      EliminarMoneda
    // -----------------------------------------------------------

    public void Eliminar(uint id)
    {
        var consulta = $"DELETE FROM Moneda WHERE idMoneda = {id};";

        Conexion.Query<Moneda>(consulta);
    }

    public async Task EliminarAsync(uint id)
    {
        var consulta = $" DELETE FROM Moneda WHERE idMoneda = {id};";

        await Conexion.QueryAsync<Moneda>(consulta);
    }
}
