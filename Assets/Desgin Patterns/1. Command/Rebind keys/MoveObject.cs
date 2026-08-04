using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CommandPattern.RebindKeys
{
    // Esta clase controla todosl los metodos que mueven el objeto al que esta adjunto

    public class MoveObject : MonoBehaviour
    {
        // Velocidad del objeto
        private const float MOVE_SPEED_DISTANCE = 1f;

        // Estos metodos ejecutar su propio comando

        public void MoveForward(){
            Move(Vector3.forward);
        }

        public void MoveBack(){
            Move(Vector3.back);
        }

        public void TurnLeft(){
            Move(Vector3.left);
        }

        public void TurnRight(){
            Move(Vector3.right);
        }

        // Metodo de ayuda para hacerlo de forma mas general
        private void Move (Vector3 dir)
        {
            transform.Translate(dir * MOVE_SPEED_DISTANCE);
        }
    }
}

