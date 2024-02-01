using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            
        }
        float totalBMI = 0f;
        int numberOfPatient = 0;

        

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            //Create variables
            string patientName, gender, obesityIndex;
            float patientHeight, patientWeight, patientBMI;
            float averageBMI;
            int numberOfExercise = 0;

            //readin input without validation
            patientName = tbName.Text;
            //patientHeight = float.Parse(tbHeight.Text);
            //patientWeight = float.Parse(tbWeight.Text);

            //Validation code here

            // The start of the actual processing
            //Input with some processing
            if (tbName.Text == "")
            {
                MessageBox.Show("Please enter your name.");
                tbName.Focus();
            }
            else if (float.TryParse(tbHeight.Text, out patientHeight) == false)
            {
                MessageBox.Show("Please enter a numeric number.");
                tbHeight.Focus();
            }
            else if (patientHeight <= 0f)
            {
                MessageBox.Show("Height has to be more than zero");
                tbHeight.Focus();
            }
            else if (float.TryParse(tbWeight.Text, out patientWeight) == false)
            {
                MessageBox.Show("Please enter a numeric number.");
                tbWeight.Focus();
            }
            else if (patientWeight <= 0f)
            {
                MessageBox.Show("Weight has to be more than zero");
                tbWeight.Focus();
            }
            else if ((rbFemale.Checked || rbMale.Checked)==false)
            {
                MessageBox.Show("Please select your gender");
   
            }
            else
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
            }
        }
    }
}
