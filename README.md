Título del Proyecto: Jako Knight

Desarrollado por EquipoMC

datos de los desarrolladores 
 *Ramos Luca Joaquin LU: TUV000403

 *Nayla Aixa Leiva LU: TUV000028

 *Robert Fuentes Facundo Guillermo LU: TU000808

 *Gutierrez Gianella Alexandra LU: TUV000800

 *Miranda Sara Barbara LU: TUV000749


Roguelike de Acción 2D (Inspirado en "Soul Knight")
Proyecto desarrollado para Programación de Videojuegos I. 
Este juego es un roguelike de acción 2D construido íntegramente utilizando Unity, estructurado bajo un diseño modular y colaborativo entre 5 integrantes.

**• Descripción del Juego: Prototipo de juego estilo Roguelike 2D.**

 
**• Guía de Controles: 
•Moverse: WASD
•Disparar: click derecho
•Direccion del disparo: mouse
**

**• Detalle de Opcionales Elegidos: 
•Tienda de objetos 
•Inventario **


Integrantes y Módulos del Equipo

Integrante 1 (Jugador) — Movimiento, Combate y Estadísticas del Jugador
  * Rama de GitHub: "facundo-robert-1"
  * Nombre: Facundo Robert
  * ¿Cómo lo realizó?:
Desarrollé el personaje principal utilizando una primitiva 2D equipada con los componentes físicos Rigidbody2D y Collider2D para interactuar con el escenario. Programé el sistema de movimiento cenital mediante WASD y orienté la rotación del personaje hacia la posición del cursor del ratón. Implementé la mecánica de disparo con el clic izquierdo, gestionando la cadencia mediante un sistema de cooldown y creando un proyectil dinámico con velocidad y daño configurables que se destruye al impactar o salir de pantalla. Además, integré el sistema de vida y la detección de daño, configurando la notificación de muerte hacia el GameManager y conectando las estadísticas de combate directamente a PlayerStatsSO para evitar valores constantes en el código. Finalmente, empaqueté todo en prefabs funcionales y validé el comportamiento completo en una escena sandbox propia.

Integrante 2 (Enemigos) — Inteligencia Artificial, IA de persecución/distancia y combate enemigo
  * Rama de GitHub: "RamosLuca"
  * Nombre: Ramos Luca Joaquin
  * ¿Cómo lo realizó?:
Implementé un diseño modular basado en un script de comportamiento general (EnemyAI2D.cs) asistido por datos centralizados en ScriptableObjects (EnemyDataSO.cs). Creé dos tipos de enemigos visualmente diferenciables mediante primitivas de Unity (un perseguidor cuerpo a cuerpo y un atacante a distancia) y configuré sus componentes físicos para gestionar las colisiones. Programé la detección automática del jugador por proximidad para activar los estados de persecución en vista cenital y establecí los rangos de ataque y los cooldowns para regular la frecuencia de daño y el lanzamiento de proyectiles. Desarrollé el script EnemyHealth.cs para administrar la recepción de daño y la destrucción del enemigo, emitiendo notificaciones de muerte y recompensas de forma desacoplada. Todo esto lo probé y validé exhaustivamente en su respectiva escena aislada.

* Integrante 3 (Niveles y Mundo) Objetos del mundo, Pickups y Persistencia 
  * Rama de GitHub: gianellaGutierrez
  * Nombre: Gianella Alexandra Gutierrez 
  * ¿Cómo lo realizó?:
Construí y diseñé por completo las escenas Level1 y Level2, configurando habitaciones, paredes, obstáculos, puntos de aparición y ubicaciones estratégicas para los elementos del juego. Implementé la transición de salida del nivel 1 hacia el nivel 2 utilizando GameManager. Organice las salas por medio de una ronda donde si el jugador vence a todos los enemigos generados por spawners se le otorgará una cantidad de puntos y podrá avanzar hacia otras salas.

* Integrante 4 (UI y GameManager) Menú principal, HUD global, Sistema de Pausa, Victoria/Derrota y Control de Flujo 
  * Rama de GitHub: mirandaSara
  * Nombre: Miranda Sara Bárbara 
  * ¿Cómo lo realizó?:
Desarrollé la escena independiente del menú principal (MainMenu) integrando la identidad visual y los botones de navegación para iniciar la partida, ver créditos o salir del juego. Implementé el flujo de control central mediante GameManager.cs y el GameState, gestionando la comunicación con los eventos del jugador, enemigos y niveles. Construí el HUD dinámico con TextMeshPro y los paneles de pausa, victoria y derrota con botones funcionales para reiniciar el juego o regresar al menú. Además, configuré el control de escala de tiempo en el menú de pausa para congelar y reanudar la acción fluidamente sin alterar las escenas de juego.

* Integrante 5 (Datos y Economía) — ScriptableObjects, Tienda de mejoras e Inventario
  * Rama de GitHub: 
  * Nombre: Leiva Nayla Aixa
  * ¿Cómo lo realizó?:
Diseñé y creé la arquitectura base de datos del proyecto mediante los ScriptableObjects, generando los assets iniciales de configuración (Player_Default.asset, variantes de enemigos e ítems) para alimentar dinámicamente los sistemas del juego desde el Inspector. Implementé la tienda de mejoras entre niveles, permitiendo visualizar los artículos disponibles, descontar saldo de monedas y aplicar las mejoras de forma inmediata a las estadísticas de la partida. Desarrollé el sistema de inventario interactivo accesible, estructurando los slots de almacenamiento y la visualización de descripciones de objetos, y expuse las interfaces públicas necesarias para registrar los ítems recogidos en el mundo sin interferir con las lógicas externas.

Flujo de Trabajo y Normas de Colaboración

1. Ramas independientes: Cada integrante trabaja exclusivamente en su propia rama creada a partir de un "main".
2. Commits constantes: Se realizan commits pequeños, frecuentes y descriptivos que reflejan los avances reales de cada módulo.
3. Integración mediante PR: Los cambios se integran a "main" a través de Pull Requests revisados, asegurando que no existan errores de compilación previos a la fusión.
