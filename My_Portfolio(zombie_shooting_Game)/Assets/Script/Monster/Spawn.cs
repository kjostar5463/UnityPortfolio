using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject monster;
    [SerializeField] GameObject player;

    LayerMask _layerMask;

    // Start is called before the first frame update
    void Start()
    {
        _layerMask = -1;
        _layerMask &= ~LayerMask.GetMask("Ground");
        StartCoroutine(spawnMonster());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetRandomPosition()
    {
        float radius = 10f;
        Vector3 playerPosition = player.transform.position;

        float playerX = playerPosition.x;
        float playerZ = playerPosition.z;

        float x = Random.Range(-radius + playerX, radius + playerX);
        float z_b = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - playerX, 2));
        z_b *= Random.Range(0, 2) == 0 ? -1 : 1;
        float z = z_b + playerZ;

        Vector3 randomPosition = new Vector3(x, 1, z);

        return randomPosition;
    }

    IEnumerator spawnMonster()
    {
        Vector3 spawnPoint = GetRandomPosition();
        Debug.Log(_layerMask.value);
        Debug.Log(Physics.CheckSphere(spawnPoint, 1.5f, _layerMask));
        if (!Physics.CheckSphere(spawnPoint, 1.5f, _layerMask))
            Instantiate(monster, spawnPoint, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);

        StartCoroutine(spawnMonster());
    }
}
