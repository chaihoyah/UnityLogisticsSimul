using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToCustomer : MonoBehaviour
{

    // Start is called before the first frame update
    public Transform[] Objs = new Transform[4];

    void Start()
    {
        //StartCoroutine(Cor());
    }

    public IEnumerator Cor()
    {
        while(true)
        {
            int r = Random.Range(1, 5);
            MakeObject(r);
            yield return new WaitForSeconds(2f);
        }
    }


    public void MakeObject(int type)
    {
        Transform obj_ = Instantiate(Objs[(type-1)], this.gameObject.transform.position, Quaternion.identity);
        obj_.GetComponent<ObjectScript>().SetInfo(2, type);

        StartCoroutine(MoveObj(obj_));
    }

    IEnumerator MoveObj(Transform obj_)
    {
        float ran = Random.Range(-70.0f, 70.0f);
        int i = 0;
        obj_.transform.Rotate(0, ran, 0);
        while(i<300)
        {

            obj_.transform.Translate(0,0,0.25f);
            i++;
            yield return new WaitForEndOfFrame();
        }
        Destroy(obj_.gameObject);
    }

    public void MakeObjectFast(int type)
    {
        Transform obj_ = Instantiate(Objs[(type - 1)], this.gameObject.transform.position, Quaternion.identity);
        obj_.GetComponent<ObjectScript>().SetInfo(2, type);

        StartCoroutine(MoveObjFast(obj_));
    }
    IEnumerator MoveObjFast(Transform obj_)
    {
        float ran = Random.Range(-70.0f, 70.0f);
        int i = 0;
        obj_.transform.Rotate(0, ran, 0);
        while (i < 100)
        {

            obj_.transform.Translate(0, 0, 0.65f);
            i++;
            yield return new WaitForEndOfFrame();
        }
        Destroy(obj_.gameObject);
    }
}
