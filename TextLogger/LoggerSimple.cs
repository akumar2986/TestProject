using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace TextLogger
{
    public class LoggerSimple
    {
        public static void WriteFatal(Exception ex)
        {
            WriteFatal(ex, ""); 
        }

        public static void WriteFatal(Exception ex, string point)
        {
            System.IO.StreamWriter fs2 = null;
            fs2 = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LoggerTrack.log", true);


            string writeText = point + "-" + DateTime.Now.ToString() + "-" + ex.Message + char.ConvertFromUtf32(13); ;
            fs2.WriteLine(writeText);
            //byte[] data = System.Text.Encoding.Unicode.GetBytes(writeText);
            //fs2.Write(data, 0, data.Length);
            fs2.Flush();
            fs2.Close();
        }
        public static void WriteMessage(string message)
        {
            return;
            //System.IO.FileStream fs2 = null;
            System.IO.StreamWriter fs2 = null;

            fs2 = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LoggerTrack.log", true);

            string writeText = DateTime.Now.ToString() + "-" + message + char.ConvertFromUtf32(13);
            fs2.WriteLine(writeText);
            fs2.Flush();
            fs2.Close();
        }
        public static void WriteLastAckFileName(string fileName,string processID)
        {
            //System.IO.FileStream fs2 = null;
            System.IO.StreamWriter fs2 = null;
            
            fs2 = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\PassportAck.log", true);
            string writeText = processID + " - " + fileName;
            fs2.WriteLine(writeText);
            fs2.Flush();
            fs2.Close();
        }

        public static MethodBase GetCurrentMethod()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            return stackFrame.GetMethod();
        }

      

        public static void WriteDownloadMessage(string message,string boLocationId,string fileType,string posType)
        {
            //System.IO.FileStream fs2 = null;
            System.IO.StreamWriter fs2 = null;

            fs2 = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\DownloadTotals.log", true);

            string writeText = DateTime.Now.ToString() + "-" +posType +"-" + boLocationId+" -"+fileType+ " - "+  message + char.ConvertFromUtf32(13);
            fs2.WriteLine(writeText);
            fs2.Flush();
            fs2.Close();
        }

        public static void WriteNotificationMessage(string message, string statusCode, string stackTrace)
        {
            System.IO.StreamWriter fs2 = null;

            fs2 = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\DownloadTotals.log", true);

            string writeText = DateTime.Now.ToString() + "-" + statusCode + "-" + message + " -" + stackTrace + char.ConvertFromUtf32(13);
            fs2.WriteLine(writeText);
            fs2.Flush();
            fs2.Close();
        
        }
    }
}
