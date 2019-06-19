using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//程序启动器
public class ProcedureStarter
{
    private bool isInExecution = false;
    public bool IsInExecution { get { return isInExecution; } }
    private Procedure mainProcedure = null;
    private Procedure endProcedure = null;

    private void SetMainProcedure(Procedure mp)
    {
        mainProcedure = mp;
        if (mainProcedure!=null)
        {
            mainProcedure.EmptyPrevious();
        }
    }
    private void SetEndProcedure(Procedure mp)
    {
        endProcedure = mp;
    }
    public void OnProcedureFinality(Procedure p)
    {
        isInExecution = false;
       // Debug.Log("End isInExecution = "+ isInExecution);
    }
    private void OnReverseProcedureFinality(Procedure p)
    {
        isInExecution = false;
       // Debug.Log("End isInExecution = " + isInExecution);

    }
    private void DrawProcedureStarter()
    {
        //
    }
    public  void AddProcedure(Procedure newpro,Procedure previous )
    {
        newpro.onProcedureFinality = OnProcedureFinality;
        newpro.onReverseProcedureFinality = OnReverseProcedureFinality;
        if (previous == null)
        {
            //Debug.Log("id:"+ newpro .runid + "ADD Procedure :" + newpro.procedureName + "  previous:null");
        }
        else
        {
           // Debug.Log("id:" + newpro.runid + "ADD Procedure :" + newpro.procedureName + "  previous:" + previous.procedureName);
        }
       
        if (mainProcedure == null)//无头
        {
            SetMainProcedure( newpro);
            SetEndProcedure( newpro);
            return ;
        }

        //有头&&有前置
        if (previous!=null)
        {
            if (previous == endProcedure)
            {

                newpro.LinkNext(null);
                previous.LinkNext(newpro);
               
                SetEndProcedure(newpro);
            }
            else
            {
                newpro.LinkNext(previous.GetNext());
                previous.LinkNext(newpro);
               
            }
            //newpro.LinkNext( previous.GetNext());
           
        }
        else //有头&&无前置
        {
            newpro.LinkNext(mainProcedure);

            SetMainProcedure(newpro);
        }
        

    }
    public  void RemoveProcedure(Procedure reProcedure)
    {
        Procedure previous = reProcedure.GetPrevious();
        Procedure next = reProcedure.GetNext();
        if (mainProcedure != reProcedure && endProcedure != reProcedure) //非头非尾
        {
            previous.LinkNext(next);
            reProcedure = null;
            return ;
        }
        if (mainProcedure == reProcedure && endProcedure == reProcedure)//唯一一个
        {
            SetMainProcedure(null);
            SetEndProcedure(null);
            reProcedure = null;
            return;
        }
        if (mainProcedure == reProcedure && endProcedure != reProcedure)//头 & !尾
        {
            SetMainProcedure(next);
            next.EmptyPrevious();
            reProcedure = null;
            return;
        }
        if (mainProcedure != reProcedure && endProcedure == reProcedure)//!头 & 尾
        {
            SetEndProcedure(previous);
            previous.EmptyNext();
            reProcedure = null;
            return;
        }

          
    }
    public  void ExeProcedure()
    {
        mainProcedure.Execute();
        isInExecution = true;
        //Debug.Log("Exe isInExecution = " + isInExecution);

    }
    public void ReverseExeProcedure()
    {
        endProcedure.ReverseExecute();
        isInExecution = true;
        //Debug.Log("Reverse Exe isInExecution = " + isInExecution);
    }



    public static ProcedureStarter GenerateProcedureStarterByStarterList(Dictionary<int, But_StarterListProcedureItem> StarterList)
    {
        if (StarterList.Count <= 0)
        {
            Debug.Log("StarterList.Count <= 0");
            return null;
        }
        ProcedureStarter ps = new ProcedureStarter();
     
        Procedure previous = null;
        for (int i = 1; i < StarterList.Count+1; i++)
        {
            previous = AddProcedureByProcedureItemButton(ps, previous, StarterList[i]);
        }

        return ps;
    }
    public static Procedure AddProcedureByProcedureItemButton(ProcedureStarter ps, Procedure previous, But_StarterListProcedureItem but)
    {
        Procedure pro = null;
        But_StarterListProcedureItem curProcedureItemBut = but;
        switch (curProcedureItemBut.eProcedureType)
        {
            case ProcedureType.Waiting:
                pro = new WaitingProcedure(curProcedureItemBut.GetWaitingSecond());
                pro.runid = but.childID;
                pro.AddToProcedureStarter(ps, previous);
              
                break;
            case ProcedureType.Switchgear:
                pro = new SwitchableDeviceProcedure(curProcedureItemBut.GetProcedureName(), curProcedureItemBut.GetProcedureName_ch(), curProcedureItemBut.GetSwitchgearId(), curProcedureItemBut.GetDeviceSliderValue());
                pro.runid = curProcedureItemBut.childID;
                pro.AddToProcedureStarter(ps, previous);


                break;
            default:
                break;
        }

        return pro;
    }


    

}
