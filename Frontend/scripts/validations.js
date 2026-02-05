/**
 * validations.js - Lógica de validación para los campos del formulario
 */

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

    // validación: todos los campos son obligatorios
    for (const campo of campos) {
        const elemento = document.querySelector('#'+campo.id);
        const valor = elemento.value.trim();

        if (valor === "") {
            mostrarToast(`Falta completar el campo "${ campo.nombre }".`, 'error');
            elemento.focus();
            return false;
        }

        // validación: mínimo 3 caracteres para Nombre y Apellido
        if ((campo.id === 'nombre' || campo.id === 'apellido') && valor.length < 3) {
            mostrarToast(`El ${ campo.nombre } tiene que tener al menos 3 caracteres.`, 'error');
            elemento.focus();
            return false;
        }
    }

    // validación: DNI numérico y de 8 caracteres (ignorando puntos)
    const inputDNI = document.querySelector('#dni');
    const valueDNI = inputDNI.value.trim().replace(/\./g, ''); // quitar los puntos

    if (isNaN(valueDNI) || valueDNI === "") {
        mostrarToast("El DNI tiene que ser un número.", 'error');
        inputDNI.focus();
        return false;
    }

    if (valueDNI.length !== 8) {
        mostrarToast("El DNI no es correcto (deben ser 8 números).", 'error');
        inputDNI.focus();
        return false;
    }

    // validación: formato de email
    const inputMail = document.querySelector('#mail');
    const regexMail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!regexMail.test(inputMail.value)) {
        mostrarToast("El formato del mail no parece válido. Revísalo, por favor.", 'error');
        inputMail.focus();
        return false;
    }

    return true;
}
