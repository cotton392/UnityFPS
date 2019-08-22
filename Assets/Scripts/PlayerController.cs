using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    Rigidbody rb;
    public float speed = 2f;
    public float jumpPower = 150;
    private Animator animator;

    [SerializeField] private Transform cameraObj;
    [SerializeField] private GameObject unityChan;
    [SerializeField] bool stopMove;

    void Start(){
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update(){
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        if(stopMove){
            if(z > 0){
                transform.rotation = Quaternion.Euler(new Vector3(
                    transform.rotation.x,cameraObj.eulerAngles.y, transform.rotation.z));
                animator.SetBool("Front", true);
            }
            else{
                animator.SetBool("Front", false);
            }

            if(z < 0){
                animator.SetBool("Back", true);
            }
            else{
                animator.SetBool("Back", false);
            }

            if(x > 0){
                animator.SetBool("Right", true);
            }
            else{
                animator.SetBool("Right", false);
            }

            if(x < 0){
                animator.SetBool("Left", true);
            }
            else{
                animator.SetBool("Left", false);
            }
        }
        else{
            AnimatorAllFalse();
        }

        transform.position += transform.forward * z + transform.right * x;

        if(Input.GetButton("Jump") && stopMove){
            AnimatorAllFalse();
            rb.AddForce(transform.up * jumpPower);
            animator.Play("Jump");
        }
    }

    public void GroundDecitionTrue(){
        stopMove = true;
        animator.SetBool("Jump", false);
    }

    public void GroundDecitionFalse(){
        stopMove = false;
    }

    void AnimatorAllFalse(){
        animator.SetBool("Front", false);
        animator.SetBool("Back", false);
        animator.SetBool("Right", false);
        animator.SetBool("Left", false);
    }
}
