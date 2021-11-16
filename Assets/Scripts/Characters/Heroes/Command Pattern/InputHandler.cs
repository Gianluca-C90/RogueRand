using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    Command jumpCommand;

    private void Update()
    {
        HandleInput();
    }

    public Command HandleInput()
    {
        if (Input.GetButton("Jump"))
        {
            jumpCommand = new JumpCommand();
            return jumpCommand;
        }
        else
            return null;
    }
}
