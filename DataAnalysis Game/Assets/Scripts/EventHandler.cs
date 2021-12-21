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

    public void OnReceiveMessage(MessageType type, object sender, object msg)
    {
        switch (type)
        {
            case MessageType.DAMAGED:
                {
                    if(((Damageable)sender).GetComponentInParent<objectData>().isMonster)
                    {
                        Debug.Log(((DamageMessage)msg).damager.transform.root.name);
                    }
                    else if(((Damageable)sender).GetComponent<objectData>().isPlayer)
                    {
                        Debug.Log(((DamageMessage)msg).damager.transform.root.name);
                    }
                }
                break;
            case MessageType.DEAD:
                break;
            case MessageType.RESPAWN:
                break;
            default:
                throw new System.NotImplementedException();
        }
       
    }
}
