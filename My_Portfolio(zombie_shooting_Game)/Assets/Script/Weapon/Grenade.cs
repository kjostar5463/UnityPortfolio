using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    WaitForSeconds _explosionTime = new WaitForSeconds(1.5f);

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GrenadeExplosion());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GrenadeExplosion()
    {
        yield return _explosionTime;

        Destroy(gameObject);
    }
}
