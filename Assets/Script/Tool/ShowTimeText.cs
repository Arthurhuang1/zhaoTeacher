using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 显示时间文本 不是表演时间
/// </summary>
[RequireComponent(typeof(Text))]
public class ShowTimeText : MonoBehaviour {

    public enum ShowConnect : byte
    {
        毫秒 = 1,

        秒 = 2,
        分 = 4,
        时 = 8,

        日 = 16,
        月 = 32,
        年 = 64,

        分秒 = 分 + 秒,
        时分秒 = 时 + 分秒,
        时分秒毫秒 = 时分秒 + 毫秒,

        年月 = 年 + 月,
        年月日 = 年 + 月 + 日,
        年月日时= 年月日+ 时,
        年月日时分 = 年月日 + 时+ 分,
        年月日时分秒 = 年月日 + 时分秒,
        年月日时分秒毫秒 = 年月日时分秒 + 毫秒,

        月日时 = 月+ 日+ 时,

    }
    public bool isUpdate = true;//是否帧跟新
    public string spacingSymbol = "-";//分隔符
    [SerializeField]
    private ShowConnect defaultShowConnect;//默认显示内容 编辑器接口

    private Text showtext;
    private string show = "";
    private byte showConnectByte = 0xFF;

    public bool connectByteFollowEnum = false;//是否以枚举为主 通过编辑器中设置枚举属性来改变字节，默认以字节为主更灵活 
    public ShowConnect ShowConnectEnum
    {
        get
        {
            defaultShowConnect = (ShowConnect)showConnectByte;
            return defaultShowConnect;
        }
        set
        {
            defaultShowConnect = value;
            showConnectByte = (byte) defaultShowConnect;
        }
    }
    public byte ShowConnectByte { get { return showConnectByte; } set { showConnectByte = value; } }
    
    public void OnUpdateConnect()
    {
        show = "";
        if (connectByteFollowEnum)
        {
            showConnectByte = (byte)defaultShowConnect;
        }
        int[] nowData = new int[7] 
        {
            System.DateTime.Now.Millisecond ,
            System.DateTime.Now.Second ,
            System.DateTime.Now.Minute ,
            System.DateTime.Now.Hour ,
            System.DateTime.Now.Day ,
            System.DateTime.Now.Month ,
            System.DateTime.Now.Year
        };
        for (int i = nowData.Length; i >=0 ; i--)
        {
            if ((showConnectByte & (0x1 << i)) == (0x1 << i))
            {
                show +=  nowData[i].ToString();
                if (showConnectByte != ((showConnectByte >> i) << i))
                {
                    show += spacingSymbol;
                }
            }
        }
        showtext.text = show;
    }

    private void Awake()
    {
        showtext = GetComponent<Text>();
        showConnectByte = (byte)defaultShowConnect;
    }
    private void OnEnable()
    {
        OnUpdateConnect();
    }

    private void Update()
    {
        if (isUpdate)
        {
            OnUpdateConnect();
        }
    }

  


}

