// AlarmHelper.cs adında yeni bir sınıf dosyası
using System;
using System.Windows.Forms;

namespace Veresiye2025
{
    public static class AlarmHelper
    {
        public static Form _alarmListeForm = null;

        public static void GosterAlarmListesi()
        {
            // Mevcut form açıksa, öne getir
            if (_alarmListeForm != null && !_alarmListeForm.IsDisposed)
            {
                _alarmListeForm.BringToFront();
                _alarmListeForm.WindowState = FormWindowState.Normal;
                return;
            }

            // Değilse, yeni bir form oluştur
            Alarmkur tempForm = new Alarmkur();
            tempForm.AlarmListesiniGoster();
        }
    }
}