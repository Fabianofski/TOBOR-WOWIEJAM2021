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

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (!_collision.CompareTag("Player")) return;

        if (MoveWithHazard)
        {
            _collision.transform.parent = transform;
            if(setPos)
                _collision.transform.localPosition = new Vector2(0, _collision.transform.localPosition.y);
            Rigidbody2D _rb2d = _collision.GetComponent<Rigidbody2D>();

            _rb2d.constraints = (freezeY ? RigidbodyConstraints2D.FreezePositionY:0) 
                             | (freezeX ? RigidbodyConstraints2D.FreezePositionX:0) 
                             | RigidbodyConstraints2D.FreezeRotation;
        }

        PlayerController _playerController = _collision.GetComponent<PlayerController>();
        if (_playerController)
            _playerController.Die();
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (!_collision.CompareTag("Player")) return;

        if (MoveWithHazard && _collision.transform.parent == transform)
        {
            _collision.transform.parent = null;
            _collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

}
