using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace myEnigmaMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {      
        private ringImport ringImport = new ringImport();
        private allChar allChar = new allChar();
        private int[][] rings = new int[][] { };
        private uint transUserInput = 0;
        private List<int> ring1Back = new List<int>();
        private List<int> ring2Back = new List<int>();
        private List<int> ring3Back = new List<int>();
        private int[] selectedRings = new int[3];
        private int[][] selRings = new int[3][];
        private int[] refRing = new int[] { };
        private List<char> allUpLet = new List<char>();
        private List<char> allLowLet = new List<char>();
        private List<char> allNum = new List<char>();
        private List<char> allSpecKeys = new List<char>();
        private List<int> ring1Temp = new List<int>();
        private List<int> ring2Temp = new List<int>();
        private List<int> ring3Temp = new List<int>();
        private int ring1RotCounter = 0;
        private int ring2RotCounter = 0;    
        private int ring3RotCounter = 0;
        private Ellipse[][] ellipses = new Ellipse[7][];
        private Label[][] labels = new Label[7][];
        private Grid elGrid = new Grid();

        public MainWindow()
        {
            InitializeComponent();
            filePathLbl.MinWidth = 500;

            ringCmbBx.IsEnabled = false;

            if (!ringCmbBx.IsEnabled) 
            {
                ringCmbBx1.IsEnabled = false;
                ringCmbBx2.IsEnabled = false;
            }

            if (filePathLbl.Content.ToString() == "") 
            {
                loadRingsBtn.IsEnabled = false;
            }

            lockSelectedRingsBtn.IsEnabled = false;

            inputTbx.IsEnabled = false;

            for (int i = 0; i < 92; i++)
            {
                ringOffCmbBx.Items.Add(i);
            }

            for (int i = 0; i < 92; i++)
            {
                ringOffCmbBx1.Items.Add(i);
            }

            for (int i = 0; i < 92; i++)
            {
                ringOffCmbBx2.Items.Add(i);
            }

            ringOffCmbBx.IsEnabled = false;
            ringOffCmbBx2.IsEnabled = false;
            ringOffCmbBx1.IsEnabled = false;
            lockSelectedRingsOffBtn.IsEnabled = false;

            ring1NumLbl.Content = " ";
            ring2NumLbl.Content = " ";
            ring3NumLbl.Content = " ";

            enEl();

            
        }

        private void importBtn_Click(object sender, RoutedEventArgs e)
        {
            string filePath = " ";

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Csv Files (*.csv)|*.csv";
            ofd.Title = "Searching for CSV files (Rings)";
            ofd.FileName = "Select a Csv File";

            if ((bool)ofd.ShowDialog())
            {
               filePath = ofd.FileName;              
               filePathLbl.Content = filePath;
                loadRingsBtn.IsEnabled=true;

            }
            else
            {
                MessageBox.Show("Cancelled");
            }

        }

        private void loadRingsBtn_Click(object sender, RoutedEventArgs e)
        {
            string filePath = filePathLbl.Content.ToString();

            rings = ringImport.RingImport(filePath);

            if (rings.Length != 0)
            {
                ringCmbBx.IsEnabled = true;
            }
            else 
            {
                ringCmbBx.IsEnabled = false;
            }                  
            
            loadRingsBtn.IsEnabled = false;
            importBtn.IsEnabled = false;
         
        }

        private void ringCmbBx1_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {          

            for (int i = 1; i < rings.Length; i++)
            {
                ringCmbBx1.Items.Add(i);    
            }
           
          
        }

        private void ringCmbBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            ringCmbBx1.IsEnabled = true;

            selectedRings[0] = (int)ringCmbBx.SelectedItem;

            if (ringCmbBx1.SelectedIndex != -1 
                && ringCmbBx2.SelectedIndex != -1) 
            {
                if (ringCmbBx.SelectedItem.ToString() == ringCmbBx1.SelectedItem.ToString() ||
                ringCmbBx.SelectedItem.ToString() == ringCmbBx2.SelectedItem.ToString())
                {
                    lockSelectedRingsBtn.IsEnabled = false;
                }
                else
                {
                    lockSelectedRingsBtn.IsEnabled = true;
                }
            }
          
        }

        private void ringCmbBx1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ringCmbBx2.IsEnabled = true;
            selectedRings[1] = (int)ringCmbBx1.SelectedItem;

            if (ringCmbBx.SelectedIndex != -1
                && ringCmbBx2.SelectedIndex != -1)
            {
                if (ringCmbBx1.SelectedItem.ToString() == ringCmbBx.SelectedItem.ToString() ||
                ringCmbBx1.SelectedItem.ToString() == ringCmbBx2.SelectedItem.ToString())
                {
                    lockSelectedRingsBtn.IsEnabled = false;
                }
                else
                {
                    lockSelectedRingsBtn.IsEnabled = true;
                }
            }

        }

        private void ringCmbBx2_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            for (int i = 1; i < rings.Length; i++)
            {
                ringCmbBx2.Items.Add(i);
            }       
        }

        private void ringCmbBx_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            for (int i = 1; i < rings.Length; i++)
            {
                ringCmbBx.Items.Add(i);
            }  
        }

        private void ringCmbBx2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRings[2] = (int)ringCmbBx2.SelectedItem;

            if (ringCmbBx1.SelectedIndex != -1
                && ringCmbBx.SelectedIndex != -1)
            {
                if (ringCmbBx2.SelectedItem.ToString() == ringCmbBx.SelectedItem.ToString() ||
                ringCmbBx2.SelectedItem.ToString() == ringCmbBx1.SelectedItem.ToString())
                {
                    lockSelectedRingsBtn.IsEnabled = false;
                }
                else
                {
                    if (ringCmbBx2.SelectedIndex == -1)
                    {
                        lockSelectedRingsBtn.IsEnabled = false;
                    }
                    else
                    {
                        lockSelectedRingsBtn.IsEnabled = true;
                    }
                }
            }            
        }

        private void lockSelectedRingsBtn_Click(object sender, RoutedEventArgs e)
        {
            const string message = "Do you want to select these rings? You have to restart the application to change rings";
            const string caption = "Finalizing Selected Rings";
            var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.No)
            {
                MessageBox.Show("Cancelled");
            }
            else
            {
                
                selRings = new int[3][];
                refRing = rings[0];

                for (int i = 0; i < selectedRings.Length; i++ ) 
                {
                    selRings[i] = rings[selectedRings[i]];
                }

                ringOffCmbBx.IsEnabled = true;
                ringOffCmbBx1.IsEnabled = true;
                ringOffCmbBx2.IsEnabled = true;
                lockSelectedRingsOffBtn.IsEnabled = true;

                lockSelectedRingsBtn.Visibility = Visibility.Hidden;
              
            }

            ringCmbBx.IsEnabled = false;
            ringCmbBx1.IsEnabled = false;
            ringCmbBx2.IsEnabled = false;


        }

        private void inputTbx_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void inputTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            allChar.allCharInt(allUpLet, allLowLet, allNum, allSpecKeys);

            int tbxTextLength = inputTbx.Text.Length;
            char userInput = ' ';       

            if (tbxTextLength >= 1)
            {
                userInput = inputTbx.Text[tbxTextLength - 1];

                if (Keyboard.IsKeyDown(Key.Back))
                {
                    int length = EncryptTbx.Text.Length;
                    string temp = EncryptTbx.Text.Remove(length - 1, 1);
                    EncryptTbx.Text = temp;
                    backRotate();
                }
                else
                {
                    foreach (char el in allUpLet)
                    {
                        if (userInput == el)
                        {
                            transUserInput = (uint)el;
                            EncryptTbx.Text += (char)encryption();
                            rotate();
                            
                            break;
                        }
                    }
                    foreach (char el in allLowLet)
                    {
                        if (userInput == el)
                        {
                            transUserInput = (uint)el;
                            EncryptTbx.Text += (char)encryption();
                            rotate();
                            
                            break;
                        }
                    }
                    foreach (char el in allNum)
                    {
                        if (userInput == el)
                        {
                            transUserInput = (uint)el;
                            EncryptTbx.Text += (char)encryption();
                            rotate();
                            
                            break;
                        }
                    }
                    foreach (char el in allSpecKeys)
                    {
                        if (userInput == el)
                        {
                            transUserInput = (uint)el;
                            EncryptTbx.Text += (char)encryption();
                            rotate();
                            
                            break;
                        }
                    }
                }
            } 
            
           else if (Keyboard.IsKeyDown(Key.Back))
            {
                int length = EncryptTbx.Text.Length;
                string temp = EncryptTbx.Text.Remove(length - 1, 1);
                EncryptTbx.Text = temp;

                if (inputTbx.Text.Length < 1) 
                {
                    transUserInput = 0;                   
                }

                backRotate();
            }
            else if (Keyboard.IsKeyDown(Key.Space))
            {       
                rotate();
            }

            ring1NumLbl.Content = selRings[0][0];
            ring2NumLbl.Content = selRings[1][0];
            ring3NumLbl.Content = selRings[2][0];

            colorenEl();
            enColoRenEl();

            
        }

        private uint encryption() 
        {
            uint toEnRaw = transUserInput;
            int impIndex = 0;

            if (toEnRaw == 0) 
            {
                toEnRaw = 32;
            }

            for (int x = 0; x < refRing.Length; x++) 
            {
                if (refRing[x] == toEnRaw) 
                {
                    impIndex = x;
                }
            }

            toEnRaw = (uint)selRings[0][impIndex] ;

            for (int x = 0; x < refRing.Length; x++)
            {
                if (refRing[x] == toEnRaw)
                {
                    impIndex = x;
                }
            }

            toEnRaw = (uint)selRings[1][impIndex];

            for (int x = 0; x < refRing.Length; x++)
            {
                if (refRing[x] == toEnRaw)
                {
                    impIndex = x;
                }
            }

            toEnRaw = (uint)selRings[2][impIndex];

            for (int x = 0; x < refRing.Length; x++)
            {
                if (refRing[x] == toEnRaw)
                {
                    impIndex = x;
                }
            }

            toEnRaw = (uint)selRings[2][impIndex];

            for (int x = 0; x < refRing.Length; x++)
            {
                if (refRing[x] == toEnRaw)
                {
                    impIndex = x;
                }
            }

            toEnRaw = (uint)selRings[1][impIndex];

            for (int x = 0; x < refRing.Length; x++)
            {
                if (refRing[x] == toEnRaw)
                {
                    impIndex = x;
                }

            }

            toEnRaw = (uint)selRings[0][impIndex];

            return toEnRaw;
        }

        private void rotate() 
        {
            
            int hol = 0;
            int hol2 = 0;
            int hol3 = 0;

            ring1Temp = new List<int>(selRings[0]);
            hol = ring1Temp[0];
            ring1Temp.RemoveAt(0);
            ring1Temp.Add(hol);

            for (int i = 0; i < ring1Temp.Count; i++) 
            {
                selRings[0][i] = ring1Temp[i];
            }
            ring1RotCounter += 1;           
                
            if (ring1RotCounter > 91) 
            {
                
                ring2Temp = new List<int>(selRings[1]);
                hol2 = ring2Temp[0];
                ring2Temp.RemoveAt(0);
                ring2Temp.Add(hol2);

                for (int i = 0; i < ring2Temp.Count; i++)
                {
                    selRings[1][i] = ring2Temp[i];
                }
                
                ring1RotCounter = 0;
                ring2RotCounter += 1;

                if (ring2RotCounter > 91) 
                {
                    
                    ring3Temp = new List<int>(selRings[2]);
                    hol3 = ring3Temp[0];
                    ring3Temp.RemoveAt(0);
                    ring3Temp.Add(hol3);

                    for (int i = 0; i < ring3Temp.Count; i++)
                    {
                        selRings[2][i] = ring3Temp[i];
                    }
                    ring2RotCounter = 0;
                    ring3RotCounter += 1;

                    if (ring3RotCounter > 91) 
                    {
                        ring3RotCounter = 0;
                    }
                }
            }
        }

        private void backRotate() 
        {
            int hol1 = 0;
            int hol2 = 0;
            int hol3 = 0;           

            ring1Back = new List<int>(selRings[0]);
            ring2Back = new List<int>(selRings[1]);
            ring3Back = new List<int>(selRings[2]);

                List<int> temp = new List<int>();
                List<int> temp1 = new List<int>();
                List<int> temp2 = new List<int>();

            hol1 = ring1Back.ElementAt(91);
            ring1Back.Remove(hol1);
           
            for (int i = 0; i < selRings[0].Length - 1; i++) 
            {
                int tem = ring1Back.ElementAt(i);
                temp.Add(tem);               
            }

            for (int i = 0; i < temp.Count; i++) 
            {
                ring1Back.Remove(temp[i]);
            }

            ring1Back.Add(hol1);
            for (int i = 0; i < temp.Count; i++)
            {
                ring1Back.Add(temp.ElementAt(i));                
            }

            for (int i = 0; i < ring1Back.Count; i++) 
            {
                selRings[0][i] = ring1Back.ElementAt(i);
            }

            ring1RotCounter -= 1;

            if (ring1RotCounter < 0) 
            {
                hol2 = ring2Back.ElementAt(91);
                ring2Back.Remove(hol2);

                for (int i = 0; i < selRings[1].Length - 1; i++)
                {
                    int tem = ring2Back.ElementAt(i);
                    temp1.Add(tem);
                }

                for (int i = 0; i < temp1.Count; i++)
                {
                    ring2Back.Remove(temp1[i]);
                }

                ring2Back.Add(hol2);
                for (int i = 0; i < temp1.Count; i++)
                {
                    ring2Back.Add(temp1.ElementAt(i));
                }

                for (int i = 0; i < ring2Back.Count; i++)
                {
                    selRings[1][i] = ring2Back.ElementAt(i);
                }

                ring2RotCounter -= 1;
                ring1RotCounter = 91;

                if (ring2RotCounter < 0) 
                {
                    hol3 = ring3Back.ElementAt(91);
                    ring3Back.Remove(hol3);

                    for (int i = 0; i < selRings[2].Length - 1; i++)
                    {
                        int tem = ring3Back.ElementAt(i);
                        temp2.Add(tem);
                    }

                    for (int i = 0; i < temp2.Count; i++)
                    {
                        ring3Back.Remove(temp2[i]);
                    }

                    ring3Back.Add(hol3);
                    for (int i = 0; i < temp2.Count; i++)
                    {
                        ring3Back.Add(temp2.ElementAt(i));
                    }

                    for (int i = 0; i < ring3Back.Count; i++)
                    {
                        selRings[2][i] = ring3Back.ElementAt(i);
                    }

                    ring2RotCounter = 91;
                    ring3RotCounter -= 1;

                    if (ring3RotCounter < 0) 
                    {
                        ring3RotCounter = 91;
                    }

                }
            }
          
        }

        private void offsetRing1(int offsetVal) 
        {
            int hol = 0;

            ring1Temp = new List<int>(selRings[0]);

            for (int i = 0; i < offsetVal; i++)
            {
                hol = ring1Temp[0];
                ring1Temp.RemoveAt(0);
                ring1Temp.Add(hol);

                for (int x = 0; x < ring1Temp.Count; x++)
                {
                    selRings[0][x] = ring1Temp[x];
                }
                ring1RotCounter += 1;
            }
        }

        private void offsetRing2(int offsetVal)
        {
            int hol2 = 0;

            ring2Temp = new List<int>(selRings[1]);

            for (int i = 0; i < offsetVal; i++)
            {
                
                hol2 = ring2Temp[0];
                ring2Temp.RemoveAt(0);
                ring2Temp.Add(hol2);

                for (int x = 0; x < ring2Temp.Count; x++)
                {
                    selRings[1][x] = ring2Temp[x];
                }

                ring2RotCounter += 1;
            }
        }

        private void offsetRing3(int offsetVal)
        {
            int hol3 = 0;

            ring3Temp = new List<int>(selRings[2]);

            for (int i = 0; i < offsetVal; i++)
            {           
                hol3 = ring3Temp[0];
                ring3Temp.RemoveAt(0);
                ring3Temp.Add(hol3);

                for (int x = 0; x < ring3Temp.Count; x++)
                {
                    selRings[2][x] = ring3Temp[x];
                }

                ring3RotCounter++;
            }
        }
        
        private void lockSelectedRingsOffBtn_Click(object sender, RoutedEventArgs e)
        {
            int ringOffset1 = 0;
            int ringOffset2 = 0;
            int ringOffset3 = 0;
          
            ringOffset1 = int.Parse(ringOffCmbBx.SelectedItem.ToString());
            ringOffset2 = int.Parse(ringOffCmbBx1.SelectedItem.ToString());
            ringOffset3 = int.Parse(ringOffCmbBx2.SelectedItem.ToString());

            offsetRing1(ringOffset1);
            offsetRing2(ringOffset2);
            offsetRing3(ringOffset3);

            lockSelectedRingsBtn.IsEnabled = false;
            lockSelectedRingsOffBtn.IsEnabled = false;
            inputTbx.IsEnabled = true;

            ring1NumLbl.Content = selRings[0][0];
            ring2NumLbl.Content = selRings[1][0];
            ring3NumLbl.Content = selRings[2][0];

            MessageBox.Show(selRings[0][0].ToString());

        }

        private void enEl()
        {
            allChar.allCharInt(allUpLet, allLowLet, allNum, allSpecKeys);

            int v = 10;
            int h = 10;
            int tempInd = 0;
            List<char> temp = new List<char>();
         
            elGrid.VerticalAlignment = VerticalAlignment.Top;
            elGrid.HorizontalAlignment = HorizontalAlignment.Left;
            elGrid.Width = 700;
            elGrid.Height = 700;

            elGrid.Margin = new Thickness(800,20,0,0);
            enigmaMachineGrid.Children.Add(elGrid);

            ellipses[0] = new Ellipse[13];
            ellipses[1] = new Ellipse[13];
            ellipses[2] = new Ellipse[13];
            ellipses[3] = new Ellipse[13];
            ellipses[4] = new Ellipse[10];
            ellipses[5] = new Ellipse[15];
            ellipses[6] = new Ellipse[15];

            labels[0] = new Label[13];
            labels[1] = new Label[13];
            labels[2] = new Label[13];
            labels[3] = new Label[13];
            labels[4] = new Label[10];
            labels[5] = new Label[15];
            labels[6] = new Label[15];

            for (int x = 0; x < ellipses.Length; x++) 
            {
                for (int y = 0; y < ellipses[x].Length; y++) 
                {
                    Ellipse newEl = new Ellipse();

                    newEl.VerticalAlignment = VerticalAlignment.Top;
                    newEl.HorizontalAlignment = HorizontalAlignment.Left;

                    newEl.Width = 40;
                    newEl.Height = 40;

                    newEl.Margin = new Thickness( h, v , 0, 0);
                    newEl.Stroke =new SolidColorBrush(Colors.Black);
                    newEl.Fill = new SolidColorBrush(Colors.Transparent);

                    ellipses[x][y] = newEl;
                    ellipses[x][y].Name = "el";
                    elGrid.Children.Add(ellipses[x][y]);

                    h += 10 + (int)newEl.Width;                   
                }
                h = 10;
                v += 60;
            }

            v = 0;
            for (int x = 0; x < labels.Length; x++)
            {
                if (x == 0)
                {
                    temp = allUpLet;
                    tempInd = 0;
                }
                else if (x == 2)
                {
                    temp = allLowLet;
                    tempInd = 0;
                }
                else if (x == 4)
                {
                    temp = allNum;
                    tempInd = 0;
                }
                else if (x == 5) 
                {
                    temp = allSpecKeys;
                    tempInd = 0;
                }
                for (int y = 0; y < labels[x].Length; y++)
                {
                    Label newLa = new Label();

                    newLa.VerticalAlignment = VerticalAlignment.Top;
                    newLa.HorizontalAlignment = HorizontalAlignment.Left;
                    newLa.Width = 40;
                    newLa.Height = 40;
                    newLa.Margin = new Thickness(h, v, 0, 0);
                    newLa.Padding =new Thickness(17.5, 40/2, 0, 0);
                    

                    labels[x][y] = newLa;
                    labels[x][y].Content = temp.ElementAt(tempInd);
                    elGrid.Children.Add(labels[x][y]);

                    tempInd++;

                    h += 10 + (int)newLa.Width;           
                }
                h = 10;
                v += 60;
            }



        }

        private void colorenEl() 
        {
            resColorEnEl();

            allChar.allCharInt(allUpLet, allLowLet, allNum, allSpecKeys);

            uint userInput = 0;
            int impInd = 0;
            int tbxTextLength = inputTbx.Text.Length;

            if (Keyboard.IsKeyDown(Key.Back))
            {
                if (tbxTextLength < 1)
                {
                    userInput = 0;
                }
                else 
                {
                    userInput = (uint)inputTbx.Text[tbxTextLength - 1];
                }
            }
            else
            {
                userInput = (uint)inputTbx.Text[tbxTextLength - 1];
            }

            if (userInput >= allUpLet.ElementAt(0) && userInput <= allUpLet.ElementAt(25))
            {
                for (int x = 0; x < allUpLet.Count; x++)
                {
                    if ((char)userInput == (char)allUpLet[x])
                    {
                        impInd = x;
                        break;
                    }
                }

                if (impInd <= 12)
                {
                    for (int x = 0; x < ellipses[0].Length; x++)
                    {
                        if ((char)userInput == (char)labels[0][x].Content)
                        {
                            ellipses[0][x].Fill = new SolidColorBrush(Colors.Purple);
                        }
                    }
                }
                else
                {
                    for (int x = 0; x < ellipses[1].Length; x++)
                    {
                        if ((char)userInput == (char)labels[1][x].Content)
                        {
                            ellipses[1][x].Fill = new SolidColorBrush(Colors.Purple);
                        }
                    }
                }
            }
            else if (userInput >= allLowLet.ElementAt(0) && userInput <= allLowLet.ElementAt(25))
            {

                for (int x = 0; x < allLowLet.Count; x++)
                {
                    if ((char)userInput == (char)allLowLet[x])
                    {
                        impInd = x;
                        break;
                    }
                }

                if (impInd <= 12)
                {
                    for (int x = 0; x < ellipses[2].Length; x++)
                    {
                        if ((char)userInput == (char)labels[2][x].Content)
                        {
                            ellipses[2][x].Fill = new SolidColorBrush(Colors.Purple);
                        }
                    }
                }
                else
                {
                    for (int x = 0; x < ellipses[3].Length; x++)
                    {
                        if ((char)userInput == (char)labels[3][x].Content)
                        {
                            ellipses[3][x].Fill = new SolidColorBrush(Colors.Purple);
                        }
                    }
                }
            }
            else if (userInput >= allNum.ElementAt(0) && userInput <= allNum.ElementAt(9))
            {
                for (int x = 0; x < allNum.Count; x++)
                {
                    if ((char)userInput == (char)allNum[x])
                    {
                        impInd = x;
                        break;
                    }
                }

                ellipses[4][impInd].Fill = new SolidColorBrush(Colors.Purple);

            }
            else if (userInput >= allSpecKeys.ElementAt(0) && userInput <= allSpecKeys.ElementAt(29)) 
            {
                for (int x = 0; x < allSpecKeys.Count; x++)
                {
                    if ((char)userInput == (char)allSpecKeys[x])
                    {
                        impInd = x;
                        break;
                    }
                }

                if (impInd <= 14)
                {
                    for (int x = 0; x < ellipses[5].Length; x++)
                    {
                        if ((char)userInput == (char)labels[5][x].Content)
                        {
                            ellipses[5][x].Fill = new SolidColorBrush(Colors.Purple);
                        }
                    }
                }
                else
                {
                    for (int x = 0; x < ellipses[6].Length; x++)
                    {
                        if ((char)userInput == (char)labels[6][x].Content)
                        {
                            ellipses[6][x].Fill = new SolidColorBrush(Colors.Purple);
                        }
                    }
                }
            }
            else
            {
                for (int x = 0; x < ellipses.Length; x++)
                {
                    for (int y = 0; y < ellipses[x].Length; y++)
                    {
                        ellipses[x][y].Fill = new SolidColorBrush(Colors.Transparent);
                    }
                }
            }
        }

        private void enColoRenEl()
        {       
            allChar.allCharInt(allUpLet, allLowLet, allNum, allSpecKeys);

            uint userInput = 0;
            int impInd = 0;
            int tbxTextLength = EncryptTbx.Text.Length;

            if (Keyboard.IsKeyDown(Key.Back))
            {            
                if (tbxTextLength < 1)
                {
                    userInput = 0;
                }
                else
                {
                    userInput = (uint)EncryptTbx.Text[tbxTextLength - 1];
                }
            }
            else
            {
                userInput = (uint)EncryptTbx.Text[tbxTextLength - 1];
            }

            if (userInput >= allUpLet.ElementAt(0) && userInput <= allUpLet.ElementAt(25))
            {
                for (int x = 0; x < allUpLet.Count; x++)
                {
                    if ((char)userInput == (char)allUpLet[x])
                    {
                        impInd = x;
                        break;
                    }
                }

                if (impInd <= 12)
                {
                    for (int x = 0; x < ellipses[0].Length; x++) 
                    {
                        if ((char)userInput == (char)labels[0][x].Content) 
                        {
                           ellipses[0][x].Fill = new SolidColorBrush(Colors.Blue);
                        }
                    }
                }
                else
                {
                    for (int x = 0; x < ellipses[1].Length; x++)
                    {
                        if ((char)userInput == (char)labels[1][x].Content)
                        {
                            ellipses[1][x].Fill = new SolidColorBrush(Colors.Blue);
                        }
                    }
                }
            }
            else if (userInput >= allLowLet.ElementAt(0) && userInput <= allLowLet.ElementAt(25))
            {

                for (int x = 0; x < allLowLet.Count; x++)
                {
                    if ((char)userInput == (char)allLowLet[x])
                    {
                        impInd = x;
                        break;
                    }
                }

                if (impInd <= 12)
                {
                    for (int x = 0; x < ellipses[2].Length; x++)
                    {
                        if ((char)userInput == (char)labels[2][x].Content)
                        {
                            ellipses[2][x].Fill = new SolidColorBrush(Colors.Blue);
                        }
                    }
                }
                else
                {
                    for (int x = 0; x < ellipses[3].Length; x++)
                    {
                        if ((char)userInput == (char)labels[3][x].Content)
                        {
                            ellipses[3][x].Fill = new SolidColorBrush(Colors.Blue);
                        }
                    }
                }
            }
            else if (userInput >= allNum.ElementAt(0) && userInput <= allNum.ElementAt(9))
            {
                for (int x = 0; x < allNum.Count; x++)
                {
                    if ((char)userInput == (char)allNum[x])
                    {
                        impInd = x;
                        break;
                    }
                }

                ellipses[4][impInd].Fill = new SolidColorBrush(Colors.Blue);

            }
            else if (userInput >= allSpecKeys.ElementAt(0) && userInput <= allSpecKeys.ElementAt(29))
            {
                for (int x = 0; x < allSpecKeys.Count; x++)
                {
                    if ((char)userInput == (char)allSpecKeys[x])
                    {
                        impInd = x;
                        break;
                    }
                }

                if (impInd <= 14)
                {
                    for (int x = 0; x < ellipses[5].Length; x++)
                    {
                        if ((char)userInput == (char)labels[5][x].Content)
                        {
                            ellipses[5][x].Fill = new SolidColorBrush(Colors.Blue);
                        }
                    }
                }
                else
                {
                    for (int x = 0; x < ellipses[6].Length; x++)
                    {
                        if ((char)userInput == (char)labels[6][x].Content)
                        {
                            ellipses[6][x].Fill = new SolidColorBrush(Colors.Blue);
                        }
                    }
                }
            }
            else
            {
                for (int x = 0; x < ellipses.Length; x++)
                {
                    for (int y = 0; y < ellipses[x].Length; y++)
                    {
                        ellipses[x][y].Fill = new SolidColorBrush(Colors.Transparent);
                    }
                }
            }
        }

        private void resColorEnEl()
        {

            for (int x = 0; x < ellipses.Length; x++)
            {
                for (int y = 0; y < ellipses[x].Length; y++)
                {
                    ellipses[x][y].Fill = new SolidColorBrush(Colors.Transparent);
                }
            }

        }
    }
}
