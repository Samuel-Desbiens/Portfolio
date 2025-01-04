using System.Collections;
using UnityEngine;

public class DummyEnemy : Enemy
{
    protected override IEnumerator StartAttack()
    {
        //Nope
        throw new System.NotImplementedException();
    }
}
