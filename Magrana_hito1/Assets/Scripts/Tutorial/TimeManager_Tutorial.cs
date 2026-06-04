using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeManager_Tutorial : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text countdownText;
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
    
    [Header("Audio")]
    public AudioClip spawnClip;
    public AudioClip timeWarningClip;
    public AudioClip initialCountdownClip;
    public AudioClip winnerClip;
    public AudioClip finalCountdownClip;
    private RectTransform rect;
    
    void Start()
    {
        ScriptMoviment1.potMoure = false;
        ScriptMoviment2.potMoure = false;

        rect = PointsContainer.GetComponent<RectTransform>();

        SetActive(false);
        winnerScreen.SetActive(false);

        GestioSo.instance.PlaySound(spawnClip, transform, 1f);
        GestioSo.instance.PlaySound(initialCountdownClip, transform, 1f);

        StartCoroutine(InitialCountdownCoroutine());
    }

    void Update()
    {
        PointsJ1.text = edificisTransformatJug1.ToString();
        PointsJ2.text = edificisTransformatJug2.ToString();

        if (edificisTransformatJug1 + edificisTransformatJug2 >= 4)
        {
            Acaba();
        }

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
        
        Comenca();
    }
    
    void Comenca()
    {
        ScriptMoviment1.potMoure = true;
        ScriptMoviment2.potMoure = true;
        isGameActive = true;

        SetActive(true); // NOU

        currentGameTime = gameDuration;
        StartCoroutine(GameTimerCoroutine());
    }
    
    IEnumerator GameTimerCoroutine()
    {
        while (isGameActive && currentGameTime > 0)
        {
            //if (gameTimerText != null)
            {
                int minutes = Mathf.FloorToInt(currentGameTime / 60);
                int seconds = Mathf.FloorToInt(currentGameTime % 60);
            }
            yield return new WaitForSeconds(1f);
            currentGameTime--;
            if (currentGameTime == 30f)
            {
                GestioSo.instance.PlaySound(timeWarningClip, transform, 1f);
            }
            if (currentGameTime == 10f)
            {
                GestioSo.instance.PlaySound(finalCountdownClip, transform, 1f);
            }
            if (currentGameTime <= 0)
            {
                Acaba();
            }
        }
    }
    
    void Acaba()
    {
        ScriptMoviment1.potMoure = false;
        ScriptMoviment2.potMoure = false;        
        isGameActive = false;
        winnerScreen.SetActive(true);

        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, scoreFinalPosY);

        if(edificisTransformatJug1>edificisTransformatJug2){
            winner.text="GUANYA J1";
        }

        else if(edificisTransformatJug1<edificisTransformatJug2){
            winner.text="GUANYA J2";        
        }

        else if(edificisTransformatJug1==edificisTransformatJug2){
            winner.text="EMPAT"; 
        }

        Invoke("TornarMenu", 5f);
    }

    // NOVA FUNCIÓ PER TORNAR AL MENU PRINCIPAL DESPRÉS DE MOSTRAR LA PANTALLA DE GUANYADOR
    void TornarMenu()
    {
        SceneManager.LoadScene("SeleccioNivells");
        Debug.Log("Tornant al menú principal...");
    }
    private void SetActive(bool active)
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