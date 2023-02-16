// NOTE: deprecated scripts

// using UnityEngine;
// using UnityEditor;
// using System.IO;


// public static class FolderCreator
// {
//     [MenuItem("Assets/Create/Create Folder/All Files", false, -10)]
//     public static void CreateFolders()
//     {
//         CreateFolder("*.*");
//     }
//
//     [MenuItem("Assets/Create/Create Folder/FBX files", false, -10)]
//     public static void CreateFBXFolders()
//     {
//         CreateFolder("*.fbx");
//     }
//
//     private static void CreateFolder(string fileFormat)
//     {
//         string path = GetClickedDirFullPath() + "/";
//         int count = 0;
//         var info = new DirectoryInfo(path);
//         var files = fileFormat == "*.*" ? Directory.GetFiles(path) : Directory.GetFiles(path, fileFormat);
//         // Debug.Log(path);
//         foreach (var file in files)
//         {
//             if (Path.GetExtension(file) == ".meta") continue;
//             var strs = file.Split('/');
//             var fileName = strs[strs.Length - 1];
//             strs = fileName.Split('.');
//             var destinationPath = path + strs[0];
//             if (!Directory.Exists(destinationPath)) Directory.CreateDirectory(destinationPath);
//             File.Move(file, destinationPath + "/" + fileName);
//             count++;
//         }
//         if (count > 0) Debug.Log(count + " Folders for " + fileFormat + " Created.");
//     }
//
//     private static string GetClickedDirFullPath()
//     {
//         string clickedAssetGuid = Selection.assetGUIDs[0];
//         string clickedPath = AssetDatabase.GUIDToAssetPath(clickedAssetGuid);
//         string clickedPathFull = Path.Combine(Directory.GetCurrentDirectory(), clickedPath);
//
//         FileAttributes attr = File.GetAttributes(clickedPathFull);
//         return attr.HasFlag(FileAttributes.Directory) ? clickedPathFull : Path.GetDirectoryName(clickedPathFull);
//     }
// }
