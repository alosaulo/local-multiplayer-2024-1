using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public int vida;
    public float velocidade;

    PlayerController player;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        SeguirPlayer();
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

        rb.Move(moveTowards, look);
    }

    private void OnTriggerEnter(Collider outro)
    {
        if (outro.gameObject.tag == "PlayerAtk") 
        {
            vida--;
            if (vida <= 0) 
            {
                Destroy(gameObject);
            }
            Debug.LogWarning("Fui atacado! Vida:"+vida);
        }
    }

}
