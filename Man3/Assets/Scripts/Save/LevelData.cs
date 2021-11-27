using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public float[] position;
    public LevelData(player p)
    {
        position = new float[3];
        position[0] = p.transform.position.x;
        position[1] = p.transform.position.y;
        position[2] = p.transform.position.z;
    }
}
