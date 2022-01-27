using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Text;


public class Socket_Commu : MonoBehaviour
{
    // Start is called before the first frame update
    Socket sock;
    byte[] receiverBuff;
    string data_arr;

    public Text[] text_place_cat = new Text[17];//0메인,1~4 예측량, 5~8 실 주문, 9~12 합계, 13~16 소창 재고
    public Text day_text;
    public GameObject TPanel;
    public int[] realorder;
    public int[] prediction;
    Vector3 BigScale;
    Vector3 SmallScale;
    Vector3 BigPos;
    Vector3 SmallPos;
    Vector3 LastPos;
    byte[] buff;

    public static int[] stock_by_categories = new int[4];

    //Objects
    public CompanyScript[] Companys = new CompanyScript[4];
    public WarehouseScript WScript;
    public ToCustomer TCScript;
    public GameObject cam;
    public Vector3 camFirstPos;
    public Vector3 camSecondPos;
    public Vector3 camThirdPos;
    public Vector3 camFourthPos;
    public Vector3 camMovePos;
    public Vector3 camFinalPos;
    public Vector3 camRealFinalPos;


    public CompanyTruckMove CTMscript;
    public Text infotext;
    int[] dif;
    int EstimateLoss;
    int reverse_Accuracy;
    float totalHour;
    float totalNum;
    public Text resultText;

    void Awake()
    {
        sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
        try
        {
            sock.Connect(ep);
        }
        catch
        {
            Debug.Log("Failed");
        }
        
        receiverBuff = new byte[1024];
    }

    void Start()
    {
        SmallPos = new Vector3(689, -288, 0);
        LastPos = new Vector3(0, -288, 0);
        BigPos = TPanel.gameObject.transform.localPosition;
        SmallScale = TPanel.gameObject.transform.localScale;
        BigScale = SmallScale * 1.5f;
        TPanel.gameObject.transform.localScale = SmallScale;
        TPanel.gameObject.transform.localPosition = SmallPos;

        cam.transform.position = new Vector3(193f, 247f, -85f);

        camFirstPos = new Vector3(104.1f, 118.6f, -96.2f);//컴->대
        camSecondPos = new Vector3(96f, 107f, 34f); //대->소
        camMovePos = new Vector3(58f, 91f, 34f);//대->소
        camThirdPos = new Vector3(58f, 91f, 115.1f);//소->비자
        camFourthPos = new Vector3(53.6f,82.9f,99f);//소->비자
        camFinalPos = new Vector3(93.5f, 103.3f, 27f);
        camRealFinalPos = new Vector3(117.7f, 138.2f, -30.5f);
        dif = new int[4] { 0,0,0,0};
        EstimateLoss = 0;
        reverse_Accuracy = 0;
        totalHour = 0;
        totalNum = 0;

        StartCoroutine(SendRcv());
    }

    IEnumerator test()
    {
        while(true)
        {
            int ran = UnityEngine.Random.Range(1, 5);
            TCScript.MakeObject(ran);
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator SendRcv()
    {
        yield return new WaitForSeconds(3f);
        int j = 0;
        int i = 0;
        realorder = new int[40];
        prediction = new int[4];

        string isHol = "";
        day_text.text = "0일차";
        while (j<40)
        {
            int n = sock.Receive(receiverBuff);
            string data = Encoding.UTF8.GetString(receiverBuff, 0, n);
            float f = float.Parse(data);
            realorder[j] = (int)Mathf.Ceil(f);
            totalHour += realorder[j] * 4;
            totalNum += realorder[j];
            j++;
            buff = Encoding.UTF8.GetBytes(data);
            sock.Send(buff, SocketFlags.None);
            yield return new WaitForSeconds(0.1f);
        }
       
        j = 0;
        infotext.text = "NearBuy 물류 시뮬레이션";
        yield return new WaitForSeconds(4f);
        yield return StartCoroutine(MoveCam(cam.transform.position, camFirstPos, 0));

        yield return StartCoroutine(Blink(day_text.gameObject, 3));

        infotext.text = "0일차 시작.\n서버로부터 다음날 주문 예측량을 받아옴";
        yield return new WaitForSeconds(4f);
        while (i<10)
        {
            if (i.Equals(2) || i.Equals(3) || i.Equals(9))
            {
                isHol = "(주말)";
            }
            else isHol = "(주중)";
            text_place_cat[0].text = "A 지역 "+(i).ToString()+" 일차 " + isHol;
            j = 0;
            //아침 1일차부턴 소형->소비자, 0일차는 예측표
            day_text.text = i.ToString() + "일차";
            if (i.Equals(0))//0일차
            {
                TPanel.gameObject.transform.localScale = BigScale;
                TPanel.gameObject.transform.localPosition = BigPos;

                while (j < 4)
                {
                    int n = sock.Receive(receiverBuff);
                    string data = Encoding.UTF8.GetString(receiverBuff, 0, n);
                    float f = float.Parse(data);
                    prediction[j] = (int)Mathf.Ceil(f);
                    text_place_cat[j + 1].text = prediction[j].ToString();
                    j++;
                    buff = Encoding.UTF8.GetBytes(data);
                    sock.Send(buff, SocketFlags.None);
                    yield return new WaitForSeconds(0.5f);
                }
                TPanel.gameObject.transform.localPosition = SmallPos;
                TPanel.gameObject.transform.localScale = SmallScale;
                infotext.text = "1일차의 \n a 카테고리 예측량: " + prediction[0].ToString() + "\n b 카테고리 예측량: " + prediction[1].ToString() +
                    "\n c 카테고리 예측량: " + prediction[2].ToString() + "\n d 카테고리 예측량: " + prediction[3].ToString();
                yield return new WaitForSeconds(2f);
            }
            else//1일차부터 소->비자
            {
                day_text.text = (i).ToString() + "일차";
                if (i.Equals(1))
                {
                    infotext.text = "다음날 주문량만큼 소형창고에서 소비자에게 물건 전달" +
                        "\n a 카테고리: " + realorder[0].ToString() + " b 카테고리: " + realorder[1].ToString() + "\n c 카테고리: " + realorder[2].ToString() + " d 카테고리: " + realorder[3].ToString();
                    yield return new WaitForSeconds(2f);

                    yield return StartCoroutine(Blink(day_text.gameObject, 3));
                    yield return StartCoroutine(MoveCam(cam.transform.position, camThirdPos, 1));
                    yield return StartCoroutine(PopMoveCam(camFourthPos));
                    int k = 0;
                    for (j = 0; j < 4; j++)
                    {
                        k = 0;
                        while (k < realorder[(i - 1) * 4 + j])
                        {
                            if (stock_by_categories[j].Equals(0))
                            {
                                break;
                            }
                            TCScript.MakeObject(j + 1);
                            stock_by_categories[j]--;
                            text_place_cat[13 + j].text = stock_by_categories.ToString();
                            k++;
                            yield return new WaitForSeconds(1f);
                        }
                        text_place_cat[5 + j].text = realorder[(i - 1) * 4 + j].ToString();
                        if ((prediction[j] + dif[j] - realorder[(i - 1) * 4 + j]) > 0)
                        {
                            dif[j] = (prediction[j] + dif[j] - realorder[(i - 1) * 4 + j]);
                            EstimateLoss += Mathf.Abs(dif[j]);
                            text_place_cat[9 + j].color = new Color(0, 0, 1, 1);
                        }
                        else if ((prediction[j] + dif[j] - realorder[(i - 1) * 4 + j]) < 0)
                        {
                            dif[j] = (prediction[j] + dif[j] - realorder[(i - 1) * 4 + j]);
                            text_place_cat[9 + j].color = new Color(1, 0, 0, 1);
                            EstimateLoss += Mathf.Abs(dif[j]);
                        }
                        else
                        {
                            dif[j] = 0;
                        }

                        text_place_cat[9 + j].text = (prediction[j] - realorder[(i - 1) * 4 + j]).ToString();
                    }
                    infotext.text = "소형창고 재고" + "\n a 카테고리: " + (prediction[0] - realorder[0]).ToString() + " b 카테고리: " + (prediction[1] - realorder[1]).ToString() +
        "\n c 카테고리: " + (prediction[2] - realorder[2]).ToString() + " d 카테고리: " + (prediction[3] - realorder[3]).ToString();
                    yield return new WaitForSeconds(3.5f);
                }
                else
                {
                    yield return StartCoroutine(Blink(day_text.gameObject, 2));

                    int k = 0;
                    for (j = 0; j < 4; j++)
                    {
                        k = 0;
                        while (k < realorder[(i - 1) * 4 + j])
                        {
                            if (stock_by_categories[j].Equals(0))
                            {
                                break;
                            }
                            TCScript.MakeObjectFast(j + 1);
                            stock_by_categories[j]--;
                            text_place_cat[13 + j].text = stock_by_categories.ToString();
                            k++;
                            yield return new WaitForSeconds(0.2f);
                        }
                        text_place_cat[5 + j].text = realorder[(i - 1) * 4 + j].ToString();
                        if ((prediction[j] + dif[j] - realorder[(i - 1) * 4 + j]) > 0)
                        {
                            dif[j] = (prediction[j] + dif[j] - realorder[(i - 1) * 4 + j]);
                            text_place_cat[9 + j].color = new Color(0, 0, 1, 1);
                            EstimateLoss += Mathf.Abs(dif[j]);
                            reverse_Accuracy += 1;
                        }
                        else if ((prediction[j] + dif[j] - realorder[(i - 1) * 4 + j]) < 0)
                        {
                            dif[j] = (prediction[j] + dif[j] - realorder[(i - 1) * 4 + j]);
                            text_place_cat[9 + j].color = new Color(1, 0, 0, 1);
                            EstimateLoss += Mathf.Abs(dif[j]);
                            reverse_Accuracy += 1;
                            totalHour += 24;
                        }
                        else
                        {
                            dif[j] = 0;
                        }

                        text_place_cat[9 + j].text = (prediction[j] - realorder[(i - 1) * 4 + j]).ToString();
                    }
                }

            }
            yield return new WaitForSeconds(2f);
            // 아침 끝, 점심으로, 0일차 사업체->대형, 1일차부턴 다음날 예측
            day_text.text = i.ToString() + "일차";

            j = 0;
            if (i.Equals(0))
            {
                infotext.text = "각 사업체에서 대형창고로 물품 배송";
                yield return new WaitForSeconds(1f);
                yield return StartCoroutine(CTMscript.MoveTwo());
            }
            else
            {
                if (i.Equals(1))
                {
                    yield return new WaitForSeconds(2f);
                    TPanel.gameObject.transform.localScale = BigScale;
                    TPanel.gameObject.transform.localPosition = BigPos;

                    while (j < 4)
                    {
                        int n = sock.Receive(receiverBuff);
                        string data = Encoding.UTF8.GetString(receiverBuff, 0, n);
                        float f = float.Parse(data);
                        prediction[j] = (int)Mathf.Ceil(f);
                        text_place_cat[j + 1].text = prediction[j].ToString();
                        if (!dif[j].Equals(0))
                        {
                            yield return new WaitForSeconds(2f);
                            yield return StartCoroutine(Blink(text_place_cat[9 + j].gameObject, 2));
                            prediction[j] = prediction[j] - dif[j];
                            //text_place_cat[9 + j].text = 0.ToString();
                            text_place_cat[j + 1].text = prediction[j].ToString();
                            yield return StartCoroutine(Blink(text_place_cat[j + 1].gameObject, 2));
                        }
                        text_place_cat[9 + j].text = 0.ToString();
                        text_place_cat[13 + j].text = 0.ToString();
                        text_place_cat[9 + j].color = new Color(0, 0, 0, 1);
                        buff = Encoding.UTF8.GetBytes(data);
                        sock.Send(buff, SocketFlags.None);
                        yield return new WaitForSeconds(0.5f);
                        j++;
                    }
                    TPanel.gameObject.transform.localPosition = SmallPos;
                    TPanel.gameObject.transform.localScale = SmallScale;
                }
                else
                {

                    while (j < 4)
                    {
                        int n = sock.Receive(receiverBuff);
                        string data = Encoding.UTF8.GetString(receiverBuff, 0, n);
                        float f = float.Parse(data);
                        prediction[j] = (int)Mathf.Ceil(f);
                        text_place_cat[j + 1].text = prediction[j].ToString();
                        if (!dif[j].Equals(0))
                        {
                            prediction[j] = prediction[j] - dif[j];
                            text_place_cat[9 + j].text = 0.ToString();
                            text_place_cat[j + 1].text = prediction[j].ToString();
                        }
                        text_place_cat[9 + j].text = 0.ToString();
                        text_place_cat[13 + j].text = 0.ToString();
                        text_place_cat[9 + j].color = new Color(0, 0, 0, 1);
                        buff = Encoding.UTF8.GetBytes(data);
                        sock.Send(buff, SocketFlags.None);
                        yield return new WaitForSeconds(0.05f);
                        j++;
                    }
                }
            }
            for (int k = 0; k < 4; k++)
            {
                text_place_cat[5 + j].text = 0.ToString();
            }
            j = 0;
            if(i.Equals(0))
            {
                infotext.text = "대형창고에서 소형창고로 예측 수량만큼 물품 배송";
                yield return new WaitForSeconds(3f);
            }
            else if(i.Equals(1))
            {
                infotext.text = "소형창고에 남거나 모자란만큼 예측량을 조절";
                yield return new WaitForSeconds(3f);
            }
            //점심끝, 저녁으로 넘어감, 대형->소형
            if(i.Equals(0)) yield return StartCoroutine(MoveCam(cam.transform.position, camSecondPos, 1));
            if(i.Equals(0)||i.Equals(1))
            {
                yield return StartCoroutine(PopMoveCam(camMovePos));

                yield return new WaitForSeconds(2f);

                yield return StartCoroutine(CTMscript.MoveToLittle(prediction[0], prediction[1], prediction[2], prediction[3]));

                yield return new WaitForSeconds(2f);

            }
            else
            {


                yield return StartCoroutine(CTMscript.MoveToLittleFast(prediction[0], prediction[1], prediction[2], prediction[3]));

            }
            if(i.Equals(1))
            {
                infotext.text = "10일차까지의 시뮬레이션 시작";
                yield return new WaitForSeconds(4f);
                infotext.transform.parent.gameObject.SetActive(false);
                yield return StartCoroutine(PopMoveCam(camFinalPos));
                TPanel.gameObject.transform.localPosition = LastPos;
            }
            i++;
            buff = Encoding.UTF8.GetBytes("ok");
            sock.Send(buff, SocketFlags.None);
            Debug.Log("next day");
            if (i.Equals(0))
                yield return new WaitForSeconds(4f);
            else yield return new WaitForSeconds(1f);

        }
        day_text.text = "10일차";
        StartCoroutine(Blink(day_text.gameObject, 2));
        yield return StartCoroutine(PopMoveCam(camRealFinalPos));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(CTMscript.MoveFast());

        resultText.transform.parent.gameObject.SetActive(true);
        //resultText.text = "10일차 까지의 시뮬레이션 끝";
        float realAcc = (float)(40 - reverse_Accuracy)/40 *100;
        float realLoss = (float)EstimateLoss / 10;
        yield return new WaitForSeconds(2f);
        resultText.text = "결과표\n 4개 카테고리, 10일. 총 예측량 40개\n" +
            "정확한 예측 비율: " + realAcc.ToString() + "%\n" + "오차율: " + realLoss.ToString() + "/일\n" +
            "\n소형창고에 재고가 있을시 배달이 주문으로부터 4시간 걸린다고 가정시\n(재고 없을시 24시간 더 걸린다고 가정)\n" +
            "\n 평균 배달 시간: " + ((float)(totalHour / totalNum)).ToString() + "시간/물품";
        yield return new WaitForSeconds(10f);
        resultText.text = "이상으로 NearBuy의 물류 시뮬레이션 이었습니다. 감사합니다.";
        Debug.Log(reverse_Accuracy.ToString());
        Debug.Log(EstimateLoss.ToString());
        Debug.Log(totalHour.ToString());
        Debug.Log(totalNum.ToString());
        exit();
        yield return null;

    }


    public void exit()
    {
        byte[] buff = Encoding.UTF8.GetBytes("exit");
        sock.Send(buff, SocketFlags.None);

        sock.Close();
        Debug.Log("종료");
    }

    public IEnumerator Blink(GameObject objt, int num)
    {
        int i = 0;
        while(i<num)
        {
            objt.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            objt.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            i++;
        }
    }

    public IEnumerator MoveCam(Vector3 firstPos, Vector3 secondPos, int axis)
    {
        if (axis.Equals(0))
        {
            Vector3 dif = new Vector3(0, 0, 0);
            dif = secondPos - firstPos;
            while (cam.transform.position.x>=secondPos.x)
            {

                cam.transform.position+=(dif * 0.005f);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            Vector3 dif = new Vector3(0, 0, 0);
            dif = secondPos - firstPos;
            while (cam.transform.position.z <= secondPos.z)
            {

                cam.transform.position += (dif * 0.05f);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public IEnumerator PopMoveCam(Vector3 pos)
    {
        yield return new WaitForSeconds(0.5f);
        cam.transform.position = pos;
        yield return null;
    }

    // Update is called once per frame

}
