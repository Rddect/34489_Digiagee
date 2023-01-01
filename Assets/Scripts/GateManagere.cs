using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateManagere : MonoBehaviour
{
    public TextMeshPro GateNo;
    public int randomNumbeer;
    public bool multiply;
    void Start()
    {
        if (multiply)
        {
            randomNumbeer = Random.Range(1, 3);
            GateNo.text = "X" + randomNumbeer;
        }
        else
        {
            randomNumbeer = Random.Range(10, 100);
            if (randomNumbeer % 2 != 0)
                randomNumbeer += 1;

            GateNo.text = randomNumbeer.ToString();
        }
    }
}
