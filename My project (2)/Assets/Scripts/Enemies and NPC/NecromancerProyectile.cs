using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerProyectile : MonoBehaviour
{
    public GameObject necromancerProyectile;
    GameObject PC;

    void Awake()
    {
        PC = GameObject.Find("Player");
    }

    public void NecromancerLightning()
    {
        GameObject proyectile = Instantiate(necromancerProyectile, new Vector2(PC.transform.position.x, transform.position.y - 1), necromancerProyectile.transform.rotation);
        //Vector3 origScale = proyectile.transform.localScale;
        //proyectile.transform.localScale = new Vector3(origScale.x * transform.localScale.x > 0 ? 1 : -1, origScale.y, origScale.z);

    }
}
