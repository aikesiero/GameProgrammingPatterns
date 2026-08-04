# Command
## ¿Cuándo es útil?

- Para reasignar teclas. Un ejemplo está disponible en la sección de código.

- Para crear un sistema de repetición. Al jugar, se almacena en una estructura de datos qué botón se pulsó en cada actualización. Para repetir lo sucedido, basta con iterar sobre cada comando durante la ejecución del juego. Un ejemplo está disponible en la sección de código.

- Para crear un sistema de deshacer y rehacer. Es similar al sistema de repetición, pero cada comando incluye un método llamado Undo() que permite realizar la acción opuesta. Un ejemplo está disponible en la sección de código.

- Para encapsular comportamientos y acciones de IA. Cada comportamiento de IA se puede representar como un comando, lo que facilita el control y la actualización de las acciones de IA durante el juego.

- Para definir la secuencia de acciones que se ejecutarán durante eventos o escenas cinemáticas en los juegos.

- Para gestionar y aplicar diferentes habilidades, potenciadores o efectos durante el juego.

- Para gestionar el manejo de eventos encapsulando acciones específicas como comandos y ejecutándolas cuando ocurren los eventos correspondientes.

- Para simplificar la comunicación en red en juegos multijugador. Los comandos del juego se pueden serializar y enviar por red para sincronizar las acciones entre los diferentes jugadores.


## Patrones relacionados:

- **Subclass Sandbox**. Es posible que se generen muchas subclases de comandos. Para facilitar la gestión del código, se pueden definir métodos de alto nivel en la clase padre. [Subclass Sandbox](./11-subclass-sandbox.md)



- **Memento**. Con este patrón también se puede volver a un estado anterior.
