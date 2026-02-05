/**
 * utilities.js - Funciones utilitarias
 */

/**
 * Capitalizar la primera letra de cada palabra en un string.
 * @param {string} texto - El texto a capitalizar.
 * @returns {string} - El texto capitalizado.
 */
function capitalizar(texto) {
    if (!texto) { return ""; }

    texto = texto.split(' ').map(palabra => {
        if (palabra.length === 0) { return ""; }
        return palabra.charAt(0).toUpperCase() + palabra.slice(1).toLowerCase();
    }).join(' ');

    return texto;
}
