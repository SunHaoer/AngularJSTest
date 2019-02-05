using System.Collections.Generic;

namespace AngularTest.Cache
{
    public class Step
    {
        public static Dictionary<long, bool[,]> stepTable = new Dictionary<long, bool[,]>();
        public static int loginPage = 0;
        public static int choosePage = 1;
        public static int addPhone = 2;
        public static int addPhoneCheck = 3;
        public static int replacePhone = 4;
        public static int replacePhoneCheck = 5;
        public static int deletePhone = 6;
        public static int deletePhoneCheck = 7;
        public static int successPage = 8;
        public static int errorPage = 9;
        public static int choosePageSubmit = 10;
        public static int addPhoneSubmit = 11;
        public static int addPhoneCheckSubmit = 12;
        public static int replacePhoneSubmit = 13;
        public static int replacePhoneCheckSubmit = 14;
        public static int deletePhoneSubmit = 15;
        public static int deletePhoneCheckSubmit = 16;
        public static int nowNode = loginPage;
        public static int maxNode = 20;

        public static bool[,] GetStepTableByUserId(long userId)
        {
            return stepTable.GetValueOrDefault(userId);
        }

        public static void InitStepTableByUserId(long userId)
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
            table[errorPage, choosePage] = true;
            stepTable.Add(userId, table);
        }
    }
}


