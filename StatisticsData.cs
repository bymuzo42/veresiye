using System;
using System.Collections.Generic;

namespace Veresiye2025
{
    public class StatisticsData
    {
        // Genel İstatistikler
        public decimal MonthlyTotalAmount { get; set; }
        public decimal TodayIncomingAmount { get; set; }
        public decimal TotalBlockedAmount { get; set; }
        public decimal AverageValorPeriod { get; set; }

        // Banka İstatistikleri
        public string MostUsedBank { get; set; }
        public decimal MostUsedBankAmount { get; set; }
        public string LeastCommissionBank { get; set; }
        public decimal LeastCommissionValue { get; set; }
        public Dictionary<string, decimal> BankDistribution { get; set; }
        public Dictionary<string, int> BankTransactionCounts { get; set; } // Banka işlem sayıları

        // Cihaz İstatistikleri
        public string MostActiveDevice { get; set; }
        public decimal MostActiveDeviceAmount { get; set; }
        public List<DeviceStats> DevicePerformance { get; set; }
        public List<string> InactiveDevices { get; set; }

        // Trend Bilgileri
        public decimal PreviousMonthAmount { get; set; }
        public decimal ChangePercentage { get; set; }
        public decimal AverageDailyAmount { get; set; }
        public decimal MonthEndEstimate { get; set; }

        // Hedef İstatistikleri
        public decimal MonthlyTarget { get; set; }
        public decimal TargetCompletionRate { get; set; }

        // Constructor
        public StatisticsData()
        {
            BankDistribution = new Dictionary<string, decimal>();
            BankTransactionCounts = new Dictionary<string, int>(); // Initialize
            DevicePerformance = new List<DeviceStats>();
            InactiveDevices = new List<string>();
        }
    }

    public class DeviceStats
    {
        public string DeviceName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DailyAverage { get; set; }
        public int TransactionCount { get; set; }
    }

    public class MonthlyComparisonData
    {
        public string Month { get; set; }
        public decimal TotalAmount { get; set; }
    }
}