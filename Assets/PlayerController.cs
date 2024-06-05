using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float vAxis;
    float hAxis;
    Rigidbody rb;
    Animator animator;

    [SerializeField] float speed;

    [SerializeField] GameObject playerAtk;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        vAxis = Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("Horizontal");

        float movement = new Vector3(vAxis,0,hAxis).sqrMagnitude;
        animator.SetFloat("movement", movement);

        if (Input.GetButtonDown("Fire1")) 
        {
            animator.SetTrigger("atk");
        }

    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(hAxis, 0, vAxis).normalized;

        rb.velocity = movement * speed * Time.fixedDeltaTime;
        
        if(movement.magnitude > 0) 
        { 
            Quaternion orientacaoRotacao = 
                Quaternion.LookRotation(movement, Vector3.up);
        
           rb.rotation = Quaternion.RotateTowards(rb.rotation, 
                            orientacaoRotacao, 
                            10);
        }

    }

    public void AtivarAtk() { playerAtk.SetActive(true); }

    public void DesativarAtk() { playerAtk.SetActive(false); }


}
