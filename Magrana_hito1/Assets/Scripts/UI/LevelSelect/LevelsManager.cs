using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    private int tutorialLevelIndex = 3;

    public void LoadTutorialLevel()
    {
        SceneManager.LoadScene(tutorialLevelIndex);
    }
}
