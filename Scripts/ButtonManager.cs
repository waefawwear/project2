using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    Button[] buttons;
    public Player player;

    private void Awake()
    {
        buttons = new Button[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            buttons[i] = transform.GetChild(i).GetComponent<Button>();
        }
    }

    private void Start()
    {
        buttons[0].onClick.AddListener(TutorialBtn);
        buttons[1].onClick.AddListener(GameStartBtn);
        buttons[2].onClick.AddListener(HardCoreBtn);
    }

    void TutorialBtn()
    {
        SceneManager.LoadScene("Scene_Tutorial_01");
    }

    void GameStartBtn()
    {
        player.playermode = Player.PlayerMode.Basic;
        SceneManager.LoadScene("Scene_Stage_1-1");
    }

    void HardCoreBtn()
    {
        player.playermode = Player.PlayerMode.Hardcore;
        SceneManager.LoadScene("Scene_Stage_1-1");
    }
}
