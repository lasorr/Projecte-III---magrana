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
    
    [Header("Player Settings")]
    public List<string> playerTags;
    
    [Header("Timer Settings")]
    public float initialCountdown = 3f;
    public float gameDuration = 150f;
    
    private bool isGameActive = false;
    private float currentGameTime;
    
    // CANVIAT: Moviment_jugadora en lloc de PlayerMovementController
    private List<Moviment_jugadora> playerScripts = new List<Moviment_jugadora>();
    
    public Colpejar ScriptCop1;
    public Colpejar ScriptCop2;
    
    void Start()
    {
        FindAllPlayers();
        SetAllPlayersMovement(false);
        StartCoroutine(InitialCountdownCoroutine());
    }

    void Update()
    {
            PointsJ1.text = ScriptCop1.edificisTransformatJug1.ToString();
            PointsJ2.text = ScriptCop2.edificisTransformatJug2.ToString();
    }

    void FindAllPlayers()
    {
        playerScripts.Clear();
        
        if (playerTags == null || playerTags.Count == 0)
        {
            Debug.LogError("No hi ha tags definits a la llista playerTags");
            return;
        }
        
        foreach (string tag in playerTags)
        {
            GameObject[] playersWithThisTag = GameObject.FindGameObjectsWithTag(tag);
            
            foreach (GameObject player in playersWithThisTag)
            {
                // CANVIAT: Moviment_jugadora en lloc de PlayerMovementController
                Moviment_jugadora movement = player.GetComponent<Moviment_jugadora>();
                if (movement != null)
                {
                    playerScripts.Add(movement);
                    Debug.Log($"✓ Trobada jugadora: {player.name} (tag: {tag})");
                }
                else
                {
                    Debug.LogWarning($"La jugadora {player.name} no té el component Moviment_jugadora!");
                }
            }
        }
        
        Debug.Log($"Total jugadores controlades: {playerScripts.Count}");
    }
    
    void SetAllPlayersMovement(bool enabled)
    {
        foreach (Moviment_jugadora player in playerScripts)
        {
            player.SetMovimentPerTimer(enabled);
        }
        
        Debug.Log($"Moviment de {playerScripts.Count} jugadores: {(enabled ? "ACTIVAT" : "BLOQUEJAT")}");
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
        isGameActive = true;
        currentGameTime = gameDuration;
        SetAllPlayersMovement(true);
        StartCoroutine(GameTimerCoroutine());
    }
    
    IEnumerator GameTimerCoroutine()
    {
        while (isGameActive && currentGameTime > 0)
        {
            UpdateGameTimerDisplay();
            yield return new WaitForSeconds(1f);
            currentGameTime--;
        }
        
        if (currentGameTime <= 0)
        {
            EndGame();
        }
    }
    
    void UpdateGameTimerDisplay()
    {
        if (gameTimerText != null)
        {
            int minutes = Mathf.FloorToInt(currentGameTime / 60);
            int seconds = Mathf.FloorToInt(currentGameTime % 60);
            gameTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
    
    void EndGame()
    {
        isGameActive = false;
        SetAllPlayersMovement(false);
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
}