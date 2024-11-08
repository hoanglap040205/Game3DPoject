using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigid;
    private CapsuleCollider capSul;
   // private Animator anim;
    private float inPutHorizontal;
    private float inPutVertical;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private Vector3 target;
    [SerializeField] private bool isGround;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        capSul = GetComponent<CapsuleCollider>();
        //anim = GetComponent<Animator>();
        isGround = true;
    }
    private void Update()
    {
        if (isGround == true)
        {
            Movement();
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            Jump();
        }
    }
    private void Movement()
    {
        inPutHorizontal = Input.GetAxis("Horizontal");
        inPutVertical = Input.GetAxis("Vertical");

        Vector3 movementDir = new Vector3(inPutHorizontal,0, inPutVertical).normalized * moveSpeed;
        //anim.SetBool("isRun", movementDir != Vector3.zero);
        rigid.velocity = movementDir;
       if(movementDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDir,Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,toRotation,360 * Time.deltaTime);
        }
    }
    
    private void Jump()
    {
        rigid.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
        isGround = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGround = true;
        }
    }
   
}
