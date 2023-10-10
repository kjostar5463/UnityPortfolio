using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FactoryUse : MonoBehaviour
{
    [SerializeField] Factory factory;
    WaitForSeconds wait = new WaitForSeconds(10);

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreatZombie());
    }

    private IEnumerator CreatZombie()
    {
        while(true)
        {
            factory.CreateUnit((UnitType)Random.Range(0, 3));
            
            yield return wait;
        }
    }
}
