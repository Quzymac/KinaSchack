using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class SaveFile  {

    public List<int> gameBoardState = new List<int>();
    public bool[] playersActive = new bool[6];
    public int playersTurn;


}
