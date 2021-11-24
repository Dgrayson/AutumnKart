using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
public class RaceManager : MonoBehaviour
{
    [SerializeField] private int currentLap;
    [SerializeField] private int maxLaps;

    [SerializeField] private TextMeshProUGUI lapText;
    [SerializeField] private TextMeshProUGUI finshText;
    [SerializeField] private GameObject resultsPanel;

    private KartController kartController; 

    private int currCheckpoint = 0; 

    private static RaceManager _instance;

    public static RaceManager Instance { get { return _instance; } }

    // Start is called before the first frame update
    void Start()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject); 
        }
        else
        {
            _instance = this; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentLap > maxLaps)
        {
            FinishRace(); 
        }
    }

    private void UpdateText()
    {
        lapText.text = "LAP: " + currentLap + "/" + maxLaps; 
    }
    
    public void OnChildTriggerEnter(int checkpointNum)
    {
        if (checkpointNum == 4 && currCheckpoint == 3)
        {
            NextLap(); 
        }
        else if (checkpointNum == currCheckpoint + 1)
        {
            currCheckpoint++;
            UpdateText(); 
        }
    }

    private void NextLap()
    {
        currCheckpoint = 1;
        currentLap++;

        UpdateText(); 
    }

    private void FinishRace()
    {
        ShowResults();
        kartController.enabled = false; 
    }

    private void ShowResults()
    {
        resultsPanel.SetActive(true);
    }
}
