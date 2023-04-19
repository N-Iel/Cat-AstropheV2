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

    public class Tutorial
    {
        public const string
            Title = "Mom's Note",
            Spring = "Sweetheart, the house needs a little bit of colour. \r\nCould you bring some flowers from outside ? That would look perfect in the kitchen.\r\n\r\nPtd: You may encounter some mouses, take care please.",
            Summer = "Sweetheart, the weather is drying up the river. Could you keep it clean from rocks and dirt ? We don`t want to run out of water at home.\r\n\r\nP.S.: The ducks could feel attacked if you get to close to the river, watch out please.",
            Autumn = "Sweetheart, the wind is getting stronger, could you bring down the leafs reamining on the trees? I don't want them all over the place at home.\r\n\r\nPtd: The owls may find your intervention... disturbing, take care please.",
            Winter = "Sweetheart, the river is frozen. Could you melt the frozen parts ? With your fur should be enough, just stay a few seconds close to the ice.\r\n\r\nPtd: The deers are hungry, try to avoid them, you can not run from them.";
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
