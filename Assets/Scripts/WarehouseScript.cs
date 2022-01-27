using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//여기서 목표지에 발송
public class WarehouseScript : MonoBehaviour
{
    /**public static int[] Stock_Count = {0, 0, 0, 0};
    public Transform[] Targets = new Transform[4];
    Vector3[] Targets_pos = new Vector3[4];
    public SimulatorController SC;
    private Vector3 pos = new Vector3(0, 0, 25);
    public Transform Car;

    private void Start()
    {
        for(int i = 0; i < 4; i++)
            Targets_pos[i] = Targets[i].position;

        //StartCoroutine(DeliveryObj());
    }

    public IEnumerator DeliveryObj(int target_stock, int count)
    {

            //int target_stock = GetBigStockCount();
            //int count = Stock_Count[target_stock];

            Transform Package = SC.MakePackageForWarehouse(target_stock, count, pos);

            Transform[] list = new Transform[count];

            for (int i = 0; i < count; i++)
            {
                list[i] = Package.GetChild(i);

                SetCountForType(list[i]);
            }

            Transform car = Instantiate(Car, pos, Quaternion.identity);
            CarScript carScript = car.GetComponent<CarScript>();
            carScript.LoadPackage(Package, Targets_pos[target_stock]);

            Stock_Count[target_stock] -= count;

            yield return new WaitForSeconds(1);
    }

    IEnumerator DeliveryObj()
    {
        yield return new WaitForSeconds(10);


        while(true)
        {
            int target_stock = GetBigStockCount();
            int count = Stock_Count[target_stock];

            Transform Package = SC.MakePackageForWarehouse(target_stock, count, pos);

            Transform[] list = new Transform[count];

            for(int i = 0; i < count; i++)
            {
                list[i] = Package.GetChild(i);

                SetCountForType(list[i]);
            }

            Transform car = Instantiate(Car, pos, Quaternion.identity);
            CarScript carScript = car.GetComponent<CarScript>();
            carScript.LoadPackage(Package, Targets_pos[target_stock]);

            Stock_Count[target_stock] -= count;

            yield return new WaitForSeconds(3);
        }
    }

    private int GetBigStockCount()
    {
        int temp = 0;

        for(int i = 0; i < 4; i++)
        {
            if(Stock_Count[temp] < Stock_Count[i])
                temp = i;
        }

        return temp;
    }

    private void SetCountForType(Transform obj)
    {
        int type = obj.GetComponent<ObjectScript>().Get_Type();

        switch(type)
        {
            case 1: SimulatorController.Type1_Count--; break;
            case 2: SimulatorController.Type2_Count--; break;
            case 3: SimulatorController.Type3_Count--; break;
            case 4: SimulatorController.Type4_Count--; break;
        }
    }**/
}
