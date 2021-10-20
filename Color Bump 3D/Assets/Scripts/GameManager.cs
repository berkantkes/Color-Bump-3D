using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text currentLevelText;
    [SerializeField] private Text nextLevelText;
    [SerializeField] private TextMesh levelNo;
    [SerializeField] private Image fill;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject finish;
    [SerializeField] private GameObject hand;

    private float startDistance;
    private float distance;
    public int level;

    private void Start()
    {
        levelNo.text = "LEVEL " + level;

        nextLevelText.text = (level + 1) + "";
        currentLevelText.text = level + "";
        //SceneManager.LoadScene("Level" + level);
        startDistance = Vector3.Distance(player.transform.position, finish.transform.position);
    }

    private void Update()
    {
        distance = Vector3.Distance(player.transform.position, finish.transform.position);
        if(player.transform.position.z < finish.transform.position.z)
        {
            fill.fillAmount = 1 - (distance / startDistance);
        }
    }

    public void RemoveUI()
    {
        hand.SetActive(false);
    }

    public void LevelUp()
    {
        level++;
    }

    public int  GetLevel()
    {
        return level;
    }

    public void LevelNoUpdate()
    {
        levelNo.text = "LEVEL " + level;

        nextLevelText.text = (level + 1) + "";
        currentLevelText.text = level + "";
    }

}
