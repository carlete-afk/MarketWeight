const authLinks = document.getElementById("authLinks");

function updateAuthButtons() {
  if (window.currentUser) {
    authLinks.innerHTML = `
            <li class="nav-item me-2">
                <button class="btn btn-outline-primary" id="accountButton">Cuenta</button>
            </li>

            <li class="nav-item">
                <button class="btn btn-danger" id="logoutButton">Cerrar Sesión</button>
            </li>
        `;

    const logoutButton = document.getElementById("logoutButton");
    const accountButton = document.getElementById("accountButton");

    logoutButton.addEventListener("click", () => {
      fetch("/Cuenta/Logout", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
      })
        .then((response) => {
          response.ok
            ? (window.location.href = "/Home/Index")
            : console.error("Error al cerrar sesión");
        })
        .catch((error) =>
          console.error("Error al realizar la solicitud de logout:", error)
        );
    });

    accountButton.addEventListener("click", () => {
      window.location.href = "/Cuenta/Perfil";
    });
  } else {
    authLinks.innerHTML = `
            <li class="nav-item me-2">
                <a class="btn btn-outline-primary" href="/Cuenta/Registro">Registrate</a>
            </li>
            <li class="nav-item">
                <a class="btn btn-primary" href="/Cuenta/Login">Inicia Sesión</a>
            </li>
    `;
  }
}

updateAuthButtons();
