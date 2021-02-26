using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Goal");
        collision.GetComponentInChildren<Animator>().SetTrigger("Win");
        collision.GetComponent<PlayerController>().FreezeMovement = true; ;
        collision.transform.position = transform.position;
        Invoke("LoadNextLevel", 5f);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
