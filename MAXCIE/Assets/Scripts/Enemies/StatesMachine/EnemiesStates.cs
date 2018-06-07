using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemiesStates {
    public Patrolling patrolling;
    public Chasing chasing;
    public Asleep asleep;
    public PlayerInDialog inDialog;

    public void EnemiesStatesInitializer()
    {
        if (patrolling == null && chasing == null && asleep == null)
        {
            patrolling = new Patrolling();
            chasing = new Chasing();
            asleep = new Asleep();
            inDialog = new PlayerInDialog();
        }
    }
}
