using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

   Vector2 moveDirection;
   float jumpDirection;
   [SerializeField] float moveSpeed = 2;
   [SerializeField] float maxForwardSpeed = 8;
   float desiredSpeed;
   float forwardSpeed;
   float jumpSpeed = 150;
   Rigidbody rb;
   public float turnSpeed = 100;
   const float groundAccel = 5;
   const float groundDecel = 25; 

    Animator anim;

   bool IsMoveInput
   {
       get { return !Mathf.Approximately(moveDirection.sqrMagnitude, 0f); }
   }
    //on moving for moving
   public void OnMove(InputAction.CallbackContext context)
   {
       moveDirection = context.ReadValue<Vector2>();
       
   }
    //for jumping
   public void OnJump(InputAction.CallbackContext context)
   {
       jumpDirection = context.ReadValue<float>();
   }

   void Jump(float direction){

       if(direction>0){
           rb.AddForce(0,jumpSpeed,0);
        Debug.Log("Dirction is this jumping"+direction);
        anim.SetBool("ReadyJump",true);
        
       }else{
        anim.SetBool("ReadyJump",false);   
       }
       
   }

   void Move(Vector2 direction)
   {
       float turnAmount = direction.x;
       float fDirection = direction.y;
       if(direction.sqrMagnitude > 1f){
           direction.Normalize();
       }

        desiredSpeed = direction.magnitude * maxForwardSpeed * Mathf.Sign(fDirection); 
        float acceleration = IsMoveInput ? groundAccel : groundDecel;

        forwardSpeed = Mathf.MoveTowards(forwardSpeed, desiredSpeed, acceleration * Time.deltaTime);
        anim.SetFloat("ForwardSpeed",forwardSpeed);
       
        transform.Rotate(0,turnAmount * turnSpeed *Time.deltaTime,0);

   }

   void Start()
   {
       anim = this.GetComponent<Animator>();
       rb = this.GetComponent<Rigidbody>(); 
   }

   void Update()
   {
       Move(moveDirection);
       Jump(jumpDirection); 
   }
}
