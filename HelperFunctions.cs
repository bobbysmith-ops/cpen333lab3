using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;


namespace Lab3Q1
{
    public class HelperFunctions
    {





        /**
         * Counts number of words, separated by spaces, in a line.
         * @param line string in which to count words
         * @param start_idx starting index to search for words
         * @return number of words in the line
         */

        public static int WordCount(ref string line, int start_idx)
        {

            int count = 1;
            int i = start_idx;

            while (i <= line.Length - 1)
            {
                if (line[i] == ' ' || line[i] == '\n' || line[i] == '\t')
                {
                    count++;
                }

                i++;
            }

            return count;

        }






        /**
        * Reads a file to count the number of words each actor speaks.
        *
        * @param filename file to open
        * @param mutex mutex for protected access to the shared wcounts map
        * @param wcounts a shared map from character -> word count
        */
        public static void CountCharacterWords(string filename, Mutex mutex, Dictionary<string, int> wcounts)//' inserts word counts into the Character → Word Count map
        {

            //===============================================
            //  IMPLEMENT THIS METHOD INCLUDING THREAD SAFETY
            //===============================================


             int index;//for storing index
             int count;//for storing wordcount
             string line;  // for storing each line read from the file
             string character = "";  // empty character to start
             System.IO.StreamReader file = new System.IO.StreamReader(filename);

             while ((line = file.ReadLine()) != null)
             {


                if ( IsDialogueLine(line,ref character) != -1)
                {
                    index = IsDialogueLine(line, ref character);//' function is able to modify character since its passed by reference so character is extracted in this line

                    if (index > 0 && character != "")
                    {
                        count = WordCount(ref line, index);//'get the wordcount

                        if (wcounts.ContainsKey(character))//'if the key exists
                            wcounts[character] = count;//' update the word count for that character in the dictionary
                        else 
                            wcounts.Add(character, count);//' add a new key-value pair to the dictionary

                    }

                }




                //'dont reset the character
               //=================================================
               // YOUR JOB TO ADD WORD COUNT INFORMATION TO MAP
               //=================================================

                 // Is the line a dialogueLine?
                 //    If yes, get the index and the character name.
                 //      if index > 0 and character not empty
                 //        get the word counts
                 //          if the key exists, update the word counts
                 //          else add a new key-value to the dictionary
                 //    reset the character

              }

            file.Close();//'close the file

            // Close the file
        }







        /**
         * Checks if the line specifies a character's dialogue, returning
         * the index of the start of the dialogue.  If the
         * line specifies a new character is speaking, then extracts the
         * character's name.
         *
         * Assumptions: (doesn't have to be perfect)
         *     Line that starts with exactly two spaces has
         *       CHARACTER. <dialogue>
         *     Line that starts with exactly four spaces
         *       continues the dialogue of previous character
         *
         * @param line line to check
         * @param character extracted character name if new character,
         *        otherwise leaves character unmodified
         * @return index of start of dialogue if a dialogue line,
         *      -1 if not a dialogue line
         */
        static int IsDialogueLine(string line, ref string character)
        {

            // new character
            if (line.Length >= 3 && line[0] == ' '
                && line[1] == ' ' && line[2] != ' ')
            {
                // extract character name

                int start_idx = 2;
                int end_idx = 3;
                while (end_idx <= line.Length && line[end_idx - 1] != '.')
                {
                    ++end_idx;
                }

                // no name found
                if (end_idx >= line.Length)
                {
                    return -1;//' this should be -1 not 0
                }

                // extract character's name
                character = line.Substring(start_idx, end_idx - start_idx - 1);
                return end_idx;
            }

            // previous character
            if (line.Length >= 5 && line[0] == ' '
                && line[1] == ' ' && line[2] == ' '
                && line[3] == ' ' && line[4] != ' ')
            {
                // continuation
                return 4;
            }

            return -1; //'this should be -1 not 0
        }






        /**
         * Sorts characters in descending order by word count
         *
         * @param wcounts a map of character -> word count
         * @return sorted vector of {character, word count} pairs
         */

        //'argument is a dictionary called wordcount that contains the characters in the play mapped to their word counts
        //'string is the character in the play, int is the wordcount for that character
        //'return a list of tuples that is sorted in descending order of word count (aka descending order of value)

        //'the dictionary here has keys as strings, and values as ints
        public static List<Tuple<int, string>> SortCharactersByWordcount(Dictionary<string, int> wordcount)
        {

            // Implement sorting by word count here

            //'declare list of tuples that ill return at the end
            List<Tuple<int,string>> sortedByValueList = new List<Tuple<int, string>>();



            // var tup = new Tuple<int, string>(wordcount.ElementAt(i).Value, wordcount.ElementAt(i).Key);

            //var result = wordcount.OrderByDescending(key => key.Value);




            ////'this method was good just incomplete
            //for (int i = 0;i< wordcount.Count;i++)
            //sortedByValueList.Add(new Tuple<int, string>(wordcount.ElementAt(i).Value, wordcount.ElementAt(i).Key));

            //List<Tuple<int, string>> result = sortedByValueList.OrderByDescending(Key => Key.Value);

          // declare temp var to hold ordered wordcount
            var temp =  wordcount.OrderBy(key => key.Key);
            temp.OrderBy(key => key.Value);


            //not sure about this part???
            sortedByValueList = temp.Select(x => new Tuple<int, string>(x.Value, x.Key)).ToList();
            sortedByValueList.Sort();

            return sortedByValueList;







           // var result = sortedByValueList.OrderByDescending(Key => Key.Value);//' use orderbydescending sorting operator on wordcount and store sorted dictionary in result. the key=>key.value just says order the keys by the value associated with the keys





            //foreach ( KeyValuePair<string,int> pairItem in result)
            //{
            //   sortedByValueList = (List<Tuple<int, string>>)result;//'cast result into this format
            //}

        }







        /**
         * Prints the List of Tuple<int, string>
         *
         * @param sortedList
         * @return Nothing
         */
        public static void PrintListofTuples(List<Tuple<int, string>> sortedList)
        {

            // Implement printing here

            foreach (var tuple in sortedList)
            {
                Console.WriteLine("{0} - {1}", tuple.Item1, tuple.Item2);
            }


        }







    }
}


