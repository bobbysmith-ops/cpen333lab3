using System;
namespace Lab3Q1
{

    //'DONT NEED TO MODIFY ANYTHING HERE
    public class UnitTestException: Exception //' custom exception class, UnitTestException is derived from Exception base class (recall custom exceptions from lecture) to throw for unit test failures
    {

        //'these are the fields that have been added to the derived class (aka the custom exception) these dont appear in the base class
        private string line_;
        private int idx_;
        private int results_;
        private int expected_;


       //'define a constructor for the derived class (aka the custom exception)
        public UnitTestException(ref string line, int idx, int results, int expected, string message) : base(message)
        {
            line_ = line;
            idx_ = idx;
            results_ = results;
            expected_ = expected;
        }



    }
}
