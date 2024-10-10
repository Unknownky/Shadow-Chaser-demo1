using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 该脚本用于按照一定规则分割文本，以便于在对话框中显示
/// </summary>
public class TextSpilter
{
    public static List<string> SplitText(string text)
    {
        string[] separators = { "<break>", "<finish>" };
        string[] splitText = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        List<string> result = new List<string>();
        //将分割后的文本加上间断符号
        for (int i = 0; i < splitText.Length; i++)
        {
            string sentence = splitText[i];
            //去掉开头的\r和末尾的\r
            sentence = sentence.Trim('\r');
            //去掉开头的\n和末尾的\n
            sentence = sentence.Trim('\n');
            if (i < splitText.Length - 1)
            {
                sentence += "<break>";
            }
            else
            {
                sentence += "<finish>";
            }
            result.Add(sentence);
        }

        return result;
    }

}

