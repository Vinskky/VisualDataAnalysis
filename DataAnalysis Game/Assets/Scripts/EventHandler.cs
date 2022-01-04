using Gamekit3D.Message;
using Gamekit3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Gamekit3D.Damageable;

public class EventHandler : MonoBehaviour, IMessageReceiver
{
    List<EventPlayerHit> listEventHits;
    List<EventTrackPlayerPosition> positionTrackerList;
    List<EventPlayerDead> listPlayerDeaths;
    List<EventMonsterDead> listMonsterDeaths;

    GameObject monster;
    GameObject player;

    public void OnReceiveMessage(MessageType type, object sender, object msg)
    {
        switch (type)
        {
            case MessageType.DAMAGED:
                {
                    if(((Damageable)sender).GetComponentInParent<objectData>().isMonster)
                    {

                        monster = ((Damageable)sender).gameObject;

                        player = ((DamageMessage)msg).damager.transform.root.gameObject;

                        Debug.Log("Damage Attacker: " + player.name);

                        Debug.Log("Damage Reciever: " + monster.name);

                    }
                    else if(((Damageable)sender).GetComponent<objectData>().isPlayer)
                    {
                        monster = ((DamageMessage)msg).damager.transform.root.gameObject;

                        player = ((Damageable)sender).gameObject;

                        //EventPlayerHit playerHitEvent = new EventPlayerHit(player.transform.position, ((Damageable)sender).GetComponent<objectData>().objectId);
                        //listEventHits.Add(playerHitEvent);

                        Debug.Log("Damage Attacker: " + monster.name);

                        Debug.Log("Damage Reciever: " + player.name);
                    }
                }
                break;
            case MessageType.DEAD:
                {
                    if (((Damageable)sender).GetComponentInParent<objectData>().isMonster)
                    {
                        monster = ((Damageable)sender).gameObject;

                        player = ((DamageMessage)msg).damager.transform.root.gameObject;

                        //EventMonsterDead eventMonsterDead = new EventMonsterDead(player.transform.position, int playerId, int sessionId, float timestamp, monster.transform.position);

                        //listMonsterDeaths.Add(eventMonsterDead);

                        Debug.Log("Damage Attacker: " + player.name);

                        Debug.Log("DEAD: " + monster.name);
                    }
                    else if (((Damageable)sender).GetComponent<objectData>().isPlayer)
                    {

                        monster = ((DamageMessage)msg).damager.transform.root.gameObject;

                        player = ((Damageable)sender).gameObject;

                        Debug.Log("Damage Attacker: " + monster.name);

                        Debug.Log("DEAD: " + player.name);
                    }



                }


                break;
            case MessageType.RESPAWN:
                break;
            default:
                throw new System.NotImplementedException();
        }
       
    }
}
