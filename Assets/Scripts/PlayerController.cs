using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtVida;

    [SerializeField] int numeroJogador;

    [SerializeField] int vida;

    bool vivo = true;

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
        if (vivo)
        {
            Movimentar();
            Atacar();
        }
        MostrarVida();            
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

    void Movimentar() 
    {
        vAxis = Input.GetAxis("VerticalP" + numeroJogador);
        hAxis = Input.GetAxis("HorizontalP" + numeroJogador);

        float movement = new Vector3(vAxis, 0, hAxis).sqrMagnitude;
        animator.SetFloat("movement", movement);
    }

    void Atacar() 
    {
        if (Input.GetButtonDown("Fire1P" + numeroJogador))
        {
            animator.SetTrigger("atk");
        }
    }

    void MostrarVida() 
    {
        txtVida.text = "";
        for (int i = 0; i < vida; i++)
        {
            txtVida.text += "♥";
        }
    }

    public void LevarDano(int dano) 
    {
        animator.SetTrigger("dano");
        vida -= dano;
        if (vida <= 0)
        {
            animator.SetBool("die", true);
            vivo = false;

            GetComponent<Collider>().enabled = false;
            rb.useGravity = false;
        }
    }

    public void AtivarAtk() { playerAtk.SetActive(true); }

    public void DesativarAtk() { playerAtk.SetActive(false); }


}
