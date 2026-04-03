using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShip : Ship
{
    [Header("Player Movement Settings")]
    public InputActionReference playerMovementVector;
    InputAction movementAction;
    private Vector2 moveInput;
    [Header("Movement Settings")]
    public float acceleration = 10f;
    public float maxSpeed = 5f;
    public float rotationSpeed = 5f;
    [Header("Invincibility Settings")]
    public float invincibilityCooldown = 2f;
    bool currentlyInvincible = false;
    public float invincibilityAnimationTime = 1f;
    public GameObject invincibilityEffectObject;
    public float invincibilityTransformExtra = 0.5f;

    public float maxFuel = 5000;
    public float currentFuel = 0;
    Animator animator;

    bool animationOut = false;
    Vector2 currentScaleInvincibility;
    void Start()
    {
        base.Start();
        animator = gameObject.GetComponent<Animator>();
        currentScaleInvincibility = invincibilityEffectObject.transform.localScale;
        currentFuel = maxFuel;
    }
    void OnEnable()
    {
        playerMovementVector.action.Enable();
        movementAction = playerMovementVector.action;
    }
    void OnDisable()
    {
        playerMovementVector.action.Disable();
    }


    void Update()
    {
        if (currentlyInvincible)
        {
            if (!animationOut)
            {
                invincibilityEffectObject.LeanScale(new Vector2(currentScaleInvincibility.x + invincibilityTransformExtra, currentScaleInvincibility.y + invincibilityTransformExtra), invincibilityAnimationTime / 2);
                animationOut = true;
                StartCoroutine(InvincibilityAnimationDown());
            }
        }
    }
    void FixedUpdate()
    {
        moveInput = movementAction.ReadValue<Vector2>();
        HandleMovement();
        HandleRotation();
    }
    private void HandleMovement()
    {
        currentFuel -= Math.Abs(moveInput.y / 50);
        if (moveInput.magnitude > 0)
        {
            rb.AddRelativeForce(new Vector2(0, moveInput.y * acceleration));
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    private void HandleRotation()
    {
        if (moveInput != Vector2.zero)
        {
            rb.AddTorque(-moveInput.x * rotationSpeed);
        }
    }
    public override bool Damage(float amount, string source)
    {
        if (!currentlyInvincible)
        {
            if (base.Damage(amount, source))
            {
                StartCoroutine(InvincibilityCooldown());
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    public float GetFuel()
    {
        return currentFuel;
    }
    public float GetMaxFuel()
    {
        return maxFuel;
    }
    IEnumerator InvincibilityCooldown()
    {
        currentlyInvincible = true;
        yield return new WaitForSeconds(invincibilityCooldown);
        currentlyInvincible = false;
    }
    IEnumerator InvincibilityAnimationDown()
    {
        yield return new WaitForSeconds(invincibilityAnimationTime / 2);
        invincibilityEffectObject.LeanScale(currentScaleInvincibility, invincibilityAnimationTime / 2);
        animationOut = false;
    }

}
