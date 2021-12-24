# Logger (Moein Unity Package)
<img src="https://github.com/seyedmoeinsaadati/UILogger/blob/main/media/unitylogo.png" align="right" height="50px">
A Simple tools to log data on scene screen in real-time

## Features

  - Log everything (text, properties,...)
  - Default properties (such as fps counter, screen size)

## How to use?

1. Create a C# script:

   ```c#
   public class CustomLogger : MonoBehaviour
   {
       void Start()
       {
       }
   
       void Update()
       {
       }
   }
   ```

2. Implement ILogable interface method.

   ```c#
   public class CustomLogger : MonoBehaviour, ILogable
   {
       void Start()
       {
       }
   
       void Update()
       {
       }
       public void CollectLogData()
       {
           // add whatever you want to UILogger
           // use Logger.Add(string msg) to add data.
           // for example
           // Logger.Add("Object Position " + transform.position.ToString());
           // to Log object position
       }
   }
   ```

3. In your scene, create a canvas and text UI object and add Logger component to canvas object.

4. Play Scene and Press 'Tab' to enable UILogger.

   

   **It is prohibited to sublicense and/or sell copies of this project in stores such as the Unity Asset Store!**
