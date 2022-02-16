using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game1 : MonoBehaviour
{
    public static Game1 instance;
    private int points = 0;
    public TextMeshPro text;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying this instance");
            Destroy(this);
        }
    }

    public void AddPoints(int amount)
    {
        Points += amount;
    }
    public int Points
    {
        set {
            points = value;
            text.text = "Points:"+points;
        }
        get { return points; }
    }
}
