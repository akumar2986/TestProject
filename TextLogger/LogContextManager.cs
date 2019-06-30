using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Timers;
using log4net.Appender;
using System.Configuration;
using System.IO;
using log4net.Repository.Hierarchy;
using log4net.Layout;
using System.Data;
using System.Data.SqlClient;


namespace TextLogger
{
    public class LogContextManager
    {
       
        static DataTable storeLocations = new DataTable();
   
        public static LogContextManager _default = null;
        public static LogContextManager Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new LogContextManager();
                }
                return _default;
            }

        }

       
       
        public Dictionary<long, SyncServiceLogAppender> _items;
         public Timer LogUsabilityTimer = new Timer(1000 * 10);
         public LogContextManager()
        {
            _items = new Dictionary<long, SyncServiceLogAppender>();
           
            LogUsabilityTimer.Elapsed +=LogUsabilityTimer_Elapsed;
            LogUsabilityTimer.Start();
        }

         void LogUsabilityTimer_Elapsed(object sender, ElapsedEventArgs e)
         {
             
             //var itemsToDelete=(from x in _items
             //              where x.Value.LastUsedDateTime.AddSeconds(60)<DateTime.Now
             //              select x.Key).ToList();
                          
             //foreach (var item in itemsToDelete)
             //{

             //    SyncServiceLogAppender appender = null;
             //    _items.TryGetValue(item,out appender);
             //    if (appender != null)
             //    {
             //        var appenderToRemove = appender.Writer.Logger.Repository.GetAppenders().FirstOrDefault(i => i.Name == "SyncServiceLogAppender-" + item);

             //        log4net.Repository.Hierarchy.Hierarchy repository = (log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository();
             //        log4net.Repository.Hierarchy.Logger logger = (log4net.Repository.Hierarchy.Logger)repository.GetLogger("SyncServiceLogAppender-" + item);
             //        logger.RemoveAppender(appenderToRemove);
                     
             //        _items.Remove(item);

             //        LoggerSimple.WriteMessage(item + "Removed by timer");
             //    }

             //}
         }
        public void Add(long storeLocationID, SyncServiceLogAppender dbHandler)
        {
            _items.Add(storeLocationID, dbHandler);
        }

        public void Remove(long storeLocationID)
        {
            _items.Remove(storeLocationID);
        }
        public int Count
        {
            get
            {
                return _items.Count;
            }
        }
        
        

        public SyncServiceLogAppender this[long storeLocationID]
        {
            get
            {
                
                    SyncServiceLogAppender handler=null;
                    _items.TryGetValue(storeLocationID, out handler);
                    string correctfileName = "";

                    LoggerSimple.WriteMessage("handler found for storelocationid-"+storeLocationID + "-"+(handler!=null).ToString());

                    if (handler == null)
                    {
                        
                        try
                        {
                          
                                handler = new SyncServiceLogAppender();
                            
                            _items.Add(storeLocationID, handler);
                            handler.LastUsedDateTime = DateTime.Now;



                        }
                        catch (Exception ex)
                        {
                            LoggerSimple.WriteMessage(ex.Message);
                        }
                        finally
                        {
                            //context.Dispose();
                            //context = null;
                        }
                    }
                    
                    
                        
                    return handler;
                

            }
        }

      

      
    }
    public class SyncServiceLogAppender
    {
        public SyncServiceLogAppender():this("",0)
        {            
        }
        public const string RollingFileAppenderName = "RollingFileLogger";
        public SyncServiceLogAppender(string fileLocation,long storeLocationID)
        {
            log4net.Repository.Hierarchy.Hierarchy repository = (log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository();



            if (fileLocation != "")
            {
                LoggerSimple.WriteMessage("Creating logger...." + fileLocation);

                var appenders = repository.GetAppenders();

                if (appenders.FirstOrDefault(i => i.Name == "ASyncServiceLogAppender-" + storeLocationID) != null)
                {
                    return;
                }


                SyncServiceFileAppender originalAppender = appenders.FirstOrDefault(i => i.Name == "SyncServiceLogAppender-0") as SyncServiceFileAppender;
                    //(appenders[0] as SyncServiceFileAppender);
                

                SyncServiceFileAppender fileAppender = new SyncServiceFileAppender();

                fileAppender.Name = "ASyncServiceLogAppender-" + storeLocationID;
                fileAppender.File = fileLocation;
                fileAppender.AppendToFile = true;
                fileAppender.MaxSizeRollBackups = originalAppender.MaxSizeRollBackups;
                fileAppender.MaximumFileSize = originalAppender.MaximumFileSize;
                fileAppender.RollingStyle = originalAppender.RollingStyle;
                fileAppender.MaximumFileSize = originalAppender.MaximumFileSize;
                fileAppender.CountDirection = originalAppender.CountDirection;
                fileAppender.DatePattern = originalAppender.DatePattern;
                fileAppender.ImmediateFlush = originalAppender.ImmediateFlush;
                fileAppender.MaxFileSize = originalAppender.MaxFileSize;


                fileAppender.StaticLogFileName = false;

                fileAppender.Threshold = log4net.Core.Level.All;

                fileAppender.Layout = originalAppender.Layout;

                fileAppender.LockingModel = new FileAppender.MinimalLock();

                fileAppender.ActivateOptions();

                fileAppender.FilePath = fileLocation;

                log4net.Repository.Hierarchy.Logger logger = (log4net.Repository.Hierarchy.Logger)repository.GetLogger("ASyncServiceLogAppender-" + storeLocationID);
                logger.AddAppender(fileAppender);

                LoggerSimple.WriteMessage("Logger Created..." + storeLocationID);

                Writer = LogManager.GetLogger("ASyncServiceLogAppender-" + storeLocationID);
            }
            else
            {
                Writer = LogManager.GetLogger("SyncServiceLogAppender-" + storeLocationID);
            }

            
                
        }
        private ILog _writer;
        public ILog Writer 
        {
            get
            {
                return _writer;
            }
            set
            {
                _writer = value;
            }
        }

        private DateTime _lastUsedDateTime;
        public DateTime LastUsedDateTime
        {
            get
            {
                return _lastUsedDateTime;
            }
            set
            {
                _lastUsedDateTime = value;
            }
        }


        
        public string FilePath
        {
            get;
            set;
        }


    }

    public class SyncServiceFileAppender : log4net.Appender.RollingFileAppender
    {
        protected override void OpenFile(string fileName, bool append)
        {
            //Inject folder [yyyyMMdd] before the file name
            if (FilePath == null)
            {
                FilePath = fileName;
            }
            base.OpenFile(FilePath, append);
        }


        public string FilePath { 
            get; set; }

        public SyncServiceFileAppender Clone()
        {
            return (SyncServiceFileAppender)this.MemberwiseClone();
        }
    }
}
