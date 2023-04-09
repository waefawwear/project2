using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public Player player;
    public Player hardCorePlayer;

    private void Start()
    {
        PlayerSpawn();
    }

    public void PlayerSpawn()
    {
        if(player.playermode == Player.PlayerMode.Basic)
        {
            GameObject obj = Instantiate(player.gameObject);
            obj.transform.position = transform.position;
            obj.GetComponent<Player>().playerDie = SceneReLoad;
        }
        else if(player.playermode == Player.PlayerMode.Hardcore)
        {
            GameObject obj = Instantiate(hardCorePlayer.gameObject);
            obj.transform.position = transform.position;
            obj.GetComponent<Player>().playerDie = BacktoMainScene;
        }
    }

    public void SceneReLoad()
    {
        StartCoroutine(SceneReLoadDelay());
    }

    public void BacktoMainScene()
    {
        StartCoroutine(MainSceneReLoadDelay());
    }

    IEnumerator SceneReLoadDelay()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator MainSceneReLoadDelay()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(0);
    }
}
