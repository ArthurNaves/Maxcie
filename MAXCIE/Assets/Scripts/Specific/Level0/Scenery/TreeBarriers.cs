using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBarriers : MonoBehaviour {
    static int BarriersDestroyed = 0;
    [SerializeField] string[] killTags;
    [SerializeField] GameObject dialogsBarriers;
    void OnParticleCollision(GameObject other)
    {
        if (CompareTags(other.tag) && BookPages.PagesCollected > BarriersDestroyed)
        {
            if (BarriersDestroyed == 0) dialogsBarriers.SetActive(false);
            BarriersDestroyed++;
            Destroy(gameObject);
        }
    }

    bool CompareTags(string other)
    {
        foreach (string tag in killTags) if (tag == other) return true;
        return false;
    }

}
