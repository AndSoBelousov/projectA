using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace projectAlfa.Input
{
    public interface IInputSevice 
    {
        Vector2 InputVector {  get; }

        //event Action SpacePressed;

        void Update();
    }

}
