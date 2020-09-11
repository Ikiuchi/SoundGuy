using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private Rigidbody rigidBody;
    private Transform mainCamera;

    private Animator animator;

    public float moveSpeed = 1f;
    public float moveSprintSpeed = 10f;
    [Range(0, 1)]
    public float slowingSpeed = 0.5f;
    private float currentMoveSpeed;
    public float turnSpeed = 1f;
    public float jumpHeight = 1f;

    private float ratio = 1;

    public float dashPower = 1f;
    public float dashDistance = 5f;
    [Range(0.01f, 0.2f)]
    public float YDashDirection = 0.05f;
    private float NewSpiritTimer;
    public float timerDash;
    private float currentTimerDash;
    private Vector3 lastPosDash;

    public float rotationSmoothTime = 0.1f;
    private float rotationSmoothVelocity = 0f;

    public Transform groundChecker;
    public LayerMask ground;

    private float GroundDistance = 0.2f;

    public float fallingTimer = 2;
    private float currentTimer;

    private Vector3 gravity = new Vector3(0f, 9.81f, 0f);
    private Vector3 movement;

    private bool useDash = false;
    private bool isGrounding;
    private bool sprinting = false;
    public GameObject jumpTrigger;
    public GameObject dashTrigger;

    public delegate void Charm();
    public Charm charm;

    public delegate void PlayerJump();
    public PlayerJump playerJump;

    public delegate void AxisPlayer(bool b);
    public AxisPlayer axisXPlayer;
    public AxisPlayer axisYPlayer;

    public bool End;

    public CinemachineFreeLook cam;
    MusicMgr music;
    public float[] fieldOfViewLvl = new float[5] { 40, 44, 48, 52, 64 };
    private float currentLvl = 0;
    public AudioSource hitsource;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();

        mainCamera = Camera.main.transform;

        currentMoveSpeed = moveSpeed;

        axisXPlayer = UpdateXAxis;
        axisYPlayer = UpdateYAxis;

        animator = GetComponent<Animator>();

        charm = AnimationCharm;
        music = FindObjectOfType<MusicMgr>();
        music.musicUpdate += CameraFieldOfView;
        if (SaveOptions.instance != null)
        {
            UpdateXAxis(SaveOptions.instance.invertXAxis);
            UpdateYAxis(SaveOptions.instance.invertYAxis);
        }

        hitsource.playOnAwake = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (End)
            return;
        if (animator.GetBool("NewSpirit"))
            NewSpiritTimer += Time.deltaTime;

        if(NewSpiritTimer >= 1f)
            animator.SetBool("NewSpirit", false);
        if (movement == Vector3.zero)
            animator.SetBool("Moving", false);
        else
            animator.SetBool("Moving", true);

        currentMoveSpeed = moveSpeed;
        if (rigidBody.freezeRotation)
        {
            /*if (useDash)
            {
                currentTimerDash += Time.deltaTime;
                movement = Vector3.zero;
                if ((lastPosDash - transform.position).magnitude >= dashDistance || currentTimerDash >= timerDash)
                {
                    currentTimerDash = 0f;
                    rigidBody.velocity = Vector3.zero;
                    useDash = false;
                }
                return;
            }*/
            movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            movement.Normalize();

            if (movement.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSmoothVelocity, rotationSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                movement = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            }


            isGrounding = Physics.CheckSphere(groundChecker.position, GroundDistance, ground, QueryTriggerInteraction.Ignore);

            /*if (isGrounding)
            {
                if (Input.GetButtonDown("Jump"))
                {
					if (transform.parent != null)
						transform.parent = null;
					rigidBody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
                    animator.SetBool("Falling", true);
                    CreateJumpTrigger();
                }
                animator.SetBool("Falling", false);
            }*/
            if(!animator.GetBool("Falling") && !isGrounding)
            {
                animator.SetBool("Falling", true);
            }
            else
            {
                animator.SetBool("Falling", false);
            }

            /*if (Input.GetButtonDown("Dash") && !useDash)
            {
                Vector3 dashDirection = movement;
                if (movement == Vector3.zero)
                    dashDirection = transform.forward;

                dashDirection.y = YDashDirection;
                rigidBody.AddForce(dashDirection.normalized * dashPower, ForceMode.VelocityChange);
                currentTimerDash = 0f;
                useDash = true;
                lastPosDash = transform.position;
                CreateDashTrigger();
            }*/

            if (Input.GetAxis("Sprint") > 0f || Input.GetButton("Sprint"))
            {
                currentMoveSpeed = moveSprintSpeed;
                sprinting = true;
            }
            else if ((Input.GetAxis("Sprint") == 0f || Input.GetButtonDown("Sprint")) && sprinting)
            {
                currentMoveSpeed = moveSpeed;
                sprinting = false;
            }
        }

        if (currentMoveSpeed == moveSpeed * ratio)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Sprint", false);
        }
        else if (currentMoveSpeed == moveSprintSpeed * ratio)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Sprint", true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("PauseScene");

        if (currentTimer > 0)
            currentTimer -= Time.deltaTime;
        else
            GetUp();
    }

    void FixedUpdate()
    {
        if (End)
            return;

        if (rigidBody.freezeRotation && transform.parent != null)
            rigidBody.velocity = movement * ratio * currentMoveSpeed * 50 * Time.fixedDeltaTime;
        else if (rigidBody.freezeRotation)
            rigidBody.MovePosition(transform.position + movement * ratio * currentMoveSpeed * Time.fixedDeltaTime);
    }

    private void CameraFieldOfView(int level)
    {
        if (currentLvl != music.currentLvl)
           StartCoroutine(LerpFieldOfView(music.currentLvl));
    }

    private IEnumerator LerpFieldOfView(int level)
    {
        float elapsedTime = 0;
        float waitTime = 1f;
        float currentPos = cam.m_Lens.FieldOfView;

        while (elapsedTime < waitTime)
        {
            cam.m_Lens.FieldOfView = Mathf.Lerp(currentPos, fieldOfViewLvl[level], (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        // Make sure we got there
        currentLvl = level;
        cam.m_Lens.FieldOfView = fieldOfViewLvl[level];
        yield return null;
    }

    public void AnimationCharm()
    {
        animator.SetBool("NewSpirit", true);
    }
    
    public void CharmEnd()
    {
        animator.SetBool("NewSpirit", false);
        NewSpiritTimer = 0f;
    }

    public void CreateJumpTrigger()
	{
        playerJump();
        //GameObject g = Instantiate(jumpTrigger, transform.position, Quaternion.identity);
        //g.transform.right = transform.forward;
    }
    public void CreateDashTrigger()
    {
        GameObject g = Instantiate(dashTrigger, transform.position, Quaternion.identity);
        g.transform.right = transform.forward;

    }

    private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.layer == LayerMask.NameToLayer("Slow"))
            ratio = slowingSpeed;
    }
	private void OnTriggerExit(Collider other)
	{
        if (other.gameObject.layer == LayerMask.NameToLayer("Slow"))
            ratio = 1;
    }

    private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.layer == LayerMask.NameToLayer("DeathBall"))
        {
            Fall();
            animator.SetBool("Hit", true);
            hitsource.Play();
        }
	}

	private void Fall()
	{
        currentTimer = fallingTimer;
        rigidBody.freezeRotation = false;
	}

    private void GetUp()
	{
        if (!rigidBody.freezeRotation)
        {
            rigidBody.freezeRotation = true;
            animator.SetBool("Hit", false);
        }
    }

    public void UpdateXAxis(bool b)
	{
        cam.m_XAxis.m_InvertInput = b;
        Debug.Log("m_XAxis invert");
    }
    public void UpdateYAxis(bool b)
    {
        cam.m_YAxis.m_InvertInput = b;
        Debug.Log("m_YAxis invert");
    }
}
