using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelection : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI disp;
    private string[] disptext = {"이건 텍스트창입니다!", "제일 쉬운 난이도입니다", "어려운 난이도입니다. 꽤 어려울지도?",
    "전문가를 위한 난이도입니다.", "스킬쓰기 좀 어려울거에요", "자원량를 잘 보세요", "이거 볼 시간이 없을텐데?", "작지만 강하다.", 
    "약간 어렵습니다. 많이 어려울지도?", "으아아아악!"};

    [SerializeField]
    private TextMeshProUGUI expl;
    private string[] expltext = {"튜토리얼", "문명의 도시", "지구", "우주", "지평선", "은하수", "미지의 행성", "성단", "심연", "네뷸라"};

    [SerializeField]
    private TextMeshProUGUI level;
    private string[] leveltext= {"GALAXY", "CIVILIZATION", "EARTH", "COSMOS", "CUSTOM", "HORIZON", "MILKYWAY", "MYSTERY", "STAR CLUSTER", "DEEP", "NEBULA"};

    void Start() {
        disp.text = disptext[0];
        expl.text = expltext[0] + "에 오신 걸 환영합니다!";
    }
}
