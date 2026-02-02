/**
 * script.js - Lógica para el formulario de registro (Metropol Challenge)
 * Requerimiento: Vanilla JS, ES-AR.
 */

document.addEventListener('DOMContentLoaded', () => {
    inicializar();
});

/**
 * Configura los eventos de los elementos del DOM.
 */
function inicializar() {
    const formulario = document.getElementById('formularioRegistro');
    const btnCancelar = document.getElementById('btnCancelar');

    // Evento para el envío del formulario
    formulario.addEventListener('submit', manejarEnvio);

    // Evento para el botón cancelar
    btnCancelar.addEventListener('click', limpiarFormulario);
}

/**
 * Procesa el intento de envío del formulario.
 * @param {Event} evento - El objeto del evento de envío.
 */
function manejarEnvio(evento) {
    evento.preventDefault();

    if (validarFormulario()) {
        enviarDatos();
    }
}

/**
 * Valida que los campos del formulario cumplan con los requerimientos.
 * @returns {boolean} - Verdadero si es válido, falso de lo contrario.
 */
function validarFormulario() {
    const campos = [
        { id: 'nombre', nombre: 'Nombre' },
        { id: 'apellido', nombre: 'Apellido' },
        { id: 'dni', nombre: 'DNI' },
        { id: 'sexo', nombre: 'Sexo' },
        { id: 'mail', nombre: 'Mail' }
    ];

    // Validación: Todos los campos obligatorios
    for (const campo of campos) {
        const elemento = document.getElementById(campo.id);
        const valor = elemento.value.trim();

        if (valor === "") {
            alert(`Che, te olvidaste de completar el campo "${campo.nombre}".`);
            elemento.focus();
            return false;
        }
    }

    // Validación: DNI numérico
    const inputDni = document.getElementById('dni');
    if (isNaN(inputDni.value)) {
        alert("El DNI tiene que ser un número, fijate de corregirlo.");
        inputDni.focus();
        return false;
    }

    // Validación: Formato de Mail
    const inputMail = document.getElementById('mail');
    const regexMail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!regexMail.test(inputMail.value)) {
        alert("El formato del mail no parece válido. Revisalo, por favor.");
        inputMail.focus();
        return false;
    }

    return true;
}

/**
 * Limpia todos los campos del formulario.
 */
function limpiarFormulario() {
    const formulario = document.getElementById('formularioRegistro');
    formulario.reset();
}

/**
 * Envía los datos del formulario en formato JSON mediante un POST fetch.
 */
async function enviarDatos() {
    const formulario = document.getElementById('formularioRegistro');
    
    // Convertir los datos a un objeto simple
    const datos = {
        nombre: document.getElementById('nombre').value,
        apellido: document.getElementById('apellido').value,
        dni: document.getElementById('dni').value,
        sexo: document.getElementById('sexo').value,
        mail: document.getElementById('mail').value
    };

    try {
        const respuesta = await fetch('https://ejemplo-api.com/registro', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(datos)
        });

        // Como no hay backend funcional aún, capturamos el resultado del fetch
        const resultado = await respuesta.json();
        console.log("Respuesta del servidor:", resultado);
        alert("¡Datos enviados con éxito!");

    } catch (error) {
        // En caso de excepción al enviar (ej. error de red)
        console.error("Falla en el envío:", error);
        alert("Che, hubo un problema al intentar enviar los datos. Probablemente porque todavía no tenemos el backend listo.");
    }
}
