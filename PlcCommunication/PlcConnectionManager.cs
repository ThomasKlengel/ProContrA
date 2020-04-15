using DeviceCommunication;
using DeviceCommunication.Siemens;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;


namespace PlcCommunication
{
    public static class PlcConnectionManager
    {
        public static List<BasePollPlcConnection> Connections { get; private set; } = new List<BasePollPlcConnection>();

        public static Logger Logger { get; private set; } = NLog.LogManager.GetCurrentClassLogger();

        private static bool initialized = false;

        /// <summary>
        /// Initializes the Logger for this class
        /// </summary>
        public static void Initialize()
        {
            if (initialized) return;
            
            var config = new NLog.Config.LoggingConfiguration();
            // file & console (where to log to)
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = @"logs\PlcConnection.log" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");
            // rules for mapping loggers to targets
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
            // apply config
            NLog.LogManager.Configuration = config;
            initialized = true;
                
        }

        public static void AddPlcConnection( string id, string ip)
        {
            Initialize();

            var messageList = new List<PlcMessage>();
            var plcConnection = new S7PollPlcConnection(id, ip, 0, 0, messageList, Logger, false);

            Connections.Add(plcConnection);
        }

        static async void WriteMessages(S7PollPlcConnection con, List<S7PlcItem> itemList)
        {
            try
            {
                await con.WriteItems(itemList);
                Debug.WriteLine("Finished writing items.");
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception Message: " + e.Message);
            }
        }

        /// <summary>
        /// CallBack for automatich update of plcitems in poll connection
        /// </summary>
        /// <param name="result">The items that have been changed</param>
        /// <param name="ex">Any exception that might have occured</param>
        private static void itemsUpdated(IEnumerable<PlcItem> result, Exception ex)
        {
            foreach (var item in result)
            {
                if (item is S7PlcItem s7item)
                {
                    if (!PlcWatcher.S7PlcItemsMapping.ContainsKey(s7item))
                    {
                        continue;
                    }

                    //Modify items
                    PlcWatcher.S7PlcItemsMapping[s7item].S7Item = s7item;
                    PlcWatcher.S7PlcItemsMapping[s7item].ExternalValue = s7item.LastPLCValue;
                }
            }
        }

        public static  void Connect (string plcID)
        {
            var plcConnection = Connections.Where(con => con.Id == plcID).FirstOrDefault();

            if (plcConnection is S7PollPlcConnection s7Con)
            {                
                //plcConnection.IsConnectedEvents.Subscribe(isConnected =>
                //{
                //    if (isConnected)
                //    {
                //    }
                //    else
                //    {
                //        Task.Run(() =>
                //        {
                //            while (!s7Con.IsConnected)
                //            {
                //                try
                //                {
                //                    s7Con.Connect();
                //                }
                //                catch
                //                {
                //                }
                //                Task.Delay(500);
                //            }
                //        });
                //    }
                //});

                Task.WaitAll(
                    Task.Run(() =>
                    {
                        while (!plcConnection.IsConnected)
                        {
                            Console.WriteLine("try to connect ...");
                            Thread.Sleep(1000);
                        }
                    }));

            }
        }
    }

    public static class PlcWatcher
    {
        public static Dictionary<S7PlcItem, MappedItem> S7PlcItemsMapping { get; set; } = new Dictionary<S7PlcItem, MappedItem>();
    }

    public class MappedItem
    {
        public delegate void OnItemValueChanged(MappedItem mappedItem);
        public event OnItemValueChanged ItemValueChanged;

        public string Name { get; set; }

        public S7PlcItem S7Item { get; set; }

        private object _externalValue;
        public object ExternalValue
        {
            get
            { return _externalValue; }
            set
            {
                if (_externalValue == value)
                {
                    return;
                }

                _externalValue = value;
                ItemValueChanged(this);
            }
        }

        public int IntValue
        {
            get
            {
                int result = 0;
                if (ExternalValue is int intVal)
                {
                    result = intVal;
                }

                return result;
            }
        }

        public Double DoubleValue
        {
            get
            {
                double result = 0;
                if (ExternalValue is double dVal)
                {
                    result = dVal;
                }

                return result;
            }
        }

        public Boolean BoolValue
        {
            get
            {
                bool result = false;
                if (ExternalValue is bool bVal)
                {
                    result = bVal;
                }

                return result;
            }
        }
    }
}
