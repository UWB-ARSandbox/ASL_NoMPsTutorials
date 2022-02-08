using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Platformer_Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Platformer_Player player = other.GetComponent<Platformer_Player>();
        if (player != null)
        {
            player.CollectCoin(gameObject);
        }
    }
}
