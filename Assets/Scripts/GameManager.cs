using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public EnumManager.Scene nextScene;
    public int coin;
    public int coinRequirement;
    public bool isWin;

    public Text coinText;
    public TextMeshProUGUI coinRequirementText;
    public GameObject chest;
    public Sprite unlockedChest;
    public GameObject chestCanvas;
    public GameObject winPanel;

    private SpriteRenderer chestSprite;
    // Start is called before the first frame update

    void Awake()
    {
        chestSprite = chest.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        coin = 0;
        coinRequirementText.text = coinRequirement.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if(chestCanvas == null) return;
        if(coinText == null) return;
        if(winPanel == null) return;

        chestCanvas.SetActive(!IsRequirementFilled());
        coinText.text = coin.ToString();
        winPanel.SetActive(isWin);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(nextScene.ToString());
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void RetryScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool IsRequirementFilled()
    {
        if(coin >= coinRequirement) 
        {
            chestSprite.sprite = unlockedChest;
            return true;
        }
        else return false;
    }
}
