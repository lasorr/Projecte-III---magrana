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
    public GameObject[] inGameUIElements; // NOU: UI ingame, que es mostra nom�s quan s'inicia el joc

    //[Header("Player Settings")]
    //public List<string> playerTags;

    [Header("Timer Settings")]
    public float initialCountdown = 3f;
    public float gameDuration = 150f;
    
    private bool isGameActive = false;
    private float currentGameTime;
    
    // CANVIAT: Moviment_jugadora en lloc de PlayerMovementController
    private List<Moviment_jugadora> playerScripts = new List<Moviment_jugadora>();
    
    public Colpejar ScriptCop1;
    public Colpejar ScriptCop2;

    public Moviment_jugadora ScriptMoviment1;
    public Moviment_jugadora ScriptMoviment2;
    
    void Start()
    {
        ScriptMoviment1.potMoure = false;
        ScriptMoviment2.potMoure = false;

        SetUIElementsActive(false);

        StartCoroutine(InitialCountdownCoroutine());
    }

    void Update()
    {
        PointsJ1.text = ScriptCop1.edificisTransformatJug1.ToString();
        PointsJ2.text = ScriptCop2.edificisTransformatJug2.ToString();
    }
    
    IEnumerator InitialCountdownCoroutine()
    {
        float remainingTime = initialCountdown;
        
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
        }
        
        if (currentGameTime <= 0)
        {
            EndGame();
        }
    }
    
    void EndGame()
    {
        ScriptMoviment1.potMoure = false;
        ScriptMoviment2.potMoure = false;        
        isGameActive = false;

        Debug.Log("Partida finalitzada! Temps esgotat.");
        
        if (gameTimerText != null)
        {
            gameTimerText.text = "00:00";

            if(ScriptCop1.edificisTransformatJug1>ScriptCop2.edificisTransformatJug2){
                winner.text="GUANYADORA: JUGADORA 1";
                Debug.Log($"GUANYADORA: JUGADORA 1");  
            }

            else if(ScriptCop1.edificisTransformatJug1<ScriptCop2.edificisTransformatJug2){
                winner.text="GUANYADORA: JUGADORA 2";
                Debug.Log($"GUANYADORA: JUGADORA 2");            
            }

            else if(ScriptCop1.edificisTransformatJug1==ScriptCop2.edificisTransformatJug2){
                winner.text="EMPAT";
                Debug.Log($"EMPAT");            
            }
        }
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