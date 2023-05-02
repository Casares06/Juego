using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadows : MonoBehaviour
{
    public static Shadows me;
    public GameObject shadow;
    public List<GameObject> pool = new List<GameObject>();
    private float time;
    public float appearingSpeed;
    public Color _color;

    void Awake()
    {
        me = this;
    }

    public GameObject GetShadows()
    {
        for(int i= 0; i < pool.Count; i++)
        {
            if(!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                pool[i].transform.position = transform.position;
                pool[i].transform.rotation = transform.rotation;
                pool[i].transform.localScale = transform.localScale;
                pool[i].GetComponent<SpriteRenderer>().sprite  = GetComponent<SpriteRenderer>().sprite;
                pool[i].GetComponent<SolidColor>()._color = _color;
                return pool[i];
            }
        }
        GameObject obj = Instantiate(shadow, transform.position, transform.rotation) as GameObject;
        obj.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        obj.GetComponent<SolidColor>()._color = _color;
        pool.Add(obj);
        return obj;
    }

    public void ShadowsSkill()
    {
        time += appearingSpeed * Time.deltaTime;

        if(time > 1)
        {
            GetShadows();
            time = 0;
        }
    }
}
