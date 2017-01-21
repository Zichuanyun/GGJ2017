using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float groundHeight = 5f;
    public LayerMask groundMask = -1;
    public float jumpSpeed = 5f;
    public float runSpeed = 10f;
    public string jumpButtonName = "Jump";
    public string h_AxisName = "Horizontal";
    public string v_AxisName = "Vertical";

    public float smashPasueTime = 0.3f;
    float runAnimSpeedFactor = 0.1f;

    bool onSmash = false; // direction is locked
    bool lockMove = false;                

    Animator anim;
    float defaultSpeed = 1.0f;
    Rigidbody rg;

    Vector3 faceDirection = new Vector3();


	// Use this for initialization
	void Start () {
        if (rg = GetComponent<Rigidbody>()) {
            Debug.Log("Rigidbody get");
        }
        if (anim = GetComponent<Animator>()) {
            Debug.Log("Animator get");
            defaultSpeed = anim.speed;
        }
    }

	// Update is called once per frame
	void Update () {
        //Reset anim speed to default, because running may change it
        anim.speed = defaultSpeed;
        //Debug.Log(checkGround());

        Vector3 moveV = new Vector3(Input.GetAxisRaw(h_AxisName), 0, Input.GetAxisRaw(v_AxisName));
        move2D(moveV);

        if (Input.GetButtonDown("Jump")) {
            if (checkGround() && !onSmash) {
                doJump();
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if(!onSmash)
                doSmash();
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

    void move2D(Vector3 v) {
        if (onSmash) {
            return;
        }    
        if (Vector3.Magnitude(v) == 0) {
            anim.SetFloat("speed", 0f);
            rg.velocity = new Vector3(0, rg.velocity.y,0);
            return;
        }

        anim.SetFloat("speed", 1f);
        anim.speed = defaultSpeed * runSpeed * runAnimSpeedFactor;
        transform.rotation = Quaternion.LookRotation(v);
        Vector3 velocity = transform.forward.normalized * runSpeed;
        //Debug.Log(velocity);
        
            rg.velocity = new Vector3(velocity.x, rg.velocity.y, velocity.z);
    }

    void doJump() {
        Vector3 jump = new Vector3(0, jumpSpeed, 0);
        rg.velocity = rg.velocity + jump;
        anim.SetTrigger("jump");
    }

    void doSmash() {
        onSmash = true;
        if (checkGround())
            lockMove = true;
        anim.SetTrigger("beginSmash");
        StartCoroutine("smashStage2");

    }

    void endSmashStage1() {
        //StartCoroutine("smashStage2");
    }

    IEnumerator smashStage2() {
        while (!checkGround()) {
            yield return null;
        }
        lockMove = true;
        anim.SetTrigger("endSmash");
    }

    void endSmashStage2()
    {

        StartCoroutine("endSmashTotally");

    }

    IEnumerator endSmashTotally() {
        Boom boom;
        boom = GameObject.FindWithTag("GroundController").GetComponent<Boom>();
        if(boom)
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.z);
            boom.boom(pos);
        }
        yield return new WaitForSeconds(smashPasueTime);
        lockMove = false;
        onSmash = false;
    }



    bool checkGround() {
        RaycastHit hit;
        Vector3 upOffset = new Vector3(0, 0.1f, 0);
        Vector3 down = transform.TransformDirection(Vector3.down) * groundHeight;
        Debug.DrawRay(transform.position + upOffset, down, Color.green);
        if (Physics.Raycast(transform.position + upOffset, Vector3.down, out hit, 100, groundMask.value)) {
            if(hit.distance <= groundHeight) {
                return true;
            }
        }
        
        return false;
    }
}
