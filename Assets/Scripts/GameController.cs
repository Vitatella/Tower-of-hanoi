using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] Disc[] allDiscs;
    [SerializeField] float diskHeight;
    private float choicedDiscHeight;
    [SerializeField] Tower[] towers;
    private Disc choicedDisc;
    private int moveCount;
    [SerializeField] Text moveCountText;
    private void Awake()
    {
        choicedDiscHeight = (allDiscs.Length + 1) * diskHeight;
    }
    void Start()
    {
        for (int i = 0; i < allDiscs.Length; i++)
        {
            allDiscs[i].transform.position = new Vector3(towers[0].GetPosition().x, towers[0].GetPosition().y + towers[0].discs.Count * diskHeight, towers[0].GetPosition().z);
            towers[0].discs.Add(allDiscs[i]);
        }
    }
    public float GetHeight()
    {
        return choicedDiscHeight;
    }
    public void OnClick(Tower tower)
    {
        if (choicedDisc == null && tower.discs.Count != 0)
        {
            Debug.Log(tower.discs.Count);
            choicedDisc = tower.discs[tower.discs.Count - 1];
            tower.discs.Remove(choicedDisc);
            Vector3 endPosition = new Vector3(tower.GetPosition().x, tower.GetPosition().y + choicedDiscHeight, tower.GetPosition().z);
            choicedDisc.GetComponent<MoveAnimation>().PickUp(choicedDisc.transform.position, endPosition);
        }
        else
        {
            if (tower.discs.Count == 0 || choicedDisc.scale < tower.discs[tower.discs.Count - 1].scale)
            {
                Vector3 endPosition = new Vector3(tower.GetPosition().x, tower.GetPosition().y + tower.discs.Count * diskHeight, tower.GetPosition().z);
                Vector3 beginPosition = new Vector3(choicedDisc.transform.position.x, choicedDiscHeight, choicedDisc.transform.position.z);
                choicedDisc.GetComponent<MoveAnimation>().DoMove(beginPosition, endPosition);
                tower.discs.Add(choicedDisc);
                choicedDisc = null;
                moveCountText.text = (++moveCount).ToString();
            }
        }
    }
}