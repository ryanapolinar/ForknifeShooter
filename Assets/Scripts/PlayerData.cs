using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    private static int health;

    public static int getHealth()
    {
        return health;
    }

    public static void setHealth(int newHealth)
    {
        health = newHealth;
    }
}
