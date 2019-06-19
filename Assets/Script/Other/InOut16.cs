using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public delegate void OnUpdateIn16Out16(byte in1, byte in2, byte out1, byte out2);
public class InOut16 : MonoBehaviour {


    public OnUpdateIn16Out16 onUpdateIn16Out16;
    public string host = "192.168.1.222";
    public int port = 3000;
    static Socket socket;
    static bool[] relay16state = new bool[17];

    internal static void CloseAlreadyStartedDevice()
    {
        if (socket == null)
        {
            Debug.LogError("socket == null");
        }
        byte[] data = new byte[8];
        data[0] = 0x55;
        data[1] = 0x01;
        data[2] = 0x13;
        data[3] = 0x00;
        data[4] = 0x00;
        data[5] = 0x00;
        data[6] = 0x00;//继电器
        for (int i = 0; i < 7; i++)
            data[7] += data[i];

        Send(data);
    }

    public Text t;
    public static InOut16 instance;
    private void Awake()
    {
        instance = this;
    }
    private bool isOpenReadThread = true;
    private Thread readThread;


    public byte[] testbyte;
    void Start ()
    {
        if (Connect(host, port))
        {
            if (isOpenReadThread)
            {
                readThread = new Thread(ReadThread);
                readThread.Start(socket);
            }
           
        }
    }
   public byte[] readbuf = new byte[8];
    private void ReadThread(object obj)
    {
        Socket sock = obj as Socket;
        if (sock==null)
        {
            Debug.LogError("ReadThread::sock==null");
            return;
        }
        
        while (sock.Connected)
        {
            if (sock.ReceiveBufferSize >= 8)
            {
                sock.Receive(readbuf,0, readbuf.Length,SocketFlags.None);
                if (readbuf[0] == 0x22)
                {
                    if (readbuf[7]== (byte)(readbuf[0] + readbuf[1] + readbuf[2] + readbuf[3] + readbuf[4] + readbuf[5] + readbuf[6]))
                    {

                        readmges.Enqueue(readbuf);
                        
                    }
                    else
                    {
                        Debug.LogError("校验未通过");//
                    }
                }
                else
                {
                    Debug.LogError("(readbuf[0] == 0x22)::" + readbuf[0]);
                }
            }//参数列表 异常参数
            else
            {
                Thread.Sleep(100);
            }
        }
    }//异常参数
    private void OnDestroy()
    {
        Close();
    }
    private Queue<byte[]> readmges = new Queue<byte[]>();
	void Update ()
    {
        lastsendtiming += Time.deltaTime;
        if (lastsendtiming>= 0.5f)
        {
            SendQueryIN16();
        }

        if (readmges.Count >=1)
        {
           HandleReadBuffer (readmges.Dequeue());
        }
 
      
        InOut16KetTest();


    }
    void InOut16KetTest()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SendQueryAnalog_93();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SendQueryAnalog_94();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchRelay16state(1, relay16state[1]);
            relay16state[1] = !relay16state[1];

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchRelay16state(2, relay16state[2]);
            relay16state[2] = !relay16state[2];

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchRelay16state(3, relay16state[3]);
            relay16state[3] = !relay16state[3];

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchRelay16state(4, relay16state[4]);
            relay16state[4] = !relay16state[4];

        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchRelay16state(5, relay16state[5]);
            relay16state[5] = !relay16state[5];

        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SwitchRelay16state(6, relay16state[6]);
            relay16state[6] = !relay16state[6];

        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SwitchRelay16state(7, relay16state[7]);
            relay16state[7] = !relay16state[7];

        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SwitchRelay16state(8, relay16state[8]);
            relay16state[8] = !relay16state[8];
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SwitchRelay16state(9, relay16state[9]);
            relay16state[9] = !relay16state[9];
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SwitchRelay16state(10, relay16state[10]);
            relay16state[10] = !relay16state[10];

        }
        if (Input.GetKeyDown(KeyCode.LeftShift)&&Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchRelay16state(11, relay16state[11]);
            relay16state[11] = !relay16state[11];
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchRelay16state(12, relay16state[12]);
            relay16state[12] = !relay16state[12];

        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchRelay16state(13, relay16state[13]);
            relay16state[13] = !relay16state[13];

        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchRelay16state(14, relay16state[14]);
            relay16state[14] = !relay16state[14];

        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchRelay16state(51, relay16state[15]);
            relay16state[15] = !relay16state[15];

        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha6))
        {
            SwitchRelay16state(16, relay16state[16]);
            relay16state[16] = !relay16state[16];

        }
    }
    static void TextLog(string str)
    {
        if (instance&& instance.t)
        {
            instance.t.text = str;
        }
    }
    static bool Connect(string host, int port)
    {
        IPAddress ip = IPAddress.Parse(host);
        IPEndPoint ipe = new IPEndPoint(ip, port);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(ipe);
        if (socket.Connected)
        {
            //Debug.Log("socket Connected");
            TextLog("socket Connected");
            return true;
        }
        else
        {
            Debug.LogError("socket no Connected");
            TextLog("socket no Connected");
            return false;

        }
    }
    static void Close()
    {
        if (socket!=null)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
    static float lastsendtiming = 0;
    static int Send( byte[] bs)
    {
        lastsendtiming = 0;
        int retState = 0;
        
        if (socket == null)
        {
            Debug.LogError("socket == null");
            return retState;
        }
        //bs[7] = (byte)(bs[0] + bs[1] + bs[2] + bs[3] + bs[4] + bs[5] + bs[6]);
        retState = socket.Send(bs, bs.Length, SocketFlags.None);//发送测试信息

        return retState;
    }
    static void Receive()
    {
        if (socket == null)
        {
            Debug.LogError("socket == null");
        }
    }
   public void SwitchRelay16state_(Toggleid id)
    {
        SwitchRelay16state(id.id, id.GetComponent<Toggle>().isOn);
    }
    public static void SwitchRelay16state(int n, bool state)
    {
        if (socket == null)
        {
            Debug.LogError("socket == null");
        }
        byte[] data = new byte[8];
        data[0] = 0x55;
        data[1] = 0x01;
        data[2] = state ? (byte)0x12 : (byte)0x11; // 开 关 翻转 0x12 0x11 0x17
        data[3] = 0x00;
        data[4] = 0x00;
        data[5] = 0x00;
        data[6] = (byte)n;//继电器
        for (int i = 0; i < 7; i++)
            data[7] += data[i];

        Send(data);

    }
    void SendQuery80()
    {
        Debug.Log("s80");
        if (socket == null)
        {
            Debug.LogError("socket == null");
        }
        byte[] data = new byte[8];

        data[0] = 0x55;
        data[1] = 0x01;
        data[2] = 0x80;
        data[3] = 0x00;
        data[4] = 0x00;
        data[5] = 0x00;
        data[6] = 0x00;//继电器
        for (int i = 0; i < 7; i++)
            data[7] += data[i];

        Send(data);


    }
    void SendQuery82()
    {
        Debug.Log("s82");

        if (socket == null)
        {
            Debug.LogError("socket == null");
        }
        byte[] data = new byte[8];

        data[0] = 0x55;
        data[1] = 0x01;
        data[2] = 0x82;
        data[3] = 0x00;
        data[4] = 0x00;
        data[5] = 0x00;
        data[6] = 0x00;
        for (int i = 0; i < 7; i++)
            data[7] += data[i];

        Send(data);
    }
    void SendQueryIN16()
    {
        if (socket == null)
        {
            Debug.LogError("socket == null");
        }
        byte[] data = new byte[8];
        data[0] = 0x55;
        data[1] = 0x01;
        data[2] = 0x81;
        data[3] = 0x00;
        data[4] = 0x00;
        data[5] = 0x00;
        data[6] = 0x00;//继电器
        for (int i = 0; i < 7; i++)
            data[7] += data[i];

        Send(data);
    }

    void SendQueryAnalog_93()
    {
        if (socket == null)
        {
            Debug.LogError("socket == null");
        }
        byte[] data = new byte[8];
        data[0] = 0x55;
        data[1] = 0x01;
        data[2] = 0x93;
        data[3] = 0x00;
        data[4] = 0x00;
        data[5] = 0x00;
        data[6] = 0x00;
        for (int i = 0; i < 7; i++)
            data[7] += data[i];

        Send(data);
    }
    void SendQueryAnalog_94()
    {
        if (socket == null)
        {
            Debug.LogError("socket == null");
        }
        byte[] data = new byte[8];
        data[0] = 0x55;
        data[1] = 0x01;
        data[2] = 0x94;
        data[3] = 0x00;
        data[4] = 0x00;
        data[5] = 0x00;
        data[6] = 0x00;
        for (int i = 0; i < 7; i++)
            data[7] += data[i];

        Send(data);
    }

    public void HandleReadBuffer(byte[] buf)
    {
        switch (buf[2])
        {
            case 0x81://read
                if (onUpdateIn16Out16!=null)
                {
                    onUpdateIn16Out16(buf[3], buf[4], buf[5], buf[6]);

                }
                break;
        }
    }
    public static void TurnRelay16state(int n)
    {
        if (socket == null)
        {
            Debug.LogError("socket == null");
        }
        byte[] data = new byte[8];
        data[0] = 0x55;
        data[1] = 0x01;
        data[2] = 0x17;  // 开 关 翻转 0x12 0x11 0x17
        data[3] = 0x00;
        data[4] = 0x00;
        data[5] = 0x00;
        data[6] = (byte)n;//继电器
        for (int i = 0; i < 7; i++)
            data[7] += data[i];

        Send(data);
    }
    public static void OnDeviceSwitchgear(string dname, int id, bool isopen)
    {
        //Debug.Log("Inout16::OnDeviceSwitchgear:"+ dname+id+isopen);
        switch (dname)
        {
            case "EmulsionPump"://乳化泵
                switch (id)
                {
                    case 1:
                        InOut16.SwitchRelay16state(1, isopen);
                        break;
                    case 2:
                        InOut16.SwitchRelay16state(2, isopen);

                        break;
                    case 3:
                        InOut16.SwitchRelay16state(3, isopen);
                        break;
                    case 4:
                        InOut16.SwitchRelay16state(4, isopen);
                        break;
                    default:
                        Debug.Log("未知设备");
                        break;
                }
                break;
            case "ClarifiedWaterPump"://清水泵
                switch (id)
                {
                    case 1:
                        InOut16.SwitchRelay16state(5, isopen);
                        break;
                    case 2:
                        InOut16.SwitchRelay16state(6, isopen);

                        break;

                    default:
                        Debug.Log("未知设备");
                        break;
                }
                break;
            case "BeltConveyor"://皮带机
                switch (id)
                {
                    case 1:
                        InOut16.SwitchRelay16state(7, isopen);
                        break;
                    case 2:
                        InOut16.SwitchRelay16state(8, isopen);

                        break;
                    case 3:
                        InOut16.SwitchRelay16state(9, isopen);
                        break;
                    case 4:
                        InOut16.SwitchRelay16state(10, isopen);
                        break;
                    default:
                        Debug.Log("未知设备");
                        break;
                }
                break;
            case "Crusher"://破碎机
                InOut16.SwitchRelay16state(11, isopen);
                break;
            case "ReversedLoader"://转运机
                InOut16.SwitchRelay16state(12, isopen);
                break;

            case "ScraperConveyor_Front"://前刮板输送机
                InOut16.SwitchRelay16state(13, isopen);
                break;

            case "ScraperConveyor_Back"://后刮板输送机
                InOut16.SwitchRelay16state(14, isopen);

                break;

            case "CoalCutter"://采煤机
                InOut16.SwitchRelay16state(15, isopen);
                break;

            case "Warning"://警告
                InOut16.SwitchRelay16state(16, isopen);
                break;

            default:
                Debug.Log("未知设备");
                break;
        }
    }


    private IFormatter formatter = new BinaryFormatter();
    private ValueType deserializeByteArrayToInfoObj(byte[] bytes)
    {
        ValueType vt;
        if (bytes == null || bytes.Length == 0)
        {
            return null;
        }

        try
        {
            MemoryStream stream = new MemoryStream(bytes);
            stream.Position = 0;
            stream.Seek(0, SeekOrigin.Begin);
            vt = (ValueType)formatter.Deserialize(stream);
            stream.Close();
            return vt;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return null;
        }
    }
    private byte[] serializeInfoObjToByteArray(ValueType infoStruct)
    {
        if (infoStruct == null)
        {
            return null;
        }
        try
        {
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, infoStruct);

            byte[] bytes = new byte[(int)stream.Length];
            stream.Position = 0;
            stream.Read(bytes, 0, (int)stream.Length);
            stream.Close();
            return bytes;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return null;
        }
    }
}
