using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject prefabSlime;

    public Transform[] spawns;

    public float tempoSpawn;

    float contadorSpawn;

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Spawnar();
    }

    void Spawnar() 
    {
        contadorSpawn += Time.deltaTime;

        if(contadorSpawn > tempoSpawn)
        {
            contadorSpawn = 0;
            int sorteio = Random.Range(0, spawns.Length);
            Vector3 posicaoSpawnar = spawns[sorteio].position;
            Instantiate
                (prefabSlime, posicaoSpawnar, prefabSlime.transform.rotation);
        }

    }

}
