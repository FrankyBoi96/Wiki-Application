using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WikiApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region Global 2D String Array

        // 9.1	Create a global 2D string array, use static variables for the dimensions (row = 12, column = 4)
        static int row = 12;
        static int column = 4;
        int data; 
        string[,] myArray = new string[row, column];
        

    }
}
