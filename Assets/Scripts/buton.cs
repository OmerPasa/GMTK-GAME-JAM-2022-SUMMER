using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buton : MonoBehaviour
{
    public GameObject yonet;
    public int gidilecekOdaNumarasi;
    public Sprite sprite1;
    public Sprite sprite2;
    public GameObject ayarlarMenusu;

    bool icerdema;
    bool []ayarlar = {false, false}; // işlemde, açık/kapalı

    void OnTriggerEnter2D(Collider2D temas)
    {
        if (temas.tag == "mouse")
        {
            GetComponent<SpriteRenderer>().sprite = sprite2;
            icerdema = true;
        }
    }
    void OnTriggerExit2D(Collider2D temas)
    {
        if (temas.tag == "mouse")
        {
            GetComponent<SpriteRenderer>().sprite = sprite1;
            icerdema = false;
        }
    }

    void Update() {

        if (Input.GetMouseButtonDown(0) && icerdema) {
            if (gidilecekOdaNumarasi == -3) { //cikis
                Application.Quit();
            }
            else if (gidilecekOdaNumarasi == -2) { //ayarlar
                if (ayarlar[1]) {
                    ayarlar[1] = false;
                } else {
                    ayarlarMenusu.SetActive(true);
                    ayarlar[1] = true;
                }
                ayarlar[0] = true;
            }
            else if (gidilecekOdaNumarasi == -1) { //yükle
                SceneScript.loadDataAfterSceneWait(SaveSystem.loadGame());
            }
            else if (gidilecekOdaNumarasi == 1) {
                SceneManager.LoadScene(gidilecekOdaNumarasi);
                //yonet.GetComponent<yonet>().girisYap();
            }
        }

        if (ayarlar[0]) {
            ayarlarMenusuHareket();
        }
    }

    void ayarlarMenusuHareket() {
        float []limtler = {0.5f, 5.5f};
        int speed = 20;
        
        if (!ayarlar[1]) {
            if (ayarlarMenusu.transform.position.y < limtler[1]) {
                ayarlarMenusu.transform.position += new Vector3(0, Time.deltaTime*speed, 0);
            }
            else {
                ayarlarMenusu.transform.position = new Vector3(ayarlarMenusu.transform.position.x, limtler[1], ayarlarMenusu.transform.position.z);
                ayarlar[0] = false;
                ayarlarMenusu.SetActive(false);
            }
        }
        else {
            if (ayarlarMenusu.transform.position.y > limtler[0]) {
                ayarlarMenusu.transform.position -= new Vector3(0, Time.deltaTime*speed, 0);
            }
            else {
                ayarlarMenusu.transform.position = new Vector3(ayarlarMenusu.transform.position.x, limtler[0], ayarlarMenusu.transform.position.z);
                ayarlar[0] = false;
            }
        }
    }

}
