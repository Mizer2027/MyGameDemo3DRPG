
/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:客户端服务端公用工具类
*************************************************************/ 
using PENet;

public enum LogTypes
{
    Log=0,
    Warn=1,
    Error=2,
    Info=3
}
public class PECommon
{
    public static void Log(string msg= "",LogTypes logType=LogTypes.Log)
    {
        LogLevel lv = (LogLevel)logType;
        PETool.LogMsg(msg,lv);

    }                       
}

