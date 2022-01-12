using System.Collections;
using System;
using UnityEngine;

[Serializable]
public class Monster
{
    public int monsterId;
    public string monsterType;
    public int initPosX;
    public int initPosY;

    public Monster(int monsterId, string monsterType, int initPosX, int initPosY)
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
