using System.Collections;
using System;
using UnityEngine;

//Main class event to inherit different data
[Serializable]
public class EventData
{
    public Vector3 position;
    public int playerId;
    public int sessionId;
    public float timestamp;
    
    public EventData(Vector3 pos, int playerId, int sessionId, float timestamp)
    {
        this.playerId = playerId;
        this.position = pos;
        this.sessionId = sessionId;
        this.timestamp = timestamp;
    }

    public string GetSerialized()
    {
        return JsonUtility.ToJson(this);
    }
}

[Serializable]
public class EventTrackPlayerPosition : EventData
{
    public EventTrackPlayerPosition(Vector3 pos, int playerId, int sessionId, float timestamp) : base(pos,playerId,sessionId,timestamp)
    {}
}

[Serializable]
public class EventPlayerHit: EventData
{
    Vector3 monsterPosition;
    int     monsterId;
    int     playerHp;

    public EventPlayerHit(Vector3 pos, int playerId, int sessionId, float timestamp, Vector3 monsterPosition, int monsterId, int playerHp) : base(pos, playerId, sessionId, timestamp)
    {
        this.monsterPosition = monsterPosition;
        this.monsterId = monsterId;
        this.playerHp = playerHp;
    }
}

[Serializable]
public class EventPlayerDead: EventData
{
    int monsterId;

    public EventPlayerDead(Vector3 pos, int playerId, int sessionId, float timestamp, int monsterId) : base(pos, playerId, sessionId, timestamp)
    {
        this.monsterId = monsterId;
    }
}

[Serializable]
public class EventMonsterDead: EventData
{
    Vector3 monsterPosition;

    public EventMonsterDead(Vector3 pos, int playerId, int sessionId, float timestamp, Vector3 monsterPosition) : base(pos, playerId, sessionId, timestamp)
    {

        this.monsterPosition = monsterPosition;
    }
}

[Serializable]
public class EventSession
{
    public int sessionId;
    public float sessionStart;
    public float sessionEnd;
    public int userId;

    public EventSession(int sessionId, float sessionStart, float sessionEnd, int userId)
    {
        this.sessionId = sessionId;
        this.sessionStart = sessionStart;
        this.sessionEnd = sessionEnd;
        this.userId = userId;
    }

    public string GetSerialized()
    {
        return JsonUtility.ToJson(this);
    }
} 