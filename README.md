# 🔧 Taller Mecánico MVC — 14 Noviembre de 2025

**Taller Mecánico MVC** es un proyecto académico cuyo objetivo es desarrollar una **plataforma web para administrar un taller de mecánica automotriz**, permitiendo gestionar clientes, mecánicos, repuestos y órdenes de reparación de forma clara e intuitiva.

El sistema utiliza **ASP.NET Core MVC con Razor** y una **biblioteca de clases** independiente que encapsula toda la lógica del dominio mediante POO, eventos, aspectos y principios SOLID (especialmente Inversión de Dependencias).

---

## 🧩 Descripción general

La aplicación permite simular el funcionamiento real de un taller mecánico, ofreciendo:

- 🧑‍🔧 **Gestión de mecánicos**
- 🚗 **Registro de clientes y vehículos**
- 📄 **Creación y edición de órdenes de reparación**
- 🧩 **Asignación de repuestos a órdenes**
- 💵 **Cálculo automático de costos**
- 🔄 **Actualización del estado de las órdenes**
- ⚡ **Uso de AJAX para una experiencia fluida**

---

## 🧠 Arquitectura del sistema

El proyecto está dividido en dos capas principales:

### **1. Biblioteca de Clases (Dominio y Lógica de Negocio)**
Implementa:

- Programación Orientada a Objetos (POO)
- Programación Orientada a Eventos
- Programación Orientada a Aspectos (AOP)
- Inversión de Dependencias (DIP)
- Principios SOLID

Incluye:

- Modelos del dominio  
- Servicios del negocio  
- Eventos de actualización  
- Validaciones  
- Reglas de lógica interna  

---

### **2. Proyecto Web (ASP.NET Core MVC con Razor)**
Encargado de:

- Controladores  
- Vistas Razor  
- Scripts JS + Fetch AJAX  
- Organización de rutas  
- Renderizado de formularios y vistas parciales  

---

---

## 🛠️ Funcionalidades principales

| Módulo        | Funcionalidades                                                         |
|---------------|-------------------------------------------------------------------------|
| 🧑‍🔧 Mecánicos | Registrar, editar, listar y eliminar mecánicos                         |
| 🧑 Clientes    | Registrar clientes y sus vehículos                                      |
| 🚗 Vehículos   | Asociar vehículos a un cliente                                          |
| 📄 Órdenes     | Crear órdenes, asignar mecánico, cambiar estado                         |
| 🧩 Repuestos   | Asignar repuestos y calcular costo                                      |
| 💵 Costos      | Cálculo automático según repuestos                                      |
| ⚡ AJAX        | Envío de formularios sin recargar la página                             |

---

## 🛠️ Tecnologías utilizadas

| Tecnología            | Descripción                         |
|----------------------|-------------------------------------|
| **C# .NET 10**       | Lenguaje principal                  |
| **ASP.NET Core MVC**  | Arquitectura del proyecto           |
| **Razor Views**       | Sistema de vistas                   |
| **JavaScript (Fetch)**| Uso de AJAX moderno                 |
| **Biblioteca POO/AOP**| Lógica del dominio                  |
| **Patrón MVC**        | Separación de responsabilidades     |

---

## 🚀 Resumen final

Este proyecto representa un sistema completo para la administración de un taller mecánico, implementado con buenas prácticas profesionales, arquitectura limpia y una biblioteca de clases altamente desacoplada.

