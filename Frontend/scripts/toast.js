/**
 * toast.js - Funcionalidad (mínima) para mostrar notificaciones tipo Toast
 */

/**
 * Muestra una notificación tipo Toast en pantalla.
 * @param {string} mensaje - El texto a mostrar.
 * @param {string} tipo - El tipo de notificación ('exito' o 'error').
 */
function mostrarToast(mensaje, tipo = 'exito') {
    const contenedor = document.querySelector('#toast-container');
    if (!contenedor) { return; }

    const toast = document.createElement('div');
    
    // configuración de colores según el tipo
    const colorClase = tipo === 'exito' ? 'bg-green-600' : 'bg-red-600';
    
    // clases de Tailwind para el diseño del Toast
    toast.className = `${colorClase} text-white px-6 py-4 rounded-2xl shadow-2xl flex items-center justify-between min-w-[300px] transform translate-y-10 opacity-0 transition-all duration-300 ease-out pointer-events-auto`;
    
    toast.innerHTML = `
        <!-- el mensaje -->
        <div class="flex items-center gap-3">
            <span class="font-medium">${mensaje}</span>
        </div>
        <!-- la 'X' para cerrar el toast -->
        <button class="ml-4 text-white/70 hover:text-white transition-colors">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" viewBox="0 0 20 20" fill="currentColor">
                <path fill-rule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clip-rule="evenodd" />
            </svg>
        </button>
    `;

    // agregar al contenedor
    contenedor.appendChild(toast);

    // animación de entrada
    setTimeout(() => { toast.classList.remove('translate-y-10', 'opacity-0'); }, 10);

    // función para cerrar el toast
    const cerrarToast = () => {
        toast.classList.add('opacity-0', 'translate-y-2');
        setTimeout(() => toast.remove(), 300);
    };

    // cerrar al hacer clic en la X
    toast.querySelector('button').onclick = cerrarToast;

    // auto-cierre después de 4 segundos
    setTimeout(cerrarToast, 4000);
}
