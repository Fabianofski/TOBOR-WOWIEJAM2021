using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    [SerializeField] GameObject EndScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Goal");
        collision.GetComponentInChildren<Animator>().SetTrigger("Win");
        collision.GetComponent<PlayerController>().FreezeMovement = true; ;
        collision.transform.position = transform.position;
        Invoke("ActivateEndScreen", 1f);
    }

    void ActivateEndScreen()
    {
        EndScreen.SetActive(true);
    }
}
