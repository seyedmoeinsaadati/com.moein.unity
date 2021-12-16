using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;
using Moein.Core;

namespace Moein.Recorder
{
    public class UnityFileHandler : Singleton<UnityFileHandler>
    {
        public List<T> Load<T>(string directory, string fileName)
        {
            directory += "/";
            string fname = Constants.MAIN_DIRECTORY + directory + fileName + Constants.FILE_FORMAT;
            try
            {
                TextAsset tobjFile = Resources.Load<TextAsset>(fname) as TextAsset;
                Stream s = new MemoryStream(tobjFile.bytes);
                BinaryFormatter bformatter = new BinaryFormatter();
                Debug.Log("File Loaded Succussfully. from " + Constants.ASSETS_PATH + Constants.MAIN_DIRECTORY + directory + fileName);
                return (List<T>)bformatter.Deserialize(s);
            }
            catch (Exception ex)
            {
                Debug.LogError("File Loaded Failed. from " + Constants.ASSETS_PATH + Constants.MAIN_DIRECTORY + directory + fileName + "\n" + ex.Message);
                return new List<T>();
            }
        }
    }
}