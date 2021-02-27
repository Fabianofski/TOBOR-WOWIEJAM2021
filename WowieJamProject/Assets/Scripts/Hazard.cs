using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{

    [SerializeField] bool MoveWithHazard;
    [SerializeField] bool setPos;
    [SerializeField] bool freezeX;
    [SerializeField] bool freezeY;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (MoveWithHazard)
        {
            collision.transform.parent = transform;
            if(setPos)
                collision.transform.localPosition = new Vector2(0, collision.transform.localPosition.y);
            Rigidbody2D rb2d = collision.GetComponent<Rigidbody2D>();
            if (freezeY && freezeX)
                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            else if (freezeY)
                rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            else if (freezeX)
                rb2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            else
                rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if(collision.GetComponent<PlayerController>())
            collision.GetComponent<PlayerController>().Die();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (MoveWithHazard && collision.transform.parent == transform)
        {
            collision.transform.parent = null;
            collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

}
