using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer_WinZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Platformer_Player player = other.GetComponent<Platformer_Player>();
        if (player != null)
        {
            player.EnterWinZone();
        }
    }
}
