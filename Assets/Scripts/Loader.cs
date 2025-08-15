using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    // must have static field in static class 
    public static Scene targetScene; 

    public enum Scene
    {
        MainMenu, 
        GameScene, 
        LoadingScene
    }
    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
        
    }


    //this function is going to be triggered at the first update.
    public static void LoaderCallBack()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }

}
