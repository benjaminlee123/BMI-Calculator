using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO; // You need to add this to write/read from text file

namespace BMI_Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /*
         * Design a program that prompts and reads in the 
         name, height (in metres) and weight (in kg)  of a person. 
         It then calculates/determines the following:
         a) Body Mass Index (BMI) of each person
         b) Average BMI
         c) Health Category of each person
         
         The formula for BMI = weight (kg) / (height (m) x height (m))

         
        It will also display based on the BMI whether the person is:
        - Severly underweight
        - Severely overweight
        - ...

        IPO
        - Input: Name, Height, Weight
        - Processing: Read in Name
                      Read in Height
                      Read in Weight
                      Calculate BMI
                      Display BMI
                      Display Health Category
        - Ouput: BMI, Health Category
         */

        private void Form1_Load(object sender, EventArgs e)
        {
            // This statement calls the DisplayWelcomeMessage() method
            DisplayWelcomeMessage();
        }

        // A Method without any parameter and use of class variable
        // This method is called by the Form1_Load method
        void DisplayWelcomeMessage()
        {
            MessageBox.Show("Welcome!!! I hope you like this program. Please send your feedback to feedback@me.com .");
        }

        float totalBMI = 0f;
        int numberOfPatient = 0;

        // Need these class variables for method DetermineObesityIndex().
        string obesityIndex;
        float patientBMI;

        void DetermineObesityIndex()
        {
            // These set of code are copied from the original to become a method
            if (patientBMI > 27.5f)
            {
                obesityIndex = "High Risk";
            }
            else if (patientBMI > 23f)
            {
                obesityIndex = "Moderate Risk";
            }
            else if (patientBMI > 18.5f)
            {
                obesityIndex = "Healthy";
            }
            else
                obesityIndex = "Deficient";
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            // Create var
            //string patientName, gender, obesityIndex;
            string patientName, gender;
            //float patientHeight, patientWeight, patientBMI;
            float patientHeight, patientWeight;
            float averageBMI;
            int numberOfExercise = 0;

            // read inputs without validation
            patientName = tbName.Text;
            //patientHeight = float.Parse(tbHeight.Text);
            //patientWeight = float.Parse(tbWeight.Text);


            if (tbName.Text == "")
            {
                // error
                MessageBox.Show("Please enter your name. ");
                tbName.Focus();
            }
            else if (float.TryParse(tbHeight.Text, out patientHeight) == false)
            {
                MessageBox.Show("Please input a numeric value for height");
                tbHeight.Focus();
            }
            else if (patientHeight <= 0f)
            {
                MessageBox.Show("Please input a numeric value greater than 0 for height");
                tbHeight.Focus();
            }
            else if (float.TryParse(tbWeight.Text, out patientWeight) == false)
            {
                MessageBox.Show("Please input a numeric value for weight");
                tbWeight.Focus();
            }
            else if (patientWeight <= 0f)
            {
                MessageBox.Show("Please input a numeric value greater than 0 for weight");
                tbWeight.Focus();
            }
            else if ((rbFemale.Checked || rbMale.Checked) == false)
            {
                MessageBox.Show("Please select your gender");
            }

            else
            // proc & display
            {
                if (rbFemale.Checked == true)
                {
                    gender = "F";
                }
                else
                {
                    gender = "M";
                }

                if (cbJog.Checked == true)
                {
                    numberOfExercise = numberOfExercise + 1;
                }

                if (cbRun.Checked)
                {
                    numberOfExercise = numberOfExercise + 1;
                }
                if (cbSwim.Checked)
                {
                    numberOfExercise = numberOfExercise + 1;
                }
                if (cbWalk.Checked)
                {
                    numberOfExercise = numberOfExercise + 1;
                }

                //Processing
                patientBMI = patientWeight / (patientHeight * patientHeight);

                // These set of code are now in the DetermineObesityIndex() method

                //if (patientBMI > 27.5f)
                //{
                //    obesityIndex = "High Risk";
                //}
                //else if (patientBMI > 23f)
                //{
                //    obesityIndex = "Moderate Risk";
                //}
                //else if (patientBMI > 18.5f)
                //{
                //    obesityIndex = "Healthy";
                //}
                //else
                //    obesityIndex = "Deficient";

                // This is to call the DetermineObesityIndex()
                DetermineObesityIndex();

                numberOfPatient = numberOfPatient + 1;
                totalBMI = totalBMI + patientBMI;

                averageBMI = totalBMI / numberOfPatient;

                // Display results
                lblAverage.Text = averageBMI.ToString("N2");

                rtbDisplay.AppendText(patientName.PadRight(20)
                    + gender.PadRight(7)
                    + " "
                    + patientHeight.ToString("N2").PadLeft(8)
                    + patientWeight.ToString("N2").PadLeft(12)
                    + patientBMI.ToString("N2").PadLeft(8)
                    + numberOfExercise.ToString().PadLeft(12)
                    + " "
                    + obesityIndex.PadRight(10)
                    + Environment.NewLine
                    );

                //Write (ie store) data to text file
                StreamWriter sw = new StreamWriter("BMIRecord.txt", false);
                string BMIRecord = patientName + "%" +
                    gender+"%"+
                    patientHeight.ToString() + "%" +
                    patientWeight.ToString() + "%" +
                    patientBMI.ToString() + "%" +
                    numberOfExercise.ToString() + "%" +
                    obesityIndex;
                sw.WriteLine(BMIRecord);
                sw.Close();

            }   
        }

        private void btnRetrieveAll_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("BMIRecord.txt");
            string [] bmiRecord;
            string pName, pGender, pHeight, pWeight, pBMI, noExercise, pOIndex;

            string lineRecord = sr.ReadLine();

            while (lineRecord != null)
            {
                bmiRecord = lineRecord.Split('%');
                pName = bmiRecord[0];
                pGender = bmiRecord[1];
                pHeight = bmiRecord[2];
                pWeight = bmiRecord[3];
                pBMI = bmiRecord[4];
                noExercise = bmiRecord[5];
                pOIndex = bmiRecord[6];

                rtbReport.AppendText(pName.PadRight(20)
                    + pGender.PadRight(7)
                    + " "
                    + pHeight.PadLeft(8)
                    + pWeight.PadLeft(12)
                    + pBMI.PadLeft(8)
                    + noExercise.PadLeft(12)
                    + " "
                    + pOIndex.PadRight(10)
                    + Environment.NewLine
                    );

                lineRecord = sr.ReadLine();
            }
            sr.Close();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {

        }
    }
}
