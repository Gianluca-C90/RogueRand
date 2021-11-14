using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState
{
    public IEnumerator Idle()
    {
        yield break;
    }
    public IEnumerator Attack()
    {
        yield break;
    }
    public IEnumerator Block()
    {
        yield break;
    }
    public IEnumerator Dead()
    {
        yield break;
    }
}
