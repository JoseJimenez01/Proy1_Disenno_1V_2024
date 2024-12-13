// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//-------------------------- Para hacer los cambios de tema ----------------------------

// Verifica si el tema oscuro está activo al cargar la página
const isDarkMode = () => document.body.classList.contains('dark-mode');

/* Para cambiar el icono segun el tema */
const themeToggle = document.getElementById('theme-toggle');
const iconSun = document.getElementById('icon-sun');
const iconMoon = document.getElementById('icon-moon');

// Actualiza los íconos al cambiar el tema
const updateIcons = () => {
    if (isDarkMode()) {
        iconSun.style.display = 'block';
        iconMoon.style.display = 'none';
    } else {
        iconSun.style.display = 'none';
        iconMoon.style.display = 'block';
    }
};

// Alterna el tema y actualiza los íconos
themeToggle.addEventListener('click', () => {
    document.body.classList.toggle('dark-mode');
    updateIcons();
});

// Establece los íconos correctos al cargar la página
updateIcons();