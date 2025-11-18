using System.Data;
using Dapper;
using MarketWeight.Core;
using MarketWeight.Core.Persistencia;
using System.Data.Common;

namespace MarketWeight.Ado.Dapper;

public class RepoUsuarioMoneda : RepoGenerico, IRepoUsuarioMoneda
{
    public RepoUsuarioMoneda(IDbConnection conexion) : base(conexion) { }

    // -----------------------------------------------------------
    //      ObtenerCriptosUsuario
    // -----------------------------------------------------------

    public UsuarioMoneda? ObtenerCriptosUsuario(uint idUsuario, uint idMoneda)
    {
        var consulta = $@"
        SELECT UM.*
        FROM UsuarioMoneda UM
        WHERE UM.idUsuario = {idUsuario} AND UM.idMoneda = {idMoneda}";
        var monedas = Conexion.QueryFirstOrDefault<UsuarioMoneda>(consulta);
        return monedas;
    }

    public async Task<UsuarioMoneda?> ObtenerCriptosUsuarioAsync(uint idUsuario, uint idMoneda)
    {
        var consulta = $@"
        SELECT UM.*
        FROM UsuarioMoneda UM
        WHERE UM.idUsuario = {idUsuario} AND UM.idMoneda = {idMoneda}";
        var monedas = await Conexion.QueryFirstOrDefaultAsync<UsuarioMoneda>(consulta);
        return monedas;
    }
}
