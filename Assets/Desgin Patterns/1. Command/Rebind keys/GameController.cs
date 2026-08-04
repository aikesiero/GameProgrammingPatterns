using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CommandPattern.RebindKeys
{
    // Ejemplo del patron Command para reasignar teclas, tomado del libro "Game Programming Patterns"
    // Incluye tambien sistemas de deshacer, rehacer y repeticion (replay)
    public class GameController : MonoBehaviour
    {
        public MoveObject objectThatMoves;

        // Las teclas que estaran conectadas a los comandos
        private Command buttonW;
        private Command buttonA;
        private Command buttonS;
        private Command buttonD;

        //Almacena aqui los comandos para facilitar las operaciones de deshacer, rehacer y reproducir
        //El libro utiliza una lista y un indice
        //private List<Command> oldCommands = new List<Command>();
        //Se empieza en -1 porque inicialmente no se ha añadido ningun comando
        //private int currentCommandIndex = -1;
        //Pero creo que es mas sencillo utilizar dos pilas (Stacks)
        //Al reproducir, convertimos la pila de deshacer en un array
        private Stack<Command> undoCommands = new Stack<Command>();
        private Stack<Command> redoCommands = new Stack<Command>();

        private bool isReplaying = false;

        // Para hacerlo trabajar neceistamos saber donde empieza el objeto
        private Vector3 startPos;

        // El tiempo entre la ejecucion de cada comando durante la reproduccion, para que podamos ver que esta sucediendo.
        private const float REPLAY_PAUSE_TIMER = 0.5f;

        void Start(){

            // Asignar las teclas a los comandos predeterminados
            buttonW = new MoveForwardCommand(objectThatMoves);
            buttonA = new TurnLeftCommand(objectThatMoves);
            buttonS = new MoveBackCommand(objectThatMoves);
            buttonD = new TurnRightCommand(objectThatMoves);

            startPos = objectThatMoves.transform.position;
        }


        void Update(){

            // Podemos comprobar si hay entrada mientras reproducimos
            if (isReplaying)
            {
                return;
            }



            if (Input.GetKeyDown(KeyCode.W))
            {
                ExecuteNewCommand(buttonW);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                ExecuteNewCommand(buttonA);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                ExecuteNewCommand(buttonS);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                ExecuteNewCommand(buttonD);
            }
            // Deshacer con «u» (Ctrl + Z a veces interfiere con el sistema de deshacer del editor).
            else if (Input.GetKeyDown(KeyCode.U))
            {
                if (undoCommands.Count == 0)
                {
                    Debug.Log("Can't undo because we are back where we started");
                }
                else
                {
                    Command lastCommand = undoCommands.Pop();

                    lastCommand.Undo();

                    // Agregamos esto a Redo si queremos que rehaga el deshacer
                    redoCommands.Push(lastCommand);
                }
            }
            // Rehacer con R
            else if (Input.GetKeyDown(KeyCode.R))
            {
                if (redoCommands.Count == 0)
                {
                    Debug.Log("Can't redo because we are at the end");
                }
                else
                {
                    Command nextCommand = redoCommands.Pop();

                    nextCommand.Execute();

                    // Agregamos esto si queremos que de deshaga el Redo
                    undoCommands.Push(nextCommand);
                }
            }

            // Reasigna las teclas simplemente intercambiando los botones A y D.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 'ref' es importante, o de lo contrario las claves no se intercambiaran.
                SwapKeys(ref buttonA, ref buttonD);
            }


            //Start replay
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(Replay());

                isReplaying = true;
            }
        }


        //Replay
        private IEnumerator Replay()
        {
            // Mueve el objeto de vuelta a donde empezo.
            objectThatMoves.transform.position = startPos;

            // Haz una pausa para que podamos ver que ha comenzado en la posicion inicial.
            yield return new WaitForSeconds(REPLAY_PAUSE_TIMER);

            // Convertir la pila de deshacer en un array
            Command[] oldCommands = undoCommands.ToArray();
            
            // El array está invertido por lo que iteramos desde el final
            for (int i = oldCommands.Length - 1; i >= 0; i--)
            {
                Command currentCommand = oldCommands[i];

                currentCommand.Execute();

                yield return new WaitForSeconds(REPLAY_PAUSE_TIMER);
            }

            isReplaying = false;
        }


        // Ejecutara el comando y realizara operaciones en la lista para hacer que funcionen los sistemas de reproduccion, deshacer y rehacer.
        private void ExecuteNewCommand(Command commandButton)
        {
            commandButton.Execute();

            // Agregamos el nuevo comando a la ultima posicion de la lista.
            undoCommands.Push(commandButton);

            // Eliminar todos los comandos de rehacer, ya que la accion de rehacer no esta definida cuando se ha agregado un nuevo comando.
            redoCommands.Clear();
        }

        // Intercambia el puntero a los dos comandos
        private void SwapKeys(ref Command key1, ref Command key2)
        {
            Command temp = key1;

            key1 = key2;
            
            key2 = temp;
        }
    }
}

