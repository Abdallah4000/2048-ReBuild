using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TailGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] TailCells;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject Tail;
    [SerializeField] TileState tileState;
    [SerializeField] GameObject GameOverAlert;

    int j = 0;
    int[] index;
    int TileTemValue = 2;
    public bool CanSpown = true;


    public void GeneratTail()
    {
        checkTheStateOfEveryTail();

        int RondomIndex = UnityEngine.Random.Range(0, index.Length);
        int RondomNomber = UnityEngine.Random.Range(0, 100);
        if (RondomNomber > 85)
        {
            TileTemValue = 4;
        }
        else
        {
            TileTemValue = 2;
        }

        if (CanSpown)
        {
            GameObject TailC = Instantiate(Tail);
            TailC.transform.SetParent(TailCells[index[RondomIndex]].transform);
            TailC.transform.position = TailCells[index[RondomIndex]].transform.position;
            TailC.transform.localScale = TailCells[index[RondomIndex]].transform.localScale;
            TailC.gameObject.GetComponent<Tile>().TileValue = TileTemValue;
            TailC.gameObject.GetComponent<Tile>().TileColor = tileState.setColorsWiteValue(TileTemValue);
            TailC.gameObject.GetComponent<Tile>().TextColor = tileState.setTextColorsWiteValue(TileTemValue);

            gameManager.ScoreManeger(TileTemValue);
            TailC.gameObject.GetComponent<Tile>().SetTileValue();

            TailCells[index[RondomIndex]].GetComponent<TileCell>().isItEmpety = false;

            
        }
        if (j == 1)
        {
            if (gameManager.CheckIfTheGameIsEnd())
            {
                Debug.Log("Game Over");
            }
        }

    }

    public void checkTheStateOfEveryTail()
    {
        j = 0;
        for (int i = 0; i < TailCells.Length; i++)
        {
            if (TailCells[i].GetComponent<TileCell>().isItEmpety)
            {
                j++;
            }
        }
        if (j == 0 )
        {
            CanSpown = false;
            if(gameManager.CheckIfTheGameIsEnd())
            {
                GameOverAlert.SetActive(true);
            }
            return;
        }
        else if (j > 0) 
        {
            CanSpown = true;
        }
        index = new int[j];
        j = 0;
        for (int i = 0; i < TailCells.Length; i++)
        {
            if (TailCells[i].GetComponent<TileCell>().isItEmpety)
            {
                index[j] = i;
                j++;
            }
        }
    }




}
