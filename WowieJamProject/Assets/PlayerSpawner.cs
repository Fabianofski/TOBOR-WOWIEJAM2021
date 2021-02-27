using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSpawner : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera cinemachineCam;
    [SerializeField] GameObject PlayerPrefab;

    public void SpawnPlayer()
    {
        GameObject player = Instantiate(PlayerPrefab, transform.position, Quaternion.identity);
        cinemachineCam.Follow = player.transform;
    }

}
