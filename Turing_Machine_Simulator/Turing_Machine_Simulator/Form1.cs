using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Turing_Machine_Simulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();          
        }

        private Turing_Machine machine;
        string addTable = "1;+;B\n" +
               "q0;q0,1,R;q1,1,R;_\n" +
              "q1;q1,1,R;_;q2,B,L\n" +
               "q2;q3,B,N;_;_\n" +
               "*q3";
        string subTable = "1;-;B\n" +
                        "q0;q1,B,R;_;_\n" +
                        "q1; q1,1,R; q2,-,R;_\n" +
                        "q2; q2,1,R; _; q3,B,L\n" +
                        "q3; q5,B,L; q6,B,N;_\n" +
                        "q4; q4,1,L; q4,-,L; q0,B,R\n" +
                        "q5; q4,1,L; q6,B,N; _\n" +
                        "*q6";

        string multTable = "1;*;=;S;B\n" +
            "q0; q1,B,R; q8,B,R; _; _; _\n" +
            "q1; q1,1,R; q2,*,R; _; _; _\n" +
            "q2; q3,S,R; _; q6,=,L; q2,S,R; _\n" +
            "q3; q3,1,R; _; q4,=,R; _; _\n" +
            "q4; q4,1,R; _; _; _; q5,1,L\n" +
            "q5; q5,1,L; q2,*,R; q5,=,L; q5,S,L; _\n" +
            "q6; _; q7,*,L; _; q6,1,L; _\n" +
            "q7; q7,1,L; _; _; _; q0,B,R\n" +
            "q8; q8,B,R; _; q9,B,N; _; _\n" +
            "*q9";

        string palindromeTable = "a;b;c;B\n" +
        "q0; q1,B,R; q4,B,R; q6,B,R; q8,B,N\n" +
        "q1; q1,a,R; q1,b,R; q1,c,R; q2,B,L\n" +
        "q2; q3,B,L; _; _; q8,B,N\n" +
        "q3; q3,a,L; q3,b,L; q3,c,L; q0,B,R\n" +
        "q4; q4,a,R; q4,b,R; q4,c,R; q5,B,L\n" +
        "q5; _; q3,B,L; _; q8,B,N\n" +
        "q6; q6,a,R; q6,b,R; q6,c,R; q7,B,L\n" +
        "q7; _; _; q3,B,L; q8,B,N\n" +
        "*q8";

        string copyTable = "a;b;c;?;B;X;Y;Z\n" +
        "q0; q0,a,R; q0,b,R; q0,c,R; _; q1,?,L; _; _; _\n" +
        "q1; q1,a,L; q1,b,L; q1,c,L; _; q2,B,R; _; _; _\n" +
        "q2; q3,X,R; q5,Y,R; q7,Z,R; q9,?,R; _; _; _; _\n" +
        "q3; q3,a,R; q3,b,R; q3,c,R; q3,?,R; q4,a,L; _; _; _\n" +
        "q4; q4,a,L; q4,b,L; q4,c,L; q4,?,L; _; q2,a,R; _; _\n" +
        "q5; q5,a,R; q5,b,R; q5,c,R; q5,?,R; q6,b,L; _; _; _\n" +
        "q6; q6,a,L; q6,b,L; q6,c,L; q6,?,L; _; _; q2,b,R; _\n" +
        "q7; q7,a,R; q7,b,R; q7,c,R; q7,?,R; q8,c,L; _; _; _\n" +
        "q8; q8,a,L; q8,b,L; q8,c,L; q8,?,L; _; _; _; q2,c,R\n" +
        "q9; q10,?,L; q11,?,L; q12,?,L; _; q13,B,L; _; _; _\n" +
        "q10; _; _; _; q2,a,R; _; _; _; _\n" +
        "q11; _; _; _; q2,b,R; _; _; _; _\n" +
        "q12; _; _; _; q2,c,R; _; _; _; _\n" +
        "q13; _; _; _; q14,B,N; _; _; _; _\n" +
        "*q14";

        int x;
        int y;
        private void headerMovement(bool where)
        {
            x = pictureBox1.Location.X;
            y = pictureBox1.Location.Y;
            if (where)
                pictureBox1.Location = new Point(x + 37, y);
            else
                pictureBox1.Location = new Point(x - 37 , y);
        }

        private void ResetHeader()
        {
            pictureBox1.Location = new Point(233, 205);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            machine.DoTransition();
            lbTape.Text = machine.tape;
            lbSteps.Text = machine.stepCounter.ToString();
            lbCurrentStep.Text = machine.current.name;
            headerMovement(machine.headerMovement);
            if (machine.current.isFinal)
            {
                if(radioButton5.Checked)
                    lbTape.Text = "Congrats :J";

                timer1.Enabled = false;
            }
            else if (!machine.accepted)
            {
                lbTape.Text = "The input wasn't accepted :'(";
                timer1.Enabled = false; 
                   
            }

        }
        int control = 0;
        private void button1_Click(object sender, EventArgs e)
        {

            if (radioButton1.Checked)
            {
                machine = new Turing_Machine(addTable);
                machine.tape = txtInput.Text; 
            }
            else if(radioButton2.Checked)
            {
                machine = new Turing_Machine(subTable);
                machine.tape = txtInput.Text;
                
            }
            else if (radioButton3.Checked)
            {
                machine = new Turing_Machine(multTable);
                if (!txtInput.Text.EndsWith("="))
                {
                    MessageBox.Show("Please add = at the end of the input");
                    control = 1; 
                }
                else
                {
                    machine.tape = txtInput.Text;
                }
               
            }
            else if (radioButton4.Checked)
            {
                machine = new Turing_Machine(copyTable);
                machine.tape = txtInput.Text; 
            }
            else if (radioButton5.Checked)
            {
                machine = new Turing_Machine(palindromeTable);
                machine.tape = txtInput.Text; 
            }
            else
            {
                MessageBox.Show("Please select a turing machine");
            }
            if (control == 0)
            {
                timer1.Enabled = true;
                control = 1;
                button1.Text = "Reiniciar";
            }
            else
            {
                timer1.Enabled = false;
                control = 0;
                button1.Text = "Iniciar";
                ResetHeader();
                lbTape.Text = "Cinta";
                lbSteps.Text = "0";
                lbCurrentStep.Text = "q0"; 
            }
  
        }
    }
}
