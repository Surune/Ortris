using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Linq;

public class SkillManager : MonoBehaviour {
    private static SkillManager instance;
    private void Awake() {
        instance = this;
    }

    public Transform content;
    public GameObject skillPrefab;
    public List<int> randomSkillList;
    public int hold = 0;

    public Image holdImage;

    private List<Skill> skillDetailsLoaded;
    public List<SkillDetails> skillDetails;
    public ToggleGroup toggleGroup;
    public TextMeshProUGUI text;
    
    void Start() {
        skillDetailsLoaded = new List<Skill>();
        for(int i = 0; i < 4; i++) {
            var skill = Instantiate(skillPrefab, content).GetComponent<Skill>();
            skill.Set(skillDetails[i]);
            skillDetailsLoaded.Add(skill);
        }
        randomSkillList = new List<int>();
        for(int i = 0; i < 2; i++) {
            AppendRandomSkills();
        }
    }

    private void AppendRandomSkills() {
        List<int> skillList = new List<int> {1, 2, 3, 4, 5, 6};

        while (skillList.Count > 0) {
            int randomIndex = Random.Range(0, skillList.Count);
            randomSkillList.Add(skillList[randomIndex]);
            skillList.RemoveAt(randomIndex);
        }

        UpdateSkillImages();
    }

    public void UpdateSkillImages() {
        for(int i=0; i<4; i++){
            skillDetailsLoaded[i].Set(skillDetails[randomSkillList[i]-1]);
        }
        if(hold != 0){
            holdImage.GetComponent<Image>().sprite = skillDetails[hold-1].image;
        }
        text.text = skillDetailsLoaded[randomSkillList[0]].detail.description;
    }

    public void UseSkill() {
        int skill_idx = randomSkillList[0];
        Toggle selectedToggle = toggleGroup.ActiveToggles().FirstOrDefault();

        if(selectedToggle != null){
            Debug.Log(skill_idx);
            switch(skill_idx) {
                case 1:
                // Move the selected toggle button to the leftmost position
                MoveButtonsToLeft(selectedToggle);
                break;
                case 2:
                // Get the sibling indexes of the selected toggle and the previous toggle
                int selectedIndex = selectedToggle.transform.GetSiblingIndex();
                int previousIndex = selectedIndex - 1;
                // Swap the positions of the selected toggle and the previous toggle
                SwapPositions(selectedToggle, previousIndex);
                var temp = BarManager.val_list[selectedIndex];
                BarManager.val_list[selectedIndex] = BarManager.val_list[previousIndex];
                BarManager.val_list[previousIndex] = temp;
                break;
                case 3:
                break;
                case 4:
                break;
                case 5:
                break;
                case 6:
                break;
                case 7:
                break;
            }
            selectedToggle.isOn = false;
        }

        randomSkillList.RemoveAt(0);
        if(randomSkillList.Count < skillDetails.Count) {
            AppendRandomSkills();
        }
        
        BarManager.instance.ShowAscendingSortedParts();
        UpdateSkillImages();
    }

    public void HoldSkill() {
        if(hold == 0) {
            hold = randomSkillList[0];
            randomSkillList.RemoveAt(0);
        }
        else {
            var temp = hold;
            hold = randomSkillList[0];
            randomSkillList[0] = temp;
        }

        BarManager.instance.ShowAscendingSortedParts();
        UpdateSkillImages();
    }

    private void MoveButtonsToLeft(Toggle selectedToggle) {
        List<Toggle> toggleButtons = new List<Toggle>(toggleGroup.GetComponentsInChildren<Toggle>());

        int selectedIndex = toggleButtons.IndexOf(selectedToggle);
        for (int i = selectedIndex; i < toggleButtons.Count; i++) {
            toggleButtons[i].transform.SetAsLastSibling();
        }

        for (int i = 0; i < selectedIndex; i++) {
            toggleButtons[i].transform.SetAsLastSibling();
        }
    }

    private void SwapPositions(Toggle selectedToggle, int previousIndex) {
        // Get the transform of the toggle group
        Transform toggleGroupTransform = toggleGroup.transform;

        // Get the transform of the previous toggle
        Transform previousToggleTransform = toggleGroupTransform.GetChild(previousIndex);

        // Swap the sibling indexes of the selected toggle and the previous toggle
        selectedToggle.transform.SetSiblingIndex(previousIndex);
        previousToggleTransform.SetSiblingIndex(previousIndex + 1);
    }
}
