using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static List<Task> lst = new List<Task>();
        static List<int> lstInt = new List<int>();
        static AggregateException ae = new AggregateException();
        static void Main(string[] args)
        {


            Random r = new Random();
            for (int i = 0; i < 15; i++) lstInt.Add(i);
            
            var exceptions = new ConcurrentQueue<Exception>();

            try
            {
                ProcessMessages();
            }

            //catch (Exception ex)
            //{

            //    Console.WriteLine(ex);
            //    Console.ReadKey();
            //}



            //if (exceptions.Count > 0) ae = new AggregateException(exceptions);

            //for (int i = 0; i < 10; i++)
            //{
            //    try
            //    {
            //        int state;
            //        //DoSomethingAsync(i).ConfigureAwait(false); ;

            //        //DoSomethingAsync().ContinueWith((t) => { if (t.Exception != null) Console.WriteLine(t.Exception); });
            //        lst.Add(Task.Factory.StartNew(() => { DoSomethingAsync(5); }, TaskCreationOptions.LongRunning));
            //    }
            //    catch (Exception e)
            //    {

            //       Console.WriteLine(e);
            //    }
            //    //lst.Add(Task.Factory.StartNew(() => { DoSomethingAsync(); }).ContinueWith((t) => { if (t.Exception != null) Console.WriteLine(t.Exception); }));
            //}
            //try
            //{
            //    Task.WaitAll(lst.ToArray());
            //}
            catch (AggregateException aex)
            {
                int i = 0;
                foreach (var item in aex.InnerExceptions)
                {
                    Console.WriteLine("Excetion No :  {0} -->{1}", i++, item.Message);
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            Console.ReadKey();
        }

        private static void NewMethod()
        {
            ProcessMessages();
        }

        private static  void ProcessMessages()
        {
            List<Task> lstTask = new List<Task>();
            //Task.Factory.StartNew(t => { }, TaskCreationOptions.AttachedToParent);
            ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();
            Parallel.ForEach(lstInt, new ParallelOptions() { MaxDegreeOfParallelism = 10 }, (t) =>
                    {

                        //lstTask.Add(DoSomethingAsync(5));
                        //DoSomethingAsync(5).ConfigureAwait(false) ;
                        lstTask.Add(DoSomethingAsync(5).ContinueWith((x) =>
                        {
                            if (x.Exception != null) exceptions.Enqueue(x.Exception);
                        }));
                        lstTask.RemoveAll(y => y.IsCompleted);
                        //if (b.IsExceptional) { b.ToString(); }
                    });
            if (exceptions.Count > 0) { throw new AggregateException(exceptions); }
            //ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();
            //int maxDegreeOfParallelism = 5;
            //List<Task> lstTask = new List<Task>();
            //foreach (var item in lstInt)
            //{
            //    Task t = DoSomethingAsync(item).ContinueWith(x => { exceptions.Enqueue(x.Exception); });
            //    //lstTask.Add(DoSomethingAsync(item));
            //    //t.Start();
            //    if (lstTask.Count == maxDegreeOfParallelism)
            //    {
            //        await Task.WhenAny(lstTask.ToArray());

            //        lstTask.RemoveAll(y => y.IsCompleted);
            //    }
            //}
            //await Task.WhenAll(lstTask.ToArray()).ContinueWith(t =>
            //{
            //    exceptions.Enqueue(t.Exception);
            //});
            //if (exceptions.Count > 0) { ae = new AggregateException(exceptions); }

            //Parallel.ForEach(lstInt, new ParallelOptions() { MaxDegreeOfParallelism = 10 }, (t) =>
            //        {

            //            lstTask.Add(DoSomethingAsync(5));
            //            //DoSomethingAsync(5).ConfigureAwait(false) ;
            //            //DoSomethingAsync(5).ContinueWith((x) =>
            //            //{
            //            //    if (x.Exception != null) exceptions.Enqueue(x.Exception);
            //            //});

            //            //if (b.IsExceptional) { b.ToString(); }
            //        });
            //Task.WaitAll(lstTask.ToArray());
            //for (int y = 0; y < 10; y++)
            //{
            //    Console.WriteLine("sometask-->{0}", y);
            //}


        }


        //private static void ProcessMessages()
        //{
        //    Parallel.ForA(lstInt, new ParallelOptions() { MaxDegreeOfParallelism = 10 }, (t, b) =>
        //    {
        //        DoSomethingAsync(5).ConfigureAwait(false);
        //        //DoSomethingAsync(5).ContinueWith((x) =>
        //        //{
        //        //    if (x.Exception != null) exceptions.Enqueue(x.Exception);
        //        //});

        //        //if (b.IsExceptional) { b.ToString(); }
        //    });

        //    for (int y = 0; y < 10; y++)
        //    {
        //        Console.WriteLine("sometask-->{0}", y);
        //    }
        //}

        public static async Task DoSomethingAsync(int x)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int y = 0; y < 100000000; y++)
                {

                }
                Console.WriteLine(Task.CurrentId + "-->" + i);

                if (i == 5) throw new Exception("Test");
            }

            await Task.Delay(100);
        }
    }
}
