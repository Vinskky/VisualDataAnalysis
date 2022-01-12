using Gamekit3D.Message;
using Gamekit3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Gamekit3D.Damageable;

public class EventHandler : MonoBehaviour, IMessageReceiver
{
    public List<EventTrackPlayerPosition> positionTrackerList = new List<EventTrackPlayerPosition>();
    public List<EventPlayerHit> listEventHits = new List<EventPlayerHit>();
    public List<EventPlayerDead> listPlayerDeaths = new List<EventPlayerDead>();
    public List<EventMonsterDead> listMonsterDeaths = new List<EventMonsterDead>();
    public List<EventSession> listSessions = new List<EventSession>();
    private EventSession currentSession;

    public Writer writer;
    public GameObject playerGO;
    private GameObject monster;
    private GameObject player;
    private float currentTime = 0.0f;
    public float generatePosTimer = 2.0f;
    bool trackPlayerPos = false;

    void Start()
    {
        // writer.DeserializeEventData(this);
        playerGO.GetComponent<objectData>().objectId = writer.GetRandomUserID();
        currentSession = new EventSession((writer.GetLastRegisteredSessionID() + 1), Time.realtimeSinceStartup, -1.0f, playerGO.GetComponent<objectData>().objectId);
    }

    void Update()
    {
        if(trackPlayerPos)
            currentTime += Time.deltaTime;

        if (currentTime >= generatePosTimer)
        {
            GeneratePlayerPositionEvent();
            currentTime = 0;
        }
    }

    public void OnReceiveMessage(MessageType type, object sender, object msg)
    {
        switch (type)
        {
            case MessageType.DAMAGED:   { GeneratePlayerHitEvent(sender, msg); } break;
            case MessageType.DEAD:      { GeneratePlayerDeadEvent(sender, msg); } break;
            default:                    { throw new System.NotImplementedException(); }
        }
       
    }

    public void GeneratePlayerPositionEvent()
    {
        EventTrackPlayerPosition playerPositionEvent = new EventTrackPlayerPosition(playerGO.transform.position, playerGO.GetComponent<objectData>().objectId, 1, Time.realtimeSinceStartup);
        positionTrackerList.Add(playerPositionEvent);
    }

    public void GeneratePlayerHitEvent(object sender, object msg)
    {
        if(((Damageable)sender).GetComponent<objectData>().isPlayer)
        {
            monster = ((DamageMessage)msg).damager.transform.root.gameObject;
            player  = ((Damageable)sender).gameObject;

            EventPlayerHit playerHitEvent = new EventPlayerHit(player.transform.position, player.GetComponent<objectData>().objectId, currentSession.sessionId, Time.realtimeSinceStartup, 
                                                                monster.transform.position, monster.GetComponent<objectData>().objectId, ((Damageable)sender).currentHitPoints);
                        
            listEventHits.Add(playerHitEvent);

            //-----LOGS------
            Debug.Log("Damage Attacker: " + monster.name);
            Debug.Log("Damage Reciever: " + player.name);
        }
    }

    public void GeneratePlayerDeadEvent(object sender, object msg)
    {
        if (((Damageable)sender).GetComponentInParent<objectData>().isMonster)
        {
            monster = ((Damageable)sender).gameObject;
            player  = ((DamageMessage)msg).damager.transform.root.gameObject;

            EventMonsterDead monsterDeadEvent = new EventMonsterDead(monster.transform.position, player.GetComponent<objectData>().objectId, currentSession.sessionId, 
                                                                        Time.realtimeSinceStartup, monster.transform.position);

            listMonsterDeaths.Add(monsterDeadEvent);

            //-----LOGS------
            Debug.Log("Damage Attacker: " + player.name);
            Debug.Log("monsterDeadEvent: " + monsterDeadEvent);
        }
        else if (((Damageable)sender).GetComponent<objectData>().isPlayer)
        {

            monster = ((DamageMessage)msg).damager.transform.root.gameObject;
            player  = ((Damageable)sender).gameObject;

            EventPlayerDead playerDeadEvent = new EventPlayerDead(player.transform.position, player.GetComponent<objectData>().objectId, currentSession.sessionId, Time.realtimeSinceStartup,
                                                                    monster.GetComponent<objectData>().objectId);

            listPlayerDeaths.Add(playerDeadEvent);

            //-----LOGS------
            Debug.Log("Damage Attacker: " + monster.name);
            Debug.Log("DEAD: " + player.name);
        }
    }

    public void ActivateTrackPlayerPos()
    {
        trackPlayerPos = true;
    }

    void OnApplicationQuit()
    {
        currentSession.sessionEnd = Time.realtimeSinceStartup;
        listSessions.Add(currentSession);

        writer.SerializeAndSave(this);
    }
}