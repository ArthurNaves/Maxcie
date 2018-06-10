using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossStateMachine
{
    /*
     * O boss aparece primeiro idle, 
     * acontece uma série de eventos
     * entra para o modo moving, 
     * fica dibrando o jogador até atacar ela,
     * 2 estados principais:
     * invizivel
     * rapidao
     */

    public abstract class BossInvizible 
    {

    }
    public class BossAttacking : IBossStates
    {

    }

    public class BossIdle : IBossStates
    {
        
    }

    public class BossMoving : IBossStates
    {
        public class Invizible
        {

        }
    }

    public class BossTakingDamage : IBossStates
    {

    }

    public struct BossStates
    {
        
    }

    public interface IBossStates
    {

    }

}
