using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healer : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]

    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private bool collected = false;
    PlayerController Pc;

    public void LoadData(GameData data)
    {
        data.healerBagCollected.TryGetValue(id, out collected);
        if(collected)
        {
            Destroy(gameObject);
        }
    }

    public void SaveData(ref GameData data)
    {
        if(data.healerBagCollected.ContainsKey(id))
        {
            data.healerBagCollected.Remove(id);
        }
        data.healerBagCollected.Add(id, collected);
    }

    void Start()
    {
        Pc = GameObject.Find("Player").GetComponent<PlayerController>();
       // if(Pc.HasHealers)
        //{
      //      Destroy(gameObject);
       // }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !collected)
        {
            Pc.HasHealers = true;
            Pc.maxHealers += 2;
            Pc.healers = Pc.maxHealers;
            collected = true;
            Destroy(gameObject);
        }

    }
}