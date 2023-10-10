using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    WarZombie,
    ZombieMan,
    ZombieGirl,
    MonsterZombie
}


public class Factory : MonoBehaviour
{
    [SerializeField] Transform[] createPoint; 

    public void CreateUnit(UnitType type)
    {   
        switch (type)
        {
            case UnitType.WarZombie: Instantiate
                (
                    Resources.Load<GameObject>("Warzombie"),
                    createPoint[Random.Range(0, 3)]
                );
                break;
            case UnitType.ZombieMan: Instantiate
            (
                Resources.Load<GameObject>("ZombieMan"),
                createPoint[Random.Range(0, 3)]
            );
                break;
            case UnitType.ZombieGirl: Instantiate
            (
                Resources.Load<GameObject>("Zombiegirl"),
                createPoint[Random.Range(0, 3)]
            );
                break;
        }

    }

}
