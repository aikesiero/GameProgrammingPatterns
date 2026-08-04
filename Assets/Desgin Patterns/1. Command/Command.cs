using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CommandPattern {

    // Clase base para los comandos
    // Esta clase siempre debe tener este aspecto para ser mas general; asi que nada de constructores, parametros, etc.!!!

    public abstract class Command
    {
        
        public abstract void Execute();

        public abstract void Undo();
    }
}
