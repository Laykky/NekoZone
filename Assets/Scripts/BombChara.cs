using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombChara : GameEntity
{
    protected override void OnClick()
    {
        print("BOOM");

        base.OnClick();
    }
}

