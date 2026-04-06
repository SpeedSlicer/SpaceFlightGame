using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShip : Ship
{
    [Header("Player Movement Settings")]
    public InputActionReference playerMovementVector;
    public InputActionReference hyperDriveAction;

    InputAction movementAction;
    private Vector2 moveInput;

    [Header("Movement Settings")]
    public float acceleration = 10f;
    public float maxSpeed = 5f;
    public float rotationSpeed = 5f;
    public bool canGoBackwards = false;

    [Header("Hyperdrive Settings")]
    public float hyperDriveAcceleration = 50f;
    public float hyperDriveSpeed = 25f;
    public float hyperDriveRotationSpeed = 0f;
    public float hyperDriveFuelDepletion = 10f;
    public bool hyperDriveUnlocked = true;
    bool hyperDriveActive = false;

    [Header("Invincibility Settings")]
    public float invincibilityCooldown = 2f;
    bool currentlyInvincible = false;
    public float invincibilityAnimationTime = 1f;
    public GameObject invincibilityEffectObject;
    public float invincibilityTransformExtra = 0.5f;

    [Header("Fuel")]
    public float maxFuel = 5000;
    public float currentFuel = 0;

    [Header("Thruster Particles")]
    public ParticleSystem leftThruster;
    public ParticleSystem rightThruster;
    public float particleSmoothSpeed = 5f;
    public float thresholdWarning = 20;
    public float thresholdWait = 5;
    float leftSmooth = 0f;
    float rightSmooth = 0f;
    float leftTarget = 0f;
    float rightTarget = 0f;

    [Header("Other Settings")]
    [SerializeField]
    GameObject packageObject;
    Animator animator;

    bool animationOut = false;
    Vector2 currentScaleInvincibility;

    [SerializeField]
    AlertManager alertManager;
    bool canWarn = true;

    public bool freezePlayer = false;

    void Start()
    {
        base.Start();
        SetPackageEnabled(false);
        animator = gameObject.GetComponent<Animator>();
        currentScaleInvincibility = invincibilityEffectObject.transform.localScale;
        currentFuel = maxFuel;
    }

    void OnEnable()
    {
        playerMovementVector.action.Enable();
        movementAction = playerMovementVector.action;
        hyperDriveAction.action.Enable();
    }

    void OnDisable()
    {
        playerMovementVector.action.Disable();
        hyperDriveAction.action.Disable();
    }

    void Update()
    {
        if (currentlyInvincible)
        {
            if (!animationOut)
            {
                invincibilityEffectObject.LeanScale(
                    new Vector2(
                        currentScaleInvincibility.x + invincibilityTransformExtra,
                        currentScaleInvincibility.y + invincibilityTransformExtra
                    ),
                    invincibilityAnimationTime / 2
                );

                animationOut = true;
                StartCoroutine(InvincibilityAnimationDown());
            }
        }
        if (currentFuel / maxFuel < thresholdWarning / 100f && canWarn)
        {
            alertManager.SendAlert(
                thresholdWait,
                "Low Fuel",
                "Your fuel is running low. Find a fuel station to refuel.",
                AlertManager.AlertType.Warning
            );
            canWarn = false;
        }
        if (currentFuel / maxFuel >= thresholdWarning / 100f && !canWarn)
        {
            canWarn = true;
        }
    }

    void FixedUpdate()
    {
        if (freezePlayer)
        {
            moveInput = Vector2.zero;
        }
        else
        {
            moveInput = movementAction.ReadValue<Vector2>();
        }
        hyperDriveActive =
            hyperDriveAction.action.IsPressed() && currentFuel > 10 && hyperDriveUnlocked;
        HandleMovement();
        HandleRotation();
        UpdateThrusters();
    }

    private void HandleMovement()
    {
        currentFuel -=
            Math.Abs(moveInput.y / 50) * (hyperDriveActive ? hyperDriveFuelDepletion : 1f);

        if ((moveInput.magnitude > 0) && (canGoBackwards || moveInput.y > 0))
        {
            rb.AddRelativeForce(
                new Vector2(
                    0,
                    moveInput.y * (hyperDriveActive ? hyperDriveAcceleration : acceleration)
                )
            );
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity =
                rb.linearVelocity.normalized * (hyperDriveActive ? hyperDriveSpeed : maxSpeed);
        }
    }

    private void HandleRotation()
    {
        if (moveInput != Vector2.zero)
        {
            rb.AddTorque(
                -moveInput.x * (hyperDriveActive ? hyperDriveRotationSpeed : rotationSpeed)
            );
        }
    }

    private void UpdateThrusters()
    {
        float forward = moveInput.y;
        float turn = moveInput.x;

        leftTarget = 0f;
        rightTarget = 0f;

        if (Mathf.Abs(forward) > 0.1f)
        {
            float power = Mathf.Abs(forward);
            leftTarget = power;
            rightTarget = power;
        }

        if (Mathf.Abs(turn) > 0.1f)
        {
            float power = Mathf.Abs(turn);

            if (turn > 0)
            {
                leftTarget = Mathf.Max(leftTarget, power);
            }
            else
            {
                rightTarget = Mathf.Max(rightTarget, power);
            }
        }

        float speedFactor = rb.linearVelocity.magnitude / maxSpeed;
        leftTarget *= speedFactor;
        rightTarget *= speedFactor;

        leftSmooth = Mathf.Lerp(leftSmooth, leftTarget, Time.fixedDeltaTime * particleSmoothSpeed);
        rightSmooth = Mathf.Lerp(
            rightSmooth,
            rightTarget,
            Time.fixedDeltaTime * particleSmoothSpeed
        );

        ApplyThruster(leftThruster, leftSmooth);
        ApplyThruster(rightThruster, rightSmooth);
    }

    private void ApplyThruster(ParticleSystem ps, float strength)
    {
        var emission = ps.emission;

        if (strength > 0.05f)
        {
            if (!ps.isPlaying)
                ps.Play();

            emission.rateOverTime = 60f * strength;
        }
        else
        {
            ps.Stop();
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

    public void Refuel(float amount)
    {
        currentFuel = Mathf.Min(currentFuel + amount, maxFuel);
    }

    public float GetFuel() => currentFuel;

    public float GetMaxFuel() => maxFuel;

    IEnumerator InvincibilityCooldown()
    {
        currentlyInvincible = true;
        yield return new WaitForSeconds(invincibilityCooldown);
        currentlyInvincible = false;
    }

    IEnumerator InvincibilityAnimationDown()
    {
        yield return new WaitForSeconds(invincibilityAnimationTime / 2);

        invincibilityEffectObject.LeanScale(
            currentScaleInvincibility,
            invincibilityAnimationTime / 2
        );

        animationOut = false;
    }

    public void SetFreezePlayer(bool freeze)
    {
        freezePlayer = freeze;
    }

    public void SetPackageEnabled(bool enabled)
    {
        packageObject.SetActive(enabled);
    }
}
