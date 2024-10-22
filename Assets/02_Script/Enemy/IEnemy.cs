using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public Vector2 EnemyPos();

    public abstract void Upgrade();

    public abstract void Stop(bool value);

    public abstract void Death(float minusTime);
}
