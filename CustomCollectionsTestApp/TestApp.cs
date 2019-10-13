using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomCollections;

namespace CustomCollectionsTestApp
{
    /// <summary>
    /// Test application for ObservableList.
    /// </summary>
    public partial class TestApp : Form
    {

        ObservableList<string> observableList = new ObservableList<string>();

        public string inputValue { get; private set; }

        public TestApp()
        {
            InitializeComponent();
            // Two subscribers to respond to the events and defines methods which signatures much match the signature of the delegate for the 
            // event it is handling.
            observableList.Changed += ListChangedMethod;
            observableList.BeforeChange += ListBeforeChangeMethod;             
        }

        /// <summary>
        /// The event handler that performs the actions that are required when the event Changed event is raised - clear the listbox if there are any changes made and print out every current item in the observableList. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListChangedMethod(object sender, EventArgs e)
        {
            listBox.Items.Clear();
            foreach (string item in observableList)
            {
                listBox.Items.Add(item);
            }
        }

        /// <summary>
        /// The event handler that performs the actions that are required when the BeforeChange event is raised - defines a constraint for something that is not allowed to add to the list and rejects the change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBeforeChangeMethod(object sender, CustomDatastructures.Core.RejectableEventArgs<string> e)
        {
            inputValue = textBox.Text;
            if (inputValue == "10") 
            {
                e.RejectOperation();
            }
        }

        /// <summary>
        /// The event that is raised when the user clicks "Add". It tries to add the user input and shows an error massage if it is not possible to make the change in the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, EventArgs e)
        {
            try
            {
                inputValue = textBox.Text;
                observableList.Add(inputValue);
            }
            catch (OperationRejectedException)
            {
                MessageBox.Show("You cannot add this number!");
            }         
        }

        /// <summary>
        /// The event that is raised when the user clicks "Remove". It tries to remove the user input and shows an error message depending on the occuring error.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Remove_Click(object sender, EventArgs e)
        {
            try
            {
                inputValue = textBox.Text;
                observableList.Remove(inputValue);
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException)
                {
                    MessageBox.Show("The number is not on the list!");
                }
                else if (ex is OperationRejectedException)
                {                  
                    MessageBox.Show("You cannot remove this number!");
                }
            }           
        }

        /// <summary>
        /// The event that is raised when the user clicks "Contains?". It sends the user input to the correct method to check the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Contains_Click(object sender, EventArgs e)
        {
            string inputValue = textBox.Text;
            observableList.Contains(inputValue);
        }
    }
}
