using System;
using TMPro;
using UnityEngine;

public class DailyBonus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rewardCoin;

    private int[] сountDayInMonth = new []{31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
    private void Awake()
    {
        GetRewardCoin(GetNumberDay());
    }
    private int GetNumberDay()
    {
        int month = DateTime.Now.Month;
        int day = DateTime.Now.Day;
        int numberDay = 0;

        if (month == 12 || month <= 2)
        {
            numberDay= NumberDayInSeason(month, day, 1);
        }
        else if (month > 2 && month <= 5)
        {
            numberDay= NumberDayInSeason(month, day, 3);
        }
        else if (month > 5 && month <= 8)
        {
            numberDay= NumberDayInSeason(month, day, 6);
        }
        else if (month > 8 && month <= 11)
        {
            numberDay= NumberDayInSeason(month, day, 9);
        }

        return numberDay;
    }
    private int NumberDayInSeason(int numberMonth, int numberDay, int numberFirstMonthSeason)
    {
        int numberDaySeason = 0;
        while (numberMonth > numberFirstMonthSeason)
        {
            numberMonth--;
            if (numberMonth == 0)
            {
                numberMonth = 12;
            }
            numberDaySeason += сountDayInMonth[numberMonth];
        }
        numberDaySeason += numberDay;
        return numberDaySeason;
    }

    private void GetRewardCoin(int numberDay)
    {
      double countCoin=0;
      switch (numberDay)
       {
           case 1:
               countCoin = 2;
               break;
           case 2:
               countCoin = 3;
               break;
           default:
               double lastDay=2;
               double beforeLastDay=3;
               for(int i = 3; i < numberDay; i++)
               {
                   countCoin = Math.Round( lastDay * 0.6f+beforeLastDay);
                   beforeLastDay = lastDay;
                   lastDay = countCoin;
               }
               break;
       }
       _rewardCoin.text = countCoin.ToString();
    }
}
