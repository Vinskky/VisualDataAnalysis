using System.Collections;
using UnityEngine;

//Main class event to inherit different data
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

public class EventUpdate : EventData
{
    public EventUpdate(Vector3 pos, int playerId, int sessionId, float timestamp) : base(pos,playerId,sessionId,timestamp)
    {}
}

public class EventHit: EventData
{
    Vector3 monsterPosition;
    int     monsterId;
    int     playerHp;
    public EventHit(Vector3 pos, int playerId, int sessionId, float timestamp, Vector3 monsterPosition, int monsterId, int playerHp) : base(pos, playerId, sessionId, timestamp)
    {
        this.monsterPosition = monsterPosition;
        this.monsterId = monsterId;
        this.playerHp = playerHp;
    }
}