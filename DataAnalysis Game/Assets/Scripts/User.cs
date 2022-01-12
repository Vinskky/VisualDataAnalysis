using System.Collections;
using System;
using UnityEngine;

[Serializable]
public class User
{
    public int userId;
    public string name;
    public int age;
    public string gender;
    
    public User(int userId, string name, int age, string gender)
    {
        this.userId = userId;
        this.name   = name;
        this.age    = age;
        this.gender = gender;
    }

    public string GetSerialized()
    {
        return JsonUtility.ToJson(this);
    }
}
