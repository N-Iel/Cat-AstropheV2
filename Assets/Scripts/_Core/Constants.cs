using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public class Animations
    {
        public const string dash = "dash",
                            idle = "idle",
                            walk = "walk",
                            hit = "hit",
                            dead = "dead",
                            attack = "attack",
                            exhausted = "exhausted",
                            recovered = "recovered",
                            invincible = "invincible";
    }

    public class States
    {
        public const string detecting = "detecting",
                            following = "following",
                            attacking = "attacking",
                            recovering = "recovering";
    }
    
}