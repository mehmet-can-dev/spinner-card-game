using System;
using UnityEngine;

namespace Economy
{
    public static class GameEconomy
    {
        // ToDo integer max limit must be check or change currency type
        // ToDo Temporary save PlayerPrefs 
        private static int Coin
        {
            get => PlayerPrefs.GetInt(nameof(CurrencyType.COIN), 0);
            set => PlayerPrefs.SetInt(nameof(CurrencyType.COIN), value);
        }

        // ToDo integer max limit must be check or change currency type
        // ToDo Temporary save PlayerPrefs 
        private static int Cash
        {
            get => PlayerPrefs.GetInt(nameof(CurrencyType.CASH), 0);
            set => PlayerPrefs.SetInt(nameof(CurrencyType.CASH), value);
        }


        //Temporary Functions
        public static void AdjustCurrency(CurrencyType type, int amount)
        {
            switch (type)
            {
                case CurrencyType.COIN:
                    Coin += amount;
                    break;
                case CurrencyType.CASH:
                    Cash += amount;
                    break;
            }
        }

        //Temporary Functions
        public static int GetCurrency(CurrencyType type)
        {
            switch (type)
            {
                case CurrencyType.COIN:
                    return Coin;
                case CurrencyType.CASH:
                    return Cash;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}