using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool DoorIsOpen;
    private Animator animator;

    [SerializeField] Transform TeleportTo;
    Animator TeleportDoorAnimator;
    Transform player;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        TeleportDoorAnimator = TeleportTo.GetComponentInChildren<Animator>();
    }

    public void OpenDoor(bool isOpen)
    {
        DoorIsOpen = isOpen;
        if (DoorIsOpen)
            animator.SetTrigger("OpenDoor");
        else
            animator.SetTrigger("CloseDoorBehind");
    }

    public void EnterDoor(Transform _player)
    {
        if (!DoorIsOpen) return;
        player = _player;
        player.position = transform.position;

        animator.SetTrigger("CloseDoor");
        animator.SetTrigger("OpenDoor");

        TeleportDoorAnimator.SetTrigger("CloseDoor");
        TeleportDoorAnimator.SetTrigger("OpenDoor");

        player.GetComponent<PlayerController>().FreezeMovement = true;

        Invoke("Teleport", .8f);
    }

    public void Teleport()
    {
        player.GetComponent<PlayerController>().FreezeMovement = false;
        player.position = TeleportTo.position;
    }
}
