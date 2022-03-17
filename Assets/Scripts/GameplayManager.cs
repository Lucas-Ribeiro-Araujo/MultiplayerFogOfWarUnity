using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameplayManager : NetworkBehaviour
{
    public LayerMask TerrainLayer;

    private void Start()
    {
        GameManager.instance.GameplayManager = this;
    }

    public void SpawnCharacterWithOwnerShip(string playerID)
    {
    }
}
