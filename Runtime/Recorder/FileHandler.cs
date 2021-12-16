using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

namespace Moein.Recorder
{
    public static class FileHandler
    {
        public static void Save<T>(string directory, string fileName, List<T> list)
        {
            // check directory
            if (!Directory.Exists(Constants.ASSETS_PATH + Constants.MAIN_DIRECTORY + directory))
            {
                Directory.CreateDirectory(Constants.ASSETS_PATH + Constants.MAIN_DIRECTORY + directory);
            }
            else
            {
                Debug.Log("Directory exists.");
            }

            directory += "/";
            string fname = Constants.ASSETS_PATH + Constants.MAIN_DIRECTORY + directory + fileName + Constants.FILE_FORMAT;
            try
            {
                FileStream fs = new FileStream(fname, FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, list);
                fs.Close();
                Debug.Log("File Saved Succussfully. in" + Constants.MAIN_DIRECTORY);
            }
            catch (Exception)
            {
                Debug.LogError("File Saved Failed. from" + Constants.MAIN_DIRECTORY);
            }
        }

        public static List<T> Load<T>(string directory, string fileName)
        {
            directory += "/";
            string fname = Constants.MAIN_DIRECTORY + directory + fileName + Constants.FILE_FORMAT;
            TextAsset tobjFile = Resources.Load<TextAsset>(fname) as TextAsset;
            try
            {
                Stream s = new MemoryStream(tobjFile.bytes);
                BinaryFormatter bformatter = new BinaryFormatter();
                Debug.Log("File Loaded Succussfully. from Assets/" + directory + fileName);
                return (List<T>)bformatter.Deserialize(s);
            }
            catch (Exception ex)
            {
                Debug.LogError("File Loaded Failed. from Assets/" + directory + fileName + "\n" + ex.Message);
                return new List<T>();
            }
        }
    }
}