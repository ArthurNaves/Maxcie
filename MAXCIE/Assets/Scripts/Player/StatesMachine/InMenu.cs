﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InMenu : IPlayerStates
{
    public void CheckInput(Player player, PlayerStates pStates, MouseTrail mousetrail, List<Vector3> mousePath, Texture2D mouseTex)
    {
    }

    public void Move(CharacterController playerController, Transform playerTrans, float movVelocity)
    {
    }

    public void OnEnterState(Player player, List<Vector3> mousePath, Light bookLight, MeshCollider fogOfWarSpot, Texture2D mouseTex)
    {
    }

    public void OnExitState(Player player, List<Vector3> mousePath, Texture2D mouseTex)
    {
        player.ChangeAnim(PlayerAnimation.Waking);
    }

    public void Rotate(Transform playerTransfrm, LayerMask ground)
    {
    }

    public void TrackMouse(List<Vector3> mousePath)
    {
    }
}
