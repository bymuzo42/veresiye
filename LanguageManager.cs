using Guna.UI2.WinForms; // Guna kütüphanesini dahil ettik
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

public class LanguageManager
{
    public static LanguageManager _instance;
    public Dictionary<string, Dictionary<string, string>> _languageData; // Nested Dictionary
    public string _selectedLanguage;

    // Singleton pattern ile tek bir örnek
    public static LanguageManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LanguageManager();
            }
            return _instance;
        }
    }

    // Dil dosyasını yükle
    public void LoadLanguage(string selectedLanguage)
    {
        _selectedLanguage = selectedLanguage;
        string jsonPath = $@"Languages\{_selectedLanguage}.json";

        if (File.Exists(jsonPath))
        {
            var jsonData = File.ReadAllText(jsonPath);
            _languageData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonData);
        }
        else
        {
            MessageBox.Show("Dil dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // UI öğelerini JSON verileriyle güncelle
    internal void ApplyLanguage(Form form)
    {
        if (_languageData != null && _languageData.ContainsKey(form.Name))
        {
            var formLanguageData = _languageData[form.Name];

            foreach (Control control in form.Controls)
            {
                if (formLanguageData.ContainsKey(control.Name))
                {
                    if (control is Guna2TextBox gunaTxt)
                    {
                        // Normal text değişimi
                        gunaTxt.Text = formLanguageData[gunaTxt.Name];

                        // PlaceholderText kontrolü
                        if (formLanguageData.ContainsKey(gunaTxt.Name + "Placeholder"))
                        {
                            gunaTxt.PlaceholderText = formLanguageData[gunaTxt.Name + "Placeholder"];
                            gunaTxt.Refresh();
                        }
                    }
                    else if (control is TextBox txt)
                    {
                        txt.Text = formLanguageData[txt.Name];
                    }
                    else if (control is Button btn)
                    {
                        btn.Text = formLanguageData[btn.Name];
                    }
                    else if (control is Label lbl)
                    {
                        lbl.Text = formLanguageData[lbl.Name];
                    }
                    else if (control is CheckBox chk)
                    {
                        chk.Text = formLanguageData[chk.Name];
                    }
                    else if (control is Guna2Button gunaBtn)
                    {
                        gunaBtn.Text = formLanguageData[gunaBtn.Name];
                    }
                    else if (control is Guna2CheckBox gunaChk)
                    {
                        gunaChk.Text = formLanguageData[gunaChk.Name];
                    }
                }
            }

            // Form başlığını güncelle
            if (formLanguageData.ContainsKey("formTitle"))
            {
                form.Text = formLanguageData["formTitle"];
            }
        }
    }



    // Kaydedilen dil tercihini geri yükle
    public string GetSavedLanguage()
    {
        string jsonPath = @"settings.json";

        if (File.Exists(jsonPath))
        {
            var json = File.ReadAllText(jsonPath);
            var settings = JsonConvert.DeserializeObject<dynamic>(json);
            return settings.Language;
        }

        return "tr"; // Varsayılan dil
    }

    // Dil tercihini kaydet
    public void SaveLanguagePreference(string language)
    {
        string jsonPath = @"settings.json";

        var settings = new { Language = language };
        string json = JsonConvert.SerializeObject(settings);

        // Dil tercihini kaydet
        File.WriteAllText(jsonPath, json);
    }

    public string GetMessage(string key)
    {
        if (_languageData != null && _languageData.ContainsKey("Form1") && _languageData["Form1"].ContainsKey(key))
        {
            return _languageData["Form1"][key];
        }
        return "Mesaj bulunamadı."; // Eğer JSON'da karşılığı yoksa varsayılan metin
    }

    public string GetTranslation(string formName, string key)
    {
        if (_languageData != null && _languageData.ContainsKey(formName))
        {
            var formLanguageData = _languageData[formName];
            if (formLanguageData.ContainsKey(key))
            {
                return formLanguageData[key];
            }
        }
        return string.Empty; // Eğer çeviri bulunamazsa boş döndür
    }

}
