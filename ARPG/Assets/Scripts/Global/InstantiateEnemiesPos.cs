using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InstantiateEnemiesPos : MonoBehaviour
{
    const float numRango = 40;

    static Vector3[] posEne = new Vector3[4];
    static float posXright;
    static float posXleft;
    static float posZright;
    static float posZleft;
    public static Vector3 PosEnem(Transform player)
    {

        posXright = player.position.x + numRango;
        posXleft = player.position.x - numRango;
        posZright = player.position.z + numRango;
        posZleft = player.position.z - numRango;


        posEne[0] = new Vector3(posXright, 1, Random.Range(-numRango, numRango));
        posEne[1] = new Vector3(posXleft, 1, Random.Range(-numRango, numRango));
        posEne[2] = new Vector3(Random.Range(-numRango, numRango), 1, posZright);
        posEne[3] = new Vector3(Random.Range(-numRango, numRango), 1, posZleft);

        return posEne[Random.Range(0, 4)];
    }
}
