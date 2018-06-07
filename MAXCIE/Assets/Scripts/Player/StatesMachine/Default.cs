using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Default : IPlayerStates
{
    public void CheckInput(Player player, PlayerStates pStates, MouseTrail mousetrail, List<Vector3> mousePath, Texture2D mouseTex)
    {
        if (Input.GetMouseButtonDown(1))
        {
            //mousetrail.FollowingMouse = true;
            player.ChangeState(pStates.drawingState);
        }
    }

    public void Move(CharacterController playerController, Transform playerTrans, float movVelocity)
    {
        Vector3 mov = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        playerController.Move(mov * movVelocity * Time.deltaTime);
    }

    public void OnEnterState(Player player, List<Vector3> mousePath, Light bookLight, MeshCollider fogOfWarSpot, Texture2D mouseTex)
    {
        fogOfWarSpot.enabled = false;
        bookLight.enabled = false;
    }

    public void OnExitState(Player player, List<Vector3> mousePath, Texture2D mouseTex)
    {
    }

    public void Rotate(Transform playerTransfrm, LayerMask ground)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, ground.value))
        {
            playerTransfrm.LookAt(new Vector3(hit.point.x, playerTransfrm.position.y, hit.point.z));
        }
    }
    public void TrackMouse(List<Vector3> mousePath)
    {
        //throw new System.NotImplementedException();
    }
}
