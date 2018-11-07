using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;
    [SerializeField] GameObject _BeginingInfoCanvas;
    public bool canContinue;
    public  int firstTime;

    private void Awake()
    {
        _Instance = this;
        firstTime = PlayerPrefs.GetInt("INFO");
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Info()
    {
        if (firstTime <= 0)
        {
            _BeginingInfoCanvas.SetActive(true);
            yield return new WaitUntil(() => canContinue);
            _BeginingInfoCanvas.SetActive(false);
        }


        PlayerPrefs.SetInt("INFO", 1);
    }

    public void SetInfo()
    {
        StartCoroutine(Info());
    }

    public void BeginingInfo()
    {
        canContinue = true;
    }
}
