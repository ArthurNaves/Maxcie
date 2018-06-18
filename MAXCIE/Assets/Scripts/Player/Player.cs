using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour {
    public delegate void FogOfWarOff(Collider fogOfWarCollider);
    static FogOfWarOff fogOfWarOffDel;
    public static FogOfWarOff FogOfWarOffDel
    {
        get { return fogOfWarOffDel; }
        set
        {
            fogOfWarOffDel = value;
            delegateHaSubs = true;
        }
    }
    static bool delegateHaSubs;

    public static Player Instance
    {
        get { return instance; }
    }
    static Player instance;

    public MouseAnimgController MouseAnim { get; set; }

    public bool BookInCoolDown { get; private set; }

    [SerializeField] GameObject model;
    [SerializeField] MeshCollider spotlightTrigger;
    [SerializeField] Light bookLight;
    [SerializeField] Spell[] spellBook;
    [SerializeField] MouseTrail mouseTrail;
    [SerializeField] Texture2D mouseIdle;
    [SerializeField] Texture2D mouseReadyToPaint;
    [SerializeField] Texture2D mousePainting;
    [SerializeField] Cubemap[] lightCookies; //do de menor vida para o maior;
    [SerializeField] LayerMask ground;
    [SerializeField] Light lightBeneathPlayer;

    int Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp - 1 >= 0) lightBeneathPlayer.cookie = lightCookies[hp - 1];
        }
    }
    [SerializeField] int hp;
    [SerializeField] int numberOfFlashes;
    [SerializeField] float flashSpeed;
    [SerializeField] float spellSensitivity;
    [SerializeField] float trackMousePeriod;
    [SerializeField] float movVelocity;
    [SerializeField] bool debugBook;
    [SerializeField] bool debug;

    int originalHp;

    IPlayerStates CurrentState
    {
        get { return currentState; }
        set
        {
            currentState.OnExitState(this, mousePath, mouseIdle);
            currentState = value;
            currentState.OnEnterState(this, mousePath, bookLight, spotlightTrigger, mouseReadyToPaint);
        }
    }

    IPlayerStates currentState;
    PlayerStates states;

    List<Vector3> mousePath;
    CharacterController charController;
    QuadsDrawing newSpellCast;
    DrawingGrid spellsGrid;
    Vector3 cameraOffset;

    bool flashing;
    float originalY;


    void Awake()
    {
        PatternSingletonAwake();

        originalHp = hp;
        mouseTrail.FollowingMouse = false;
        delegateHaSubs = false;
        spellsGrid = DrawingGrid.GetGrid(3);
        newSpellCast = new QuadsDrawing();
        mousePath = new List<Vector3>();
        states.PlayerStatesInitializer();
        charController = GetComponent<CharacterController>();
    }

    void Start()
    {
        flashing = false;
        if (debug) currentState = states.defaultState;
        else currentState = states.inMenuState;
        originalY = transform.position.y;
        BookInCoolDown = false;
    }

    void Update()
    {
        Rotate();
        CheckMouseInput();
        
        if (debugBook)
        {
            Debug.Log(BookPages.PagesCollected);
            debugBook = false;
        }
    }

    void FixedUpdate()
    {
        Move();
        if (Mathf.Abs(transform.position.y - originalY) >= 0.04f) transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
    }

    void CheckMouseInput()
    {
        CurrentState.CheckInput(this, states, mouseTrail, mousePath, mousePainting);
    }

    void Move()
    {
        CurrentState.Move(charController, transform, movVelocity);
    }

    void Rotate()
    {
        CurrentState.Rotate(transform, ground);
    }

    void PatternSingletonAwake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void UnlockSpell(int spellUnlocked)
    {
        spellBook[spellUnlocked].learned = true;
    }

    bool SpellEffect(int index, out float coolDownDuration)
    {
        if (spellBook[index].learned)
        {
            if (MouseAnim != null) MouseAnim.OnPlayerSpell(index);
            coolDownDuration = spellBook[index].coolDownTime;
            spellBook[index].projectiles.Play();
            BookInCoolDown = true;
            StartCoroutine(RunCoolDown(spellBook[index].coolDownTime));
            return true;
        }
        coolDownDuration = 0;
        return false;
    }

    public SaveSystem.SaveInfo GetSaveInfo()
    {
        bool[] spellsLearned = new bool[spellBook.Length];
        for (int i = 0; i < spellsLearned.Length; i++) spellsLearned[i] = spellBook[i].learned;

        return new SaveSystem.SaveInfo(transform.position, spellsLearned, Hp, UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void Collect(Collectables collected)
    {
        switch (collected.CollectableType)
        {
            case "BookPage":                
                UnlockSpell(BookPages.PagesCollected); 
                break;
        }
    }

    public bool TryCastSpell(out float coolDownDuration)
    {
        if (!BookInCoolDown)
        {
            newSpellCast = spellsGrid.ReturnMouseQuad(mousePath);
            mousePath.Clear();

            float[] percenteges = new float[spellBook.Length];
            float max;

            for (int i = 0; i < spellBook.Length; i++)
            {
                percenteges[i] = QuadsDrawing.CompareSpells(newSpellCast, spellBook[i].drawing);
            }
            max = percenteges.Max();
            //print(newSpellCast.ReturnQuadsID());
            //print(max);
            if (max >= spellSensitivity) return SpellEffect(Array.IndexOf(percenteges, max), out coolDownDuration);
        }
        coolDownDuration = 0;
        return false;
    }

    public void SendMessageColliderOff()
    {
        if(fogOfWarOffDel != null) FogOfWarOffDel(spotlightTrigger);
    }

    public void ChangeState(IPlayerStates newState)
    {
        CurrentState = newState;
    }

    public void ChangeState()
    {
        if (CurrentState == states.inMenuState || CurrentState == states.inDialogState) CurrentState = states.defaultState;
    }

    public void OnEnterDialog(DialogBoxBase currentDialog)
    {
        states.inDialogState.CurrentDialog = currentDialog;
        CurrentState = states.inDialogState;
    }

    public void OnDialogClosed()
    {
        CurrentState = states.defaultState;
        
    }

    public void TakeDamage()
    {
        if(!flashing)
        {
            Hp--;
            StartCoroutine(Flash());
            if (Hp <= 0)
            {
                StopAllCoroutines();
                gameObject.SetActive(false);
            }
        }
    }

    public void LoadPlayer(SaveSystem.SaveInfo saveInfo)
    {
        transform.position = saveInfo.playerPos;
        Hp = saveInfo.playerHp;

        for (int i = 0; i < spellBook.Length; i++)
        {
            spellBook[i].learned = saveInfo.spellsLearned[i];
        }
    }

    public void RestoreHp()
    {
        hp = originalHp;
        lightBeneathPlayer.cookie = null;
    }

    IEnumerator RunCoolDown(float spellCoolDown)
    {
        yield return new WaitForSeconds(spellCoolDown);
        BookInCoolDown = false;
    }

    IEnumerator TrackMouse()
    {
        while (true)
        {
            yield return new WaitForSeconds(trackMousePeriod);
            CurrentState.TrackMouse(mousePath);
        }
    }
    IEnumerator Flash()
    {
        flashing = true;
        for (int i = 0; i < numberOfFlashes; i++)
        {
            model.SetActive(!model.activeInHierarchy);
            yield return new WaitForSeconds(flashSpeed);
        }
        flashing = false;
    }
}

//void OnTriggerEnter(Collider other)
//{
//    //if (other.CompareTag("bookpage"))
//    //{
//    //    UnlockSpell(int.Parse(other.gameObject.name[other.gameObject.name.Length - 1].ToString()));
//    //    Destroy(other.gameObject);
//    //}
//}
