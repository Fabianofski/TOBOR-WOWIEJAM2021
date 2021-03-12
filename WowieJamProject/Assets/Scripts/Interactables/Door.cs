using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

public class Door : MonoBehaviour
{
    private bool DoorIsOpen;

    private Animator animator;
    private Animator TeleportDoorAnimator;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer TeleportDoorspriteRenderer;

    [SerializeField] Transform TeleportTo;
    [SerializeField] BoolEventReferenceListener ButtonPressedEvent;
    Transform player;

    bool DoorSerialized = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        TeleportDoorAnimator = TeleportTo.GetComponentInChildren<Animator>();

        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        TeleportDoorspriteRenderer = TeleportTo.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void OpenDoor(bool isOpen)
    {
        if (!DoorSerialized)
        {
            animator.SetTrigger("OpenDoor");
            DoorSerialized = true;
        }
        DoorIsOpen = isOpen;
        animator.SetBool("DoorOpen", DoorIsOpen);
        spriteRenderer.sortingLayerName = "BehindPlayer";
        TeleportDoorspriteRenderer.sortingLayerName = "BehindPlayer";
    }

    public void EnterDoor(Transform _player)
    {
        if (!DoorIsOpen) return;
        player = _player;
        player.position = transform.position;

        animator.SetBool("DoorOpen", false);
        TeleportDoorAnimator.SetBool("DoorOpen", false);

        spriteRenderer.sortingLayerName = "BeforePlayer";
        TeleportDoorspriteRenderer.sortingLayerName = "BeforePlayer";

        player.GetComponent<PlayerController>().FreezeMovement = true;

        Invoke("Teleport", .85f);
    }

    public void Teleport()
    {
        animator.SetBool("DoorOpen", true);
        TeleportDoorAnimator.SetBool("DoorOpen", true);
        ButtonPressedEvent.Event.Raise();

        player.GetComponent<PlayerController>().FreezeMovement = false;
        player.position = TeleportTo.position;
    }

}
