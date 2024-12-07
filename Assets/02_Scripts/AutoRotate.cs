using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour {
    [SerializeField]
    private bool fix_speed;
    [SerializeField]
    private float rotate_speed;

    void Start() {
        if (!fix_speed) {
            rotate_speed = Random.value * 10f;
        }
        if (Random.value > 0.5f) rotate_speed = -rotate_speed;
    }

    void Update() {
        transform.Rotate(new Vector3(0, 0, 1f) * Time.deltaTime * rotate_speed);
    }
}