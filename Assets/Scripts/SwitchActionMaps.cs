using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class SwitchActionMaps : MonoBehaviour
{
    [SerializeField] private bool _allPlayers = true;

    public void SwitchMap(string newMap)
    {
        PlayerInput[] playerInputs = FindObjectsByType<PlayerInput>(FindObjectsSortMode.InstanceID);
        for (int i = 0; i < playerInputs.Length; i++)
        {
            PlayerInput playerInput = playerInputs[i];
            playerInput.SwitchCurrentActionMap(newMap);
            if (!_allPlayers) return;
        }
    }
}
