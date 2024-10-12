using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[Serializable]
public class PlayerSetting
{
    public string Name { get; set; }
    public int Score { get; set; }
    public int TeamScore { get; internal set; }
}
