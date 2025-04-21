Introducción
El Sistema de Gestión Académica (SGA) es una solución informática diseñada para facilitar la administración de estudiantes y cursos en instituciones educativas. Su propósito es permitir la ejecución de operaciones CRUD (Crear, Leer, Actualizar y Eliminar) de manera eficiente, garantizando un manejo adecuado de asignaciones, validaciones y excepciones. Además, mediante la implementación de una arquitectura por capas, se busca optimizar la separación de responsabilidades entre el frontend y el backend, asegurando una mayor escalabilidad y mantenimiento del sistema.
Frontend y Backend
El frontend es la parte del sistema con la que el usuario interactúa directamente. Se encarga de la presentación de la información y la experiencia de usuario mediante tecnologías como HTML, CSS y JavaScript, además de frameworks como ASP.NET Web Forms. En el contexto del SGA, el frontend se utiliza para diseñar las páginas web de gestión de estudiantes y cursos, mostrando formularios, listas y botones de acción.
Por otro lado, el backend es el núcleo lógico del sistema, donde se procesan las solicitudes y se maneja la base de datos. Utiliza tecnologías como ASP.NET con C#, ADO.NET o Entity Framework. En el SGA, el backend gestiona la lógica de negocio mediante una biblioteca de clases, permitiendo el registro, consulta, actualización y eliminación de estudiantes, cursos y asignaciones, además de validar los datos y manejar excepciones.
Objetivo General
Implementar un Sistema de Gestión Académica que permita administrar estudiantes y cursos mediante operaciones CRUD, asignaciones y validaciones, separando la lógica de negocio en un backend optimizado y presentando una interfaz funcional en el frontend.
Objetivos Específicos
1.	Desarrollar una interfaz gráfica intuitiva y accesible que permita gestionar estudiantes y cursos de manera eficaz en el frontend.
2.	Implementar una arquitectura de backend organizada con validaciones, manejo de excepciones y conexión a bases de datos.
3.	Optimizar el flujo de información mediante la integración de estructuras de control, ciclos y manipulación de datos con vectores y matrices en la lógica de negocio.
Marco Teórico
Introducción
En el contexto actual, la gestión académica es fundamental para la eficiencia operativa de las instituciones educativas, facilitando la administración de estudiantes, cursos y asignaciones dentro de un entorno digital. Para abordar esta necesidad, se ha diseñado el Sistema de Gestión Académica (SGA), una solución que permite realizar operaciones CRUD (Crear, Leer, Actualizar y Eliminar), garantizando el correcto manejo de datos y mejorando la organización de la información.
Este sistema está basado en ASP.NET y C#, tecnologías ampliamente utilizadas en el desarrollo de software profesional. Mediante una arquitectura por capas, el SGA separa la lógica de negocio en una biblioteca de clases, asegurando una implementación modular que facilita la escalabilidad y el mantenimiento del software. Además, el sistema incorpora validaciones en la entrada de datos, manejo de excepciones, estructuras condicionales y ciclos para optimizar su funcionamiento.
Este documento expone los aspectos teóricos, metodológicos y prácticos del desarrollo del SGA, abordando su diseño, implementación y las estrategias utilizadas para garantizar su eficacia.
Marco Teórico
El Sistema de Gestión Académica se fundamenta en diversas disciplinas dentro de la ingeniería de software, asegurando que el desarrollo del sistema cumpla con estándares de calidad y eficiencia. A continuación, se presentan los principales conceptos teóricos que sustentan este proyecto:
1. Sistemas de Gestión Académica
Un Sistema de Gestión Académica (SGA) es una plataforma digital diseñada para centralizar la administración de estudiantes y cursos dentro de instituciones educativas. Estos sistemas automatizan procesos administrativos como el registro de estudiantes, asignación de cursos, gestión de notas y generación de reportes. Su objetivo es minimizar errores en la gestión de datos y mejorar la eficiencia operativa.
2. Arquitectura por Capas
La arquitectura del SGA sigue un enfoque modular, dividiendo el sistema en capas funcionales:
•	Capa de Presentación: Interfaz de usuario basada en ASP.NET Web Forms, donde los estudiantes y administradores interactúan con el sistema.
•	Capa de Lógica de Negocio: Biblioteca de clases que gestiona la validación de datos y las operaciones CRUD mediante clases como EstudianteManager y CursoManager.
•	Capa de Acceso a Datos: Uso de ADO.NET o Entity Framework para la interacción con la base de datos, asegurando consultas eficientes y almacenamiento estructurado.
3. Métodos CRUD
CRUD representa las cuatro principales operaciones sobre datos en una base de datos:
•	Crear (Create): Registro de nuevos estudiantes y cursos.
•	Leer (Read): Consulta de estudiantes y cursos con filtros y búsqueda.
•	Actualizar (Update): Modificación de datos almacenados en la base.
•	Eliminar (Delete): Eliminación de registros de estudiantes y cursos.
4. Validaciones y Manejo de Excepciones
Es crucial garantizar que los datos ingresados sean correctos y evitar errores en la ejecución del sistema. Las validaciones incluyen:
•	Formato correcto de correos electrónicos para estudiantes.
•	Restricción en nombres y códigos de cursos, evitando duplicaciones.
•	Manejo de excepciones mediante try-catch, gestionando errores en conexiones de base de datos y errores de usuario.
