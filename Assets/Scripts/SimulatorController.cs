using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//중앙 제어, 리소스 관리, WareHouse(BIG) 역할
public class SimulatorController : MonoBehaviour {
    /**
    public Transform[] Objs = new Transform[4];
    public Transform[] COMPANYS = new Transform[4];

    //Positions
    private Vector3[] companys = new Vector3[4];
    private Vector3 big = new Vector3();

    //Targets
    public Transform BIG;
    public Transform[] SMALL = new Transform[4];
    GameObject EMPTYOBJ;
    public Transform[] TargetWareHouse = new Transform[4];**/
    public TextMesh Type1_Text, Type2_Text, Type3_Text, Type4_Text;

    public static int Type1_Count = 0, Type2_Count = 0, Type3_Count = 0, Type4_Count = 0;

    public Text[] text_Warehouse = new Text[4];//소창 재고 패널, 소창 재고 위에
    public TextMesh[] text_Waretop = new TextMesh[4];

    private void Start()
    {
        StartCoroutine(TextCheck());
    }
    /**
    private void Awake()
    {
        for(int i = 0; i < 4; i++)
        {
            companys[i] = COMPANYS[i].position;
        }

        big = BIG.position;

        EMPTYOBJ = new GameObject();
    }
    
    // Company용
    public Transform MakePackage(Transform[] list, int count, Vector3 pos)
    {
        Transform obj = Instantiate(EMPTYOBJ, pos, Quaternion.identity).transform;

        for(int i = 0; i < count; i++)
        {
            list[i].SetParent(obj, true);
        }
        
        return obj;
    }

    // WareHouse용
    public Transform MakePackageForWarehouse(int target, int count, Vector3 pos)
    {
        Transform obj = Instantiate(EMPTYOBJ, pos, Quaternion.identity).transform;

        for(int i = 0; i < count; i++)
        {
            TargetWareHouse[target].GetChild(0).SetParent(obj, true);
        }

        return obj;
    }

    public Transform ObjectPop(Vector3 pos, int type, int target)
    {
        Transform obj_ = Instantiate(Objs[(type-1)], pos, Quaternion.identity, transform);

        //int target = Random.Range(0, 4);

        obj_.GetComponent<ObjectScript>().SetInfo(target, type);

        return obj_;
    }

    public IEnumerator MovePackage(Transform package, int count)
    {
        yield return new WaitForSeconds(1);

        Vector3 moveVec = new Vector3();

        moveVec = (-package.position) * 0.01f;

        while (package.position.z < 0)
        {
            package.position += moveVec;

            yield return new WaitForSeconds(0.01f);
        }

        for(int i = 0; i < count; i++)
        {
            Transform obj = package.GetChild(0);
            StoreThis(obj);
        }

        yield return new WaitForSeconds(0.5f);

        Destroy(package.gameObject);
    }

    public void StoreThis(Transform obj)
    {
        ObjectScript script_ = obj.GetComponent<ObjectScript>();
        int type = script_.Get_Type();
        int target_num = script_.GetTarget();

        switch(type)
        {
            case 1: Type1_Count++; break;
            case 2: Type2_Count++; break;
            case 3: Type3_Count++; break;
            case 4: Type4_Count++; break;
        }

        obj.SetParent(TargetWareHouse[target_num], true);
        WarehouseScript.Stock_Count[target_num]++;

        obj.gameObject.SetActive(false);
    }
    **/

    IEnumerator TextCheck()
    {
        while(true)
        {
            Type1_Text.text = Type1_Count.ToString();
            Type2_Text.text = Type2_Count.ToString();
            Type3_Text.text = Type3_Count.ToString();
            Type4_Text.text = Type4_Count.ToString();
            text_Warehouse[0].text = Socket_Commu.stock_by_categories[0].ToString();
            yield return new WaitForSeconds(0.05f);
            text_Waretop[0].text = Socket_Commu.stock_by_categories[0].ToString();
            yield return new WaitForSeconds(0.05f);
            text_Warehouse[1].text = Socket_Commu.stock_by_categories[1].ToString();
            yield return new WaitForSeconds(0.05f);
            text_Waretop[1].text = Socket_Commu.stock_by_categories[1].ToString();
            yield return new WaitForSeconds(0.05f);
            text_Warehouse[2].text = Socket_Commu.stock_by_categories[2].ToString();
            yield return new WaitForSeconds(0.05f);
            text_Waretop[2].text = Socket_Commu.stock_by_categories[2].ToString();
            yield return new WaitForSeconds(0.05f);
            text_Warehouse[3].text = Socket_Commu.stock_by_categories[3].ToString();
            yield return new WaitForSeconds(0.05f);
            text_Waretop[3].text = Socket_Commu.stock_by_categories[3].ToString();
            yield return new WaitForSeconds(0.05f);
        }
    }

}
