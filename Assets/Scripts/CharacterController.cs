using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Unity.Netcode;
public class CharacterController : NetworkBehaviour
{
    private NavMeshAgent navAgent;

    private Vector3 lastPosition;
    private NetworkVariable<Vector3> desiredPosition = new NetworkVariable<Vector3>();

    [Header("Inputs")]
    [SerializeField] InputAction commandCharacter;

    private void Update()
    {
        if (IsServer)
        {
            desiredPosition.Value = transform.position;
        }
        else if (IsClient)
        {
            transform.position = Vector3.Lerp(lastPosition, desiredPosition.Value, 0.5f);
        }
    }

    private void OnEnable()
    {
        //if (IsOwner)
        //{
            commandCharacter.Enable();
            commandCharacter.performed += MoveCharacter;
        //}
    }

    private void OnDisable()
    {
        commandCharacter.Disable();
    }

    private void Start()
    {
        navAgent = this.GetComponent<NavMeshAgent>();

        if (IsClient)
        {
            desiredPosition.OnValueChanged += UpdateClientPosition;
        }
    }

    private void UpdateClientPosition(Vector3 oldPos, Vector3 newPos)
    {
        lastPosition = oldPos;
    }

    public void MoveCharacter(InputAction.CallbackContext context)
    {
        if (!context.action.triggered) { return; }

        Debug.Log("mouse Clicked");

        if (IsOwner)
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out hit, Mathf.Infinity, GameManager.instance.GameplayManager.TerrainLayer);

            MoveCharacterServerRpc(hit.point);
        }


    }

    [ServerRpc]
    private void MoveCharacterServerRpc(Vector3 pos)
    {
        navAgent.SetDestination(pos);
    }
}
