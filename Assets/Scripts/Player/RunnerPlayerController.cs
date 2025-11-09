using System;
using System.Collections;
using UnityEngine;

public class RunnerPlayerController : MonoBehaviour
{
    [Header("Forward Run")]
    public float forwardSpeed = 8.0f;
    public float laneOffset = 2.0f;
    public float laneChangeSpeed = 12.0f;

    [Header("Jump / Gravity")]
    public float jumpSpeed = 10.0f;
    public float gravity = 20.0f;

    [Header("Slide")]
    public float slideDuration = 0.6f;
    public float slideHeight = 1.2f;
    public float slideCenterY = 0.6f;

    [Header("Reference")]    
    public Animator anim;
    public Transform modelRoot;

    [Header("BoostPad")]
    public RunnerSpeedBooster booster;
    public float boostMultiplier = 3.0f;    // 속도 배율(1.2~1.6 권장)
    public float slowMultiplier = -3.0f;    // 속도 배율(1.2~1.6 권장)
    public float duration = 0.5f;   // 지속 시간(초)

    [Header("HP")]
    public int maxHp = 3;

    private CharacterController cc;
    private int laneIndex = 0;
    private float targetX = 0.0f;
    private float vy = 0.0f;
    private bool isSliding = false;
    private float slideTimer = 0.0f;

    private float defaultHeight;
    private Vector3 defaultCenter;

    private int currentHp;
    
    public event Action<int, int> OnChangedHP;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        defaultHeight = cc.height;
        defaultCenter = cc.center;
        targetX = 0.0f;
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) == true || Input.GetKeyDown(KeyCode.LeftArrow) == true)
        {
            laneIndex = laneIndex - 1;
            if(laneIndex < -1)
            {
                laneIndex = -1;
            }
        }

        if (Input.GetKeyDown(KeyCode.D) == true || Input.GetKeyDown(KeyCode.RightArrow) == true)
        {
            laneIndex = laneIndex + 1;
            if (laneIndex > 1)
            {
                laneIndex = 1;
            }
        }

        targetX = laneIndex * laneOffset;

        bool grounded = cc.isGrounded;

        if(grounded == true)
        {
            vy = -0.5f;
        }
        else
        {
            vy = vy - (gravity * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.Space) == true && grounded == true && isSliding == false)
        {
            vy = jumpSpeed;
            TriggerJumpAnim();
        }

        if(Input.GetKeyDown(KeyCode.LeftControl) == true && grounded == true && isSliding == false)
        {
            StartSlide();
        }

        if(isSliding == true)
        {
            slideTimer = slideTimer - Time.deltaTime;
            if(slideTimer <= 0.0f)
            {
                EndSlide();
            }
        }

        float curX = transform.position.x;
        float newX = Mathf.MoveTowards(curX, targetX, laneChangeSpeed * Time.deltaTime);

        float moveZ = forwardSpeed * Time.deltaTime;
        Vector3 delta = new Vector3(newX - curX, vy * Time.deltaTime, moveZ);

        cc.Move(delta);

        if(anim != null)
        {
            anim.SetFloat("Speed", forwardSpeed);
            anim.SetBool("IsGrounded", grounded);
            anim.SetBool("IsSliding", isSliding);
        }
    }

    void TriggerJumpAnim()
    {
        if(anim != null)
        {
            anim.SetTrigger("Jump");
        }
    }

    void StartSlide()
    {
        isSliding = true;
        slideTimer = slideDuration;

        cc.height = slideHeight;
        cc.center = new Vector3(cc.center.x, slideCenterY, cc.center.z);
    }

    void EndSlide()
    {
        isSliding = false;

        cc.height = defaultHeight;
        cc.center = defaultCenter;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider != null && hit.collider.CompareTag("BoostPad") == true)
        {
            if (booster != null)
            {
                Debug.Log("Boost!!");
                booster.TriggerBoost(boostMultiplier, duration);

                if (AudioSimple.Instance != null)
                {
                    AudioSimple.Instance.PlayBoost();
                }
            }
        }

        if (hit.collider != null && hit.collider.CompareTag("SlowPad") == true)
        {
            if (booster != null)
            {
                Debug.Log("Slow!!");
                booster.TriggerBoost(slowMultiplier, duration);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHp -= amount;
        if(OnChangedHP != null)
        {
            OnChangedHP.Invoke(currentHp, maxHp);
        }

        if(currentHp <= 0)
        {
            RunnerGameManager gm = FindAnyObjectByType<RunnerGameManager>();
            if (gm != null)
            {
                //gm.GameOver();
                StartCoroutine(ProcessDead(gm));
            }

            if (AudioSimple.Instance != null)
            {
                AudioSimple.Instance.StopAllSounds();
            }
        }
        else
        {
            anim.SetTrigger("Hit");
        }
    }

    IEnumerator ProcessDead(RunnerGameManager gm)
    {
        gm.SetOver(true);
        Time.timeScale = 0.0f;

        if (anim != null)
        {
            anim.updateMode = AnimatorUpdateMode.UnscaledTime;
            anim.SetTrigger("Dead");
        }

        yield return new WaitForSecondsRealtime(3.0f);

        if(anim != null)
        {
            anim.updateMode = AnimatorUpdateMode.Normal;
        }

        gm.GameOver();
    }

    public int GetCurrentHp()
    {
        return currentHp;
    }

    public int GetMaxHp()
    {
        return maxHp;
    }
}
