using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kameraKontrol : MonoBehaviour
{
    Vector3 ofset;
   public GameObject oyn;
    // Start is called before the first frame update
    void Start()
    { //ofset=kamera konumu - oyuncu konumu
        ofset = transform.position - oyn.transform.position;

    }

    private void LateUpdate()
    {
        // K' = O + ofset
        transform.position = oyn.transform.position + ofset;
    }
}
