using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public int vida;
    public float velocidade;

    [SerializeField] float distanciaAtk;
    [SerializeField] float delayAtk;

    bool vivo = true;
    bool podeAtacar = true;

    PlayerController player;
    Rigidbody rb;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vivo) 
        { 
            float distancia = Vector3.Distance
                (player.transform.position, transform.position);
        
            if(distancia > distanciaAtk)
                SeguirPlayer();
            else if(podeAtacar)
                AtacarPlayer();
        }
    }

    private void SeguirPlayer() 
    {
        Vector3 direction = (player.transform.position -
            transform.position).normalized;

        Quaternion look = Quaternion.LookRotation(direction);

        Vector3 moveTowards = Vector3.MoveTowards(
            transform.position,
            player.transform.position,
            Time.deltaTime * velocidade);

        float distancia = 
            (moveTowards - player.transform.position).magnitude - 2;

        animator.SetFloat("move", distancia);

        rb.Move(moveTowards, look);
    }

    private void AtacarPlayer() 
    {
        animator.SetTrigger("atk");
        StartCoroutine(DelayAtaque());
        Vector3 origemRay = transform.position;
        Vector3 direcaoRay = transform.forward;
        float distanciaRay = distanciaAtk;
        Debug.DrawRay
            (transform.position, transform.forward * distanciaAtk, Color.red, 1);
        RaycastHit hit;
        if (Physics.Raycast(origemRay, direcaoRay, out hit, distanciaRay)) 
        {
            if (hit.collider.tag == "Player") 
            { 
                PlayerController player = hit.collider.GetComponent<PlayerController>();
                player.LevarDano(1);
            }
        }
    }

    private void OnTriggerEnter(Collider outro)
    {
        if (outro.gameObject.tag == "PlayerAtk") 
        {
            animator.SetTrigger("dano");

            vida--;

            if (vida <= 0) 
            {
                animator.SetBool("die", true);
                vivo = false;

                GetComponent<Collider>().enabled = false;
                rb.useGravity = false;
            }

            Debug.LogWarning("Fui atacado! Vida:"+vida);
        }
    }

    IEnumerator DelayAtaque() 
    {
        podeAtacar = false;
        yield return new WaitForSeconds(delayAtk);
        podeAtacar = true;
    }

}
