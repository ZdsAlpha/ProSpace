using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Game {
    public static UserProfile Profile;
    public static bool LoadProfile()
    {
        try
        {
            Profile = JsonUtility.FromJson<UserProfile>(File.ReadAllText(Path.Combine(Constants.ProfilePath,Constants.ProfileFileName)));
            return true;
        }
        catch
        {
            return false;
        }
    }
    public static bool SaveProfile()
    {
        try
        {
            if (!Directory.Exists(Constants.ProfilePath)) Directory.CreateDirectory(Constants.ProfilePath);
            File.WriteAllText(Path.Combine(Constants.ProfilePath, Constants.ProfileFileName), JsonUtility.ToJson(Profile, true));
            return true;
        }
        catch
        {
            return false;
        }
    }
    //Working Space
    public static bool IsCustom = false;
    public static int Level = 1;
    public static int Seed = 0;
    public static int Regions = 50;
}
public class UserProfile
{
    public string Name = "Player";
    public int Seed = 0;
    public float Score = 0f;
    public float Health = 1f;
    public float HealthGeneration = 0f;
    public float Fuel = 1f;
    public float FuelGeneration = 0f;
    public float Boost = 1f;
    public float BoostGeneration = 0f;
    public float AttackDamage = 1f;
    public float AttackFrequency = 1f;
    public float AttackRange = 1f;
    public float JumpHeight = 1f;
    public int Jumps = 1;
    public int Lives = 2;
    public int Level = 1;
}