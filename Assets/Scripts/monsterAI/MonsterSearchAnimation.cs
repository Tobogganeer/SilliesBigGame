using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSearchAnimation : MonoBehaviour
{

    public GameObject monsterImage;

    float sin;
    float timer;
    public float movementSpeed = 1f;
    // Start is called before the first frame update
    void Awake()
    {
        monsterImage.transform.localPosition = new Vector3(-1.15f, 0, 0.673f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * movementSpeed;
        sin = Mathf.Sin(timer);
        //sin = Mathf.Abs(sin);
        monsterImage.transform.localPosition = new Vector3(monsterImage.transform.localPosition.x + (0.5f * Time.deltaTime), -0.1f + sin/16, 0.673f);
        //monsterImage.transform.localPosition = new Vector3(monsterImage.transform.localPosition.x + Random.Range(0.0005f, 0.0010f), monsterImage.transform.localPosition.y + Random.Range(0.0005f, 0.0010f));

        if (monsterImage.transform.localPosition.x > 1.15f)
        {
            monsterImage.transform.localPosition = new Vector3(-1.15f, 0, 0.673f);
            monsterImage.SetActive(false);
        }
    }
}
