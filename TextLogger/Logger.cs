using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextLogger
{
    public static class Logger
    {

        private static log4net.ILog Log { get; set; }

        static Logger()
        {
            Log = log4net.LogManager.GetLogger(typeof(Logger));
        }

        public static void Error(object msg)
        {
            Log.Error(msg);
        }


        public static string GetCurrentMethod()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            return stackFrame.GetMethod().ToString();
        }



        public static void Error(Exception ex, object msg)
        {
            Log.Error(msg, ex);
        }

        public static void WriteFatal(Exception ex, object msg)
        {
            Log.Error(msg, ex);
            CreateLog(ex, msg, "none");

        }

        public static void WriteLog(Exception ex, object msg, string BusinessMethodName)
        {
            Log.Error(msg, ex);
            CreateLog(ex, msg, BusinessMethodName);

        }

        public static void CreateLog(Exception ex, object msg, string BusinessMethodName)
        {
            //var filepath = "C:/Apps/testadmin.affitem.com/logFile.log";
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"error.log");

            if (!File.Exists(filepath))
                File.Create(filepath).Close();

            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                string errormsg = "no error";
                string stacktrace = "no error";
               
                if (ex != null)
                {
                    stacktrace = ex.StackTrace;
                    errormsg = ex.Message;

                    writer.WriteLine("BusinessMethodName : " + BusinessMethodName + Environment.NewLine + Environment.NewLine +
                        "Message :" + errormsg + "<br/>" + Environment.NewLine + Environment.NewLine + "StackTrace : " + stacktrace +
                       "" + Environment.NewLine + Environment.NewLine + "Date : " + DateTime.Now.ToString());

                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);

                }
            }
        }
        public static void LogMessage1(Exception ex, ImageDetailLog msg, string filepath)
        {


            if (!File.Exists(filepath))
                File.Create(filepath).Close();

            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                string errormsg = "No Error";
                string stacktrace = "no error";
                long imageMapingId = msg.ImageMappingId;
                string imageName = msg.ImageName;
                int imageReturnId = msg.ReturnImageId;
                int imageType = msg.TypeId;


                if (ex != null)
                {
                    stacktrace = ex.StackTrace;
                    errormsg = ex.Message;

                }

                writer.WriteLine("Message :" + errormsg + "<br/>" + Environment.NewLine + "StackTrace :" + stacktrace +
                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString()
                   + Environment.NewLine + "ImageMappingId :" + imageMapingId
                   + Environment.NewLine + "ImageName :" + imageName
                   + Environment.NewLine + "ReturnImageId :" + imageReturnId
                   + Environment.NewLine + "ImageTypeId :" + imageType



                   );
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
        }



        public static void Error(Exception ex)
        {
            Log.Error(ex.Message, ex);
        }

        public static void Info(object msg)
        {
            Log.Info(msg);
        }
    }
}
