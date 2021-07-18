using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public Vector3 CheckPoint { get; set; }
    public bool GameStarted { get; set; }
    [SerializeField] private int countDown;
    [SerializeField] private GameObject TimerPanel;
    [SerializeField] private GameObject HintPanel;
    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI HighscoreText;
    [SerializeField] private TextMeshProUGUI CrystalText;
    [SerializeField] private Image scoreImage;
    [SerializeField] private List<Image> healthImages;
    private UIAnimation animUI;
    private GameObject player;
    private Procedural procedural;
    private int crystals = 0;
    private int score = 0;

    private void Awake()
    {
        CheckPoint = new Vector3(0, 2.5f, 0);

        player = GameObject.FindGameObjectWithTag("Player");
        procedural = FindObjectOfType<Procedural>();

        HighscoreText.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
        ScoreText.text = score.ToString();                                  // Change the value of the cyrstalsText to crystals.
        TimerText.text = countDown.ToString();                              // Change the value of the countDownText to countDown value.

        CrystalText.text = PlayerPrefs.GetInt("Crystals", 0).ToString();
    }

    private void Start()
    {
        animUI = GetComponent<UIAnimation>();                   // Get the UIAnimation script from component.
        animUI.ChangeColor(TimerText, countDown);               // Change the color of the countDownText.
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(StartGame());
        }
    }

    public IEnumerator StartGame()
    {
        // Wait before closing the titlePanel
        animUI.Close(HintPanel);                                // Close titlePanel with animation.
        yield return new WaitForSeconds(0.5f);                  // Wait 0.4 seconds before continue. 
        animUI.Open(TimerPanel);                                // Open CountDownPanel.

        int count = countDown;

        while (count > 0)
        {
            // Waiting 0.4seconds for the shrink animation to play
            yield return new WaitForSeconds(0.4f);                  // Wait 0.4 seconds before continue. 
            animUI.Grow(TimerText);                             // Grow the text animation in 0.4 seconds.
            animUI.ChangeColor(TimerText, count);           // Change color according to countDonw.

            TimerText.text = count.ToString();              // Change countDownText to countDown value.
            yield return new WaitForSeconds(0.6f);                  // Wait 0.6 seconds before continue. 
            count--;                                            // Decrease the countDown value by minus one.

            if (count == 0) { break; }                          // if countDown zero don't let last number fade before the panel.
            animUI.Shrink(TimerText);                           // Shrink the text animation in 0.4 seconds.
        }
        animUI.Close(TimerPanel);                                        // Shrink the Panel.
        GameStarted = true;                                         // GameStarted is equal to true.

        StartCoroutine(BeginScoreCount());
    }

    public void EndGame()
    {

        GameStarted = false;

        if (healthImages.Count <= 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        // Get the correct direction of next tile.
        Vector3 checkOne = procedural.Roads[0].transform.position;
        Vector3 checkTwo = procedural.Roads[1].transform.position;

        player.GetComponent<PlayerController>().SetIdleAnim();
        if (checkOne.x - checkTwo.x == 0)
        {
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            player.GetComponent<PlayerController>().SetIsWalkingRight(false);
        }
        else
        {
            player.transform.rotation = Quaternion.Euler(0, -90, 0);
            player.GetComponent<PlayerController>().SetIsWalkingRight(true);
        }

        // Reset the position of the player.
        player.transform.position = CheckPoint + new Vector3(0, 2.5f, 0);

        // Open hint panel.
        animUI.Open(HintPanel);

        // Remove health from UI.
        Image health = healthImages[healthImages.Count - 1];
        healthImages.Remove(health);
        Destroy(health);

        // Reset timer.
        TimerText.text = countDown.ToString();
        TimerText.color = Color.yellow;
    }


    public void IncreaseCrystal(int value)
    {
        crystals += value;
        CrystalText.text = crystals.ToString();

        if (crystals > PlayerPrefs.GetInt("Crystals", 0))
        {
            PlayerPrefs.SetInt("Crystals", crystals);
            CrystalText.color = Color.yellow;
        }
    }

    private IEnumerator BeginScoreCount()
    {
        yield return new WaitForSeconds(1f);
        IncreaseScore();
        CheckScore();

        if (GameStarted)
        {
            StartCoroutine(BeginScoreCount());
        }
    }

    private void IncreaseScore() { score++; }
    private void CheckScore()
    {
        if (score > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", score);
            ScoreText.color = Color.red;
            scoreImage.color = Color.red;
        }
        ScoreText.text = score.ToString();
    }

}
