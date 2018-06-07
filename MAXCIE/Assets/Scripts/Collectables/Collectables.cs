using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour {
    public string CollectableType { get; protected set; }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Player.Instance.Collect(this);
    }
}
