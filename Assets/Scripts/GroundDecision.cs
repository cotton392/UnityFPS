using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDecision : MonoBehaviour{
    GameObject player;
    PlayerController pc;

    void Start(){
        player = transform.root.gameObject;
        pc = player.GetComponent<PlayerController>();
    }

    void OnTriggerStay(Collider other){
        pc.GroundDecitionTrue();
    }

    void OnTriggerExit(Collider other){
        pc.GroundDecitionFalse();
    }
}
