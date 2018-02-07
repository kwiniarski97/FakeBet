using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBet.Models;

namespace FakeBet.Repository.Interfaces
{
    interface IHighScoreRepository
    {
        HighScore GetHighScores();

        void SetDailyHighScore(List<User> topDaily);
        void SetWeeklyHighScore(List<User> topWeekly);
        void SetMonthlyHighScore(List<User> topMonthly);
        void SetYearlyHighScore(List<User> topYearly);
    }
}