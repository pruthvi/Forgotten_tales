using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialogue {

    public string Name = "Dialogue";
    public string TextDescription;
    public AudioClip AudioDescription;
    public Option[] Options;
}
