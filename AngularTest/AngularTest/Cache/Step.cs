using System.Collections.Generic;

namespace AngularTest.Cache
{
    public class Step
    {
        public static int loginPage = 1;
        public static int choosePage = 2;
        public static int addPhone = 3;
        public static int addPhoneCheck = 4;
        public static int replacePhone = 5;
        public static int replacePhoneCheck = 6;
        public static int deletePhone = 7;
        public static int deletePhoneCheck = 8;
        public static int successPage = 9;
        public static int errorPage = 10;
        public static int choosePageSubmit = 11;
        public static int addPhoneSubmit = 12;
        public static int addPhoneCheckSubmit = 13;
        public static int replacePhoneSubmit = 14;
        public static int replacePhoneCheckSubmit = 15;
        public static int deletePhoneSubmit = 16;
        public static int deletePhoneCheckSubmit = 17;
        public static int successPageSubmit = 18;
        public static int errorPageSubmit = 19;
        public static int maxNode = 20;
        public static bool[,] stepTable = new bool[maxNode, maxNode];
        public static Dictionary<long, int> nowNodeTable = new Dictionary<long, int>(); 
        public static int isSubmitTrue = 1;
        public static int isSubmitFalse = 0;

        public static void InitStepTable()
        {
            bool[,] table = new bool[maxNode, maxNode];
            for(int i = 0; i < maxNode; i++)
            {
                for(int j = 0; j < maxNode; j++)
                {
                    if(i != j)
                    {
                        table[i, j] = false;
                    }
                    else
                    {
                        table[i, j] = true;
                    }
                }
            }
            table[loginPage, choosePage] = true;
            table[loginPage, errorPage] = true;
            table[choosePage, addPhone] = true;
            table[choosePage, replacePhone] = true;
            table[choosePage, deletePhone] = true;
            table[choosePage, errorPage] = true;
            table[choosePage, choosePageSubmit] = true;
            table[addPhone, choosePage] = true;
            table[addPhone, addPhoneCheck] = true;
            table[addPhone, errorPage] = true;
            table[addPhone, addPhoneSubmit] = true;
            table[addPhoneCheck, addPhone] = true;
            table[addPhoneCheck, choosePage] = true;
            table[addPhoneCheck, successPage] = true;
            table[addPhoneCheck, errorPage] = true;
            table[addPhoneCheck, addPhoneCheckSubmit] = true;
            table[replacePhone, choosePage] = true;
            table[replacePhone, replacePhoneCheck] = true;
            table[replacePhone, errorPage] = true;
            table[replacePhone, replacePhoneSubmit] = true;
            table[replacePhoneCheck, replacePhone] = true;
            table[replacePhoneCheck, choosePage] = true;
            table[replacePhoneCheck, successPage] = true;
            table[replacePhoneCheck, errorPage] = true;
            table[replacePhoneCheck, replacePhoneCheckSubmit] = true;
            table[deletePhone, choosePage] = true;
            table[deletePhone, deletePhoneCheck] = true;
            table[deletePhone, errorPage] = true;
            table[deletePhone, deletePhoneSubmit] = true;
            table[deletePhoneCheck, deletePhone] = true;
            table[deletePhoneCheck, choosePage] = true;
            table[deletePhoneCheck, successPage] = true;
            table[deletePhoneCheck, errorPage] = true;
            table[deletePhoneCheck, deletePhoneCheckSubmit] = true;
            table[successPage, choosePage] = true;
            table[successPage, successPageSubmit] = true;
            table[errorPage, choosePage] = true;
            table[errorPage, errorPageSubmit] = true;
            stepTable = table;
        }
    }
}


