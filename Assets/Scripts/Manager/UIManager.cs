using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text TextHeader;
    public Text TextContent;

    public string Header
    {
        get
        {
            return TextHeader.text;
        }
        set
        {
            TextHeader.text = value;
        }
    }

    public string Content
    {
        get
        {
            return TextContent.text;
        }
        set
        {
            TextContent.text = value;
        }
    }

}