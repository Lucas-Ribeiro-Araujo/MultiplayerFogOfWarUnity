using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Unity.Netcode;
public class PlayerController : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private InputAction commandCharacter;

    UnitController[] SelectedUnits;

    private void Update()
    {

    }
    private void OnEnable()
    {
        commandCharacter.Enable();
        commandCharacter.performed += AttackMoveSelectedUnits;
    }

    private void OnDisable()
    {
        commandCharacter.Disable();
    }

    private void Start()
    {

    }

    private void UpdateClientPosition(Vector3 oldPos, Vector3 newPos)
    {

    }

    public void AttackMoveSelectedUnits(InputAction.CallbackContext context)
    {
        if (!context.action.triggered) { return; }

        Debug.Log("mouse Clicked");

        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out hit, Mathf.Infinity, GameManager.instance.GameplayManager.TerrainLayer);

        foreach(UnitController u in SelectedUnits)
        {
            u.SetTarget(hit.point);
        }
    }
}
