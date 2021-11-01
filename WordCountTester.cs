using System;
using System.Collections.Generic;
namespace Lab3Q1
{
    public class WordCountTester
    {



        static int Main()
        {


          try {


                //=================================================
                // Implement your tests here. Check all the edge case scenarios.
                // Create a large list which iterates over WCTester
                // YOUR TESTS HERE. Create a large list which includes the line, the // starting index and the expected result. You would want to check //// all the edge case scenarios.
                //=================================================

                //'start with simple test

                string line = "he saw it";
                int startIdx = 0;
                int expectedResults = 3;
                WCTester(line, startIdx, expectedResults); //'run the tester function


                //'lists for testing different startIdx, (keep string same, and have a list of different start indexes, and a corresponding list of expected results)
                List<int> startIdxlist = new List<int>();
                startIdxlist.Add(0); startIdxlist.Add(1); startIdxlist.Add(2); startIdxlist.Add(3); startIdxlist.Add(4); startIdxlist.Add(5); startIdxlist.Add(6); startIdxlist.Add(7); startIdxlist.Add(8); //startIdxlist.Add(9);//'do i test an out of bounds? aka start at index 9

                List<int> expectedList = new List<int>();
                expectedList.Add(3); expectedList.Add(3); expectedList.Add(3); expectedList.Add(2); expectedList.Add(2); expectedList.Add(2); expectedList.Add(2); expectedList.Add(1); expectedList.Add(1); //expectedList.Add(1);


                //'now run the tester function in a for loop that iterates through the two corresponding lists
                for (int i = 0;i<startIdxlist.Count;i++)
                WCTester(line, startIdxlist[i], expectedList[i]);


                //'test different number of words
                line = "he saw it run along the path";
                startIdx = 0;
                expectedResults = 7;
                WCTester(line, startIdx, expectedResults);

                //'test with extra spaces at start
                line = " he saw it";
                startIdx = 0;
                expectedResults = 3;
                WCTester(line, startIdx, expectedResults);

                //'test with extra spaces between
                line = "he  saw it";
                startIdx = 0;
                expectedResults = 3;
                WCTester(line, startIdx, expectedResults);

                //'test with extra spaces at end
                line = "he saw it ";
                startIdx = 0;
                expectedResults = 3;
                WCTester(line, startIdx, expectedResults);




            } 

            catch(UnitTestException e)
            {
              Console.WriteLine(e);
            }


            return 0;
        }





        /**
         * Tests word_count for the given line and starting index
         * @param line line in which to search for words
         * @param start_idx starting index in line to search for words
         * @param expected expected answer
         * @throws UnitTestException if the test fails
         */

        //'TESTER FUNCTION, takes the same inputs as the WordCount, along with an expected answer
        //'calls Wordcount and compares the result from that to the expected result

          static void WCTester(string line, int start_idx, int expected) {

            //=================================================
            // Implement: comparison between the expected and
            // the actual word counter results
            //=================================================
            // Call your WordCount(ref line, start_idx) method


            int result = HelperFunctions.WordCount(ref line, start_idx);//'calling the Wordcount function, note line must be passed with ref keyword

            if (result != expected) 
            {
              throw new Lab3Q1.UnitTestException(ref line, start_idx, result, expected, String.Format("UnitTestFailed: result:{0} expected:{1}, line: {2} starting from index {3}", result, expected, line, start_idx));
            }
            //'do i need to use i instead of start_idx here? maybe not

           }




    }
}
