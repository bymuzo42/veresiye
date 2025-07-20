using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Veresiye2025
{
    internal static class ThemeManager
    {
        public static bool IsDarkMode { get; set; } = false; // Varsayılan açık tema
                                                             // ThemeManager sınıfına eklenecek kısım:
        public static event EventHandler<bool> ThemeChanged;

        // Tema renkleri - merkezi olarak tanımlanmış
        public static class Colors
        {
            // Açık tema renkleri
            public static class Light
            {
                public static Color Background = Color.FromArgb(245, 245, 245);
                public static Color ForeColor = Color.Black;
                public static Color PanelBackground = Color.White;
                public static Color PrimaryButton = Color.FromArgb(0, 123, 255);
                public static Color SecondaryButton = Color.FromArgb(108, 117, 125);
                public static Color TitleBar = Color.FromArgb(0, 123, 255);
                public static Color Border = Color.FromArgb(220, 220, 220);
                public static Color FocusedBorder = Color.FromArgb(94, 148, 255);
                public static Color TextBoxBackground = Color.White;
                public static Color DisabledBackground = Color.FromArgb(226, 226, 226);
                public static Color DisabledForeColor = Color.FromArgb(138, 138, 138);
            }

            // Koyu tema renkleri
            public static class Dark
            {
                public static Color Background = Color.FromArgb(40, 40, 40);
                public static Color ForeColor = Color.White;
                public static Color PanelBackground = Color.FromArgb(50, 50, 50);
                public static Color PrimaryButton = Color.FromArgb(80, 80, 80);
                public static Color SecondaryButton = Color.FromArgb(73, 80, 87);
                public static Color TitleBar = Color.FromArgb(30, 30, 30);
                public static Color Border = Color.FromArgb(70, 70, 75);
                public static Color FocusedBorder = Color.FromArgb(94, 148, 255);
                public static Color TextBoxBackground = Color.FromArgb(60, 60, 60);
                public static Color DisabledBackground = Color.FromArgb(40, 40, 45);
                public static Color DisabledForeColor = Color.FromArgb(150, 150, 150);
            }
        }

        public static void ApplyTheme(Form form)
        {
            // Form arkaplan ve yazı rengini ayarla
            form.BackColor = IsDarkMode ? Colors.Dark.Background : Colors.Light.Background;
            form.ForeColor = IsDarkMode ? Colors.Dark.ForeColor : Colors.Light.ForeColor;

            // Formdaki tüm kontrollere tema uygula
            foreach (Control ctrl in form.Controls)
            {
                ApplyControlTheme(ctrl);
            }
        }

        public static void ApplyControlTheme(Control ctrl)
        {
            // Guna.UI2 kontrolleri için özel işlemler
            if (ctrl is Guna2Panel)
            {
                var gunaPanel = (Guna2Panel)ctrl;
                ApplyThemeToGunaPanel(gunaPanel);
            }
            else if (ctrl is Guna2Button)
            {
                var gunaButton = (Guna2Button)ctrl;
                ApplyThemeToGunaButton(gunaButton);
            }
            else if (ctrl is Guna2TextBox)
            {
                var gunaTextBox = (Guna2TextBox)ctrl;
                ApplyThemeToGunaTextBox(gunaTextBox);
            }
            else if (ctrl is Guna2ComboBox)
            {
                var gunaComboBox = (Guna2ComboBox)ctrl;
                ApplyThemeToGunaComboBox(gunaComboBox);
            }
            else if (ctrl is Guna2DateTimePicker)
            {
                var gunaDatePicker = (Guna2DateTimePicker)ctrl;
                ApplyThemeToGunaDateTimePicker(gunaDatePicker);
            }
            else if (ctrl is Guna2DataGridView)
            {
                var gunaDataGridView = (Guna2DataGridView)ctrl;
                ApplyThemeToGunaDataGridView(gunaDataGridView);
            }
            
            // Standart Windows Forms kontrolleri
            else if (ctrl is TabControl)
            {
                var tabControl = (TabControl)ctrl;
                tabControl.BackColor = IsDarkMode ? Colors.Dark.PanelBackground : Colors.Light.PanelBackground;
                tabControl.ForeColor = IsDarkMode ? Colors.Dark.ForeColor : Colors.Light.ForeColor;
                
                foreach (TabPage tabPage in tabControl.TabPages)
                {
                    tabPage.BackColor = IsDarkMode ? Colors.Dark.PanelBackground : Colors.Light.PanelBackground;
                    tabPage.ForeColor = IsDarkMode ? Colors.Dark.ForeColor : Colors.Light.ForeColor;
                    
                    // TabPage içindeki kontrolleri de temala
                    foreach (Control tabCtrl in tabPage.Controls)
                    {
                        ApplyControlTheme(tabCtrl);
                    }
                }
            }
            else if (ctrl is Button)
            {
                var button = (Button)ctrl;
                button.BackColor = IsDarkMode ? Colors.Dark.PrimaryButton : Colors.Light.PrimaryButton;
                button.ForeColor = Color.White;
            }
            else if (ctrl is TextBox)
            {
                var textBox = (TextBox)ctrl;
                textBox.BackColor = IsDarkMode ? Colors.Dark.TextBoxBackground : Colors.Light.TextBoxBackground;
                textBox.ForeColor = IsDarkMode ? Colors.Dark.ForeColor : Colors.Light.ForeColor;
            }
            else if (ctrl is ComboBox)
            {
                var comboBox = (ComboBox)ctrl;
                comboBox.BackColor = IsDarkMode ? Colors.Dark.TextBoxBackground : Colors.Light.TextBoxBackground;
                comboBox.ForeColor = IsDarkMode ? Colors.Dark.ForeColor : Colors.Light.ForeColor;
            }
            else if (ctrl is Panel || ctrl is GroupBox)
            {
                ctrl.BackColor = IsDarkMode ? Colors.Dark.PanelBackground : Colors.Light.PanelBackground;
                
                // Panel veya GroupBox içindeki diğer kontrolleri de temala
                foreach (Control subCtrl in ctrl.Controls)
                {
                    ApplyControlTheme(subCtrl);
                }
            }
            else if (ctrl is Label)
            {
                var label = (Label)ctrl;
                label.ForeColor = IsDarkMode ? Colors.Dark.ForeColor : Colors.Light.ForeColor;
            }
            else if (ctrl is DataGridView)
            {
                var dataGridView = (DataGridView)ctrl;
                dataGridView.BackgroundColor = IsDarkMode ? Colors.Dark.PanelBackground : Colors.Light.PanelBackground;
                dataGridView.ForeColor = IsDarkMode ? Colors.Dark.ForeColor : Colors.Light.ForeColor;
                dataGridView.GridColor = IsDarkMode ? Colors.Dark.Border : Colors.Light.Border;
                
                dataGridView.DefaultCellStyle.BackColor = IsDarkMode ? Colors.Dark.PanelBackground : Colors.Light.PanelBackground;
                dataGridView.DefaultCellStyle.ForeColor = IsDarkMode ? Colors.Dark.ForeColor : Colors.Light.ForeColor;
                
                dataGridView.ColumnHeadersDefaultCellStyle.BackColor = IsDarkMode ? Colors.Dark.PrimaryButton : Colors.Light.PrimaryButton;
                dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            }
            
            // Diğer kontrol tipleri için
            else if (ctrl.HasChildren)
            {
                // Alt kontrolleri temala
                foreach (Control child in ctrl.Controls)
                {
                    ApplyControlTheme(child);
                }
            }
        }
        
        // Guna UI2 kontrollerine özel tema uygulama metotları
        public static void ApplyThemeToGunaPanel(Guna2Panel panel)
        {
            try
            {
                if (panel.Name.Contains("TitleBar") || panel.Name.Contains("titleBar"))
                {
                    panel.FillColor = IsDarkMode ? Colors.Dark.TitleBar : Colors.Light.TitleBar;
                }
                else
                {
                    panel.FillColor = IsDarkMode ? Colors.Dark.PanelBackground : Colors.Light.PanelBackground;
                }
                panel.ForeColor = IsDarkMode ? Colors.Dark.ForeColor : Colors.Light.ForeColor;
                panel.BorderColor = IsDarkMode ? Colors.Dark.Border : Colors.Light.Border;
                
                // Panel içindeki diğer kontrolleri de temala
                foreach (Control subCtrl in panel.Controls)
                {
                    ApplyControlTheme(subCtrl);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Panel tema hatası: " + ex.Message);
            }
        }
        
        public static void ApplyThemeToGunaButton(Guna2Button button)
        {
            try
            {
                if (button.Name.Contains("Close") || button.Text == "X")
                {
                    button.FillColor = Color.Transparent;
                }
                else if (button.Name.Contains("temizle") || button.Name.Contains("Sil") || button.Name.Contains("Cancel"))
                {
                    button.FillColor = IsDarkMode ? Colors.Dark.SecondaryButton : Colors.Light.SecondaryButton;
                }
                else
                {
                    button.FillColor = IsDarkMode ? Colors.Dark.PrimaryButton : Colors.Light.PrimaryButton;
                }
                button.ForeColor = Color.White;
                button.BorderColor = IsDarkMode ? Colors.Dark.Border : Colors.Light.Border;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Button tema hatası: " + ex.Message);
            }
        }
        
        public static void ApplyThemeToGunaTextBox(Guna2TextBox textBox)
        {
            try
            {
                textBox.FillColor = IsDarkMode ? Colors.Dark.TextBoxBackground : Colors.Light.TextBoxBackground;
                textBox.ForeColor = IsDarkMode ? Colors.Dark.ForeColor : Colors.Light.ForeColor;
                textBox.BorderColor = IsDarkMode ? Colors.Dark.Border : Colors.Light.Border;
                textBox.PlaceholderForeColor = IsDarkMode ? Color.FromArgb(170, 170, 170) : Color.FromArgb(125, 125, 125);
                
                // FocusedState özelliği varsa ayarla
                var focusedStateProperty = textBox.GetType().GetProperty("FocusedState");
                if (focusedStateProperty != null)
                {
                    var focusedState = focusedStateProperty.GetValue(textBox);
                    var borderColorProperty = focusedState.GetType().GetProperty("BorderColor");
                    if (borderColorProperty != null)
                    {
                        borderColorProperty.SetValue(focusedState, IsDarkMode ? Colors.Dark.FocusedBorder : Colors.Light.FocusedBorder);
                    }
                }
                
                // DisabledState özelliği varsa ayarla
                var disabledStateProperty = textBox.GetType().GetProperty("DisabledState");
                if (disabledStateProperty != null)
                {
                    var disabledState = disabledStateProperty.GetValue(textBox);
                    var fillColorProperty = disabledState.GetType().GetProperty("FillColor");
                    var foreColorProperty = disabledState.GetType().GetProperty("ForeColor");
                    
                    if (fillColorProperty != null)
                    {
                        fillColorProperty.SetValue(disabledState, IsDarkMode ? Colors.Dark.DisabledBackground : Colors.Light.DisabledBackground);
                    }
                    
                    if (foreColorProperty != null)
                    {
                        foreColorProperty.SetValue(disabledState, IsDarkMode ? Colors.Dark.DisabledForeColor : Colors.Light.DisabledForeColor);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("TextBox tema hatası: " + ex.Message);
            }
        }
        
        public static void ApplyThemeToGunaComboBox(Guna2ComboBox comboBox)
        {
            try
            {
                comboBox.FillColor = IsDarkMode ? Colors.Dark.TextBoxBackground : Colors.Light.TextBoxBackground;
                comboBox.ForeColor = IsDarkMode ? Colors.Dark.ForeColor : Colors.Light.ForeColor;
                comboBox.BorderColor = IsDarkMode ? Colors.Dark.Border : Colors.Light.Border;
                
                // ItemsAppearance özelliği varsa ayarla
                var itemsAppearanceProperty = comboBox.GetType().GetProperty("ItemsAppearance");
                if (itemsAppearanceProperty != null)
                {
                    var itemsAppearance = itemsAppearanceProperty.GetValue(comboBox);
                    var foreColorProperty = itemsAppearance.GetType().GetProperty("ForeColor");
                    var selectedBackColorProperty = itemsAppearance.GetType().GetProperty("SelectedBackColor");
                    
                    if (foreColorProperty != null)
                    {
                        foreColorProperty.SetValue(itemsAppearance, IsDarkMode ? Colors.Dark.ForeColor : Colors.Light.ForeColor);
                    }
                    
                    if (selectedBackColorProperty != null)
                    {
                        selectedBackColorProperty.SetValue(itemsAppearance, IsDarkMode ? Colors.Dark.PrimaryButton : Colors.Light.PrimaryButton);
                    }
                }
                
                // FocusedState özelliği varsa ayarla
                var focusedStateProperty = comboBox.GetType().GetProperty("FocusedState");
                if (focusedStateProperty != null)
                {
                    var focusedState = focusedStateProperty.GetValue(comboBox);
                    var borderColorProperty = focusedState.GetType().GetProperty("BorderColor");
                    if (borderColorProperty != null)
                    {
                        borderColorProperty.SetValue(focusedState, IsDarkMode ? Colors.Dark.FocusedBorder : Colors.Light.FocusedBorder);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ComboBox tema hatası: " + ex.Message);
            }
        }
        
        public static void ApplyThemeToGunaDateTimePicker(Guna2DateTimePicker datePicker)
        {
            try
            {
                datePicker.FillColor = IsDarkMode ? Colors.Dark.TextBoxBackground : Colors.Light.TextBoxBackground;
                datePicker.ForeColor = IsDarkMode ? Colors.Dark.ForeColor : Colors.Light.ForeColor;
                datePicker.BorderColor = IsDarkMode ? Colors.Dark.Border : Colors.Light.Border;
                
                // Reflection ile FocusedState özelliğine erişmeye çalış
                // NOT: Guna.UI2.WinForms versiyonuna bağlı olarak bu özellik olmayabilir
                try
                {
                    var focusedStateProperty = datePicker.GetType().GetProperty("FocusedState");
                    if (focusedStateProperty != null)
                    {
                        var focusedState = focusedStateProperty.GetValue(datePicker);
                        var borderColorProperty = focusedState.GetType().GetProperty("BorderColor");
                        if (borderColorProperty != null)
                        {
                            borderColorProperty.SetValue(focusedState, IsDarkMode ? Colors.Dark.FocusedBorder : Colors.Light.FocusedBorder);
                        }
                    }
                }
                catch (Exception) { /* Bu özellik bu versiyonda olmayabilir, hata gösterme */ }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DateTimePicker tema hatası: " + ex.Message);
            }
        }
        
        public static void ApplyThemeToGunaDataGridView(Guna2DataGridView dataGridView)
        {
            try
            {
                // Temel özellikler
                dataGridView.BackgroundColor = IsDarkMode ? Colors.Dark.PanelBackground : Colors.Light.PanelBackground;
                dataGridView.GridColor = IsDarkMode ? Colors.Dark.Border : Colors.Light.Border;
                
                // DefaultCellStyle
                dataGridView.DefaultCellStyle.BackColor = IsDarkMode ? Colors.Dark.PanelBackground : Colors.Light.PanelBackground;
                dataGridView.DefaultCellStyle.ForeColor = IsDarkMode ? Colors.Dark.ForeColor : Colors.Light.ForeColor;
                
                // HeadersStyle
                dataGridView.ColumnHeadersDefaultCellStyle.BackColor = IsDarkMode ? Colors.Dark.PrimaryButton : Colors.Light.PrimaryButton;
                dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                
                // ThemeStyle özelliği için güvenli erişim
                try
                {
                    var themeStyleProperty = dataGridView.GetType().GetProperty("ThemeStyle");
                    if (themeStyleProperty != null)
                    {
                        dynamic themeStyle = themeStyleProperty.GetValue(dataGridView);
                        
                        // BackColor ve diğer özelliklere yansıma ile erişmeye çalış
                        var backColorProperty = themeStyle.GetType().GetProperty("BackColor");
                        if (backColorProperty != null)
                        {
                            backColorProperty.SetValue(themeStyle, IsDarkMode ? Colors.Dark.PanelBackground : Colors.Light.PanelBackground);
                        }
                    }
                }
                catch (Exception) { /* ThemeStyle özelliği erişilemezse atla */ }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DataGridView tema hatası: " + ex.Message);
            }
        }

        public static void ToggleTheme(Form form)
        {
            IsDarkMode = !IsDarkMode;
            ApplyTheme(form);

            // Tema değişim olayını tetikle
            ThemeChanged?.Invoke(null, IsDarkMode);
        }
    }
}