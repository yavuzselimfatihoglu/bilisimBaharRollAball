using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class oyuncuKontrol : MonoBehaviour
{
    Rigidbody rb;
    float yatay;
    float dikey;
    Vector3 kuvvet;
   public int hiz = 10;
    int skor = 0;
    public Text skorTxt;
    string skorTitle = "Skor:";
  public  Text uyariTxt;
  public  Text tebrikTxt;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void FixedUpdate()
    {
        
      
        if(Input.GetKeyDown(KeyCode.Space))
        {
            kuvvet = new Vector3(0f, 500f, 0f);
            rb.AddForce(kuvvet);
        }
        if (hiz < 1) hiz = 1;

        if (Application.platform == RuntimePlatform.Android)
        {
            //input gyro/acc olacak
            yatay = Input.acceleration.x;
            dikey = Input.acceleration.y;
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {

            //yönü belirle
            yatay = Input.GetAxis("Horizontal");
            dikey = Input.GetAxis("Vertical");
        }
        //kuvveti oluþtur
        kuvvet = new Vector3(yatay, 0f, dikey);
        //kuvveti uygula
        rb.AddForce(kuvvet * hiz);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        
    //    Debug.Log(other.gameObject.name.ToString());
    //if(other.gameObject.name=="elmas01")
    //    {
    //        Destroy(other);
    //    }
    if(other.CompareTag("sonElmas") && skor==90)
        {
            //skor arttýrýlacak
            skor += 10;
            skorTxt.text = skorTitle+skor.ToString();
            //son elmas yok edilecek
            Destroy(other.gameObject);
            //tebrik mesajý (3sn)
            tebrikTxt.text = "Tebrikler seviye geçildi!";
            StartCoroutine(tebrikBeklet());
            
        }
    else if(other.CompareTag("elmaslar")) //elmaslarýn Istrigger'ý açýk
        {
            //yok etme
            Destroy(other.gameObject);
            skor += 10;
            skorTxt.text =skorTitle+skor.ToString();
            //gizleme
           // other.gameObject.SetActive(false);
           
        }
    else
        {//uyarý text 3sn
            uyariTxt.text = "Bu elmas son toplanacak!!";
            StartCoroutine(uyariBeklet());
            
        }
    }

   IEnumerator uyariBeklet()
    {
        yield return new WaitForSeconds(2.5f);
      //  Time.timeScale = 0; //zamaný durdurur
        uyariTxt.text = "";
    }
    IEnumerator tebrikBeklet()
    {
        yield return new WaitForSeconds(3f);
        //sonraki sahneye geçiþ
        SceneManager.LoadScene("cikisSahnesi");

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("elmaslar")) //Elmasýn isTrigger kapalý olmalý
        {
            Destroy(collision.gameObject);
            Debug.Log(collision.contacts[0].point);
        }
    }

}
