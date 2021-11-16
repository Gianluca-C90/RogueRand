using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    InputHandler inputHandler;

    private void Awake()
    {
        inputHandler = new InputHandler();
    }
    private void Update()
    {
        if (inputHandler.HandleInput() != null)
        {
            Command cmd = inputHandler.HandleInput();
            cmd.Execute();
        }
    }
}
