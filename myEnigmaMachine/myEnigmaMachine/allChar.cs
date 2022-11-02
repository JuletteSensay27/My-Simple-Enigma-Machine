using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myEnigmaMachine
{
    internal class allChar
    {
        private List<char> allUpper(List<char> allUpLetList) 
        {
            List<char> allUpLet = allUpLetList;

            for (int i = 65; i < 91; i++) //Uppercase Letters
            {
                char temp = (char)i;
                allUpLet.Add(temp);
            }

            return allUpLet;
        }

        private List<char> allLower(List<char> allLowerLetList) 
        {
            List<char> allLowLet = allLowerLetList;

            for (int i = 97; i < 123; i++) //Lowercase Letters
            {
                char temp = (char)i;
                allLowLet.Add(temp);
            }

            return allLowLet;
        }

        private List<char> allNum(List<char> allNumList)
        {
            List<char> allNum = allNumList;

            for (int i = 48; i < 58; i++) //All Numbers
            {
                char temp = (char)i;
                allNum.Add(temp);
            }

            return allNum;
        }

        private List<char> allSpecKeys(List<char> allSpecKeysList) 
        {
            List<char> allSpecKeys = allSpecKeysList;


            for (int i = 32; i < 48; i++) //All Special Characters Part 1
            {
                char temp = (char)i;
                allSpecKeys.Add(temp);
            }

            for (int i = 58; i < 65; i++) //All Special Characters Part 2
            {
                char temp = (char)i;
                allSpecKeys.Add(temp);
            }

            for (int i = 91; i < 97; i++) //All Special Characters Part 3
            {
                char temp = (char)i;
                allSpecKeys.Add(temp);
            }

            for (int i = 123; i < 126; i++) //All Special Characters Part 4
            {
                char temp = (char)i;
                allSpecKeys.Add(temp);
            }

            allSpecKeys.Remove('^');
            allSpecKeys.Remove('`');

            return allSpecKeys;
        }

        public void allCharInt(List<char> allUpLetList, List<char> allLowerLetList, List<char> allNumList, List<char> allSpecKeysList) 
        {
            allUpper(allUpLetList);
            allLower(allLowerLetList);
            allNum(allNumList);
            allSpecKeys(allSpecKeysList);
        }

    }
}
