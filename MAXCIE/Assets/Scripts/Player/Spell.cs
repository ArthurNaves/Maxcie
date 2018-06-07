using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Spell {
    public ParticleSystem projectiles;
    public QuadsDrawing drawing;
    public float coolDownTime;
    public bool learned;
}
