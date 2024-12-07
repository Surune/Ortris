using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Bar : MonoBehaviour {
    public Image img;
    public GameObject alert;
    static int num = 0;
    private int mynum;
    public float amount = 0f;
    public Color original_color;
    public Color blink_color;
    private Toggle toggle;
    private ToggleGroup toggleGroup;
    public bool refinable = false;

    Bar() {
        mynum = num;
        num++;
    }

    private void Start() {
        img = GetComponent<Image>();

        toggle = GetComponent<Toggle>();
        toggleGroup = toggle.GetComponentInParent<ToggleGroup>();
        toggle.group = toggleGroup;
    }

    public void OnToggleButtonClicked() {
        // Deselect the clicked toggle button if it is already selected
        if (refinable) {
            BarManager.instance.Refine();
        }
        else if (toggle.isOn) {
            img.color = Color.red;
        }
        else{
            img.color = Color.white;
        }
    }

    public void SetAmount(float a, bool tween = true) {
        amount = a;
        if(tween)   img.DOFillAmount(amount, 0.1f).SetEase(Ease.InQuad);
        else        img.fillAmount = amount;
    }

    public void IncreaseAmount(float a, bool tween = true){
        amount += a;
        if(amount >= 1f) {
            amount = 1f;
            GameStateManager.Instance.SetState(GameState.Paused);
            return;
        }
        if(tween)   img.DOFillAmount(amount, 0.1f).SetEase(Ease.InQuad);
        else        img.fillAmount = amount;
    }
}
