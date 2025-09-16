
USE MarketWeight
/*USERS*/

CREATE USER 'papuSupremo'@'127.0.0.1' IDENTIFIED BY 'passPapuSupremo1#';
CREATE USER 'usuario'@'%' IDENTIFIED BY 'passUsuario1#';

/*GRANTS*/
GRANT SELECT, UPDATE, INSERT ON MarketWeight.* TO 'papuSupremo'@'localhost';
GRANT SELECT, UPDATE(cantidad) ON MarketWeight.Moneda TO 'usuario'@'%';
GRANT SELECT, UPDATE, INSERT ON MarketWeight.UsuarioMoneda TO 'usuario'@'%';
GRANT SELECT, INSERT ON MarketWeight.Historial TO 'usuario'@'%';