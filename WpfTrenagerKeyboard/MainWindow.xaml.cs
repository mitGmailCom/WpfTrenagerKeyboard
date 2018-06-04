using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfTrenagerKeyboard
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool FlagForShiftPlusKey { get; set; } = false;
        private bool FlagPressShift { get; set; } = false;
        private bool FlagForRightShift { get; set; } = false;
        private bool FlagCaseSensitive { get; set; }
        private bool FlagForUpperSymbol { get; set; }
        private bool flagPressButtun = false;
        private List<Key> ListKeysNoSwow;
        //private List<string> ListKeysInKeyboard;
        private List<string> ListKeysInKeyboard2;
        private int Diffiicult { get; set; }
        private int count = 0;
        private int LengthStr { get; set; }
        private bool endFor = false;
        private string Str { get; set; }
        List<string> MasSymbols { get; set; }
        List<Button> ListButtonsWithKeys { get; set; }
        DispatcherTimer timer;
        private int sec { get; set; } = 0;
        
        public MainWindow()
        {
            ListKeysNoSwow = new List<Key>() {Key.Tab, Key.LeftCtrl, Key.LeftShift, Key.LWin, Key.LeftAlt, Key.RightAlt, Key.RWin, Key.RightShift, Key.RightCtrl, Key.Enter, Key.Space };

            ListKeysInKeyboard2 = new List<string>()
            {
                "`","~","1","!","2","@","3","#","4","$","5","%","6","^","7","&","8","*","9","(","0",")","-","_","=","+",
                "q","Q","w","W","e","E","r","R","t","T","y","Y","u","U","i","I","o","O","p","P","[","{","]","}","\\","|",
                "a","A","s","S","d","D","f","F","g","G","h","H","j","J","k","K","l","L",";",":","'","\"",
                "z","Z","x","X","c","C","v","V","b","B","n","N","m","M",",","<",".",">","/","?"
            };
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }



        private void wind_Initialized(object sender, EventArgs e)
        {
            wind.Height = System.Windows.SystemParameters.VirtualScreenHeight * 0.6;
            wind.Width = System.Windows.SystemParameters.VirtualScreenWidth;
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
           

            FlagCaseSensitive = false;
            FlagForUpperSymbol = false;
            flagPressButtun = false;
            LengthStr = 40;
            btnStop.IsEnabled = false;
            MasSymbols = new List<string>();

            ListButtonsWithKeys = new List<Button>()
            {
                btnTilda,btn1,btn2,btn3,btn4,btn5,btn6,btn7,btn8,btn9,btn0,btnSubtract,btnPlus,
                btnQ,btnWW,btnE,btnR,btnT,btnY,btnU,btnI,btnO,btnP,btnSqScobaL,btnSqScobaR,btnBSlew,
                btnA,btnS,btnD,btnF,btnG,btnH,btnJ,btnK,btnL,btnPointComma,btnUpperComma,
                btnZ,btnX,btnC,btnV,btnB,btnN,btnM,btnComa,btnPoint,btnDevide
            };

            if (sliderDifficult.Value > 0)
            {
                btnStart.IsEnabled = true;
                Formstr();
            }
        }

        // изменение размера шрифта в TextBox и TextBlock в зависимости от размера окна
        private void wind_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Hpt=(25.4/DPI*Hpix)/0,35
            int fontSize = 0;
            fontSize = (int)((25.4 / 96 * txtBlShowText.ActualHeight) / 0.35);
            txtBlShowText.FontSize = (int)fontSize * 0.75;
            txtBlInputText.FontSize = (int)fontSize * 0.75;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            sec++;
            btnStart.Content = $"{sec} секунд";
            int t = (int)((Convert.ToDouble(txtBlInputText.Text.Length) / Convert.ToDouble(sec*60)) * 3600.0);
            txtBlSpeed.Text = $"Speed: {t} chars/min";
        }


        private void Formstr()
        {
            // string tempStr = null;
            string tempSymbol = null;
            
            // копия символов клавиатуры
            List<string> CopyMasSymbols = new List<string>();

            // формирование строки с учетом верхнего регистра
            if (FlagCaseSensitive)
            {
                // массив случайных символов из CopyMasSymbols
                List<string> TempListSymbols = new List<string>();
                CopyMasSymbols = ListKeysInKeyboard2.ToList();

                // добавляем пробел
                TempListSymbols.Add(" ");

                // удаляем из массива пробел, чтобы все символы были уникальными
                CopyMasSymbols.Remove(" ");

                // наполнение случайными символами массива TempListSymbols
                for (int i = 0; i < Diffiicult - 1; i++)
                {
                    tempSymbol = CopyMasSymbols[new Random().Next(0, CopyMasSymbols.Count)];
                    CopyMasSymbols.Remove(tempSymbol);
                    TempListSymbols.Add(tempSymbol);
                }

                // формирование строки из массива TempListSymbols
                for (int j = 0; j < LengthStr; j++)
                {

                    Str += TempListSymbols[new Random().Next(0, TempListSymbols.Count)];
                    System.Threading.Thread.Sleep(10);
                    //if (Str.Length == LengthStr)
                    //    continue;
                    //endFor = false;
                    //tempSymbol = null;
                    //tempSymbol = CopyMasSymbols[new Random().Next(0, CopyMasSymbols.Count)];
                    //FormedStr(tempStr);
                }
            }

            // формирование строки без учета верхнего регистра
            if (!FlagCaseSensitive)
            {
                // массив случайных символов из CopyMasSymbols
                List<string> TempListSymbols = new List<string>();
                CopyMasSymbols = ListKeysInKeyboard2.ToList();

                for (int i = CopyMasSymbols.Count; i >= 0; i--)
                {
                    if (i >= 2)
                    {
                        if (i % 2 == 1)
                            CopyMasSymbols.RemoveAt(i);
                    }
                }
                CopyMasSymbols.RemoveAt(1);

                // добавляем пробел
                TempListSymbols.Add(" ");

                // удаляем из массива пробел, чтобы все символы были уникальными
                CopyMasSymbols.Remove(" ");

                // наполнение случайными символами массива TempListSymbols
                for (int i = 0; i < Diffiicult - 1; i++)
                {
                    tempSymbol = CopyMasSymbols[new Random().Next(0, CopyMasSymbols.Count)];
                    CopyMasSymbols.Remove(tempSymbol);
                    TempListSymbols.Add(tempSymbol);
                }

                // формирование строки из массива TempListSymbols
                for (int j = 0; j < LengthStr; j++)
                {

                    Str += TempListSymbols[new Random().Next(0, TempListSymbols.Count)];
                    System.Threading.Thread.Sleep(10);
                    //if (Str.Length == LengthStr)
                    //    continue;
                    //endFor = false;
                    //tempSymbol = null;
                    //tempSymbol = CopyMasSymbols[new Random().Next(0, CopyMasSymbols.Count)];
                    //FormedStr(tempStr);
                }
            }

            showStr();

        }


        private void FormedStr(string tempStr)
        {
            // усли длина строки больше 0
            if (Str.Length > 0)
            {
                valueStrIn(tempStr);
                if(!endFor)
                    valueStrOut(tempStr);
            }

            // если длина строки равна 0 или null
            if (Str.Length == 0 || Str == null)
            {
                Str += tempStr;
                endFor = true;
                Formstr();
            }

            System.Threading.Thread.Sleep(5);
        }

        // текущий символ = предыдущему символу
        private void valueStrOut(string tempStr)
        {
            if (tempStr != (Str[Str.Length - 1]).ToString())
            {
                Str += tempStr;
                endFor = true;
                //Formstr();
            }
        }

        // текущий символ != предыдущему символу
        private void valueStrIn(string tempStr)
        {
            if (tempStr == (Str[Str.Length - 1]).ToString())
            {
                count++;
                //tempStr = ListKeysInKeyboard[new Random().Next(0, ListKeysInKeyboard.Count)];
                System.Threading.Thread.Sleep(5);
                Formstr();
            }

        }

        // вывод строки в txtBlShowText
        private void showStr()
        {
            txtBlShowText.Text = Str + " - " + $"{Diffiicult}" + " - " + $"{Str.Length}";// +" - " + $"{count}" ;
        }

        // изменение значения в sliderDifficult
        private void sliderDifficult_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            count = 0;
            Str = "";

            // запускаем событие Нажатия btnStop (если btnStop не нажата)
            if (btnStop.IsEnabled)
            {
                Title = Keyboard.FocusedElement.ToString();
                Keyboard.ClearFocus();
                btnStop.Focus();
                ButtonAutomationPeer peer = new ButtonAutomationPeer(btnStop);
                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();
            }

            // выводим значение количества выбранных символов, если Value в sliderDifficult > 6 символов 
            if (sliderDifficult.Value > 6)
            {
                Diffiicult = (int)sliderDifficult.Value;
                txtBlDifficult.Text = $"Diffiicult:" + Diffiicult.ToString();
                Formstr();
            }

        }

        // обрабатываем событие клика по btnStart
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            sec = 0;
            txtBlInputText.Clear();
            if (Str != null)
                showStr();
            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;
            timer.Start();
            btnStop.Content = "Stop";
        }

        // обрабатываем событие клика по btnStop
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            btnStop.IsEnabled = false;
            btnStart.IsEnabled = true;
            timer.Stop();
            btnStart.Content = "Start";
            btnStop.Content = $"{sec} секунды";
        }


        private void changeViewShift(Button btn, double value)
        {
            System.Threading.Thread.Sleep(45);
            FlagForRightShift = false;
            btn.RenderTransformOrigin = new Point(0.5, 0.5);
            ScaleTransform myScaleTransform = new ScaleTransform();
            myScaleTransform.ScaleY = value;
            myScaleTransform.ScaleX = value;
            btn.RenderTransform = myScaleTransform;
            //btn.RenderTransformOrigin = new Point(0.5, 0.5);
        }

        private void changeViewToUpKeys()
        {
            string tmpSymbol = null;
            for (int i = 0; i < ListButtonsWithKeys.Count; i++)
            {
                tmpSymbol = ListButtonsWithKeys[i].Content.ToString();
                ListButtonsWithKeys[i].Content = ListKeysInKeyboard2[ListKeysInKeyboard2.IndexOf(tmpSymbol) + 1];
            }
        }

        private void changeViewToLoKeys()
        {
            string tmpSymbol = null;
            for (int i = 0; i < ListButtonsWithKeys.Count; i++)
            {
                tmpSymbol = ListButtonsWithKeys[i].Content.ToString();
                ListButtonsWithKeys[i].Content = ListKeysInKeyboard2[ListKeysInKeyboard2.IndexOf(tmpSymbol) - 1];
            }
        }





        #region Events

        // обработка события нажатия клавиши на клавиатуре
        private void wind_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            
            if (!FlagPressShift)
            {
                if (e.Key == Key.LeftShift)
                {
                    // Эффект нажатия клавиши
                    changeViewShift(btnLShift, 0.9);

                    // Смена раскладки на Верхний регистр
                    changeViewToUpKeys();
                }

                if (e.Key == Key.RightShift)
                {
                    // Эффект нажатия клавиши
                    changeViewShift(btnRShift, 0.9);

                    // Смена раскладки на Верхний регистр
                    changeViewToUpKeys();
                }
            }

            if (e.Key == Key.Enter)
            {
                btnEnter.Focus();
            }

            FlagPressShift = true;
        }


        // обработка события отпускания клавиши на клавиатуре
        private void wind_PreviewKeyUp(object sender, KeyEventArgs e)
        {

            var focs = Keyboard.FocusedElement;
            Button tt = new Button();
            //if (sender is Button)
            //{
                if (!FlagPressShift)
                    tt = e.Source as Button;
            //}

            
            if (e.Key == Key.Tab)
            {
                txtBlInputText.Text += "    ";
            }

            if (e.Key == Key.Space)
            {
                txtBlInputText.Text += " ";
            }

            if (e.Key == Key.Enter)
            {
                btnEnter.Focusable = true;
                changeViewShift(btnEnter, 0.9);
                if (txtBlShowText.Text.Length <= txtBlInputText.Text.Length & txtBlShowText.Text.Length != 0)
                {
                    btnStop.Focus();
                    typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnStop, new object[] { false });
                    return;
                }
                System.Threading.Thread.Sleep(3);
                //typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnEnter, new object[] { true });
                ButtonAutomationPeer peer = new ButtonAutomationPeer(btnEnter);
                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();

            }


            // //нажата клавиша LeftShift
            if (e.Key == Key.LeftShift)
            {
                // Эффект отжатой клавиши
                changeViewShift(btnLShift, 0.9);

                // Смена раскладки на нижний регистр
                changeViewToLoKeys();
            }


            //нажата клавиша RightShift
            if (e.Key == Key.RightShift)
            {
                // Эффект отжатой клавиши
                changeViewShift(btnRShift, 1.0);

                // Смена раскладки на нижний регистр
                changeViewToLoKeys();
            }


                if (tt.Content != null)
                    if (tt.Content.ToString() != "Tab" && tt.Content.ToString() != "Caps Lock" &&
                        tt.Content.ToString() != "Ctrl" && tt.Content.ToString() != "Alt" &&
                        tt.Content.ToString() != "Backspace" && tt.Content.ToString() != "Enter" &&
                        tt.Content.ToString() != "Win" && tt.Content.ToString() != "Space" && e.Key != Key.Enter)
                    {
                        txtBlInputText.Text += tt.Content.ToString();
                    }


            // запуск события KeyUp для клавиш клавиатуры
            #region row1
            if (e.Key == Key.OemTilde)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnTilda, new object[] { false });
            if (e.Key == Key.D1)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn1, new object[] { false });
            if (e.Key == Key.D2)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn2, new object[] { false });
            if (e.Key == Key.D3)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn3, new object[] { false });
            if (e.Key == Key.D4)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn4, new object[] { false });
            if (e.Key == Key.D5)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn5, new object[] { false });
            if (e.Key == Key.D6)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn6, new object[] { false });
            if (e.Key == Key.D7)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn7, new object[] { false });

            if (e.Key == Key.D8)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn8, new object[] { false });
            if (e.Key == Key.D9)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn9, new object[] { false });
            if (e.Key == Key.D0)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn0, new object[] { false });
            if (e.Key == Key.OemMinus)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnSubtract, new object[] { false });
            if (e.Key == Key.OemPlus)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnPlus, new object[] { false });
            //typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnBakSpace, new object[] { false });
            #endregion


            #region row2
            if (e.Key == Key.Tab)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnTab, new object[] { false });
            if (e.Key == Key.Q)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnQ, new object[] { false });
            if (e.Key == Key.W)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnWW, new object[] { false });
            if (e.Key == Key.E)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnE, new object[] { false });
            if (e.Key == Key.R)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnR, new object[] { false });
            if (e.Key == Key.T)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnT, new object[] { false });
            if (e.Key == Key.Y)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnY, new object[] { false });
            if (e.Key == Key.U)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnU, new object[] { false });
            if (e.Key == Key.I)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnI, new object[] { false });
            if (e.Key == Key.O)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnO, new object[] { false });
            if (e.Key == Key.P)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnP, new object[] { false });
            if (e.Key == Key.OemOpenBrackets)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnSqScobaL, new object[] { false });
            if (e.Key == Key.Oem6)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnSqScobaR, new object[] { false });
            if (e.Key == Key.Oem5)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnBSlew, new object[] { false });
            #endregion


            #region row3
            if (e.Key == Key.A)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnA, new object[] { false });
            if (e.Key == Key.S)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnS, new object[] { false });
            if (e.Key == Key.D)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnD, new object[] { false });
            if (e.Key == Key.F)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnF, new object[] { false });
            if (e.Key == Key.G)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnG, new object[] { false });
            if (e.Key == Key.H)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnH, new object[] { false });
            if (e.Key == Key.J)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnJ, new object[] { false });
            if (e.Key == Key.K)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnK, new object[] { false });
            if (e.Key == Key.L)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnL, new object[] { false });
            if (e.Key == Key.Oem1)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnPointComma, new object[] { false });
            if (e.Key == Key.OemQuotes)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnUpperComma, new object[] { false });
            //if (e.Key == Key.Return)
            //    typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnEnter, new object[] { false });
            #endregion


            #region row4
            if (e.Key == Key.Z)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnZ, new object[] { false });
            if (e.Key == Key.X)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnX, new object[] { false });
            if (e.Key == Key.C)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnC, new object[] { false });
            if (e.Key == Key.V)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnV, new object[] { false });
            if (e.Key == Key.B)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnB, new object[] { false });
            if (e.Key == Key.N)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnN, new object[] { false });
            if (e.Key == Key.M)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnM, new object[] { false });
            if (e.Key == Key.OemComma)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnComa, new object[] { false });
            if (e.Key == Key.OemPeriod)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnPoint, new object[] { false });
            if (e.Key == Key.OemQuestion)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnDevide, new object[] { false });
            #endregion


            #region row5
            if (e.Key == Key.Capital)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnCL, new object[] { false });
            if (e.Key == Key.LeftShift)
            {
                if(!FlagForShiftPlusKey)
                    typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnLShift, new object[] { false });
            }
                
            if (e.Key == Key.LeftCtrl)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnLCtrl, new object[] { false });
            if (e.Key == Key.LWin)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnLWin, new object[] { false });
            if (e.SystemKey == Key.LeftAlt)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnLAlt, new object[] { false });
            if (e.Key == Key.Space)
            {
                buttonSpace.Focusable = true;
                buttonSpace.Focus();
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(buttonSpace, new object[] { false });
            }
            if (e.SystemKey == Key.RightAlt)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnRAlt, new object[] { false });
            if (e.Key == Key.RWin)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnRWin, new object[] { false });
            if (e.Key == Key.RightCtrl)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnRCtrl, new object[] { false });
            if (e.Key == Key.RightShift)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnRShift, new object[] { false });
            if (e.Key == Key.Back)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnBakSpace, new object[] { false });
            #endregion

            FlagForShiftPlusKey = false;
            FlagPressShift = false;
        }


        private void wind_TextInput(object sender, TextCompositionEventArgs e)
        {
            Grid tempGrid = new Grid();
            KeyConverter convertKey = new KeyConverter();
            //string tempKey;
            for (int i = 0; i < gridForAllKeyboard.Children.Count; i++)
            {
                if (i == gridForAllKeyboard.Children.Count - 1)
                {
                    i = 0;
                    return;
                }
                tempGrid = gridForAllKeyboard.Children[i + 1] as Grid;
                for (int j = 0; j < tempGrid.Children.Count; j++)
                {
                    //tempKey = convertKey.ConvertToString(e.Text);
                    if (e.Text == tempGrid.Children[j].ToString().Substring(tempGrid.Children[j].ToString().Length - 1))
                    {
                        //ButtonAutomationPeer peer = new ButtonAutomationPeer(btnQ);
                        //IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        //invokeProv.Invoke();

                        //typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnQ, new object[] { true });


                        //((gridForAllKeyboard.Children[i + 1] as Grid).Children[j] as Button).Background = Brushes.White;
                        //System.Threading.Thread.Sleep(200);
                        //((gridForAllKeyboard.Children[i + 1] as Grid).Children[j] as Button).Background = Brushes.HotPink;
                        txtBlInputText.Text = e.Text;
                        MessageBox.Show($"{e.Text}", "TextInput");

                        return;
                    }
                }
            }
            flagPressButtun = true;
            //typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnQ, new object[] { false });
        }

        #endregion





        // Executed для клавиш клавиатуры

        #region  Keyboard Row 1 
        // 14_keys  (13 for input text)
        //_(` 1 2 3 4 5 6 7 8 9 0 - = Backspace)_
        private void CommandBinding_Executed_Tilda(object sender, ExecutedRoutedEventArgs e)
        {
            btnTilda.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnTilda, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnTilda, new object[] { true });
        }

        private void CommandBinding_Executed_Number1(object sender, ExecutedRoutedEventArgs e)
        {
            btn1.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn1, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn1, new object[] { true });
        }

        private void CommandBinding_Executed_Number2(object sender, ExecutedRoutedEventArgs e)
        {
            btn2.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn2, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn2, new object[] { true });
        }

        private void CommandBinding_Executed_Number3(object sender, ExecutedRoutedEventArgs e)
        {
            btn3.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn3, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn3, new object[] { true });
        }

        private void CommandBinding_Executed_Number4(object sender, ExecutedRoutedEventArgs e)
        {
            btn4.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn4, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn4, new object[] { true });
        }

        private void CommandBinding_Executed_Number5(object sender, ExecutedRoutedEventArgs e)
        {
            btn5.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn5, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn5, new object[] { true });
        }

        private void CommandBinding_Executed_Number6(object sender, ExecutedRoutedEventArgs e)
        {
            btn6.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn6, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn6, new object[] { true });
        }

        private void CommandBinding_Executed_Number7(object sender, ExecutedRoutedEventArgs e)
        {
            btn7.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn7, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn7, new object[] { true });
        }

        private void CommandBinding_Executed_Number8(object sender, ExecutedRoutedEventArgs e)
        {
            btn8.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn8, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn8, new object[] { true });
        }

        private void CommandBinding_Executed_Number9(object sender, ExecutedRoutedEventArgs e)
        {
            btn9.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn9, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn9, new object[] { true });
        }

        private void CommandBinding_Executed_Number0(object sender, ExecutedRoutedEventArgs e)
        {
            btn0.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn0, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn0, new object[] { true });
        }

        private void CommandBinding_Executed_Minus(object sender, ExecutedRoutedEventArgs e)
        {
            btnSubtract.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnSubtract, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnSubtract, new object[] { true });
        }

        private void CommandBinding_Executed_Plus(object sender, ExecutedRoutedEventArgs e)
        {
            btnPlus.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnPlus, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnPlus, new object[] { true });
        }

        //private void CommandBinding_Executed_Backspace(object sender, ExecutedRoutedEventArgs e)
        //{
        //    btnBakSpace.Focus();

        //    if (!flagPressButtun)
        //    {
        //        typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnBakSpace, new object[] { false });
        //    }

        //    typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnBakSpace, new object[] { true });
        //}



        // Keyboard Row 2 (14_keys-(14 for input text))
        //_(Tab q w e r t y u i o p [ ] \)_
        #endregion


        #region  Keyboard Row 2 
        private void CommandBinding_Executed_Tab(object sender, ExecutedRoutedEventArgs e)
        {
            btnTab.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnTab, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnTab, new object[] { true });
        }

        private void CommandBinding_Executed_Q(object sender, ExecutedRoutedEventArgs e)
        {
            changeViewShift(btnEnter, 1.0);
            btnQ.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnQ, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnQ, new object[] { true });
        }

        private void CommandBinding_Executed_W(object sender, ExecutedRoutedEventArgs e)
        {
            btnWW.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnWW, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnWW, new object[] { true });
        }

        private void CommandBinding_Executed_E(object sender, ExecutedRoutedEventArgs e)
        {
            btnE.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnE, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnE, new object[] { true });
        }

        private void CommandBinding_Executed_R(object sender, ExecutedRoutedEventArgs e)
        {
            btnR.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnR, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnR, new object[] { true });
        }

        private void CommandBinding_Executed_T(object sender, ExecutedRoutedEventArgs e)
        {
            btnT.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnT, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnT, new object[] { true });
        }

        private void CommandBinding_Executed_Y(object sender, ExecutedRoutedEventArgs e)
        {
            btnY.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnY, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnY, new object[] { true });
        }

        private void CommandBinding_Executed_U(object sender, ExecutedRoutedEventArgs e)
        {
            btnU.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnU, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnU, new object[] { true });
        }

        private void CommandBinding_Executed_I(object sender, ExecutedRoutedEventArgs e)
        {
            btnI.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnI, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnI, new object[] { true });
        }

        private void CommandBinding_Executed_O(object sender, ExecutedRoutedEventArgs e)
        {
            btnO.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnO, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnO, new object[] { true });
        }

        private void CommandBinding_Executed_P(object sender, ExecutedRoutedEventArgs e)
        {
            btnP.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnP, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnP, new object[] { true });
        }

        private void CommandBinding_Executed_SqueSkobaL(object sender, ExecutedRoutedEventArgs e)
        {
            btnSqScobaL.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnSqScobaL, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnSqScobaL, new object[] { true });
        }

        private void CommandBinding_Executed_SqueSkobaR(object sender, ExecutedRoutedEventArgs e)
        {
            btnSqScobaR.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnSqScobaR, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnSqScobaR, new object[] { true });
        }

        private void CommandBinding_Executed_BackSlew(object sender, ExecutedRoutedEventArgs e)
        {
            btnBSlew.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnBSlew, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnBSlew, new object[] { true });
        }
        #endregion


        #region Keyboard Row 3
        // 13_keys  (11 for input text)
        // _(CapsLock a s d f g h j k l ; ' Enter)_ 
        private void CommandBinding_Executed_A(object sender, ExecutedRoutedEventArgs e)
        {
            btnA.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnA, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnA, new object[] { true });
        }

        private void CommandBinding_Executed_S(object sender, ExecutedRoutedEventArgs e)
        {
            btnS.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnS, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnS, new object[] { true });
        }

        private void CommandBinding_Executed_D(object sender, ExecutedRoutedEventArgs e)
        {
            btnD.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnD, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnD, new object[] { true });
        }

        private void CommandBinding_Executed_F(object sender, ExecutedRoutedEventArgs e)
        {
            btnF.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnF, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnF, new object[] { true });
        }

        private void CommandBinding_Executed_G(object sender, ExecutedRoutedEventArgs e)
        {
            btnG.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnG, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnG, new object[] { true });
        }

        private void CommandBinding_Executed_H(object sender, ExecutedRoutedEventArgs e)
        {
            btnH.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnH, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnH, new object[] { true });
        }

        private void CommandBinding_Executed_J(object sender, ExecutedRoutedEventArgs e)
        {
            btnJ.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnJ, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnJ, new object[] { true });
        }
                
        private void CommandBinding_Executed_K(object sender, ExecutedRoutedEventArgs e)
        {
            btnK.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnK, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnK, new object[] { true });
        }

        private void CommandBinding_Executed_L(object sender, ExecutedRoutedEventArgs e)
        {
            btnL.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnL, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnL, new object[] { true });
        }

        private void CommandBinding_Executed_PointComma(object sender, ExecutedRoutedEventArgs e)
        {
            btnPointComma.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnPointComma, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnPointComma, new object[] { true });
        }

        private void CommandBinding_Executed_UpperRegComma(object sender, ExecutedRoutedEventArgs e)
        {
            btnUpperComma.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnUpperComma, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnUpperComma, new object[] { true });
        }

        private void CommandBinding_Executed_Enter(object sender, ExecutedRoutedEventArgs e)
        {
            btnEnter.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnEnter, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnEnter, new object[] { true });

        }
        #endregion


        #region  Keyboard Row 4
        // 12_keys  (10 for input text)
        // _(LeftShift z x c v b n m , . / RightShift)_
        private void CommandBinding_Executed_Z(object sender, ExecutedRoutedEventArgs e)
        {
            btnZ.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnZ, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnZ, new object[] { true });
        }

        private void CommandBinding_Executed_X(object sender, ExecutedRoutedEventArgs e)
        {
            btnX.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnX, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnX, new object[] { true });
        }

        private void CommandBinding_Executed_C(object sender, ExecutedRoutedEventArgs e)
        {
            btnC.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnC, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnC, new object[] { true });
        }

        private void CommandBinding_Executed_V(object sender, ExecutedRoutedEventArgs e)
        {
            btnV.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnV, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnV, new object[] { true });
        }

        private void CommandBinding_Executed_B(object sender, ExecutedRoutedEventArgs e)
        {
            btnB.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnB, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnB, new object[] { true });
        }

        private void CommandBinding_Executed_N(object sender, ExecutedRoutedEventArgs e)
        {
            btnN.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnN, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnN, new object[] { true });
        }

        private void CommandBinding_Executed_M(object sender, ExecutedRoutedEventArgs e)
        {
            btnM.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnM, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnM, new object[] { true });
        }

        private void CommandBinding_Executed_Comma(object sender, ExecutedRoutedEventArgs e)
        {
            btnComa.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnComa, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnComa, new object[] { true });
        }

        private void CommandBinding_Executed_Point(object sender, ExecutedRoutedEventArgs e)
        {
            btnPoint.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnPoint, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnPoint, new object[] { true });
        }

        private void CommandBinding_Executed_Devide(object sender, ExecutedRoutedEventArgs e)
        {
            btnDevide.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnDevide, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnDevide, new object[] { true });
        }
        #endregion


        #region  Keyboard Row 5
        // 7_keys   (1 for input text)
        // _(LeftCtrl LeftWin LeftAlt Space RightAlt RightWin RightCtrl)_

        private void CommandBinding_Executed_CL(object sender, ExecutedRoutedEventArgs e)
        {
            btnCL.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnCL, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnCL, new object[] { true });


            if (FlagForUpperSymbol)
            {
                string tmpSymbol = null;
                for (int i = 0; i < ListButtonsWithKeys.Count; i++)
                {
                    tmpSymbol = ListButtonsWithKeys[i].Content.ToString();
                    ListButtonsWithKeys[i].Content = ListKeysInKeyboard2[ListKeysInKeyboard2.IndexOf(tmpSymbol) - 1];
                }
                FlagForUpperSymbol = false;
                return;
            }

            if (!FlagForUpperSymbol)
            {
                string tmpSymbol = null;
                for (int i = 0; i < ListButtonsWithKeys.Count; i++)
                {
                    tmpSymbol = ListButtonsWithKeys[i].Content.ToString();
                    ListButtonsWithKeys[i].Content = ListKeysInKeyboard2[ListKeysInKeyboard2.IndexOf(tmpSymbol) + 1];
                }
                FlagForUpperSymbol = true;
                return;
            }
        }

        private void CommandBinding_Executed_LShift(object sender, ExecutedRoutedEventArgs e)
        {
            //btnQ.RenderTransformOrigin = new Point(0.5, 0.5);
            //ScaleTransform myScaleTransform = new ScaleTransform();
            //myScaleTransform.ScaleY = 0.9;
            //myScaleTransform.ScaleX = 0.9;
            //btnQ.RenderTransform = myScaleTransform;

            

            btnLShift.Focus();

            if (flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnLShift, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnLShift, new object[] { true });

            

            //string tmpSymbol = null;

            //if (!FlagForShift)
            //    for (int i = 0; i < ListButtonsWithKeys.Count; i++)
            //    {
            //        tmpSymbol = ListButtonsWithKeys[i].Content.ToString();
            //        ListButtonsWithKeys[i].Content = ListKeysInKeyboard2[ListKeysInKeyboard2.IndexOf(tmpSymbol) + 1];
            //    }
            //FlagForShift = true;
            //if (btnLShift.IsKeyboardFocused)
            //    Keyboard.ClearFocus();
        }

        private void CommandBinding_Executed_LCtrl(object sender, ExecutedRoutedEventArgs e)
        {
            btnLCtrl.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnLCtrl, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnLCtrl, new object[] { true });
        }

        private void CommandBinding_Executed_LWin(object sender, ExecutedRoutedEventArgs e)
        {
            btnLWin.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnLWin, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnLWin, new object[] { true });
        }

        private void CommandBinding_Executed_LAlt(object sender, ExecutedRoutedEventArgs e)
        {
            btnLAlt.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnLAlt, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnLAlt, new object[] { true });
        }

        //private void CommandBinding_Executed_Space(object sender, ExecutedRoutedEventArgs e)
        //{
        //    buttonSpace.Focus();

        //    if (!flagPressButtun)
        //    {
        //        typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(buttonSpace, new object[] { false });
        //    }

        //    typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(buttonSpace, new object[] { true });
        //}

        private void CommandBinding_Executed_RAlt(object sender, ExecutedRoutedEventArgs e)
        {
            btnRAlt.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnRAlt, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnRAlt, new object[] { true });
        }

        private void CommandBinding_Executed_RWin(object sender, ExecutedRoutedEventArgs e)
        {
            btnRWin.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnRWin, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnRWin, new object[] { true });
        }

        private void CommandBinding_Executed_RCtrl(object sender, ExecutedRoutedEventArgs e)
        {
            btnRCtrl.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnRCtrl, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnRCtrl, new object[] { true });
        }

        private void CommandBinding_Executed_RShift(object sender, ExecutedRoutedEventArgs e)
        {
            btnRShift.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnRShift, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnRShift, new object[] { true });
        }

        private void CommandBinding_Executed_SpaseBtn(object sender, ExecutedRoutedEventArgs e)
        {
            buttonSpace.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(buttonSpace, new object[] { false });
            }
            buttonSpace.Focus();
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(buttonSpace, new object[] { true });
            buttonSpace.Focus();
        }

        private void CommandBinding_Executed_Backspace(object sender, ExecutedRoutedEventArgs e)
        {
            btnBakSpace.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnBakSpace, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnBakSpace, new object[] { true });
        }
        #endregion




        private void btnStart_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!btnStart.IsEnabled)
            {
                btnStart.Background = Brushes.Gray;
                btnStart.BorderBrush = Brushes.Gray;
            }

            if (btnStart.IsEnabled)
            {
                btnStart.Background = Brushes.LightGray;
                btnStart.BorderBrush = Brushes.Black;
            }
        }

        private void btnStop_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!btnStop.IsEnabled)
            {
                btnStop.Background = Brushes.Gray;
                btnStop.BorderBrush = Brushes.Gray;
            }

            if (btnStop.IsEnabled)
            {
                btnStop.Background = Brushes.LightGray;
                btnStop.BorderBrush = Brushes.Black;
            }
        }

        private void chBoxUpper_Checked(object sender, RoutedEventArgs e)
        {
            FlagCaseSensitive = true;
        }

        private void chBoxUpper_Unchecked(object sender, RoutedEventArgs e)
        {
            FlagCaseSensitive = false;
        }




        private void ShiftPlusKey()
        {
            FlagForShiftPlusKey = true;
            if (!FlagForRightShift)
            {
                btnLShift.RenderTransformOrigin = new Point(0.5, 0.5);
                ScaleTransform myScaleTransform = new ScaleTransform();
                myScaleTransform.ScaleY = 0.9;
                myScaleTransform.ScaleX = 0.9;
                btnLShift.RenderTransform = myScaleTransform;
            }

            if (FlagForRightShift)
            {
                btnRShift.RenderTransformOrigin = new Point(0.5, 0.5);
                ScaleTransform myScaleTransform = new ScaleTransform();
                myScaleTransform.ScaleY = 0.9;
                myScaleTransform.ScaleX = 0.9;
                btnRShift.RenderTransform = myScaleTransform;
            }
        }

        private void PressButtonWithShift(Button tempBtn)
        {
            tempBtn.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(tempBtn, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(tempBtn, new object[] { true });
        }




        private void CommandBinding_Executed_ShiftTilda(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnTilda);
        }

        private void CommandBinding_Executed_Shift1(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btn1);
        }

        private void CommandBinding_Executed_Shift2(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btn2);
        }

        private void CommandBinding_Executed_Shift3(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btn3);
        }

        private void CommandBinding_Executed_Shift4(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btn4);
        }

        private void CommandBinding_Executed_Shift5(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btn5);
        }

        private void CommandBinding_Executed_Shift6(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btn6);
        }

        private void CommandBinding_Executed_Shift7(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btn7);
        }

        private void CommandBinding_Executed_Shift8(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btn8);
        }

        private void CommandBinding_Executed_Shift9(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btn9);
        }

        private void CommandBinding_Executed_Shift0(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btn0);
        }

        private void CommandBinding_Executed_ShiftMinus(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnSubtract);
        }

        private void CommandBinding_Executed_ShiftPlus(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnPlus);
        }

        private void CommandBinding_Executed_ShiftQ(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnQ);
        }

        private void CommandBinding_Executed_ShiftW(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnWW);
        }

        private void CommandBinding_Executed_ShiftE(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnE);
        }

        private void CommandBinding_Executed_ShiftR(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnR);
        }

        private void CommandBinding_Executed_ShiftT(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnT);
        }

        private void CommandBinding_Executed_ShiftY(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnY);
        }

        private void CommandBinding_Executed_ShiftU(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnU);
        }

        private void CommandBinding_Executed_ShiftI(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnI);
        }

        private void CommandBinding_Executed_ShiftO(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnO);
        }

        private void CommandBinding_Executed_ShiftP(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnP);
        }

        private void CommandBinding_Executed_ShiftSqSkobaL(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnSqScobaL);
        }

        private void CommandBinding_Executed_ShiftSqSkobaR(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnSqScobaR);
        }

        private void CommandBinding_Executed_ShiftBackSlesh(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnBSlew);
        }

        private void CommandBinding_Executed_ShiftA(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnA);
        }

        private void CommandBinding_Executed_ShiftS(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnS);
        }

        private void CommandBinding_Executed_ShiftD(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnD);
        }

        private void CommandBinding_Executed_ShiftF(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnF);
        }

        private void CommandBinding_Executed_ShiftG(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnG);
        }

        private void CommandBinding_Executed_ShiftH(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnH);
        }

        private void CommandBinding_Executed_ShiftJ(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnJ);
        }

        private void CommandBinding_Executed_ShiftK(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnK);
        }

        private void CommandBinding_Executed_ShiftL(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnL);
        }

        private void CommandBinding_Executed_ShiftPointComma(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnPointComma);
        }

        private void CommandBinding_Executed_ShiftUpComma(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnUpperComma);
        }

        private void CommandBinding_Executed_ShiftZ(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnZ);
        }

        private void CommandBinding_Executed_ShiftX(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnX);
        }

        private void CommandBinding_Executed_ShiftC(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnC);
        }

        private void CommandBinding_Executed_ShiftV(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnV);
        }

        private void CommandBinding_Executed_ShiftB(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnB);
        }

        private void CommandBinding_Executed_ShiftN(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnN);
        }

        private void CommandBinding_Executed_ShiftM(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnM);
        }

        private void CommandBinding_Executed_ShiftComma(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnComa);
        }

        private void CommandBinding_Executed_ShiftPoint(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnPoint);
        }

        private void CommandBinding_Executed_ShiftDevide(object sender, ExecutedRoutedEventArgs e)
        {
            ShiftPlusKey();
            PressButtonWithShift(btnDevide);
        }



        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            changeViewShift(btnEnter, 1.0);
            //typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnEnter, new object[] { true });
            //ButtonAutomationPeer peer = new ButtonAutomationPeer(btnEnter);
            //IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            //invokeProv.Invoke();
        }
    }
}
 