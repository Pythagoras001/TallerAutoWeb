*🔧 Taller Mecánico MVC — 23 mayo de 2025

Taller Mecánico MVC es un proyecto académico desarrollado como parte de un trabajo universitario.
El objetivo fue construir una plataforma web para la gestión integral de un taller mecánico, permitiendo administrar clientes, mecánicos, órdenes de reparación y repuestos de manera organizada e intuitiva.

---

*🧩 Descripción general

La aplicación simula el funcionamiento operativo de un taller automotriz, permitiendo a los usuarios:

🧑‍🔧 Registrar y gestionar mecánicos con sus datos básicos y funciones.

🚗 Registrar clientes y vehículos asociados.

📄 Crear órdenes de reparación para cada vehículo.

🧩 Asignar repuestos a una orden.

💵 Calcular automáticamente los costos según los repuestos asignados.

📌 Actualizar el estado de la orden (pendiente, en reparación, listo, pagado).

⚡ Usar AJAX para enviar información sin recargar la página.

---

*🧠 Arquitectura del sistema

La solución está dividida en dos partes principales:

*📚 Biblioteca de Clases (Dominio y Lógica)

Contiene toda la lógica del negocio, estructurada con:

✔️ Programación Orientada a Objetos (POO)

✔️ Programación Orientada a Eventos

✔️ Programación Orientada a Aspectos (AOP)

✔️ Inversión de Dependencias (DIP)

Incluye:

Modelos del dominio (Cliente, Mecánico, Repuesto, Orden, etc.)

Servicios y reglas del negocio

Validaciones centralizadas

Eventos que reaccionan a cambios en las órdenes

*🌐 Proyecto Web (ASP.NET Core MVC)

Se encarga de:

Controladores MVC

Vistas Razor y parciales

Formularios con validación

Scripts JavaScript + Fetch AJAX

Rutas, redirecciones y manejo visual

---

*🚀 Funcionalidades principales

🧑‍🔧 Gestión completa de mecánicos.

🧍 Registro y administración de clientes.

🚗 Registro de vehículos.

📄 Creación de órdenes de reparación.

🧩 Asignación de repuestos a cada orden.

💵 Cálculo automático del costo total.

🔄 Cambios de estado de la orden.

⚡ Funciones AJAX para mejorar la experiencia del usuario.

---

*🛠️ Tecnologías utilizadas

C#

.NET 10

ASP.NET Core MVC

Razor Views

JavaScript

Biblioteca de clases modular

Principios SOLID
