using System;
using System.Collections.Generic;
using UnityEngine;

public class Procedure
{
    public int runid= 0;
    public ProcedureType type;
    public  string procedureName;
    private Procedure previous = null;
    private Procedure next = null;
    internal Action<Procedure> onProcedureFinality;
    internal Action<Procedure> onReverseProcedureFinality;

    public void LinkNext(Procedure np)
    {
        next = np;
        if (np!=null)
        {
            np.previous = this;
        }
        
    }
    public void EmptyPrevious()
    {
        previous = null;
    }
    public void EmptyNext()
    {
        next = null;
    }
    public Procedure GetNext()
    {
        return next ;
    }
    public Procedure GetPrevious()
    {
        return previous;
    }

    public void DrawProcedure()
    {
        Debug.Log(new object());
    }


    protected virtual void OnTrigger(string eve)
    {

    }

    public void Trigger(string eve)
    {
        OnTrigger(eve);
    }
    public void Execute()
    {
        OnExecute();
    }
    public void ExecuteComplete()
    {
       // Debug.Log("ExecuteComplete："+ procedureName);
        OnExecuteComplete();
        if (next != null)
        {
            next.Execute();
        }
        else
        {
            //Debug.Log("程序执行完毕");
            if (onProcedureFinality!=null)
            {
                onProcedureFinality(this);
            }
        }
    }

    protected virtual void OnExecute()
    {

    }
    protected virtual void OnExecuteComplete()
    {
       
    }


    public void ReverseExecute()
    {
        OnReverseExecute();
    }
    public void ReverseExecuteComplete()
    {
       // Debug.Log("OnReverseExecuteComplete：" + procedureName);
        OnReverseExecuteComplete();
        if (previous != null)
        {
            previous.ReverseExecute();
        }
        else
        {
            //Debug.Log("程序执行完毕");
            if (onReverseProcedureFinality!=null)
            {
                onReverseProcedureFinality(this);
            }
        }
    }
    protected virtual void OnReverseExecute()
    {

    }
    protected virtual void OnReverseExecuteComplete()
    {

    }
    public void AddToProcedureStarter(ProcedureStarter ps, Procedure previous)
    {
        ps.AddProcedure(this, previous);
    }
}

public class WaitingProcedure: Procedure
{
    public WaitingProcedure(float f)
    {
        procedureName = "Waiting";
        type = ProcedureType.Waiting;
        waitingSeconds = f;
    }
    float waitingSeconds = 0;
    protected override void OnTrigger(string eve)
    {
        base.OnTrigger(eve);
    }
    protected override void OnExecute()
    {
        base.OnExecute();

        GlobalBehaviour.DelayedExecution(waitingSeconds, ExecuteComplete);//延时完成
    }
   
    protected override void OnExecuteComplete()
    {
        base.OnExecuteComplete();
    }

    protected override void OnReverseExecute()
    {
        base.OnReverseExecute();

        GlobalBehaviour.DelayedExecution(waitingSeconds, ReverseExecuteComplete);//延时完成
    }

   
}

public class SwitchableDeviceProcedure : Procedure
{

    public SwitchableDeviceProcedure(string name,string namech,int id,bool isopen)
    {
        type = ProcedureType.Switchgear;
        procedureName = name;
        deviceTypeName = name;
        //deviceTypeName_ch = namech;
        deviceID = id;
        openAtExecution = isopen;
    }
    // ISwitchableDevice device;
    private bool openAtExecution = false;
    private string deviceTypeName;
    //private string deviceTypeName_ch;
    private int deviceID = 1;
    protected override void OnTrigger(string eve)
    {
        base.OnTrigger(eve);
    }
    protected override void OnExecute()
    {
        base.OnExecute();
        //device.SwitchableDevice(openAtExecution);
        DeviceSwitchgearSlider.SetSliderValueByDeviceSwitchgear(deviceTypeName, deviceID, openAtExecution);
        CoalYardDevice.OnDeviceSwitchgear(deviceTypeName, deviceID, openAtExecution);

        GlobalBehaviour.DelayedExecution(0.5f, ExecuteComplete);//延时完成
    }
    protected override void OnExecuteComplete()
    {
        base.OnExecuteComplete();
    }

    protected override void OnReverseExecute()
    {
        base.OnReverseExecute();
        //device.SwitchableDevice(!openAtExecution);
        DeviceSwitchgearSlider.SetSliderValueByDeviceSwitchgear(deviceTypeName, deviceID, !openAtExecution);
        CoalYardDevice.OnDeviceSwitchgear(deviceTypeName, deviceID, !openAtExecution);
        GlobalBehaviour.DelayedExecution(0.5f, ReverseExecuteComplete);//延时完成
    }

   
}
