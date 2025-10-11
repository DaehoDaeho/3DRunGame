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

    private CharacterController cc;
    private int laneIndex = 0;
    private float targetX = 0.0f;
    private float vy = 0.0f;
    private bool isSliding = false;
    private float slideTimer = 0.0f;

    private float defaultHeight;
    private Vector3 defaultCenter;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        defaultHeight = cc.height;
        defaultCenter = cc.center;
        targetX = 0.0f;
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
        if(hit.collider != null && hit.collider.CompareTag("Obstacle") == true)
        {
            RunnerGameManager gm = FindAnyObjectByType<RunnerGameManager>();
            if(gm != null)
            {
                gm.GameOver();
            }
        }
    }
}
