# Metropol Challenge

> Proyecto demo para la solicitud de empleo en Metropol.

&nbsp;

## Listado parcial de features:

### FRONTEND

- Uso los íconos y tipografía oficial (open sans) de la marca.

- Diseño `responsive`
  - 3 breaking-points: grande, medio (tablets) y pequeño (smartphones)
  - Stackeo de los bloques de contenido

- Única salvedad respecto a no usar frameworks:
  - Agregué la librería `Tailwind CSS`, que no es framework/librería de JavaScript sino de CSS
  - Sin estilos el front se veía espantoso 😅
  - La utilizo vía CDN, no requiere instalación
  - ¿Lo puedo hacer con el CSS a mano? Sí, definitivamente, pero para una demo me parece que no valía la pena.
  - Si lo requieren ¡avisen!, cero drama de hacer los estilos con CSS plano.


### BACKEND

- Desarrollado en .NET 10 (versión recién salida del horno)
  - Desconozco las herramientas que tenga instaladas quien evalúe la demo.
  - Si prefieren por compatibilidad que utilice alguna versión previa, avisen, que sería un cambio de 5 minutos.

- Detalle de la estrucutra del backend
  - `Controllers/`: Endpoints de la API.
  - `DataAccess/`: Contexto de base de datos.
  - `Entities/`: Modelos de datos para persistencia.
  - `DTOs/`: Objetos de transferencia de datos.
  - `BusinessLogic/`: Mapeos y reglas de validación.

- Uso de la librería `FluentValidations` para las validaciones de las entidades.

- Uso de `AutoMapper` para los mapeos entre los DTOs y las Entidades.

- Patrón de diseño `Repository` para la capa de acceso a datos.
- Uso de `SQlite` para la DB (por mepa simplicidad, ya que no requiere un server externo).

- Test Unitarios implementados en `MSTest`, la librería oficial de MS.

- Logs implementados con `Serilog`, por simplicidad, al archivo `Logs/logs.txt`.

- **Docs online** con `Scalar`
  - Nuevo en .NET 10 (muy similar al `Swagger` de toda la vida)
  - URL de acceso: https://localhost:5000/scalar/v1

- Mecanismo de `autenticación`+`autorización`:
  - Parto de la base que es un formulario de registración (acceso público) y no lleva auth.
  - Si lo requieren (fuera del alcance de la consigna original) implemento uno facilito, vía `JWT` (JSON Web Tokens).  
    Algo del estilo:
    - Form. de Registración (el actual), de acceso público.
    - Form. de Login, de acceso público.
    - Landing-Page / Panel-de-Control (o algo similar), con mecanismo de autenticación`+`autorización, de acceso **privado**.

- Mecanismo de control de cambios... `GitFlow`
  - Uso de ramas `main` y `develop`.
  - Dado el alcance limitado de la demo, omití los branches independientes por feature individual.


### PROYECTO

- **LIVE DEMO**:
  <...>

- Implemento **CI/CD** vía `GitHub Actions`.
  - Frontend alojado en `GitHub Pages`.
  - Backend alojado en <...>

- Repo del proyecto:  
  https://github.com/andres-garcia-alves/demo-metropol-challenge


### NOTAS

- Hay cosas que obviamente se pueden mejorar, pero que sobrepasarían el alcance de una demo:
  - Los controllers que hereden de un ControllerBase, centalizando logs y manejo de errores
  - Usar una DB en servidor aparte, en lugar de una DB embebida
  - etc

- Aprovecho el repo de la demo, y los invito a visitar mi GitHub: son 40+ repos con aplicaciones web, desktop, IA, electrónica, videojuegos, etc... y según cada temática utilizando algunas de sus herramientas y tecnologías relevantes: .NET, frameworks para JavaScript/TypeScript, Python, C/C++, y un largo etc.

### Screenshots

| Form. de Registro                        | Validaciones                             |
|------------------------------------------|------------------------------------------|
| ![](Resources/screenshot-01.png)         | ![](Resources/screenshot-02.png)         |

| Responsive (Tablets)                     | Responsive (Mobile)                      |
|------------------------------------------|------------------------------------------|
| ![](Resources/screenshot-03.png)         | ![](Resources/screenshot-04.png)         |

| CI/CD                                    | Backend API                              |
|------------------------------------------|------------------------------------------|
| ![](Resources/screenshot-05.png)         | ![](Resources/screenshot-06.png)         |

&nbsp;

### Version History

v1.0 (2026.02.01) - First commit.  
v1.1 (2026.02.02) - Added validations and responsive design.  
v1.2 (2026.02.02) - Added CI/CD pipeline.  
v1.3 (2026.02.05) - Added Backend base code.  

&nbsp;

This source code is licensed under GPL v3.0  
Please send me your feedback about this project: andres.garcia.alves@gmail.com
