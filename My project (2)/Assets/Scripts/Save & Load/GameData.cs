using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public int arrows;
    public int maxArrows;
    public int healers;
    public int maxHealers;
    public int coins;
    public bool _hasSuperJump;
    public bool HasWallJump;
    public bool HasBow;
    public bool HasHealers;
    public bool HasQuiver;
    public bool HasClimb;
    public SerializableDictionaries<string, bool> healerBagCollected;

    public GameData()
    {
        this.arrows = 0;
        this.maxArrows = 0;
        this.healers = 0;
        this.maxHealers = 0;
        this.coins = 0;
        this._hasSuperJump = false;
        this.HasWallJump = true;
        this.HasBow = false;
        this.HasHealers = false;
        this.HasQuiver = false;
        this.HasClimb = false;
        healerBagCollected = new SerializableDictionaries<string, bool>();
    }
}
