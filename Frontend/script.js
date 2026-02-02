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

    // Eventos para capitalizar en tiempo real (al escribir)
    ['nombre', 'apellido'].forEach(id => {
        const input = document.getElementById(id);
        input.addEventListener('input', (e) => {
            const start = e.target.selectionStart;
            const end = e.target.selectionEnd;
            e.target.value = capitalizar(e.target.value);
            // Restaurar posición del cursor para que no salte al final
            e.target.setSelectionRange(start, end);
        });
    });

    // Evento para restringir solo números y puntos en el DNI en tiempo real
    const inputDni = document.getElementById('dni');
    inputDni.addEventListener('input', (e) => {
        // Removemos cualquier caracter que no sea un número o un punto
        e.target.value = e.target.value.replace(/[^0-9.]/g, '');
    });
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
        { id: 'mail', nombre: 'Correo electrónico' }
    ];

    // Validación: Todos los campos obligatorios
    for (const campo of campos) {
        const elemento = document.getElementById(campo.id);
        const valor = elemento.value.trim();

        if (valor === "") {
            mostrarToast(`Falta completar el campo "${campo.nombre}".`, 'error');
            elemento.focus();
            return false;
        }

        // Validación: Mínimo 3 caracteres para Nombre y Apellido
        if ((campo.id === 'nombre' || campo.id === 'apellido') && valor.length < 3) {
            mostrarToast(`El ${campo.nombre} tiene que tener al menos 3 caracteres.`, 'error');
            elemento.focus();
            return false;
        }
    }

    // Validación: DNI numérico y de 8 caracteres (ignorando puntos)
    const inputDni = document.getElementById('dni');
    const valorDni = inputDni.value.trim();
    const dniLimpio = valorDni.replace(/\./g, ''); // Quitamos los puntos para validar

    if (isNaN(dniLimpio) || dniLimpio === "") {
        mostrarToast("El DNI tiene que ser un número, fijate de corregirlo.", 'error');
        inputDni.focus();
        return false;
    }
    if (dniLimpio.length !== 8) {
        mostrarToast("El DNI está incompleto (deben ser 8 números).", 'error');
        inputDni.focus();
        return false;
    }

    // Validación: Formato de Mail
    const inputMail = document.getElementById('mail');
    const regexMail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!regexMail.test(inputMail.value)) {
        mostrarToast("El formato del mail no parece válido. Revisalo, por favor.", 'error');
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
    
    // Convertir los datos a un objeto simple (DNI sin puntos)
    const datos = {
        nombre: document.getElementById('nombre').value,
        apellido: document.getElementById('apellido').value,
        dni: document.getElementById('dni').value.replace(/\./g, ''),
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
        mostrarToast("¡Datos enviados con éxito!", 'exito');

    } catch (error) {
        // En caso de excepción al enviar (ej. error de red)
        console.error("Falla en el envío:", error);
        mostrarToast("Hubo un problema al enviar los datos.", 'error');
    }
}

/**
 * Muestra una notificación tipo Toast en pantalla.
 * @param {string} mensaje - El texto a mostrar.
 * @param {string} tipo - El tipo de notificación ('exito' o 'error').
 */
function mostrarToast(mensaje, tipo = 'exito') {
    const contenedor = document.getElementById('toast-container');
    if (!contenedor) return;

    const toast = document.createElement('div');
    
    // Configuración de colores según el tipo
    const colorClase = tipo === 'exito' ? 'bg-green-600' : 'bg-red-600';
    
    // Clases de Tailwind para el diseño del Toast
    toast.className = `${colorClase} text-white px-6 py-4 rounded-2xl shadow-2xl flex items-center justify-between min-w-[300px] transform translate-y-10 opacity-0 transition-all duration-300 ease-out pointer-events-auto`;
    
    toast.innerHTML = `
        <div class="flex items-center gap-3">
            <span class="font-medium">${mensaje}</span>
        </div>
        <button class="ml-4 text-white/70 hover:text-white transition-colors">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" viewBox="0 0 20 20" fill="currentColor">
                <path fill-rule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clip-rule="evenodd" />
            </svg>
        </button>
    `;

    // Agregar al contenedor
    contenedor.appendChild(toast);

    // Animación de entrada
    setTimeout(() => {
        toast.classList.remove('translate-y-10', 'opacity-0');
    }, 10);

    // Función para cerrar el toast
    const cerrarToast = () => {
        toast.classList.add('opacity-0', 'translate-y-2');
        setTimeout(() => toast.remove(), 300);
    };

    // Cerrar al hacer clic en la X
    toast.querySelector('button').onclick = cerrarToast;

    // Auto-cierre después de 4 segundos
    setTimeout(cerrarToast, 4000);
}

/**
 * Capitaliza la primera letra de cada palabra en un string.
 * @param {string} texto - El texto a capitalizar.
 * @returns {string} - El texto capitalizado.
 */
function capitalizar(texto) {
    if (!texto) return "";
    // No usamos trim() para permitir que el usuario siga escribiendo espacios
    return texto
        .split(' ')
        .map(palabra => {
            if (palabra.length === 0) return "";
            return palabra.charAt(0).toUpperCase() + palabra.slice(1).toLowerCase();
        })
        .join(' ');
}


