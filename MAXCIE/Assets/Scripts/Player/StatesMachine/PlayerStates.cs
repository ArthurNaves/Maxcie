using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerStates {
    public Default defaultState;
    public Locked drawingState;
    public InDialog inDialogState;
    public InMenu inMenuState;

   public void PlayerStatesInitializer()
   {
        if (drawingState == null && defaultState == null && inDialogState == null && inMenuState == null)
        {
            defaultState = new Default();
            drawingState = new Locked();
            inDialogState = new InDialog();
            inMenuState = new InMenu();
        }
        else Debug.LogError("inicializando struct ja inicializado");
   }
}
