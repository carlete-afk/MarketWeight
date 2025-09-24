Use MarketWeight;

CALL AltaCriptoMoneda(100.50, 100, 'Bitcoin', "https://i.imgur.com/t7LktS8.png");
CALL AltaCriptoMoneda(50.25, 100, 'Ethereum', "https://i.imgur.com/lvymhpP.png");
CALL AltaCriptoMoneda(75.00, 100, 'Ripple', "https://i.imgur.com/HEKUUuv.png");
CALL AltaCriptoMoneda(20.75, 100, 'Litecoin', "https://i.imgur.com/wfC4qGh.png");
CALL AltaCriptoMoneda(30.10, 100, 'Cardano', "https://i.imgur.com/2tFD61w.png");
CALL AltaCriptoMoneda(60.80, 100, 'Polkadot', "https://i.imgur.com/GRWFt5a.png");
CALL AltaCriptoMoneda(90.00, 100, 'Chainlink', "https://i.imgur.com/5ql5jsC.png");
CALL AltaCriptoMoneda(110.15, 100, 'Stellar', "https://i.imgur.com/aHHrq6g.png");
CALL AltaCriptoMoneda(40.60, 100, 'Dogecoin', "https://i.imgur.com/QUqtNDh.png");
CALL AltaCriptoMoneda(70.85, 100, 'Tron', "https://i.imgur.com/3YwFyDr.png");

CALL AltaUsuario('Ana', 'Garcia', 'ana.garcia@example.com', 'pass1234');
CALL AltaUsuario('Luis', 'Martinez', 'luis.martinez@example.com', '1234abcd');
CALL AltaUsuario('Marta', 'Fernandez', 'marta.fernandez@example.com', 'abcd1234');
CALL AltaUsuario('Carlos', 'Gomez', 'carlos.gomez@example.com', 'qwerty12');
CALL AltaUsuario('Laura', 'Rodriguez', 'laura.rodriguez@example.com', 'password');
CALL AltaUsuario('Pedro', 'Lopez', 'pedro.lopez@example.com', 'abcd1234');
CALL AltaUsuario('Sofia', 'Hernandez', 'sofia.hernandez@example.com', '12345678');
CALL AltaUsuario('Daniel', 'Perez', 'daniel.perez@example.com', '1q2w3e4r');
CALL AltaUsuario('Maria', 'Torres', 'maria.torres@example.com', 'letmein1');
CALL AltaUsuario('Javier', 'Ramirez', 'javier.ramirez@example.com', 'welcome1');

CALL IngresarDinero(2, 0);
CALL IngresarDinero(3, 10000);
CALL IngresarDinero(2, 10000);

-- CALL ComprarMoneda (2, 3, 2);
-- CALL Transferencia (2, 0.5, 2, 3);