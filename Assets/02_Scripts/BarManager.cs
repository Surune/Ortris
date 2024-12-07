using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static GameState;

public class BarManager : MonoBehaviour {
    public static BarManager instance;
    private void Awake() {
        instance = this;
    }

    public int bar_num;
    public Transform content;
    public GameObject bar_prefab;
    public static List<GameObject> bar_list;
    public static List<float> val_list;
    public Slider progressBar;

    public float increaseAmount = 0.1f;
    public float interval = 1f;
    public float elapsedTime = 0f;

    private static System.Random rng = new System.Random();

    void Start() {
        bar_list = new List<GameObject>();
        val_list = new List<float>();
        var numbers = ShuffleNumbers(bar_num);
        
        for (int i = 0; i < bar_num; i++) {
            bar_list.Add(Instantiate(bar_prefab, content));
            //var temp = Random.Range(0f, 0.3f);
            val_list.Add(numbers[i]);
            bar_list[i].GetComponent<Bar>().SetAmount(numbers[i], false);
            bar_list[i].GetComponent<Bar>().original_color = Color.white;
            bar_list[i].GetComponent<Image>().color = Color.white;
            //bar_list[i].GetComponent<Toggle>().onValueChanged.AddListener(OnToggleValueChanged);
        }
        ShowAscendingSortedParts();
        elapsedTime = 0f;
    }

    public static float[] ShuffleNumbers(int n)
    {
        float[] numbers = new float[n];
        for (int i = 0; i < n; i++)
        {
            numbers[i] = (float)(i + 1)/n/2;
        }

        for (int i = n - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            float temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;
        }

        return numbers;
    }

    private void Update() {
        if(GameStateManager.Instance.CurrentGameState == GameState.Gameplay) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= interval) {
                elapsedTime -= interval;
                for (int i=0; i<bar_num; i++){
                    bar_list[i].GetComponent<Bar>().IncreaseAmount(increaseAmount, false);
                    if (bar_list[i].GetComponent<Bar>().amount > 0.75f)
                        bar_list[i].GetComponent<Bar>().alert.SetActive(true);
                    val_list[i] += increaseAmount;
                }
            }
            
            progressBar.value = elapsedTime / interval;
        }
    }

    public List<List<float>> ShowAscendingSortedParts() {
        List<List<float>> sortedParts = new List<List<float>>();

        if (val_list.Count == 0) {
            return sortedParts;
        }

        List<float> currentPart = new List<float> { 0 };

        for (int i = 1; i < val_list.Count; i++) {
            if (val_list[i] >= val_list[i - 1]) {
                currentPart.Add(i);
            }
            else {
                if (currentPart.Count >= 4) {
                    sortedParts.Add(currentPart);
                }
                currentPart = new List<float> {i};
            }
        }
        if (currentPart.Count >= 4) {
            sortedParts.Add(currentPart);
        }

        foreach (List<float> part in sortedParts) {
            Debug.Log(string.Join(", ", part));
        }

        foreach(var each in sortedParts){
            foreach(int idx in each){
                bar_list[idx].GetComponent<Image>().DOColor(Color.yellow, 1f)
                                .SetLoops(-1, LoopType.Yoyo)
                                .SetEase(Ease.Linear);
                bar_list[idx].GetComponent<Bar>().refinable = true;
            }
        }
        return sortedParts;
    }

    public void Refine() {
        var parts = BarManager.instance.ShowAscendingSortedParts();
        foreach(var each in parts){
            foreach(int idx in each){
                float s = UnityEngine.Random.Range(0.05f, 0.2f);
                DOTween.Kill(bar_list[idx].GetComponent<Image>());
                bar_list[idx].GetComponent<Bar>().SetAmount(s);
                val_list[idx] = s;
                bar_list[idx].GetComponent<Bar>().refinable = false;
                bar_list[idx].GetComponent<Image>().color = Color.white;
            }
        }
    }
}
