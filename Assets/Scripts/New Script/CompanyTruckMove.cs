using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyTruckMove : MonoBehaviour
{
    // Start is called before the first frame update
    public int company;
    public GameObject[] Trucks = new GameObject[5];
    Vector3[] FirstPos = new Vector3[5];
    Quaternion[] FirstRot = new Quaternion[5];
    GameObject[] box = new GameObject[4];

    public GameObject[] Rbox = new GameObject[8];
    public GameObject[] Gbox = new GameObject[8];
    public GameObject[] Bbox = new GameObject[8];
    public GameObject[] Nbox = new GameObject[8];

    void Start()
    {
        for(int i=0; i<5;i++)
        {
            if(i.Equals(4))
            {
                FirstPos[i] = Trucks[i].transform.localPosition;
                FirstRot[i] = Trucks[i].transform.rotation;
            }
            else
            {
                FirstPos[i] = Trucks[i].transform.position;
                FirstRot[i] = Trucks[i].transform.rotation;
            }
            if(i!=4) box[i] = Trucks[i].transform.GetChild(0).gameObject;
        }

        for(int i=0;i<8;i++)
        {
            Rbox[i].SetActive(false);
            Gbox[i].SetActive(false);
            Bbox[i].SetActive(false);
            Nbox[i].SetActive(false);
        }
    }

    /**public IEnumerator Move()
    {
        int i = 0;
        if(company.Equals(0))
        {
            box.SetActive(true);
            while(this.transform.localPosition.z<=(-0.192f))
            {
                this.transform.localPosition += new Vector3(0, 0, 0.001f);
                yield return new WaitForEndOfFrame();
            }
            while(i<30)
            {
                this.transform.Rotate(0, 0, -3);
                yield return new WaitForSeconds(0.05f);
                i++;
            }
            while(this.transform.localPosition.x<=(-0.187f))
            {
                this.transform.localPosition += new Vector3(0.001f, 0, 0);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(1f);
            box.SetActive(false);
            yield return new WaitForSeconds(1f);
            this.transform.rotation = FirstRot;
            this.transform.localPosition = FirstPos;
        }
        else if(company.Equals(1))
        {
            box.SetActive(true);
            while (this.transform.localPosition.z <= (-0.27f))
            {
                this.transform.localPosition += new Vector3(0, 0, 0.001f);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(1f);
            box.SetActive(false);
            yield return new WaitForSeconds(1f);
            this.transform.rotation = FirstRot;
            this.transform.localPosition = FirstPos;
        }
        else if (company.Equals(2))
        {
            box.SetActive(true);
            while (this.transform.localPosition.z <= (-0.1f))
            {
                this.transform.localPosition += new Vector3(0, 0, 0.001f);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(1f);
            box.SetActive(false);
            yield return new WaitForSeconds(1f);
            this.transform.rotation = FirstRot;
            this.transform.localPosition = FirstPos;
        }
        else if (company.Equals(3))
        {
            box.SetActive(true);
            while (this.transform.localPosition.z <= (-0.08f))
            {
                this.transform.localPosition += new Vector3(0, 0, 0.001f);
                yield return new WaitForEndOfFrame();
            }
            while (i < 30)
            {
                this.transform.Rotate(0, 0, 3);
                yield return new WaitForSeconds(0.05f);
                i++;
            }
            while (this.transform.localPosition.x >= (0.2f))
            {
                this.transform.localPosition += new Vector3(-0.001f, 0, 0);
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(1f);
            box.SetActive(false);
            yield return new WaitForSeconds(1f);
            this.transform.rotation = FirstRot;
            this.transform.localPosition = FirstPos;
        }


    }**/
    public IEnumerator MoveTwo()
    {
        int i = 0;
        yield return new WaitForSeconds(1f);
        box[0].SetActive(true);
        while (Trucks[0].transform.position.z <= (-36f))
        {
            Trucks[0].transform.position += new Vector3(0, 0, 0.4f);
            yield return new WaitForEndOfFrame();
        }
        while (i < 9)
        {
            Trucks[0].transform.Rotate(0, 0, -10);
            yield return new WaitForSeconds(0.05f);
           i++;
        }
        while (Trucks[0].transform.position.x <= (-23f))
        {
            Trucks[0].transform.position += new Vector3(0.4f, 0, 0);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1f);
        box[0].SetActive(false);
        SimulatorController.Type1_Count += 50;

        box[1].SetActive(true);
        while (Trucks[1].transform.position.z <= (-49f))
        {
            Trucks[1].transform.position += new Vector3(0, 0, 0.4f);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1f);
        box[1].SetActive(false);
        SimulatorController.Type2_Count += 50;

        box[2].SetActive(true);
        while (Trucks[2].transform.position.z <= (-24f))
        {
            Trucks[2].transform.position += new Vector3(0, 0, 0.4f);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1f);
        box[2].SetActive(false);
        SimulatorController.Type3_Count += 50;

        box[3].SetActive(true);
        i = 0;
        while (Trucks[3].transform.position.z <= (-13f))
        {
            Trucks[3].transform.position += new Vector3(0, 0, 0.4f);
            yield return new WaitForEndOfFrame();
        }
        while (i < 9)
        {
            Trucks[3].transform.Rotate(0, 0, 10);
            yield return new WaitForSeconds(0.05f);
           i++;
        }
        while (Trucks[3].transform.position.x >= (22f))
        {
            Trucks[3].transform.position += new Vector3(-0.4f, 0, 0);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1f);
        box[3].SetActive(false);
        SimulatorController.Type4_Count += 50;

        StartCoroutine(GoFirst());

    }
    public IEnumerator MoveFast()
    {
        int i = 0;
        box[0].SetActive(true);
        while (Trucks[0].transform.position.z <= (-36f))
        {
            Trucks[0].transform.position += new Vector3(0, 0, 0.6f);
            yield return new WaitForEndOfFrame();
        }
        while (i < 9)
        {
            Trucks[0].transform.Rotate(0, 0, -10);
            yield return new WaitForEndOfFrame();
            i++;
        }
        while (Trucks[0].transform.position.x <= (-23f))
        {
            Trucks[0].transform.position += new Vector3(0.6f, 0, 0);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.5f);
        box[0].SetActive(false);
        SimulatorController.Type1_Count += 50;

        box[1].SetActive(true);
        while (Trucks[1].transform.position.z <= (-49f))
        {
            Trucks[1].transform.position += new Vector3(0, 0, 0.6f);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.5f);
        box[1].SetActive(false);
        SimulatorController.Type2_Count += 50;

        box[2].SetActive(true);
        while (Trucks[2].transform.position.z <= (-24f))
        {
            Trucks[2].transform.position += new Vector3(0, 0, 0.6f);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.5f);
        box[2].SetActive(false);
        SimulatorController.Type3_Count += 50;

        box[3].SetActive(true);
        i = 0;
        while (Trucks[3].transform.position.z <= (-13f))
        {
            Trucks[3].transform.position += new Vector3(0, 0, 0.6f);
            yield return new WaitForEndOfFrame();
        }
        while (i < 9)
        {
            Trucks[3].transform.Rotate(0, 0, 10);
            yield return new WaitForEndOfFrame();
            i++;
        }
        while (Trucks[3].transform.position.x >= (22f))
        {
            Trucks[3].transform.position += new Vector3(-0.6f, 0, 0);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);
        box[3].SetActive(false);
        SimulatorController.Type4_Count += 50;

        StartCoroutine(GoFirst());
    }

    public IEnumerator MoveToLittle(int a, int b, int c, int d)
    {
        int a_ = 0;
        int b_ = 0;
        int c_ = 0;
        int d_ = 0;
        if (a > 8) a_ = 8;
        else a_ = a;
        if (b > 8) b_ = 8;
        else b_ = b;
        if (c > 8) c_ = 8;
        else c_ = c;
        if (d > 8) d_ = 8;
        else d_ = d;
        int i = 0;
        while(i<a_)
        {
            Rbox[i].SetActive(true);
            yield return new WaitForSeconds(0.5f);
            i++;
        }
        SimulatorController.Type1_Count -= a;
        i = 0;
        while(i<b_)
        {
            Gbox[i].SetActive(true);
            yield return new WaitForSeconds(0.5f);
            i++;
        }
        SimulatorController.Type2_Count -= b;
        i = 0;
        while (i < c_)
        {
            Bbox[i].SetActive(true);
            yield return new WaitForSeconds(0.5f);
            i++;
        }
        SimulatorController.Type3_Count -= c;
        i = 0;
        while (i < d_)
        {
            Nbox[i].SetActive(true);
            yield return new WaitForSeconds(0.5f);
            i++;
        }
        SimulatorController.Type4_Count -= d;
        yield return new WaitForSeconds(0.5f);

        while (Trucks[4].transform.localPosition.z <= (-0.7f))
        {
            Trucks[4].transform.localPosition += new Vector3(0, 0, 0.03f);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.8f);

        for (int j = 0; j < 8; j++)
        {
            Rbox[j].SetActive(false);
            Gbox[j].SetActive(false);
            Bbox[j].SetActive(false);
            Nbox[j].SetActive(false);
        }
        Socket_Commu.stock_by_categories[0] += a;
        Socket_Commu.stock_by_categories[1] += b;
        Socket_Commu.stock_by_categories[2] += c;
        Socket_Commu.stock_by_categories[3] += d;
        StartCoroutine(GoFirst());

    }

    public IEnumerator MoveToLittleFast(int a, int b, int c, int d)
    {
        int a_ = 0;
        int b_ = 0;
        int c_ = 0;
        int d_ = 0;
        if (a > 8) a_ = 8;
        else a_ = a;
        if (b > 8) b_ = 8;
        else b_ = b;
        if (c > 8) c_ = 8;
        else c_ = c;
        if (d > 8) d_ = 8;
        else d_ = d;
        int i = 0;
        while (i < a_)
        {
            Rbox[i].SetActive(true);
            yield return new WaitForSeconds(0.1f);
            i++;
        }
        SimulatorController.Type1_Count -= a;
        i = 0;
        while (i < b_)
        {
            Gbox[i].SetActive(true);
            yield return new WaitForSeconds(0.1f);
            i++;
        }
        SimulatorController.Type2_Count -= b;
        i = 0;
        while (i < c_)
        {
            Bbox[i].SetActive(true);
            yield return new WaitForSeconds(0.1f);
            i++;
        }
        SimulatorController.Type3_Count -= c;
        i = 0;
        while (i < d_)
        {
            Nbox[i].SetActive(true);
            yield return new WaitForSeconds(0.1f);
            i++;
        }
        SimulatorController.Type4_Count -= d;
        yield return new WaitForSeconds(0.5f);

        while (Trucks[4].transform.localPosition.z <= (-0.7f))
        {
            Trucks[4].transform.localPosition += new Vector3(0, 0, 0.1f);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.1f);

        for (int j = 0; j < 8; j++)
        {
            Rbox[j].SetActive(false);
            Gbox[j].SetActive(false);
            Bbox[j].SetActive(false);
            Nbox[j].SetActive(false);
        }
        Socket_Commu.stock_by_categories[0] += a;
        Socket_Commu.stock_by_categories[1] += b;
        Socket_Commu.stock_by_categories[2] += c;
        Socket_Commu.stock_by_categories[3] += d;
        yield return new WaitForSeconds(0.5f);
        Trucks[0].transform.rotation = FirstRot[0];
        Trucks[0].transform.position = FirstPos[0];
        Trucks[1].transform.rotation = FirstRot[1];
        Trucks[1].transform.position = FirstPos[1];
        Trucks[2].transform.rotation = FirstRot[2];
        Trucks[2].transform.position = FirstPos[2];
        Trucks[3].transform.rotation = FirstRot[3];
        Trucks[3].transform.localPosition = FirstPos[3];
        Trucks[4].transform.localPosition = FirstPos[4];
    }

    IEnumerator GoFirst()
    {
        yield return new WaitForSeconds(5f);
        Trucks[0].transform.rotation = FirstRot[0];
        Trucks[0].transform.position = FirstPos[0];
        Trucks[1].transform.rotation = FirstRot[1];
        Trucks[1].transform.position = FirstPos[1];
        Trucks[2].transform.rotation = FirstRot[2];
        Trucks[2].transform.position = FirstPos[2];
        Trucks[3].transform.rotation = FirstRot[3];
        Trucks[3].transform.localPosition = FirstPos[3];
        Trucks[4].transform.localPosition = FirstPos[4];
    }
}
