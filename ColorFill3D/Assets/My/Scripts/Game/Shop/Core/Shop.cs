using UnityEngine;
using YG;

namespace Project
{
    public class Shop
    {
        public static bool CheckBalance(int balance, int parce)
        {
            if (balance >= parce)
                return true;

            return false;
        }

        public static void TakeBankValue(int parce)
        {
            YandexGame.savesData.Bank -= parce;
            YandexGame.SaveProgress();
        }
    }
}