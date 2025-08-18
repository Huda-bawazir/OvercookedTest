using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        PlayButton.onClick.AddListener(() =>
        {
            //Click
            Loader.Load(Loader.Scene.GameScene);    
        });

        quitButton.onClick.AddListener(() =>
        {
            //Click
            Application.Quit();
        });
        Time.timeScale = 1.0f;
    }


}
