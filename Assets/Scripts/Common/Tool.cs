using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************************************************************
����:Mizer  
����:2807665129@qq.com

����:������
*************************************************************/

public class Tool 
{
    public static int RDInt(int min, int max, System.Random random = null)
    {
        if (random == null)
        {
            random = new System.Random();
        }
        int val = random.Next(min, max + 1);
        return val;
    }
}
