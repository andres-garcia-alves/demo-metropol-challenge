/**
 * scripts.js - Lógica para el Formulario de Registro
 */

// De la consigna de 'Vanilla JS' (JS puro, sin frameworks, bundlers, etc),
// se complica utilizar variables de entorno (archivo '.env').
// Implemento a mano una selección dinámica del backend, basada en la URL del navegador.
const localHosts = ["", "localhost", "127.0.0.1"];
const isDevelopment = localHosts.includes(window.location.hostname);

const DEV_URL = "https://localhost:5000/api/v1";
const PROD_URL = "https://metropol-backend.azurewebsites.net/api/v1";
const BACKEND_URL = isDevelopment ? DEV_URL : PROD_URL;


// Inicializar la aplicación una vez que haya cargado el DOM
document.addEventListener('DOMContentLoaded', () => { inicializar(); });


/**
 * Configurar los eventos en los elementos del DOM
 */
function inicializar() {

    const formulario = document.querySelector('#formularioRegistro');
    const btnCancelar = document.querySelector('#btnCancelar');

    // evento para el envío del formulario
    formulario.addEventListener('submit', manejarEnvio);

    // evento para el botón cancelar
    btnCancelar.addEventListener('click', limpiarFormulario);

    // eventos para capitalizar en tiempo real (al escribir)
    ['nombre', 'apellido'].forEach(id => {
        const input = document.querySelector('#' + id);

        input.addEventListener('input', (e) => {
            const start = e.target.selectionStart;
            const end = e.target.selectionEnd;

            // capitaliza el texto
            e.target.value = capitalizar(e.target.value);
            // restaurar posición del cursor para que no salte al final
            e.target.setSelectionRange(start, end);
        });
    });

    // evento para restringir solo números y puntos en el DNI en tiempo real
    const inputDni = document.querySelector('#dni');
    inputDni.addEventListener('input', (e) => {
        // remover cualquier caracter que no sea número o punto
        e.target.value = e.target.value.replace(/[^0-9.]/g, '');
    });
}


/**
 * Procesar el intento de envío del formulario.
 * @param {Event} evento - El objeto del evento de envío.
 */
function manejarEnvio(evento) {

    evento.preventDefault();
    if (validarFormulario()) { enviarDatos(); }
}


/**
 * Limpiar todos los campos del formulario.
 */
function limpiarFormulario() {

    const formulario = document.querySelector('#formularioRegistro');
    formulario.reset();
}


/**
 * Envía los datos del formulario mediante la fetch API (método POST).
 */
async function enviarDatos() {

    const formulario = document.querySelector('#formularioRegistro');
    const sucessMessage = "¡Datos enviados con éxito!";
    const errorMessage1 = "Error procesando los datos.";
    const errorMessage2 = "Falló el envío de los datos.";
    
    // convertir los datos a un objeto simple (DNI sin puntos)
    const datos = {
        nombre: document.querySelector('#nombre').value,
        apellido: document.querySelector('#apellido').value,
        dni: document.querySelector('#dni').value.replace(/\./g, ''),
        sexo: document.querySelector('#sexo').value,
        mail: document.querySelector('#mail').value
    };

    try {
        // enviar los datos a la API
        const respuesta = await fetch(BACKEND_URL + '/personas', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(datos)
        });

        const resultado = await respuesta.json();
        
        if (respuesta.ok) {
            console.log("Respuesta del servidor:", resultado);
            mostrarToast(resultado.message || sucessMessage, 'exito');
            limpiarFormulario();
        } else {
            // manejo de errores controlados por el backend (BadRequest, etc)
            console.error("Error en el servidor:", resultado);
            mostrarToast(resultado.message || errorMessage1, 'error');
        }

    } catch (error) {
        // en caso de excepción al enviar (ej. error de red)
        console.error("Falló el envío de los datos:", error);
        mostrarToast(errorMessage2, 'error');
    }
}
