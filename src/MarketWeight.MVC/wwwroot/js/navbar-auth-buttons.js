const authLinks = document.getElementById("authLinks");

// Función para guardar el usuario en localStorage
function saveUserToLocalStorage(user) {
  if (user) {
    localStorage.setItem("currentUser", JSON.stringify(user));
  } else {
    localStorage.removeItem("currentUser");
  }
}

// Función para obtener el usuario del localStorage
function getUserFromLocalStorage() {
  const stored = localStorage.getItem("currentUser");
  return stored ? JSON.parse(stored) : null;
}

function updateAuthButtons() {
  // Usar el usuario de window.currentUser o localStorage
  const currentUser = window.currentUser || getUserFromLocalStorage();

  // Si hay un usuario en window.currentUser, guardarlo en localStorage
  if (window.currentUser) {
    saveUserToLocalStorage(window.currentUser);

    // Debug información de contraseñas
    if (window.currentUser.debug) {
      console.log("=== Debug Info ===");
      console.log(
        "Contraseña ingresada:",
        window.currentUser.debug.inputPassword
      );
      console.log("Hash generado:", window.currentUser.debug.hashedInput);
      console.log("Hash almacenado:", window.currentUser.debug.storedHash);
      console.log(
        "¿Coinciden?:",
        window.currentUser.debug.hashedInput ===
          window.currentUser.debug.storedHash
      );
      console.log("================");
    }
  }
  if (currentUser) {
    // Usuario está logueado
    authLinks.innerHTML = `
            <li class="nav-item dropdown">
                    Bienvenido, ${currentUser.nombre}
            </li>
        `;
  } else {
    // Usuario no está logueado
    authLinks.innerHTML = `
            <li class="nav-item me-2">
                <a class="btn btn-outline-primary" href="/Cuenta/Registro">Registrate</a>
            </li>
            <li class="nav-item">
                <a class="btn btn-primary" href="/Cuenta/Login">Iniciar Sesión</a>
            </li>
        `;
  }
}

// Llamar a la función cuando se carga la página
updateAuthButtons();

// Agregar event listener para detectar clics en el botón de logout
document.addEventListener("click", function (e) {
  if (e.target.matches('a[href="/Cuenta/Logout"]')) {
    // Limpiar localStorage antes de hacer logout
    localStorage.removeItem("currentUser");
  }
});
