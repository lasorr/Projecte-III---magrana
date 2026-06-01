using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text countdownText;
    public TMP_Text gameTimerText;
    public TMP_Text winner;
    public TMP_Text PointsJ1;
    public TMP_Text PointsJ2;
    public GameObject PointsContainer;
    public GameObject[] inGameUIElements; // NOU: UI ingame, que es mostra nomes quan s'inicia el joc
    public GameObject winnerScreen;

    //[Header("Player Settings")]
    //public List<string> playerTags;

    [Header("Timer Settings")]
    public float initialCountdown = 3f;
    public float gameDuration = 300f;
    public float winnerScreenDuration = 5f;
    
    private bool isGameActive = false;
    private float currentGameTime;
    private float scoreFinalPosY = 35f;
    
    // CANVIAT: Moviment_jugadora en lloc de PlayerMovementController
    private List<Moviment_jugadora> playerScripts = new List<Moviment_jugadora>();

    public int edificisTransformatJug1 = 0;
    public int edificisTransformatJug2 = 0;

    public Moviment_jugadora ScriptMoviment1;
    public Moviment_jugadora ScriptMoviment2;
    public NivellCompletat ScriptNivellCompletat;
    public FXsManager ScriptFXsManager;

    public static TimeManager Instance;
    private RectTransform rect;

    void Awake()
    {
        Instance = this;
        ScriptFXsManager = GameObject.FindGameObjectWithTag("SFX").GetComponent<FXsManager>();
    }
    
    void Start()
    {
        ScriptMoviment1.potMoure = false;
        ScriptMoviment2.potMoure = false;

        rect = PointsContainer.GetComponent<RectTransform>();

        SetUIElementsActive(false);
         winnerScreen.SetActive(false);

        StartCoroutine(InitialCountdownCoroutine());
    }

    void Update()
    {
        PointsJ1.text = edificisTransformatJug1.ToString();
        PointsJ2.text = edificisTransformatJug2.ToString();
    }
    
    IEnumerator InitialCountdownCoroutine()
    {
        float remainingTime = initialCountdown;
        ScriptFXsManager.sfxSource.PlayOneShot(ScriptFXsManager.initialCountdownClip);
        
        while (remainingTime > 0)
        {
            if (countdownText != null)
            {
                countdownText.text = Mathf.CeilToInt(remainingTime).ToString();
                countdownText.gameObject.SetActive(true);
            }
            
            yield return new WaitForSeconds(1f);
            remainingTime--;
        }
        
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
        }
        
        StartGame();
    }
    
    void StartGame()
    {
        ScriptMoviment1.potMoure = true;
        ScriptMoviment2.potMoure = true;
        isGameActive = true;

        SetUIElementsActive(true); // NOU

        currentGameTime = gameDuration;
        StartCoroutine(GameTimerCoroutine());
    }
    
    IEnumerator GameTimerCoroutine()
    {
        bool warningPlayed = false;
        while (isGameActive && currentGameTime > 0)
        {
            if (gameTimerText != null)
            {
                int minutes = Mathf.FloorToInt(currentGameTime / 60);
                int seconds = Mathf.FloorToInt(currentGameTime % 60);
                gameTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            yield return new WaitForSeconds(1f);
            currentGameTime--;
            if (currentGameTime <= 30f && !warningPlayed)
        {
            Debug.Log("Queden 30 segundos");

            if (ScriptFXsManager != null)
            {
                ScriptFXsManager.PlayTimeWarning();
            }
            warningPlayed = true; // Evitar que suene múltiples veces
        }
        }
        if (currentGameTime <= 0)
        {
            EndGame();
        }
    }

    public void RestaPunts(int jugadora, int punts)
    {
        if (jugadora == 1)
        {
            edificisTransformatJug1-= punts;
        }
        else if (jugadora == 2)
        {
            edificisTransformatJug2-= punts;
        }
    }
    
    void EndGame()
    {
        ScriptMoviment1.potMoure = false;
        ScriptMoviment2.potMoure = false;        
        isGameActive = false;
        winnerScreen.SetActive(true);

        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, scoreFinalPosY);

        if(currentGameTime <= 30f)
        {
            ScriptFXsManager.PlayTimeWarning();
        }
        
        if (gameTimerText != null)
        {
            gameTimerText.text = "00:00";

            if(edificisTransformatJug1>edificisTransformatJug2){
                winner.text="GUANYA J1";
                Debug.Log($"GUANYADORA: JUGADORA 1");  
            }

            else if(edificisTransformatJug1<edificisTransformatJug2){
                winner.text="GUANYA J2";
                Debug.Log($"GUANYADORA: JUGADORA 2");            
            }

            else if(edificisTransformatJug1==edificisTransformatJug2){
                winner.text="EMPAT";
                Debug.Log($"EMPAT");            
            }
            ScriptFXsManager.sfxSource.PlayOneShot(ScriptFXsManager.winnerClip);
        }
        StartCoroutine(waitAndReturnToLevelSelect(winnerScreenDuration));
    }

    IEnumerator waitAndReturnToLevelSelect(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ScriptNivellCompletat.DesbloquejarSeguent();
    }

    // NOVA FUNCI�:
    private void SetUIElementsActive(bool active)
    {
        if (inGameUIElements != null)
        {
            foreach (GameObject uiElement in inGameUIElements)
            {
                if (uiElement != null)
                {
                    uiElement.SetActive(active);
                }
            }
        }
    }
}