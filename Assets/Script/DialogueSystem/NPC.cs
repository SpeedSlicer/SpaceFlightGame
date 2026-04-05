using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{
    [SerializeField]
    GameManager gm;
    [SerializeField]
    InputActionReference interactAction;
    [SerializeField]
    public UnityAction unityAction;
    bool isEnabled = false;
    bool isTrigger = false;
    int npcID = 0;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gm.SetInteractActive(true);
            isEnabled = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gm.SetInteractActive(false);
            isEnabled = false;
        }
    }
    void Update()
    {
        if (isEnabled && interactAction.action.triggered && !isTrigger)
        {
            isTrigger = true;

        }
        else if (isEnabled && !interactAction.action.triggered && isTrigger)
        {
            isTrigger = false;
        }
    }
}
