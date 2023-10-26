
using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    static string sPath = $"{Application.persistentDataPath}/";

    
    //Creates a save data file assosiated with the data type T
    public static void SaveData<T>(T SaveData) where T : new()
    {
        
        //Binary Formatting
        BinaryFormatter formatter = new BinaryFormatter();
        //create file stream
        FileStream stream = new FileStream(sPath + $"{typeof(T).ToString()}.Dat", FileMode.Create);
        //create as json
        string sJSONString = JsonUtility.ToJson(SaveData);
        
        //serialize the data
        formatter.Serialize(stream, sJSONString);
        stream.Close();

        if (File.Exists($"{sPath}{typeof(T).ToString()}.Dat"))
        {
            Debug.Log($"Saved in file {typeof(T).ToString()}.Dat");
        }
        else
        {
            Debug.Log($"Created save file {typeof(T).ToString()}.Dat");
        }
    }

    //Returns the dat from the file assosiated with that data name
    //example: SaveData data = SaveSystem.LoadData<SaveData>();
    public static T LoadData<T>()
    {
        //get datatype name
        string sDataTypeName = typeof(T).ToString();

        //check if file exsists
        if (File.Exists($"{sPath}{typeof(T).ToString()}.Dat"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(sPath + $"{typeof(T).ToString()}.Dat", FileMode.Open);

            //create as json string
            string sJSONString = formatter.Deserialize(stream) as string;
            
            //close the stream
            stream.Close();
            
            //return the data from the unserialised data
            return JsonUtility.FromJson<T>(sJSONString);
        }
        else
        {
            Debug.LogError($"{sDataTypeName}.Dat not found in: " + sPath);
            //return null
            return default(T);

        }

        #region OldSave

        // private static string sBankFileName = "Bank";
        // private static string sVehiclePartFileName = "Vehicle";
        //player
        // public static void SaveBank()
        // {
        //     //Binary Formatting
        //     BinaryFormatter formatter = new BinaryFormatter();
        //     //create file stream
        //     FileStream stream = new FileStream(sPath + $"{sBankFileName}.Dat", FileMode.Create);
        //     //create a bank data cache
        //     BankData data = new BankData( BankManager.Instance.iMoney);
        //     //serialize the data
        //     formatter.Serialize(stream, data);
        //     stream.Close();
        //
        //     Debug.Log($"Created save file{sBankFileName}.Dat");
        // }
        // public static BankData LoadBank()
        // {
        //     if (File.Exists(sPath + sBankFileName + ".Dat"))
        //     {
        //         BinaryFormatter formatter = new BinaryFormatter();
        //         FileStream stream = new FileStream(sPath + $"{sBankFileName}.Dat", FileMode.Open);
        //
        //         BankData data = formatter.Deserialize(stream) as BankData;
        //         stream.Close();
        //         Debug.Log(data.iMoney);
        //         return data;
        //     }
        //     else
        //     {
        //         Debug.LogError($"{sBankFileName}.Dat not found in: " + sPath);
        //         return null;
        //
        //     }
        //
        // }


        #endregion
    }

}

[Serializable]
class VehiclePartData
{
    public bool[] VehicleParts = new bool[4];
};