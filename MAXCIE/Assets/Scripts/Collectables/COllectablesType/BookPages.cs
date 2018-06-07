using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPages : Collectables {
    public static int PagesCollected { get; private set; }

    [SerializeField] LightPostsManager lights;
    void Awake()
    {
        CollectableType = "BookPage";
    }
    protected override void OnTriggerEnter(Collider other)
    {
    }
    public void CollectPage(Collider other)
    {
        base.OnTriggerEnter(other);
        if(lights != null) lights.OnPlayerCollectPage();
        PagesCollected++;
        print(PagesCollected);
        Destroy(gameObject);
    }
}
