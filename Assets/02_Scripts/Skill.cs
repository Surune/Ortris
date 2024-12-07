using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour {
    public SkillDetails detail;
    private Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    public void Set(SkillDetails detail) {
        this.detail = detail;
        image.sprite = detail.image;
    }
}