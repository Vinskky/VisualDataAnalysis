using System.Collections;
using System;
using UnityEngine;

[Serializable]
public class Monster
{
    public int monsterId;
    public string monsterType;
    public float initPosX;
    public float initPosY;

    public Monster(int monsterId, string monsterType, float initPosX, float initPosY)
    {
        this.monsterId = monsterId;
        this.monsterType = monsterType;
        this.initPosX = initPosX;
        this.initPosY = initPosY;
    }

    public string GetSerialized()
    {
        return JsonUtility.ToJson(this);
    }
}
