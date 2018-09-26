using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class SaveGame : MonoBehaviour {


    BoardController boardController;


    // Use this for initialization
    void Start () {
        boardController = FindObjectOfType<BoardController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();

        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();

        }
    }



    void Save()
    {

        SaveFile saveFile = new SaveFile();


        for (int i = 0; i < boardController.GetRows; i++)
        {
            for (int j = 0; j < boardController.GetColumns; j++)
            {
                if(boardController.Matris[i,j] != null)
                {
                    saveFile.gameBoardState.Add((int)boardController.Matris[i,j].state);
                }
            }
        }

        saveFile.playersActive = boardController.playersActive;
        saveFile.playersTurn = boardController.PlayerTurn;
        
        BinaryFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(Application.persistentDataPath + "/savefile.dat", FileMode.Create);
        formatter.Serialize(stream, saveFile);
        stream.Close();

    }

    void Load()
    {
        SaveFile loadFile;
        BinaryFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(Application.persistentDataPath + "/savefile.dat", FileMode.Open);
        loadFile = (SaveFile)formatter.Deserialize(stream);
        stream.Close();

        int counter = 0;
        for (int i = 0; i < boardController.GetRows; i++)
        {
            for (int j = 0; j < boardController.GetColumns; j++)
            {
                if (boardController.Matris[i, j] != null)
                {
                    boardController.Matris[i, j].SetState(loadFile.gameBoardState[counter]);
                    counter++;
                }
            }
        }
        boardController.playersActive = loadFile.playersActive;
        boardController.PlayerTurn = loadFile.playersTurn;


    }

}
