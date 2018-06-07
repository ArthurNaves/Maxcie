using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locked : IPlayerStates
{
    Texture2D originalTex;    

    public void CheckInput(Player player, PlayerStates pStates, MouseTrail mousetrail, List<Vector3> mousePath, Texture2D mousetex)
    {
        float coolDownDuration;

        if (Input.GetMouseButtonUp(1))
        {
            TrackMouse(mousePath);

            if (player.TryCastSpell(out coolDownDuration)) mousetrail.FadeTrail(coolDownDuration);
            else mousetrail.FollowingMouse = false;

            player.StopCoroutine("TrackMouse");
            player.ChangeState(pStates.defaultState);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(originalTex, Vector2.zero, CursorMode.Auto);
            TrackMouse(mousePath);

            if (player.TryCastSpell(out coolDownDuration)) mousetrail.FadeTrail(coolDownDuration);
            else mousetrail.FollowingMouse = false;

            player.StopCoroutine("TrackMouse");
        }
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(mousetex, Vector2.zero, CursorMode.Auto);
            mousetrail.FollowingMouse = true;
            player.StartCoroutine("TrackMouse");
            TrackMouse(mousePath);
        }
    }

    public void Move(CharacterController playerController, Transform playerTrans, float movVelocity)
    {
    }

    public void OnEnterState(Player player, List<Vector3> mousePath, Light bookLight, MeshCollider fogOfWarSpot, Texture2D mouseTex)
    {
        fogOfWarSpot.enabled = true;
        bookLight.enabled = true;
        originalTex = mouseTex;
        Cursor.SetCursor(mouseTex, Vector2.zero, CursorMode.Auto);
    }

    public void OnExitState(Player player, List<Vector3> mousePath, Texture2D mouseTex)
    {
        
        player.SendMessageColliderOff();
        Cursor.SetCursor(mouseTex, Vector2.zero, CursorMode.Auto);
    }

    public void Rotate(Transform playerTransform, LayerMask ground)
    {
    }

    public void TrackMouse(List<Vector3> mousePath)
    {
        mousePath.Add(Input.mousePosition);
    }
}
