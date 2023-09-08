using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public int TileValue;
    public Color TileColor;
    public Color TextColor;

    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] Image Image;

    public void SetTileValue()
    {
        Image.GetComponent<Image>().color = TileColor;
        textMeshProUGUI.text = TileValue.ToString();
        textMeshProUGUI.color = TextColor;
    }
    
}
