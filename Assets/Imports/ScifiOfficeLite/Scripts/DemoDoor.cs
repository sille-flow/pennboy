﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScifiOffice {
    public class DemoDoor : MonoBehaviour {
        Animator anim;

        private void Start() {
            anim = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other) {
            return; //for our purposes
            if(other.gameObject.name == "Player") {
                anim.SetTrigger("Open");
            }
        }
    }
}