using System;
using System.Text;
using System.Threading;

namespace Advanced.NET.Debugging.Chapter6
{
    internal class DBWrapper1
    {
        private string connectionString;

        public DBWrapper1(string conStr)
        {
            this.connectionString = conStr;
        }
    }

    class Exc
    {
        private static DBWrapper1 db1;

        static void Main(string[] args)
        {
            db1 = new DBWrapper1("DB1");

            Thread newThread = new Thread(ThreadProc);
            newThread.Start();

            Thread.Sleep(500);
            Console.WriteLine("Acquiring lock");
            Monitor.Enter(db1);
            
            //
            // Do some work
            //

            Console.WriteLine("Releasing lock");
            Monitor.Exit(db1);
        }

        private static void ThreadProc()
        {
            try
            {
                Monitor.Enter(db1);
                Call3rdPartyCode(null);
                Monitor.Exit(db1);
            }
            catch (Exception)
            {
                Console.WriteLine("3rd party code threw an exception");
            }
        }

        private static void Call3rdPartyCode(Object obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException();
            }

            //
            // Do some work
            //
        }
    }
}
