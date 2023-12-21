using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {

            UIManager.OpenUI<DebugUI>();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
