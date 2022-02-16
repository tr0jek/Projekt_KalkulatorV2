using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kalkulatorV2
{
    public partial class Form1 : Form
    {
        
        double Value1;
        double Value2;
        double resultValue = 0;
        const string divideByZero = "Error";
        const string syntaxError = "Syntax ERROR";
        bool decimalPointActive = false;
        int Mode=10;
        void textsender(object sender)
        {
            txtDisplay.Text += (sender as Button).Text;

        }
        void disableButtonsHex()
        {

            btnA.Enabled = false;
            btnB.Enabled = false;
            btnC.Enabled = false;
            btnD.Enabled = false;
            btnE.Enabled = false;
            btnF.Enabled = false;
        }
        public Form1()
        {
            InitializeComponent();
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            disableButtonsHex();
        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            decimalPointActive = false;
            CheckButton();
            previusOperation = Operation.None;
            txtDisplay.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            decimalPointActive = false;
            CheckButton();
            if (txtDisplay.Text.Length > 0)
            {
                double d;
                if (!double.TryParse(txtDisplay.Text[txtDisplay.Text.Length - 1].ToString(), out d))
                {
                    previusOperation = Operation.None;
                }

                txtDisplay.Text = txtDisplay.Text.Remove(txtDisplay.Text.Length - 1, 1);
            }
            if (txtDisplay.Text.Length == 0)
            {
                previusOperation = Operation.None;
            }
            if (previusOperation != Operation.None)
            {
                currentOperation = previusOperation;
            }
        }

        private void BtnNum(object btn, EventArgs e)
        {
           
            EnableOperatorButtons();
            CheckButton();
            textsender(btn);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtDisplay.TextLength == 0) return;
            CheckButton();
            currentOperation = Operation.Add;
            if (Mode == 10)
            {
                Value1 = Convert.ToDouble(txtDisplay.Text);
                PerformCalculation(previusOperation);
            }
            else
            {
                Value1 = Convert.ToInt32(txtDisplay.Text, Mode);
                PerformCalculation(previusOperation);
                
            }          
            previusOperation = currentOperation;
            EnableOperatorButtons(false);
            txtDisplay.Clear();

        }
        private void btnSub_Click(object sender, EventArgs e)
        {
            if (txtDisplay.TextLength == 0) return;
            CheckButton();
            currentOperation = Operation.Sub;
            if (Mode == 10)
            {
                Value1 = Convert.ToDouble(txtDisplay.Text);
                PerformCalculation(previusOperation);
            }
            else
            {
                Value1 = Convert.ToInt32(txtDisplay.Text, Mode);
                PerformCalculation(previusOperation);

            }

            previusOperation = currentOperation;
            EnableOperatorButtons(false);
            
            txtDisplay.Clear();
        }

        private void btnMul_Click(object sender, EventArgs e)
        {
            if (txtDisplay.TextLength == 0) return;
            CheckButton();
            currentOperation = Operation.Mul;
            if (Mode == 10)
            {
                Value1 = Convert.ToDouble(txtDisplay.Text);
                PerformCalculation(previusOperation);
            }
            else
            {
                Value1 = Convert.ToInt32(txtDisplay.Text, Mode);
                PerformCalculation(previusOperation);

            }

            previusOperation = currentOperation;
            EnableOperatorButtons(false);
            
            txtDisplay.Clear();
        }

        private void btnDiv_Click(object sender, EventArgs e)
        {
            if (txtDisplay.TextLength == 0) return;
            CheckButton();
            currentOperation = Operation.Div;

            if (Mode == 10)
            {
                Value1 = Convert.ToDouble(txtDisplay.Text);
                PerformCalculation(previusOperation);
            }
            else
            {
                Value1 = Convert.ToInt32(txtDisplay.Text, Mode);
                PerformCalculation(previusOperation);

            }

            previusOperation = currentOperation;
            EnableOperatorButtons(false);
            
            txtDisplay.Clear();
        }
        private void PerformCalculation(Operation previousOperation)
        {
            if (Mode == 10)
            {
                Value2 = Convert.ToDouble(txtDisplay.Text);
            } 
            else
            {
                Value2 = Convert.ToInt32(txtDisplay.Text, Mode);
            }
            try
            {
                if (previousOperation == Operation.None)
                    return;
               

                switch (previousOperation)
                {
                    case Operation.Add:
                        if (currentOperation == Operation.Sub)
                        {
                            currentOperation = Operation.Add;
                            return;
                        }
                        
                        if(Mode==10)
                        {
                            resultValue = (Value1 + Value2);
                            txtDisplay.Text = Convert.ToString(resultValue);
                            Value1 = resultValue;
                        }
                        else
                        {

                            int value1Dec = Convert.ToInt32(Convert.ToString(Value1));
                            int value2Dec = Convert.ToInt32(Convert.ToString(Value2));
                            resultValue = (value1Dec + value2Dec);
                            txtDisplay.Text = Convert.ToString(Convert.ToInt32(resultValue), Mode);
                            Value1 = resultValue;
                        }
                        break;
                    case Operation.Sub:
                       if(Mode==10)
                        {
                            resultValue = (Value1 - Value2);
                            txtDisplay.Text = Convert.ToString(resultValue);
                            Value1 = resultValue;
                        }
                       else
                        {
                            int value1Dec = Convert.ToInt32(Convert.ToString(Value1));
                            int value2Dec = Convert.ToInt32(Convert.ToString(Value2));
                            resultValue = (value1Dec - value2Dec);
                            txtDisplay.Text = Convert.ToString(Convert.ToInt32(resultValue), Mode);
                            Value1 = resultValue;
                        }
                        break;
                    case Operation.Mul:
                        if (Mode == 10)
                        {
                            resultValue = (Value1 * Value2);
                            txtDisplay.Text = Convert.ToString(resultValue);
                            Value1 = resultValue;
                        }
                        else
                        {
                            int value1Dec = Convert.ToInt32(Convert.ToString(Value1));
                            int value2Dec = Convert.ToInt32(Convert.ToString(Value2));
                            resultValue = (value1Dec * value2Dec);
                            txtDisplay.Text = Convert.ToString(Convert.ToInt32(resultValue), Mode);
                            Value1 = resultValue;
                        }
                        break;
                    case Operation.Div:
                        if (currentOperation == Operation.Sub)
                        {
                            currentOperation = Operation.Div;
                            return;
                        }
                        try
                        {
                           if(Value2 ==  0)
                            {
                                throw new DivideByZeroException();
                            }
                           else
                            { if (Mode == 10)
                                {
                                    resultValue = (Value1 / Value2);
                                    txtDisplay.Text = Convert.ToString(resultValue);
                                    Value1 = resultValue;
                                }
                                else
                                {
                                    int value1Dec = Convert.ToInt32(Convert.ToString(Value1));
                                    int value2Dec = Convert.ToInt32(Convert.ToString(Value2));
                                    resultValue = (value1Dec / value2Dec);
                                    txtDisplay.Text = Convert.ToString(Convert.ToInt32(resultValue), Mode);
                                    Value1 = resultValue;
                                }
                            }
                        }
                        catch (DivideByZeroException)
                        {
                            txtDisplay.Text = divideByZero;
                        }
                        break;
                    case Operation.None:
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                txtDisplay.Text = syntaxError;
            }
            
        }
        
        private void CheckButton()
            {
                if (txtDisplay.Text == divideByZero || txtDisplay.Text == syntaxError)
                    txtDisplay.Clear();
                if (previusOperation != Operation.None)
                {
                    EnableOperatorButtons();
                }
            }
            private void EnableOperatorButtons(bool enable = true)
            {
                btnMul.Enabled = enable;
                btnDiv.Enabled = enable;
                btnAdd.Enabled = enable;
                btnSub.Enabled = enable;
                if (!enable)
                {
                    decimalPointActive = false;
                }
          }
            enum Operation
            {
                Add,
                Sub,
                Mul,
                Div,
                None
            }
            Operation currentOperation=Operation.None;
            Operation previusOperation=Operation.None;
        private void btnRes_Click(object sender, EventArgs e)
        {

            if (txtDisplay.TextLength == 0) return;
            if (previusOperation != Operation.None)
            PerformCalculation(previusOperation);
            previusOperation = Operation.None;
            

        }

        private void BtnDecimal_Click(object sender, EventArgs e)
        {
            if (decimalPointActive) return;
            if (txtDisplay.Text == syntaxError || txtDisplay.Text == divideByZero)
            {
                txtDisplay.Text = string.Empty;
            }
            EnableOperatorButtons();
            CheckButton();
            textsender(sender);
            decimalPointActive = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Mode = 10;
            txtDisplay.Clear();
            disableButtonsHex();
            btn0.Enabled = true;
            btn1.Enabled = true;
            btn2.Enabled = true;
            btn3.Enabled = true;
            btn4.Enabled = true;
            btn5.Enabled = true;
            btn6.Enabled = true;
            btn7.Enabled = true;
            btn8.Enabled = true;
            btn9.Enabled = true;
            button29.Enabled = true;
            negation.Enabled = true;


        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Mode = 8;
            txtDisplay.Clear();
            disableButtonsHex();
            btn0.Enabled = true;
            btn1.Enabled = true;
            btn2.Enabled = true;
            btn3.Enabled = true;
            btn4.Enabled = true;
            btn5.Enabled = true;
            btn6.Enabled = true;
            btn7.Enabled = true;
            btn8.Enabled = false;
            btn9.Enabled = false;
            button29.Enabled = false;
            negation.Enabled = false;

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Mode = 2;
            txtDisplay.Clear();
            disableButtonsHex();
            btn2.Enabled = false;
            btn3.Enabled = false;
            btn4.Enabled = false;
            btn5.Enabled = false;
            btn6.Enabled = false;
            btn7.Enabled = false;
            btn8.Enabled = false;
            btn9.Enabled = false;
            button29.Enabled = false;
            negation.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Mode = 16;
            txtDisplay.Clear();
            
            btn0.Enabled = true;
            btn1.Enabled = true;
            btn2.Enabled = true;
            btn3.Enabled = true;
            btn4.Enabled = true;
            btn5.Enabled = true;
            btn6.Enabled = true;
            btn7.Enabled = true;
            btn8.Enabled = true;
            btn9.Enabled = true;
            btnA.Enabled = true;
            btnB.Enabled = true;
            btnC.Enabled = true;
            btnD.Enabled = true;
            btnE.Enabled = true;
            btnF.Enabled = true;
            button29.Enabled = false;
            negation.Enabled = false;
        }

        private void negation_Click(object sender, EventArgs e)
        {
            if (txtDisplay.Text.StartsWith("-"))
            {
                txtDisplay.Text = txtDisplay.Text.Substring(1);
            }
            else if(!string.IsNullOrEmpty(txtDisplay.Text) && decimal.Parse(txtDisplay.Text) != 0)
            {
                txtDisplay.Text= "-"+txtDisplay.Text;
            }
        }

        
    }   
}

