using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarObjs : MonoBehaviour {
    [SerializeField] Renderer meshRendr;

    protected List<Collider> fogOfWarColliders;

    
    
    protected virtual void Awake()
    {
        meshRendr.enabled = false;
        fogOfWarColliders = new List<Collider>();
        Player.FogOfWarOffDel += OnFogOfWarOff;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FogOfWar"))
        {
            if (fogOfWarColliders.Count == 0) meshRendr.enabled = true;
            fogOfWarColliders.Add(other);
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FogOfWar"))
        {
            fogOfWarColliders.Remove(other);
            if (fogOfWarColliders.Count == 0 && gameObject) meshRendr.enabled = false;
        }
    }

    protected void OnFogOfWarOff(Collider fogOfWarCollider)
    {
        if (fogOfWarColliders.Contains(fogOfWarCollider)) OnTriggerExit(fogOfWarCollider);
    }
}
