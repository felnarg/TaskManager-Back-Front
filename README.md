# ToDoApp - TasksManager

## Descripción

**ToDoApp - TasksManager** es una aplicación para la gestión de actividades diarias que incluye funcionalidades para crear, editar, eliminar y cambiar el estado de tareas, así como gestionar las preferencias de notificación por correo electrónico mediante la API de SendGrid.

### Backend

El backend está desarrollado en ASP.NET Core utilizando la arquitectura limpia (Onion Architecture) para adherirse a los principios SOLID. Se implementaron los siguientes servicios:
- Creación, eliminación, edición y cambio de estado de tareas.
- Gestión de preferencias de notificación.
- Envío de notificaciones por correo electrónico utilizando la API de SendGrid.

Además, se aplicó inyección de dependencias para la comunicación entre capas y el patrón Repository para el acceso a datos.

### Frontend

El frontend está desarrollado en Angular y consiste en una aplicación de una sola página (SPA). Las principales características incluyen:
- Creación, actualización, eliminación y cambio de estado de tareas.
- Gestión de preferencias de notificación.
- Filtros para tareas basados en su importancia.

## Instalación

### Requisitos Previos

- Node.js
- Angular CLI
- Visual Studio (para el backend)
- Oracle Database

### Instalación del Frontend

1. Clona el repositorio:
    ```sh
    git clone https://github.com/felnarg/TaskManagerBackFront.git
    cd TaskManagerBackFront/TaskManagerFrontend
    ```

2. Instala las dependencias:
    ```sh
    npm install
    ```

3. Ejecuta la aplicación:
    ```sh
    ng serve
    ```

    La aplicación estará disponible en `http://localhost:4200`.

### Instalación del Backend

1. Navega al directorio del backend:
    ```sh
    cd ../TaskManagerBackend
    ```

2. Instala las dependencias necesarias:
    ```sh
    dotnet restore
    ```

3. Configura la base de datos:
    - Instala Oracle Database.
    - Configura la base de datos y actualiza la cadena de conexión en `appsettings.json`.
    - Elimina el contenido de la carpeta `Data/Migrations`.
    - Genera nuevas migraciones y actualiza la base de datos:
        ```sh
        dotnet ef migrations add InitialMigration -o Data/Migrations
        dotnet ef database update
        ```

4. Configura el servicio de envío de notificaciones:
    - En la clase `Worker` ubicada en `Application/Services/Worker`, ajusta el tiempo de envío de notificaciones:
        ```csharp
        Task.Delay(TimeSpan.FromMinutes(0.1)).Wait();
        ```

5. Inicia el servidor:
    ```sh
    dotnet run
    ```

    El servidor estará disponible en `http://localhost:[puerto]`.

## Uso

1. Abre `http://localhost:4200` en tu navegador para acceder a la interfaz de usuario.
2. Utiliza la interfaz para gestionar tus tareas diarias.
3. Asegúrate de que el backend esté corriendo para que la aplicación pueda interactuar con la base de datos y el servicio de notificaciones.

## Estructura del Proyecto

### Frontend

- **domains/tasks**: Contiene todos los componentes de la aplicación.
  - **pages**: 
    - `list.component.ts` (Componente padre)
  - **components**:
    - `task.component.ts` (Hijo de list)
    - `new-task.component.ts` (Hijo de task)
    - `update-task.component.ts` (Hijo de task)
  - **services**:
    - `master.service.ts` (Consumo de la API del backend)

### Backend

- **api/controllers**: Controladores de la API.
- **Application/Services**: Lógica de la aplicación.
- **Domain**: Contiene las entidades del dominio de la aplicación.
- **Infrastructure**: Servicios externos y acceso a datos.

## Contribución

¡Gracias por tu interés en contribuir! Aquí te dejamos algunos pasos para hacerlo:

1. Haz un fork del repositorio.
2. Crea una nueva rama para tu funcionalidad (`git checkout -b feature/nueva-funcionalidad`).
3. Realiza tus cambios y haz commits (`git commit -m 'Añadir nueva funcionalidad'`).
4. Empuja tus cambios a la rama (`git push origin feature/nueva-funcionalidad`).
5. Abre una pull request.

### Mejora Continua

- **Frontend**: Optimizar la distribución de los directorios y aplicar mejores prácticas para mejorar la escalabilidad de la aplicación.
- **Backend**: Agregar validaciones utilizando Fluent Validation para mejorar la robustez de la aplicación.

## Créditos

Este proyecto fue desarrollado por mi y supervizado por el equipo de especialistas de OPI Technology:

- Manuel Narvaez
- Oscar Rugeles
- Juan Diego Varon
- Juan Manuel Barrera
- William Monoga
- Jorge Parada

## Contacto

**Felipe Naranjo**
- Desarrollador Junior Full Stack
- Email: felipe.naranjo@opitech.com.co
