using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    /**Vector3[] positions = new Vector3[9];
    SimulatorController SC;

    void Awake()
    {
        for(int i = 0; i < 9; i++)
        {
            positions[i] = transform.GetChild(i).transform.position;
        }
    }

    public void LoadPackage(Transform package, Vector3 target)
    {
        Debug.Log("Loading!!");
        for(int i = 0; i < 9; i++)
        {
            positions[i] = transform.GetChild(i).transform.position;
        }

        package.SetParent(transform, true);

        int count = package.childCount;

        Transform[] list = new Transform[count];

        for(int i = 0; i < count; i++)
        {
            list[i] = package.GetChild(i);
        }

        package.localPosition = new Vector3(0, 0, 0);

        LoadStocks(count, list);

        StartCoroutine(MoveToTarget(target, package));
    }

    private void LoadStocks(int count, Transform[] list)
    {
        int a = count / 9;
        int temp = count;
        int temp_ = 0;
        Vector3[] tempPos = new Vector3[9];

        for(int i = 0; i < 9; i++)
        {
            tempPos[i] = positions[i];
        }

        for(int i = 0; i < a; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                list[9*i + j].gameObject.SetActive(true);
                list[9*i + j].position = tempPos[j];
            }
            temp -= 9;

            for(int j = 0; j < 9; j++)
            {
                tempPos[j] += new Vector3(0, 7 ,0);
            }
            temp_ += 9;
        }

        for(int i = 0; i < temp; i++)
        {
            list[temp_ + i].gameObject.SetActive(true);
            list[temp_ + i].position = tempPos[i];
        }
    }

    IEnumerator MoveToTarget(Vector3 target, Transform package)
    {
        Debug.Log("Moving!!");
        Debug.Log("Target Vec : " + target);
        transform.LookAt(target);

        yield return new WaitForSeconds(1);

        Vector3 moveVec = new Vector3();

        moveVec = (target - transform.position) * 0.01f;

        float targetZ = target.z - 5;

        while (transform.position.z < targetZ)
        {
            transform.position += moveVec;

            yield return new WaitForSeconds(0.01f);
        }

        

        if(targetZ.Equals(0))
        {
            SC = GameObject.Find("SimulatorController").GetComponent<SimulatorController>();

            int count = package.childCount;

            for(int i = 0; i < count; i++)
            {
                Transform obj = package.GetChild(0);
                
                SC.StoreThis(obj);
            }
        }

        yield return new WaitForSeconds(0.5f);

        

        Destroy(this.gameObject);
    }**/
}
