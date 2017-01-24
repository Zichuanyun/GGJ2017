using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public int playerNumber = 2;

    public float groundHeight = 5f;
    public LayerMask groundMask = -1;
    public float jumpSpeed = 5f;
    public float runSpeed = 10f;

    public float joystickThreshold = 0.1f;
    string h_AxisName;
    string v_AxisName;

    public float smashPasueTime = 0.3f;
    public float runAnimSpeedFactor = 0.25f;

    bool onSmash = false; // direction is locked
    bool lockMove = false;

    public bool canControl = true;
    public bool isDead = false;

    public float smashCD = 3f;

    Animator anim;
    float defaultSpeed = 1.0f;
    Rigidbody rg;
    bool canSmash = true;

    Vector3 faceDirection = new Vector3();

    public void Setup(Color color, int p_number)
    {
        canControl = false;

        SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = color;
        }
        playerNumber = p_number;
        h_AxisName = "Horizontal" + playerNumber.ToString();
        v_AxisName = "Vertical" + playerNumber.ToString();

        GetComponentInChildren<Light>().color = color;
    }

    // Use this for initialization
    void Start()
    {
        h_AxisName = "Horizontal" + playerNumber.ToString();
        v_AxisName = "Vertical" + playerNumber.ToString();

        if (rg = GetComponent<Rigidbody>())
        {
            Debug.Log("Rigidbody get");
        }
        if (anim = GetComponent<Animator>())
        {
            Debug.Log("Animator get");
            defaultSpeed = anim.speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Reset anim speed to default, because running may change it
        anim.speed = defaultSpeed;
        //Debug.Log(checkGround());
        anim.SetBool("dead", isDead);

        if (canControl)
        {
            float moveH = Input.GetAxisRaw(h_AxisName);
            float moveV = Input.GetAxisRaw(v_AxisName);
            if (Mathf.Abs(moveH) < joystickThreshold)
            {
                moveH = 0;
            }

            if (Mathf.Abs(moveV) < joystickThreshold)
            {
                moveV = 0;
            }
            Vector3 move = new Vector3(moveH, 0, moveV);
            //Debug.Log("h_AxisName: " + Input.GetAxisRaw(h_AxisName));
            move2D(move);
            string jumpBN = "Jump" + playerNumber.ToString();
            if (Input.GetButtonDown(jumpBN))
            {
                if (checkGround() && !onSmash)
                {
                    doJump();
                }
            }

            string fireBN = "Fire" + playerNumber.ToString();
            if (Input.GetButtonDown(fireBN) && canSmash)
            {
                if (!onSmash)
                    doSmash();
            }
        }
        if (lockMove)
            rg.velocity = new Vector3();
    }

    void FixedUpdate()
    {
        // zero out Angular Velocity
        rg.angularVelocity = new Vector3();

        if (checkGround())
        {
            anim.SetBool("isGround", true);
        }
        else
        {
            anim.SetBool("isGround", false);
        }

        if (rg.velocity.y < 0)
        {
            anim.SetBool("goingDown", true);
        }
        else
            anim.SetBool("goingDown", true);
    }

    void move2D(Vector3 v)
    {
        if (onSmash)
        {
            return;
        }
        if (Vector3.Magnitude(v) == 0)
        {
            anim.SetFloat("speed", 0f);
            rg.velocity = new Vector3(0, rg.velocity.y, 0);
            return;
        }

        anim.SetFloat("speed", 1f);
        anim.speed = defaultSpeed * runSpeed * runAnimSpeedFactor;
        transform.rotation = Quaternion.LookRotation(v);
        Vector3 velocity = transform.forward.normalized * runSpeed;
        //Debug.Log(velocity);

        rg.velocity = new Vector3(velocity.x, rg.velocity.y, velocity.z);
    }

    void doJump()
    {
        Vector3 jump = new Vector3(0, jumpSpeed, 0);
        rg.velocity = rg.velocity + jump;
        anim.SetTrigger("jump");
    }

    void doSmash()
    {
        onSmash = true;
        if (checkGround())
            lockMove = true;
        anim.SetTrigger("beginSmash");
        StartCoroutine("smashStage2");

    }

    void endSmashStage1()
    {
        //StartCoroutine("smashStage2");
    }

    IEnumerator smashStage2()
    {
        while (!checkGround())
        {
            yield return null;
        }
        lockMove = true;
        anim.SetTrigger("endSmash");
    }

    void endSmashStage2()
    {

        StartCoroutine("endSmashTotally");

    }

    IEnumerator endSmashTotally()
    {
        this.GetComponent<SmashBall>().smashBall();
        Boom boom;
        boom = GameObject.FindWithTag("GroundController").GetComponent<Boom>();
        if (boom)
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.z);
            boom.boom(pos);
        }
        StartCoroutine("smashCDCounter");
        yield return new WaitForSeconds(smashPasueTime);
        lockMove = false;
        onSmash = false;
    }

    IEnumerator smashCDCounter() {
        canSmash = false;
        int n = 10;
        float cdet = smashCD / n;
        yield return new WaitForSeconds(smashCD);

        /*
        for (int i = 0; i < n; i++) {
            yield return new WaitForSeconds(cdet);
        }
        */
        canSmash = true;
    }



    bool checkGround()
    {
        RaycastHit hit;
        Vector3 upOffset = new Vector3(0, 0.1f, 0);
        Vector3 down = transform.TransformDirection(Vector3.down) * groundHeight;
        Debug.DrawRay(transform.position + upOffset, down, Color.green);
        if (Physics.Raycast(transform.position + upOffset, Vector3.down, out hit, 100, groundMask.value))
        {
            if (hit.distance <= groundHeight)
            {
                return true;
            }
        }

        return false;
    }


}
