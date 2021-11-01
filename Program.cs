using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace Lab3Q1
{
    class Program
    {
        static void Main(string[] args)
        {
          // map and mutex for thread safety
          Mutex mutex = new Mutex();
          Dictionary<string, int> wcountsSingleThread = new Dictionary<string, int>();//'dictionary for mapping character->word count
          Dictionary<string, int> wcountsMultiThread = new Dictionary<string, int>();//'dictionary for multithreading

            var SingleThreadSortedByValueList = new List<Tuple<int, string>>();//'list of tuples 
            var MultithreadSortedByValueList = new List<Tuple<int, string>>();

            Stopwatch stopwatch_multi = new Stopwatch();
            Stopwatch stopwatch_single = new Stopwatch();


            //' file names of the plays stored in a list of strings
            var filenames = new List<string> {
                "../../data/shakespeare_antony_cleopatra.txt",
                "../../data/shakespeare_hamlet.txt",
                "../../data/shakespeare_julius_caesar.txt",
                "../../data/shakespeare_king_lear.txt",
                "../../data/shakespeare_macbeth.txt",
                "../../data/shakespeare_merchant_of_venice.txt",
                "../../data/shakespeare_midsummer_nights_dream.txt",
                "../../data/shakespeare_much_ado.txt",
                "../../data/shakespeare_othello.txt",
                "../../data/shakespeare_romeo_and_juliet.txt",
           };



            //=============================================================
            // YOUR IMPLEMENTATION HERE TO COUNT WORDS IN SINGLE THREAD
            //=============================================================
            stopwatch_single.Start();

            foreach (string filename in filenames)
            {
                HelperFunctions.CountCharacterWords(filename, mutex, wcountsSingleThread);//'call CountCharacterWords with each file to store words for each character in the dictionary
            }


            SingleThreadSortedByValueList = HelperFunctions.SortCharactersByWordcount(wcountsSingleThread);

            HelperFunctions.PrintListofTuples(SingleThreadSortedByValueList);


            Console.WriteLine( "SingleThread is Done!");

            stopwatch_single.Stop();
            TimeSpan single_ts = stopwatch_single.Elapsed;
            long single_ticks = 0;
            single_ticks = stopwatch_single.ElapsedTicks;

            string singleelapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            single_ts.Hours, single_ts.Minutes, single_ts.Seconds,
            single_ts.Milliseconds / 10);

            Console.WriteLine("Singlethreaded wordcounting RunTime = " + singleelapsedTime);





            //=============================================================
            // YOUR IMPLEMENTATION HERE TO COUNT WORDS IN MULTIPLE THREADS
            //=============================================================

            //'create an array of threads of size filenames or of size number of cores?
            //'call CountCharacterWords using the threads
            //' join the threads and then you call SortCharactersByWordCount and then PrintListofTuples

            stopwatch_multi.Start();

            var threadlist = new List<Thread>();//'declare list of threads


            foreach (string filename in filenames)
            {
                Thread th = new Thread(() => HelperFunctions.CountCharacterWords(filename, mutex, wcountsMultiThread));//'how does this lambda operator work again?
                th.Start();
                threadlist.Add(th);//'add thread to the list of threads
            }


            foreach (Thread thread in threadlist)
            {
                thread.Join();//'use the thread thing from the foreach() for the joining here, thread works similar to index variable i in for loop but slightly different
            }


            MultithreadSortedByValueList = HelperFunctions.SortCharactersByWordcount(wcountsMultiThread);

            HelperFunctions.PrintListofTuples(MultithreadSortedByValueList);


            Console.WriteLine( "MultiThread is Done!");

            stopwatch_multi.Stop();
            TimeSpan multi_ts = stopwatch_multi.Elapsed;
            long multi_ticks = 0;
            multi_ticks = stopwatch_multi.ElapsedTicks;


            string multielapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            multi_ts.Hours, multi_ts.Minutes, multi_ts.Seconds,
            multi_ts.Milliseconds / 10);

            Console.WriteLine("Multithreaded wordcounting RunTime = " + multielapsedTime);


            // return 0;
        }
    }
}
