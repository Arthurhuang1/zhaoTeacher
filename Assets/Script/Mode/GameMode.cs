
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode
{

    private static ProcedureStarter startupProcess = null;
    public static ProcedureStarter StartupProcess { get { return startupProcess; } set { startupProcess = value; } }
    private static ProcedureStarter closingProcess = null;
    public static ProcedureStarter ClosingProcess { get { return closingProcess; } set { closingProcess = value; } }
}
