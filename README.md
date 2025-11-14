🛠️ Sistema de Administración para Taller Mecánico
ASP.NET Core MVC · Razor · Class Library · POO · AOP · Eventos

Este proyecto es el sistema web principal, desarrollado con ASP.NET Core MVC y Razor Views, encargado de manejar toda la interacción del usuario, los controladores, las vistas y los servicios del sistema.
El proyecto se integra con una biblioteca de clases independiente, donde reside toda la lógica del dominio, implementada con Programación Orientada a Objetos (POO), programación orientada a aspectos (AOP), eventos y principios sólidos de diseño, incluyendo la Inversión de Dependencias (DIP).

El objetivo del sistema es proporcionar una solución completa, modular y escalable para la administración de un taller de mecánica automotriz, facilitando la gestión de clientes, mecánicos, repuestos, órdenes y procesos internos.

✨ Características del Proyecto

✔️ Arquitectura MVC bien organizada

✔️ Interfaz con Razor Views, layouts y vistas parciales

✔️ Consumo de una Class Library que contiene toda la lógica del dominio

✔️ Integración mediante servicios con Inversión de Dependencias

✔️ Formularios dinámicos con AJAX y validación en cliente

✔️ Modelo POO puro: herencia, polimorfismo y encapsulación

✔️ Uso de AOP para separación de reglas transversales

✔️ Manejo de eventos para cambios en órdenes y procesos

✔️ Gestión completa del taller: clientes, mecánicos, repuestos y órdenes

🧱 Arquitectura General del Sistema

El sistema está compuesto por dos capas principales:

🔹 1. Biblioteca de Clases (Dominio y Lógica de Negocio)

Contiene:

Entidades y estructuras basadas en POO

Programación orientada a aspectos (AOP):

Validaciones

Reglas transversales

Comportamientos repetitivos del dominio

Eventos del dominio:

Cambio de estado de órdenes

Asignación de repuestos

Modificaciones relevantes

Interfaces y contratos siguiendo DIP

Servicios y reglas del negocio

Utilidades y componentes de soporte

🔹 2. Proyecto Web — ASP.NET Core MVC + Razor

Incluye:

Controladores MVC encargados del flujo de la aplicación

Servicios de aplicación que comunican las vistas con el dominio

Razor Views estructuradas con layout, parciales y helpers

Integración con JavaScript y AJAX

Validación de formularios

Renderizado dinámico de datos

Manejo de estados y acciones del usuario

🚀 Funcionalidades Principales

Gestión completa de clientes y mecánicos

Creación, edición y seguimiento de órdenes de reparación

Control de estados:

Pendiente

En reparación

Listo para entrega

Pagado

Registro y administración de repuestos

Asignación de repuestos a órdenes

Búsqueda y filtrado dinámico

Actualizaciones mediante AJAX

Flujo operativo del taller totalmente administrado

🔧 Integración con la Biblioteca de Clases

La comunicación entre el proyecto MVC y la biblioteca se realiza mediante:

Inyección de dependencias en Program.cs

Servicios que implementan interfaces del dominio

Modelos compartidos entre capas

Contratos y reglas internas establecidos en la class library

Esto asegura una arquitectura desacoplada, escalable y limpia.

🛠️ Tecnologías Utilizadas

C#

.NET 10

ASP.NET Core MVC

Razor Views

jQuery y AJAX

Librería de clases para lógica del dominio

Principios SOLID

POO, AOP y programación orientada a eventos
