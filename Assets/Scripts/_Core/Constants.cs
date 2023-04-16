using System;
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
                            reset = "reset",
                            attack = "attack",
                            exhausted = "exhausted",
                            recovered = "recovered",
                            invincible = "invincible";
    }

    public enum Enemies
    {
        Owl,
        Mouse,
        Deer,
        Duck
    }

    public enum Seasons
    {
        Default,
        Spring,
        Autumn,
        Winter,
        Summer,
        Dark,
    }

    public enum newAnimations
    {
        dash,
        idle,
        walk,
        hit,
        dead,
        reset,
        attack,
        exhausted,
        recovered,
        invincible
    }

    public enum States
    {
        None,
        pasive,
        aggressive,
        exhausted,
        dead
    };
}
