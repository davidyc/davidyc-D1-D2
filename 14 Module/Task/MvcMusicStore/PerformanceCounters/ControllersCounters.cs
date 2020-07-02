using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PerformanceCounterHelper;

namespace MvcMusicStore.PerformanceCounters
{
    //попмоему натворил дичь, но полет фантазии было не остановить
    public class ControllerCounter
    {
        private List<CounterCreationData> counterCreationDatas;
        private static List<PerformanceCounter> counters;

        public ControllerCounter()
        {
            counterCreationDatas = new List<CounterCreationData>();
            counters = new List<PerformanceCounter>();
        }
        public void CreateCategory(string categoryName, string categoryHelp)
        {
            if (!PerformanceCounterCategory.Exists(categoryName))
            {
                PerformanceCounterCategory customCategory = new PerformanceCounterCategory(categoryName);              
                CounterCreationDataCollection collection = new CounterCreationDataCollection();
                foreach (var counterCreationData in counterCreationDatas)
                {
                    collection.Add(counterCreationData);
                }
                PerformanceCounterCategory.Create(categoryName, categoryHelp, collection);
            }
        }
        
        public static void InitCounter()
        {
            var counter = new ControllerCounter();
            counter.AddCounter("LoginCounter", "Perfomance counter login", PerformanceCounterType.NumberOfItems32);
            counter.AddCounter("LogOffCounter", "Perfomance counter logOff", PerformanceCounterType.NumberOfItems32);
            counter.AddCounter("RegPageVisitCounter", "Perfomance counter RegPageVisitCounter", PerformanceCounterType.NumberOfItems32);
            counter.CreateCategory("MVC_STORE_MUSIC", "MVCMISICSTORE related real time statistics");
        }

        public static void Increment(string categoryCounterName, string counterName)
        {
            var counter = counters.FirstOrDefault(x => x.CounterName == counterName);
            if (counter == null) {
                counter = new PerformanceCounter(categoryCounterName, counterName);
                counter.ReadOnly = false;
            }           
            counter.Increment();
        }

        private void AddCounter(string counterName, string counterHelp, PerformanceCounterType counterTypem)
        {
            counterCreationDatas.Add(new CounterCreationData(counterName, counterHelp, counterTypem));
        }
    }
}