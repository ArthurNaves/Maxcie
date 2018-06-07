using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerStates {
    void Move(CharacterController playerController, Transform playerTrans, float movVelocity);
    void Rotate(Transform playerTransfrm, LayerMask ground);
    void TrackMouse(List<Vector3> mousePath);
    void OnEnterState(Player player, List<Vector3> mousePath, Light bookLight, MeshCollider fogOfWarSpot, Texture2D mouseTex);
    void CheckInput(Player player, PlayerStates pStates, MouseTrail mousetrail, List<Vector3> mousePath, Texture2D mouseTex);
    void OnExitState(Player player, List<Vector3> mousePath, Texture2D mouseTex);
}
