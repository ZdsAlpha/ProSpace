using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {
    public Text label;
    private void Start()
    {
        label.text = Game.Profile.Level.ToString();
        Game.Level = Game.Profile.Level;
    }
    public void Increase()
    {
        int i = int.Parse(label.text);
        if (i != Game.Profile.Level)
        {
            Game.Level = i + 1;
            label.text = Game.Level.ToString();
        }
    }
    public void Decrease()
    {
        int i = int.Parse(label.text);
        if (i != 1)
        {
            Game.Level = i - 1;
            label.text = Game.Level.ToString();
        }
    }
}
