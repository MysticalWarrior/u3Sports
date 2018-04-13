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
using System.IO;

namespace u3Sports
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            System.Net.WebClient webClient = new System.Net.WebClient();
            webClient.OpenRead("http://www.cbc.ca/cmlink/rss-sports-nhl");
            StreamReader streamreader = new StreamReader(webClient.OpenRead("http://www.cbc.ca/cmlink/rss-sports-nhl"));
            StreamWriter streamWriter = new StreamWriter("Text.txt");
            String line = "";
            String[] stories = new string[20];
            int counter = 0;
            bool newline = false;

            try
            {
                while (!streamreader.EndOfStream)
                {
                    line = streamreader.ReadLine();
                    string temp = "";
                    if (line.Contains("<item"))
                    {
                        while (newline == false)
                        {
                            line = streamreader.ReadLine();
                            temp += line + "\n";
                            newline = line.Contains("</item>");
                        }
                        newline = false;
                        //MessageBox.Show(temp); //troubleshooting
                        stories[counter] = temp;
                        counter++;
                    }
                }

                for (int i = 0; i < counter; i++)
                {
                    if (stories[i].Contains("toronto"))
                    {
                        streamWriter.WriteLine(stories[i]);
                    }
                    streamWriter.Write("stuff");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
        }
    }
}
