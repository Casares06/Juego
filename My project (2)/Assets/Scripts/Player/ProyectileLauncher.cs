using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileLauncher : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject proyectilePrefab;
    PlayerController PC;

    void Awake()
    {
        PC = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void FireProyectile()
    {
        GameObject proyectile = Instantiate(proyectilePrefab, launchPoint.position, proyectilePrefab.transform.rotation);
        Vector3 origScale = proyectile.transform.localScale;
        proyectile.transform.localScale = new Vector3(origScale.x * transform.localScale.x > 0 ? 1 : -1, origScale.y, origScale.z);

        if(PC.ReduceArrows)
        {
            PC.arrows -= 1;
            PC.ReduceArrows = false;
        }
    }

}
