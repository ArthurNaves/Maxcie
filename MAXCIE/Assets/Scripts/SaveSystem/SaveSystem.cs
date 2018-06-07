using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem {
    public struct SaveInfo
    {
        public Vector3 playerPos;

        public bool[] spellsLearned;
        public int playerHp;
        public int currentLevel;

        public SaveInfo(Vector3 playerPosition, bool[] spells, int hp, int level)
        {
            playerPos = playerPosition;
            spellsLearned = spells;
            playerHp = hp;
            currentLevel = level;
        }

        public static bool operator !=(SaveInfo a, SaveInfo b)
        {
            if (a.playerHp != b.playerHp) return true;
            if (a.currentLevel != b.currentLevel) return true;

            if (a.spellsLearned.Length != b.spellsLearned.Length) return true;
            else
            {
                for(int i = 0; i < a.spellsLearned.Length; i++)
                {
                    if (a.spellsLearned[i] != b.spellsLearned[i]) return true;
                }
            }
            return false;
        }
        public static bool operator ==(SaveInfo a, SaveInfo b)
        {
            if (a.playerHp != b.playerHp) return false;
            if (a.currentLevel != b.currentLevel) return false;

            if (a.spellsLearned.Length != b.spellsLearned.Length) return false;
            else
            {
                for (int i = 0; i < a.spellsLearned.Length; i++)
                {
                    if (a.spellsLearned[i] != b.spellsLearned[i]) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Não usar, apenas dei override para o VS parar de xolar;
        /// </summary>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Não usar, apenas dei override para o VS parar de xolar;
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            throw new System.NotImplementedException();
        }
    }

    const string pathToSaveFolder = "Assets/Saves/playerSave.txt";

    static float ConvertStringToNum(string stringToRead, ref short stringIndex)
    {
        string tempNumString = string.Empty;
        char readingChar;
        float tempFloat;

        readingChar = stringToRead[stringIndex];
        while (readingChar != ',')
        {
            tempNumString += readingChar;
            readingChar = stringToRead[++stringIndex];
        }
        stringIndex++;

        if (float.TryParse(tempNumString, out tempFloat)) return tempFloat;
        else Debug.LogError("Nao foi possivel ler float");

        return 0;
    }

    static bool ConvertStringToBoolArr(string stringToRead, ref short stringIndex)
    {
        string tempBoolString = string.Empty;
        char readingChar;

        readingChar = stringToRead[stringIndex];
        while (readingChar != ',')
        {
            tempBoolString += readingChar;
            readingChar = stringToRead[++stringIndex];
        }
        stringIndex++;

        if (tempBoolString == "True") return true;
        else if (tempBoolString == "False") return false;
        else Debug.LogError("Nao foi possivel ler bool");

        return false;
    }


    public static void SaveWriter(SaveInfo saveInfo)
    {
        string toPrint = saveInfo.playerPos.x + "," + saveInfo.playerPos.y + "," + saveInfo.playerPos.z + "," + saveInfo.playerHp + "," + saveInfo.currentLevel;
        foreach (bool i in saveInfo.spellsLearned) toPrint += "," + i;
        toPrint += ",";
        File.WriteAllText(pathToSaveFolder, toPrint);
    }

    public static SaveInfo SaveReader()
    {
        Vector3 tempVector;

        bool[] spellsLearned = new bool[4];
        string toRead;
        string tempNumString = string.Empty;
        short stringIndex = 0;
        int playerHp;
        int level;

        toRead = File.ReadAllText(pathToSaveFolder);

        tempVector.x = ConvertStringToNum(toRead, ref stringIndex);
        tempVector.y = ConvertStringToNum(toRead, ref stringIndex);
        tempVector.z = ConvertStringToNum(toRead, ref stringIndex);

        playerHp = (int)ConvertStringToNum(toRead, ref stringIndex);
        level = (int)ConvertStringToNum(toRead, ref stringIndex);

        for (int i = 0; i < spellsLearned.Length; i++)
        {
            spellsLearned[i] = ConvertStringToBoolArr(toRead, ref stringIndex);
        }

        return new SaveInfo(tempVector, spellsLearned, playerHp, level);
    }
}
