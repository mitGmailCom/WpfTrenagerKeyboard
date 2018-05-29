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

namespace WpfTrenagerKeyboard
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool flagPressButtun = false;
        public MainWindow()
        {
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
            //txtBlShowText.Height = firstRowKyeboard.ActualHeight/2;
            //txtBlInputText.Height = firstRowKyeboard.ActualHeight / 2;
        }



        #region Events

        private void wind_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            txtBlInputText.Text += e.Key.ToString();
            //MessageBox.Show($"{e.Key}", "PreviewKeyDown");

            //var text = $"{e.Key}";
            //var target = Keyboard.FocusedElement;
            //var routedEvent = TextCompositionManager.TextInputEvent;

            //target.RaiseEvent(
            //  new TextCompositionEventArgs(
            //    InputManager.Current.PrimaryKeyboardDevice,
            //    new TextComposition(InputManager.Current, target, text))
            //  { RoutedEvent = routedEvent }
            //);

            if (e.Key == Key.Q)
            {

            }

            flagPressButtun = true;
            //ButtonAutomationPeer peer = new ButtonAutomationPeer(btnQ);
            //IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            //invokeProv.Invoke();
        }


        private void wind_PreviewKeyUp(object sender, KeyEventArgs e)
        {

            Button tt = new Button();
            tt = e.Source as Button;
            txtBlInputText.Text += tt.Content.ToString();
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnQ, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnWW, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnE, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnR, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnT, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnY, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnU, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnI, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnO, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnP, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnSqScobaL, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnSqScobaR, new object[] { false });

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnA, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnS, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnD, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnF, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnG, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnH, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnJ, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnK, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnL, new object[] { false });

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnZ, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnX, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnC, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnV, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnB, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnN, new object[] { false });
            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnM, new object[] { false });
        }
                

        private void wind_TextInput(object sender, TextCompositionEventArgs e)
        {
            Grid tempGrid = new Grid();
            KeyConverter convertKey = new KeyConverter();
            string tempKey;
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





        

        //private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        //{


        //    //btnQ.ClickMode = ClickMode.Press;
        //    btnQ.Focus();
            
        //    if (flagPressButtun)
        //    {
        //        typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnQ, new object[] { true });
        //        //typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnQ, new object[] { false });
               
        //    }
        //    if(!flagPressButtun)
        //    {
                
        //        typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnQ, new object[] { false });
        //    }

        //    if (flagPressButtun)
        //    {
        //        flagPressButtun = false;
        //    }

        //    typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnQ, new object[] { true });
        //}





        private void CommandBinding_Executed_Q(object sender, ExecutedRoutedEventArgs e)
        {
            btnQ.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnQ, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnQ, new object[] { true });
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

        private void CommandBinding_Executed_W(object sender, ExecutedRoutedEventArgs e)
        {
            btnWW.Focus();

            if (!flagPressButtun)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnWW, new object[] { false });
            }

            typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btnWW, new object[] { true });
        }


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
    }
}
