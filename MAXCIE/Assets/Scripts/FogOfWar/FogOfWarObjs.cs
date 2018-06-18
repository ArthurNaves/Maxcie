using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarObjs : MonoBehaviour {
    [SerializeField] Renderer meshRendr;
    [SerializeField] Renderer[] meshRenders;

    protected List<Collider> fogOfWarColliders;



    protected virtual void Awake()
    {
        if(meshRenders != null) foreach (Renderer render in meshRenders) render.enabled = false;
        else meshRendr.enabled = false;
        fogOfWarColliders = new List<Collider>();
        Player.FogOfWarOffDel += OnFogOfWarOff;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FogOfWar"))
        {
            if (meshRenders != null)
            {
                if (fogOfWarColliders.Count == 0) foreach (Renderer render in meshRenders) render.enabled = true;
            }
            else if (fogOfWarColliders.Count == 0) meshRendr.enabled = true;
            fogOfWarColliders.Add(other);
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FogOfWar"))
        {
            fogOfWarColliders.Remove(other);
            if (meshRenders != null)
            {
                if (fogOfWarColliders.Count == 0 && gameObject) foreach (Renderer render in meshRenders) render.enabled = false;
            }
            else if (fogOfWarColliders.Count == 0 && gameObject) meshRendr.enabled = false;
        }
    }

    protected virtual void OnFogOfWarOff(Collider fogOfWarCollider)
    {
        if (fogOfWarColliders.Contains(fogOfWarCollider)) OnTriggerExit(fogOfWarCollider);
    }
}
