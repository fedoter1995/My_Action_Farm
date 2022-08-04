using System.Collections.Generic;
using UnityEngine;

public class UpgradeZone : MonoBehaviour
{
    [SerializeField]
    private Weapon _weaponNew;

    private Player player;
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            this.player = player;
            player.Equipments.UpgradeWeapon(_weaponNew);
            Debug.Log(player.Equipments.CurrentWeapon);
        }


    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player == this.player)
            this.player = null;
    }
}
