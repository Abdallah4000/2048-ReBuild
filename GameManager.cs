using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.Rendering.DebugUI;
using TMPro;
using UnityEngine.UIElements;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] AllTileCells;
    [SerializeField] TailGenerator tailGenerator;
    [SerializeField] TileState tileState;

    [SerializeField] GameObject[] VerticalCells1;
    [SerializeField] GameObject[] VerticalCells2;
    [SerializeField] GameObject[] VerticalCells3;
    [SerializeField] GameObject[] VerticalCells4;

    [SerializeField] GameObject[] VerticalCells11;
    [SerializeField] GameObject[] VerticalCells22;
    [SerializeField] GameObject[] VerticalCells33;
    [SerializeField] GameObject[] VerticalCells44;

    [SerializeField] GameObject[] HorizontalCells1;
    [SerializeField] GameObject[] HorizontalCells2;
    [SerializeField] GameObject[] HorizontalCells3;
    [SerializeField] GameObject[] HorizontalCells4;

    [SerializeField] GameObject[] HorizontalCells11;
    [SerializeField] GameObject[] HorizontalCells22;
    [SerializeField] GameObject[] HorizontalCells33;
    [SerializeField] GameObject[] HorizontalCells44;

    GameObject ReciverA;
    GameObject ReciverB;
    GameObject ReciverC;

    int j = 1;
    int ReciverBIndex = 0;
    int ReciverCIndex = 0;

    int gameScore = 0;
    int highScore = 0;

    bool GameIsOver = false;
    [SerializeField] float speed = .1f;

    [SerializeField] TextMeshProUGUI textHighScore;
    [SerializeField] TextMeshProUGUI textScore;


    private void Awake()
    {
        highScore = PlayerPrefs.GetInt("highScore");
        textHighScore.text = highScore.ToString();
    }
    private void Start()
    {
        tailGenerator.GeneratTail();
        tailGenerator.GeneratTail();
        highScore = PlayerPrefs.GetInt("highScore");
        textHighScore.text = highScore.ToString();

    }

    public void ScoreManeger( int score)
    {
        gameScore += score;
        textScore.text = gameScore.ToString();

        if (gameScore > highScore)
        {
            highScore = gameScore;
            PlayerPrefs.SetInt("highScore", highScore);
            textHighScore.text = highScore.ToString();
        }


    }

    public void Right()
    {

        MoveAndMerge(HorizontalCells1);
        MoveAndMerge(HorizontalCells2);
        MoveAndMerge(HorizontalCells3);
        MoveAndMerge(HorizontalCells4);

        tailGenerator.GeneratTail();

    }

    public void Left()
    {
        MoveAndMerge(HorizontalCells11);
        MoveAndMerge(HorizontalCells22);
        MoveAndMerge(HorizontalCells33);
        MoveAndMerge(HorizontalCells44);

        tailGenerator.GeneratTail();
    }

    public void Up()
    {
        MoveAndMerge(VerticalCells11);
        MoveAndMerge(VerticalCells22);
        MoveAndMerge(VerticalCells33);
        MoveAndMerge(VerticalCells44);

        tailGenerator.GeneratTail();
    }


    public void Down()
    {
        MoveAndMerge(VerticalCells1);
        MoveAndMerge(VerticalCells2);
        MoveAndMerge(VerticalCells3);
        MoveAndMerge(VerticalCells4);

        tailGenerator.GeneratTail();
    }



    public void MoveAndMerge(GameObject[] Row)
    {
        EmptyAllVallues();

        for (int i = 0; i < Row.Length; i++)
        {

            // if the cell is empty
            j++;
            
            if (Row[Row.Length - j].gameObject.GetComponent<TileCell>().isItEmpety)
            {
                int x = Row.Length - j;
                int y = Row.Length - 1;
                
                // if it's first cell and empty
                if ( x == y)
                {
                    ReciverA = Row[Row.Length - j];
                    continue;
                }
                // if it's empty and also the cell befor it
                else if (Row[Row.Length - (j - 1)].gameObject.GetComponent<TileCell>().isItEmpety)
                {
                    continue;
                }
                // if it's empty but the cell befor it not
                else if (!Row[Row.Length - (j - 1)].gameObject.GetComponent<TileCell>().isItEmpety)
                {
                    ReciverC = Row[Row.Length - j];
                    ReciverCIndex = Row.Length - j;
                    ReciverB = Row[Row.Length - (j - 1)];
                    ReciverBIndex = Row.Length - (j - 1);
                    continue;
                }

            }

            // if the cell is not empty

            else if (!Row[Row.Length - j].gameObject.GetComponent<TileCell>().isItEmpety)
            {
                int x = Row.Length - j;
                int y = Row.Length - 1;

                // if it's first cell and not empty
                if (x == y)
                {
                    ReciverB = Row[Row.Length - j];
                    ReciverBIndex = Row.Length - j;
                    ReciverA = null;
                    continue;
                }

                // if it's not empty and the first cell is empty
                else if (ReciverA != null)
                {

                    GameObject TheSender = Row[Row.Length - j];
                    GameObject TheTail = Row[Row.Length - j].gameObject.transform.GetChild(0).gameObject;

                    ReciverC = Row[Row.Length - 2]; ;
                    ReciverCIndex = Row.Length - 2;
                    ReciverB = ReciverA;
                    ReciverBIndex = Row.Length - 1;

                    MoveToTheReciver(ReciverA, TheTail , TheSender );
                    ReciverA = null;
                    continue;
                }

                // if it's not empty and also the cell befor it
                else if (ReciverB != null)
                {

                    GameObject TheSender = Row[Row.Length - j];
                    GameObject TheSenderTail = Row[Row.Length - j].gameObject.transform.GetChild(0).gameObject;
                    GameObject TheReciverTail = ReciverB.gameObject.transform.GetChild(0).gameObject;

                    // if they have the same value
                    if (TheSenderTail.gameObject.GetComponent<Tile>().TileValue ==
                        TheReciverTail.gameObject.GetComponent<Tile>().TileValue && 
                        ReciverB.gameObject.GetComponent<TileCell>().CanMarge) 
                    {
                        ReciverB.gameObject.GetComponent<TileCell>().CanMarge = false;

                        ScoreManeger(TheReciverTail.gameObject.GetComponent<Tile>().TileValue);

                        TheReciverTail.gameObject.GetComponent<Tile>().TileValue += 
                            TheSenderTail.gameObject.GetComponent<Tile>().TileValue;


                        TheReciverTail.gameObject.GetComponent<Tile>().TileColor = 
                            tileState.setColorsWiteValue(TheReciverTail.gameObject.GetComponent<Tile>().TileValue);

                        TheReciverTail.gameObject.GetComponent<Tile>().TextColor =
                            tileState.setTextColorsWiteValue(TheReciverTail.gameObject.GetComponent<Tile>().TileValue);



                        TheReciverTail.gameObject.GetComponent<Tile>().SetTileValue();


                        MoveToTheReciver(ReciverB, TheReciverTail, TheSender) ;
                        ReciverC = Row[ReciverBIndex - 1];
                        ReciverCIndex = ReciverBIndex - 1;

                        Destroy(TheSenderTail);

                    }

                    // if they have the diffrent values
                    else if (TheSenderTail.gameObject.GetComponent<Tile>().TileValue !=
                        TheReciverTail.gameObject.GetComponent<Tile>().TileValue || 
                        (TheSenderTail.gameObject.GetComponent<Tile>().TileValue ==
                        TheReciverTail.gameObject.GetComponent<Tile>().TileValue &&
                        !ReciverB.gameObject.GetComponent<TileCell>().CanMarge))
                    {

                        if (ReciverC != null)
                        {
                            MoveToTheReciver(ReciverC, TheSenderTail, TheSender);

                            if((Row.Length - j) != 0)
                            {
                                ReciverB = ReciverC;
                                ReciverBIndex = ReciverCIndex;
                                ReciverC = Row[ReciverBIndex - 1];
                                ReciverCIndex = ReciverBIndex - 1;
                            }
                            
                        }
                        else if ((Row.Length - j) == 0)
                        {
                            ReciverA = null;
                            ReciverB = null;
                            ReciverC = null;

                            continue;

                        }
                        else 
                        {
                            ReciverB = Row[Row.Length - j];
                            ReciverBIndex = Row.Length - j;
                        }

                       

                    }
                }
            }

        }


    }

    void EmptyAllVallues()
    {
        j = 0;
        ReciverA = null ;
        ReciverB = null;
        ReciverC= null;

        for (int i = 0; i < AllTileCells.Length; i++)
        {
            AllTileCells[i].gameObject.GetComponent<TileCell>().CanMarge = true;
        }
    }


    void MoveToTheReciver(GameObject Reciver , GameObject TheRecivedTail , GameObject Sender)
    {
        TheRecivedTail.transform.SetParent(Reciver.transform);
        //TheRecivedTail.transform.position = Reciver.transform.position;
        TheRecivedTail.transform.DOMove(Reciver.transform.position, speed);
        TheRecivedTail.transform.localScale = Reciver.transform.localScale;

        Reciver.GetComponent<TileCell>().isItEmpety = false;
        Sender.GetComponent<TileCell>().isItEmpety = true;
    }

    public void ResetTheGame()
    {
        gameScore = 0;

        for (int i = 0; i < AllTileCells.Length; i++)
        {
            if(AllTileCells[i].transform.childCount > 0)
            {
                Destroy(AllTileCells[i].transform.GetChild(0).gameObject);
                AllTileCells[i].gameObject.GetComponent<TileCell>().isItEmpety = true;
                AllTileCells[i].gameObject.GetComponent<TileCell>().CanMarge = true;
            }

        }

        tailGenerator.GeneratTail();
        tailGenerator.GeneratTail();

        textHighScore.text = highScore.ToString();

    }




    public bool CheckIfTheGameIsEnd()
    {

        for (int i = 0; i < (VerticalCells1.Length - 1); i++)
        {
            if (VerticalCells1[i].gameObject.GetComponentInChildren<Tile>().TileValue ==
                VerticalCells1[i + 1].gameObject.GetComponentInChildren<Tile>().TileValue)
            {
                GameIsOver = false; return false;
            }


            else if (VerticalCells2[i].gameObject.GetComponentInChildren<Tile>().TileValue ==
                VerticalCells2[i + 1].gameObject.GetComponentInChildren<Tile>().TileValue)
            {
                GameIsOver = false; return false;
            }

            else if (VerticalCells3[i].gameObject.GetComponentInChildren<Tile>().TileValue ==
                VerticalCells3[i + 1].gameObject.GetComponentInChildren<Tile>().TileValue)
            {
                GameIsOver = false; return false;
            }

            else if (VerticalCells4[i].gameObject.GetComponentInChildren<Tile>().TileValue ==
                VerticalCells4[i + 1].gameObject.GetComponentInChildren<Tile>().TileValue)
            {
                GameIsOver = false; return false;
            }


            else if (HorizontalCells1[i].gameObject.GetComponentInChildren<Tile>().TileValue ==
                HorizontalCells1[i + 1].gameObject.GetComponentInChildren<Tile>().TileValue)
            {
                GameIsOver = false; return false;
            }

            else if (HorizontalCells2[i].gameObject.GetComponentInChildren<Tile>().TileValue ==
                HorizontalCells2[i + 1].gameObject.GetComponentInChildren<Tile>().TileValue)
            {
                GameIsOver = false; return false;
            }

            else if (HorizontalCells3[i].gameObject.GetComponentInChildren<Tile>().TileValue ==
                HorizontalCells3[i + 1].gameObject.GetComponentInChildren<Tile>().TileValue)
            {
                GameIsOver = false; return false;
            }

            else if (HorizontalCells4[i].gameObject.GetComponentInChildren<Tile>().TileValue ==
                HorizontalCells4[i + 1].gameObject.GetComponentInChildren<Tile>().TileValue)
            {
            GameIsOver = false; return false;
            }
        }

        return true;
    }

}

