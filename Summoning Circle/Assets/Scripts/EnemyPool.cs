using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class EnemyPool : ScriptableObject
{
    public List<Transform> OneThreat = new List<Transform>();
    public List<Transform> TwoThreat = new List<Transform>();
    public List<Transform> ThreeThreat = new List<Transform>();
    public List<Transform> BossThreat = new List<Transform>();

    public Transform GetEnemy(int threat)
    {
        List<Transform> list = threat switch
        {
            1 => OneThreat,
            2 => TwoThreat,
            3 => ThreeThreat,
            _ => OneThreat,
        };
        

        return list.RandomItem();
    }

    public Transform GetBoss()
    {
        return BossThreat.RandomItem();
    }
}
