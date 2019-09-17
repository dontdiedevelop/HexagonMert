using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int xTileCount = 8;
    public int yTileCount = 9;

    public bool allRandom = true;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameData gameData;

    //
    [Header("Game")]
    public int gameScore = 0;
    public GameObject hexagonObject;
    public Transform hexagonContent;
    public bool useAsInnerCircleRadius = true;
    public float radius = 0.5f;
    public Vector2 offset;

    [Header("Panels")]
    public GameObject menuPanel;
    public GameObject gameplayPanel;

    private float offsetX;
    private float offsetY;

    //unity operations

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ActivateMenu();
    }

    //ui events

    public void OnPlayButtonClicked()
    {
        gameScore = 0;

        LoadHexagonsWithClear();

        ActivateGameplay();

        //clear hud
    }

    public void OnReturnMenuButtonClicked()
    {
        ActivateMenu();
    }

    //ui operations

    public void ActivateGameplay()
    {
        menuPanel.SetActive(false);
        gameplayPanel.SetActive(true);
    }

    public void ActivateMenu()
    {
        menuPanel.SetActive(true);
        gameplayPanel.SetActive(false);
    }

    //core mechanics

    public void LoadHexagonsWithClear()
    {
        List<GameObject> objectsToDestroy = new List<GameObject>();

        for (int i = 0; i < hexagonContent.childCount; i++)
            objectsToDestroy.Add(hexagonContent.GetChild(i).gameObject);

        LoadHexagons();
    }

    public void LoadHexagons()
    {
        float unitLength = (useAsInnerCircleRadius) ? (radius / (Mathf.Sqrt(3) / 2)) : radius;

        offsetX = unitLength * Mathf.Sqrt(3);
        offsetY = unitLength * 1.5f;

        for (int i = 0; i < gameData.xTileCount; i++)
        {
            for (int j = 0; j < gameData.yTileCount; j++)
            {
                Vector2 hexpos = HexOffset(i, j);
                Vector3 pos = new Vector3(hexpos.x * 128, hexpos.y * 128, 0);
                pos += (Vector3)offset;
                Instantiate(hexagonObject, pos, Quaternion.identity, hexagonContent);
            }
        }

        //for (int y = 0; y < gameData.yTileCount; y++)
        //{
        //    for (int x = 0; x < gameData.xTileCount; x++)
        //    {
        //        GameObject obj = Instantiate(hexagonObject, hexagonContent);
        //        obj.transform.position = new Vector3(x * hexagonSize, y * hexagonSize) + (Vector3)offset;
        //    }
        //}

        OnGameStart();
    }

    Vector2 HexOffset(int x, int y)
    {
        Vector2 position = Vector2.zero;

        if (y % 2 == 0)
        {
            position.x = x * offsetX;
            position.y = y * offsetY;
        }
        else
        {
            position.x = (x + 0.5f) * offsetX;
            position.y = y * offsetY;
        }

        return position;
    }

    //game mechanics

    public void AddScore(int score)
    {
        gameScore += score;
        //update hud
    }

    //game events

    public void OnGameStart()
    {
        //close loading screen etc.
    }
    
    public void OnGamePause()
    {
        //show pause screen etc.
    }

    public void OnGameEnd(bool success)
    {
        if(success)
        {
            Debug.Log("Success");
        }
        else
        {
            Debug.Log("Failed");
        }
    }
}
