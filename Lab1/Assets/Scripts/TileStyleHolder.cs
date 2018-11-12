using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//add class to inspector
[System.Serializable]
public class TileStyle //describe tile
{
    public int Number;
    public Color32 TileColor;
    public Color32 TextColor;
}

public class TileStyleHolder : MonoBehaviour {
    //use pattern Singleton

    public static TileStyleHolder Instance;

    public TileStyle[] TileStyles;

    void Awake()
    {
        Instance = this;        
    }

}
