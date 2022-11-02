using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace myEnigmaMachine
{
    internal class ringImport
    {
        private List<string> Lines = new List<string>();
        private List<string[]> PLines = new List<string[]>();
        private int[][] rings = new int[][] { };
        private bool affirmTrigger = true;
        private string errorMessage = string.Empty;

        public int[][] RingImport(string filePath) 
        {
          
            int[] dupRings = new int[] { };
            int exactRingCount = 0;

            rings = new int[][] { };
           
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line = " ";
                    int prevVal = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        Lines.Add(line);
                    }

                    string[][] readline = new string[Lines.Count][];
                    for (int x = 0; x < readline.Length; x++)
                    {
                        readline[x] = new string[1];
                        for (int y = 0; y < readline[x].Length; y++)
                        {
                            readline[x][y] = Lines[x];
                        }
                    }

                    for (int x = 0; x < readline.Length; x++)
                    {
                        for (int y = 0; y < readline[x].Length; y++)
                        {
                            string[] temp = readline[x][y].Split(',');
                            PLines.Add(temp);

                        }
                    }

                    for (int x = 0; x < PLines.Count; x++)
                    {
                        for (int y = 0; y < PLines[x].Length; y++)
                        {
                            if (x == 0 && y == 0)
                            {
                                rings = new int[int.Parse(PLines[x][y]) + 1][];
                                exactRingCount = int.Parse(PLines[x][y]) + 1;

                            }
                            else if (x == 0 && y == 1)
                            {
                                for (int rx = 0; rx < rings.Length; rx++)
                                {
                                    rings[rx] = new int[int.Parse(PLines[x][y])];
                                }
                            }
                        }
                    }

                    for (int rx = 0; rx < PLines[rx].Length; rx++)
                    {
                        for (int i = 2; i < PLines.Count; i++)
                        {
                            rings[rx][i - 2] = int.Parse(PLines[i][rx]);
                        }

                    }

                    for (int x = 0; x < rings.Length; x++) 
                    {                       
                            for (int y = 0; y < rings[x].Length; y++)
                            {

                                if (rings[x][y] == 0)
                                {                                   
                                    rings = new int[][] { };
                                    affirmTrigger = false;
                                    errorMessage = "Ring " + x + "is missing a character!";
                                    break;

                                }
                                else if (rings[x][y] == prevVal)
                                {                                
                                    rings = new int[][] { };
                                    affirmTrigger = false;
                                    errorMessage = "Ring " + x + "Has a duplicate character!";
                                    break;

                                }
                                else
                                {
                                    affirmTrigger = true;
                                    
                                }                             
                            }
                                                             
                        dupRings = rings[x];
                    }


                    if (affirmTrigger == true)
                    {
                        MessageBox.Show("Rings Successfully Read!");
                    }
                    else 
                    {
                        MessageBox.Show(errorMessage);
                        Environment.Exit(0);
                    }

                }        
            }
            catch (Exception ex) 
            {
              MessageBox.Show(ex.Message);
            }

            return rings;
        }
    }
}
