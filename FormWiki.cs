using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;
using System.Reflection;
using System.Xml.Linq;
using System.Runtime.ConstrainedExecution;
using System.Globalization;
using System.Runtime.Remoting.Channels;

// Francisco Soares
// Student ID: 30082300
// Wiki Application
// Cluster - C Sharp Two
// Assessment 1

namespace WikiApplication
{
    public partial class FormWiki : Form
    {
        public FormWiki()
        {
            InitializeComponent();
            InitializeArray(listViewWiki);
            toolStripStatusLabel1.Text = "Welcome to Wiki Application!"; 
        }
        
        #region Global 2D String Array

        // 9.1	Create a global 2D string array, use static variables for the dimensions (row = 12, column = 4)
        static int Row = 12; 
        static int Column = 4;
        static int Index = 0;
        string[,] GlobalArray = new string[Row, Column]; // Global 2D String Array 
        const string fileName = "definitions.dat"; // Used for the Open/Save file methods
        #endregion

        #region Methods 
        private void InitializeArray(ListView listViewWiki)
        {
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                {
                    GlobalArray[i, j] = "~";
                }
            }
        }

        // 9.8	Create a display method that will show the following information in a ListView: Name and Category,
        private void DisplayList()
        {
            listViewWiki.Items.Clear(); // Clear existing items before displaying
            BubbleSort();
            // This section is to display the 'Name' and 'Category' in the listview
            for (int i = 0; i < GlobalArray.GetLength(0); i++)
            {
                if (!string.IsNullOrEmpty(GlobalArray[i, 0]))
                {
                    string display = GlobalArray[i, 0];
                    string display2 = GlobalArray[i, 1];

                    ListViewItem item = new ListViewItem(display);
                    item.SubItems.Add(display2);
                    listViewWiki.Items.Add(item);
                }
            }
        }

        // 9.6	Write the code for a Bubble Sort method to sort the 2D array by Name ascending
        private void BubbleSort()
        {
            for (int x = 0; x < GlobalArray.GetLength(0); x++)
            {
                for (int y = 0; y < GlobalArray.GetLength(0) - 1; y++)
                {
                    if (!string.IsNullOrEmpty(GlobalArray[y + 1, 0]) &&
                        string.CompareOrdinal(GlobalArray[y, 0], GlobalArray[y + 1, 0]) > 0)
                    {
                        Swap(y, y + 1); // Corrected the indices passed to Swap
                    }
                }
            }
        }

        // 9.6 ensure you use a separate swap method that passes the array element to be swapped (do not use any built-in array methods),
        private void Swap(int index, int index2)
        {
            // Swapping index x with index y if required from the sort. 
            // made the temp so its easier to swap.
            String[] temp = new string[Column];

            temp[0] = GlobalArray[index, 0];
            temp[1] = GlobalArray[index, 1];
            temp[2] = GlobalArray[index, 2];
            temp[3] = GlobalArray[index, 3];

            GlobalArray[index, 0] = GlobalArray[index2, 0];
            GlobalArray[index, 1] = GlobalArray[index2, 1];
            GlobalArray[index, 2] = GlobalArray[index2, 2];
            GlobalArray[index, 3] = GlobalArray[index2, 3];


            GlobalArray[index2, 0] = temp[0];
            GlobalArray[index2, 1] = temp[1];
            GlobalArray[index2, 2] = temp[2];
            GlobalArray[index2, 3] = temp[3];
        }

        // 9.5	Create a CLEAR method to clear the four text boxes so a new definition can be added
        private void Reset_TextBoxes()
        {
            // Clears all 4 text boxes  
            txtBoxName.Clear();
            txtBoxCategory.Clear();
            txtBoxStructure.Clear();
            txtBoxDefinition.Clear();
            txtBoxName.Focus();

        }

        // 9.2	Create an ADD button that will store the information from the 4 text boxes into the 2D array,
        private void AddData()
        {
            // Check if the index is within bounds, and if all text boxes have non-empty values
            if ((Index < Row) && (!string.IsNullOrEmpty(txtBoxName.Text)) && (!string.IsNullOrEmpty(txtBoxCategory.Text))
                && (!string.IsNullOrEmpty(txtBoxStructure.Text)) && (!string.IsNullOrEmpty(txtBoxDefinition.Text)))
            {
                try
                {
                    // Assign values from text boxes to the global array at the current index
                    GlobalArray[Index, 0] = txtBoxName.Text;
                    GlobalArray[Index, 1] = txtBoxCategory.Text;
                    GlobalArray[Index, 2] = txtBoxStructure.Text;
                    GlobalArray[Index, 3] = txtBoxDefinition.Text;
                    Index++; // Increment the index for the next entry
                    toolStripStatusLabel1.Text = "Successfully added data to list";

                }
                catch (Exception ex)
                {
                    toolStripStatusLabel1.Text = ex.Message; // If an exception occurs, display the error message
                }
            }
            else
            {
                toolStripStatusLabel1.Text = "The array is FULL or the text boxes are empty"; // Update status label if conditions are not met
            }
            Reset_TextBoxes(); // Reset all text boxes
            DisplayList(); // Display the updated list

        }

        // 9.3	Create an EDIT button that will allow the user to modify any information from the 4 text boxes into the 2D array,
        private void EditData()
        {
            // Check if any item is selected in the list view
            if (listViewWiki.SelectedIndices.Count == 0)
            {
                toolStripStatusLabel1.Text = "Must select data to edit.";
            }
            // Check if any of the text boxes are empty
            else if (string.IsNullOrEmpty(txtBoxName.Text) || string.IsNullOrEmpty(txtBoxCategory.Text) || string.IsNullOrEmpty(txtBoxStructure.Text) ||
                     string.IsNullOrEmpty(txtBoxDefinition.Text))
            {
                toolStripStatusLabel1.Text = "Need to edit data to add new data to list."; // Update status label if any text box is empty
            }
            else
            {
                int indx = listViewWiki.SelectedIndices[0]; // Get the index of the selected item
                GlobalArray[indx, 0] = txtBoxName.Text; // Update data in the global array with values from text boxes
                GlobalArray[indx, 1] = txtBoxCategory.Text;
                GlobalArray[indx, 2] = txtBoxStructure.Text;
                GlobalArray[indx, 3] = txtBoxDefinition.Text;

                // run through methods to update list and then clear boxes once done
                toolStripStatusLabel1.Text = "Successfully edited data"; // Update status label to indicate success
            }


        }

        // 9.4	Create a DELETE button that removes all the information from a single entry of the array; the user must be prompted before the final deletion occurs,  
        private void DeleteData()
        {
            // Check if any item is selected in the list view
            if (listViewWiki.SelectedIndices.Count > 0)
            {
                int indx = listViewWiki.SelectedIndices[0]; // Get the index of the selected item


                DialogResult res = MessageBox.Show("Would you like to delete this data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.Yes) // If the user confirms deletion
                {
                    // Shift elements after the deleted index
                    for (int i = indx; i < Index - 1; i++)
                    {
                        for (int j = 0; j < Column; j++)
                        {
                            GlobalArray[i, j] = GlobalArray[i + 1, j]; // Move each element one position up in the array
                        }
                    }

                    try
                    {
                        // Clears the last row
                        for (int j = 0; j < Column; j++)
                        {
                            GlobalArray[Index - 1, j] = "~"; // Mark the last row as empty
                        }

                        toolStripStatusLabel1.Text = "Data has been deleted"; // Update status label
                        DisplayList(); // Display the updated list
                        Index--; // Decrement the index
                    }
                    catch (Exception ex)
                    {
                        toolStripStatusLabel1.Text = ex.Message; // Display any exception messages in the status label
                    }


                }
            }
            else
            {
                statusStrip1.Text = "No data selected for deletion";
            }
        }

        // 9.10	Create a SAVE button so the information from the 2D array can be written into a binary file called definitions.dat which is sorted by Name, ensure the user has
        // the option to select an alternative file.Use a file stream and BinaryWriter to create the file.
        private void SaveBinaryFile()
        {
            // Create and configure a SaveFileDialog instance
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            saveFileDialog.FileName = "definitions";
            saveFileDialog.DefaultExt = ".dat";
            saveFileDialog.Filter = "DAT files (*.dat)|*.dat|All files (*.*)|*.*";

            // Show the save file dialog and wait for user input
            DialogResult writer = saveFileDialog.ShowDialog();

            // Process the user's choice
            if (writer == DialogResult.OK)
            {
                BinaryWriter bw;
                try
                {
                    // Attempt to create a new file or overwrite an existing one
                    bw = new BinaryWriter(new FileStream(fileName, FileMode.Create));
                }
                catch (Exception fe)
                {
                    // Display an error message if the file cannot be created or opened for writing
                    MessageBox.Show(fe.Message + "\n Cannot append to file.");
                    return;
                }
                try
                {
                    // Write data from the global array to the file
                    for (int i = 0; i < Index; i++)
                    {
                        bw.Write(GlobalArray[i, 0]);
                        bw.Write(GlobalArray[i, 1]);
                        bw.Write(GlobalArray[i, 2]);
                        bw.Write(GlobalArray[i, 3]);
                    }
                }
                catch (Exception fe)
                {
                    // Display an error message if data cannot be written to the file
                    MessageBox.Show(fe.Message + "\n Cannot write data to file.");
                    return;
                }
                bw.Close();
            }

        }

        //9.11	Create a LOAD button that will read the information from a binary file called definitions.dat into the 2D array,
        //ensure the user has the option to select an alternative file. Use a file stream and BinaryReader to complete this task.
        private void OpenBinaryFile()
        {
            // Create and configure an OpenFileDialog instance
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            openFileDialog.DefaultExt = ".dat";
            openFileDialog.Filter = "DAT files (*.dat)|*.dat|All files (*.*)|*.*";

            // Show the file dialog and wait for user input
            DialogResult reader = openFileDialog.ShowDialog();

            // Process the user's choice
            if (reader == DialogResult.OK)
            {
                BinaryReader br;
                int x = 0; // Initialize a counter for array indexing
                listViewWiki.Items.Clear();
                try
                {
                    // Attempt to open the selected file for reading
                    br = new BinaryReader(new FileStream(fileName, FileMode.Open));
                }
                catch (Exception fe)
                {
                    // Display an error message if the file cannot be opened
                    MessageBox.Show(fe.Message + "\n Cannot open file for reading");
                    return;
                }

                // Read data from the file until the end is reached
                while (br.BaseStream.Position != br.BaseStream.Length)
                {
                    try
                    {
                        // Read data from the file and assign it to corresponding positions in the global array
                        GlobalArray[x, 0] = br.ReadString();
                        GlobalArray[x, 1] = br.ReadString();
                        GlobalArray[x, 2] = br.ReadString();
                        GlobalArray[x, 3] = br.ReadString();
                        x++;
                    }
                    catch (Exception fe)
                    {
                        // Display an error message if data cannot be read from the file or if the end of file is reached unexpectedly
                        MessageBox.Show("Cannot read data from file or EOF" + fe);
                        break; // Exit the loop if an error occurs
                    }
                    Index = x; // Update the index for the number of elements read from the file
                    DisplayList();
                }
                br.Close();
            }

        }

        private int BinarySearch()
        {
            // Variables
            string s = txtBoxSearch.Text;
            int indx = -1;
            int low = 0;
            int mid;
            int high = Index;

            // This section demonstrates a Binary Search method
            while (low <= high)
            {
                mid = (low + high) / 2;

                // Compare using StringComparer.OrdinalIgnoreCase
                int compareResult = string.Compare(GlobalArray[mid, 0], s, StringComparison.OrdinalIgnoreCase);

                if (compareResult == 0)
                {
                    // Item found, update text boxes
                    txtBoxName.Text = GlobalArray[mid, 0];
                    txtBoxCategory.Text = GlobalArray[mid, 1];
                    txtBoxStructure.Text = GlobalArray[mid, 2];
                    txtBoxDefinition.Text = GlobalArray[mid, 3];
                    toolStripStatusLabel1.Text = "Found at index " + mid;

                    return mid; // Return the index where the item is found
                }
                else if (compareResult < 0)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }

            // If the loop finishes without finding the item, set indx to -1
            indx = -1;
            toolStripStatusLabel1.Text = "Not Found!"; 
            return indx;
        }

        #endregion

        #region Button Functions
        private void btnSearch_Click(object sender, EventArgs e)
        {
           BinarySearch(); 
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenBinaryFile();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveBinaryFile();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset_TextBoxes();
            toolStripStatusLabel1.Text = "All text boxes have been cleared";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddData();
            BubbleSort();

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditData();
            Reset_TextBoxes();
            BubbleSort();
            DisplayList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
            Reset_TextBoxes();
            BubbleSort();
            DisplayList();
        }

        private void listViewWiki_Click(object sender, EventArgs e)
        {
            //This section demostrates when you select a value from the listview, then it'll display in all 4 text boxes
            if (listViewWiki.SelectedIndices.Count > 0)
            {
                int indx = listViewWiki.SelectedIndices[0];

                txtBoxName.Text = GlobalArray[indx, 0];
                txtBoxCategory.Text = GlobalArray[indx, 1];
                txtBoxStructure.Text = GlobalArray[indx, 2];
                txtBoxDefinition.Text = GlobalArray[indx, 3];
            }
        }
    }


}
#endregion