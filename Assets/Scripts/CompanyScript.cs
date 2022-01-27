using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//사업체에서 오브젝트 생성
public class CompanyScript : MonoBehaviour
{
    /**public SimulatorController SC;
    public Transform Car;
    public int me;// 0~3
    Transform point;
    Vector3 point_pos = new Vector3();
    
    void Start()
    {
        point = transform.GetChild(1);
        point_pos = point.position;

        //StartCoroutine(MakeOBJ());
    }

    public void MakeOBJ_company(int count)
    {
        float time = Random.Range(2, 4);
        int a = Random.Range(0, 4);
        //int count = Random.Range(5, 15);
        Transform[] objs = new Transform[50];


            Vector3 temp = new Vector3(-10, -10, -10);

            for (int i = 0; i < count; i++)
            {
                objs[i] = SC.ObjectPop(temp, (me + 1),2);
            }

            Transform package = SC.MakePackage(objs, count, point_pos);

            Transform car = Instantiate(Car, point_pos, Quaternion.identity);
            CarScript carScript = car.GetComponent<CarScript>();
            carScript.LoadPackage(package, new Vector3(0, 0, 5));

            //StartCoroutine(SC.MovePackage(package, count));

            time = Random.Range(0.5f, 3);
            a = Random.Range(0, 4);
            count = Random.Range(5, 15);
        Debug.Log("YYYYYY");
        return;
    }


    IEnumerator MakeOBJ()
    {
        float time = Random.Range(2, 4);
        int a = Random.Range(0, 4);
        int count = Random.Range(5, 15);
        yield return new WaitForSeconds(time);
        Transform[] objs = new Transform[15];

        while(true)
        {
            Vector3 temp = new Vector3(-10, -10, -10);
            for(int i = 0; i < count; i++)
            {
                objs[i] = SC.ObjectPop(temp, (me + 1));
            }

            Transform package = SC.MakePackage(objs, count, point_pos);

            Transform car = Instantiate(Car, point_pos, Quaternion.identity);
            CarScript carScript = car.GetComponent<CarScript>();
            carScript.LoadPackage(package, new Vector3(0, 0, 5));

            //StartCoroutine(SC.MovePackage(package, count));

            time = Random.Range(0.5f, 3);
            a = Random.Range(0,4);
            count = Random.Range(5, 15);

            yield return new WaitForSeconds(3);
            yield return new WaitForSeconds(time);
        }
    }
    **/

}
