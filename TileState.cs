using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileState : MonoBehaviour
{
    [SerializeField] Color c2;
    [SerializeField] Color c4;
    [SerializeField] Color c8;
    [SerializeField] Color c16;
    [SerializeField] Color c32;
    [SerializeField] Color c64;
    [SerializeField] Color c128;
    [SerializeField] Color c256;
    [SerializeField] Color c512;
    [SerializeField] Color c1024;
    [SerializeField] Color c2048;
    [SerializeField] Color cAbove2048;

    [SerializeField] Color ct2;
    [SerializeField] Color ct4;
    [SerializeField] Color ct8;
    [SerializeField] Color ct16;
    [SerializeField] Color ct32;
    [SerializeField] Color ct64;
    [SerializeField] Color ct128;
    [SerializeField] Color ct256;
    [SerializeField] Color ct512;
    [SerializeField] Color ct1024;
    [SerializeField] Color ct2048;
    [SerializeField] Color ctAbove2048;

    public Color setColorsWiteValue(int value)
    {
        switch (value)
        {
            case 2:
                return c2;
            case 4:
                return c4;
            case 8:
                return c8;
            case 16:
                return c16;
            case 32:
                return c32;
            case 64:
                return c64;
            case 128:
                return c128;
            case 256:
                return c256;
            case 512:
                return c512;
            case 1024:
                return c1024;
            case 2048:
                return c2048;

            default:
                return cAbove2048;
        }
    }

    public Color setTextColorsWiteValue(int value)
    {
        switch (value)
        {
            case 2:
                return ct2;
            case 4:
                return ct4;
            case 8:
                return ct8;
            case 16:
                return ct16;
            case 32:
                return ct32;
            case 64:
                return ct64;
            case 128:
                return ct128;
            case 256:
                return ct256;
            case 512:
                return ct512;
            case 1024:
                return ct1024;
            case 2048:
                return ct2048;

            default:
                return ctAbove2048;
        }
    }

}
