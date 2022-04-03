using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTurretLasers : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> lazors = new List<ParticleSystem>();
    Enemy[] enemiesOnBoard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemiesOnBoard = FindObjectsOfType<Enemy>();

        if(enemiesOnBoard.Length > 0)
        {
            foreach(ParticleSystem p in lazors)
            {
                var emission = p.emission;
                emission.enabled = true;
            }
        }

        else
        {
            foreach (ParticleSystem p in lazors)
            {
                var emission = p.emission;
                emission.enabled = false;
            }
        }
    }
}
