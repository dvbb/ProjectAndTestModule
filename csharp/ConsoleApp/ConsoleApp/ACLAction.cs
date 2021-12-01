using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    public class ACLAction
    {
        public ACLAction(){}

        public ACLAction(string name)
        {
            System.Console.WriteLine(name);
        }

        public void UseEnvironment()
        {
            //tests: ACLAction.cs rename to AclAction in git.
        }
    }
}
