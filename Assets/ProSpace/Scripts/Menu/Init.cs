using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Init : MonoBehaviour {
    public FadePanel fadePanel;
    public Text scoresLabel;
    public float lerpRate = 2f;

    private float scores = 0f;

	void Start () {
        if (!Game.LoadProfile())
        {
            Game.Profile = new UserProfile();
        }
        fadePanel.FadeOut();
	}
	void Update () {
        scoresLabel.text = "Scores: " + (Mathf.Ceil(scores)).ToString();
        RenderSettings.skybox.SetFloat("_Rotation", Time.time / 10);
    }
    void FixedUpdate()
    {
        scores = Mathf.Lerp(scores, Game.Profile.Score, Time.deltaTime * lerpRate);
    }

    public void Continue()
    {
        SceneManager.LoadScene(1);
    }
    public void NewGame()
    {
        Game.Profile = new UserProfile();
        Game.Profile.Seed = Random.Range(int.MinValue, int.MaxValue);
        Game.SaveProfile();
        Game.Level = 1;
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
