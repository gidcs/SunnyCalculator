using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SunnyCalculator
{
    public partial class SunnyCalculator : Form
    {
        String NumButtonLastClick = "";
        float firstNum = 0, secondNum = 0;
        String opCode = "";
        bool Calculated = false;

        public SunnyCalculator()
        {
            InitializeComponent();
        }

        private void SunnyCalculator_Load(object sender, EventArgs e)
        {

        }

        private void updateFirstNum(float num, bool reset)
        {
            if (!reset)
            {
                firstNum = num;
                textBoxFirstNum.Text = firstNum.ToString();
            }
            else
            {
                firstNum = num;
                textBoxFirstNum.Text = "";
            }
        }

        private void updateSecondNum(float num, bool reset)
        {
            if (!reset)
            {
                secondNum = num;
                textBoxSecondNum.Text = secondNum.ToString();
            }
            else
            {
                secondNum = num;
                textBoxSecondNum.Text = "";
            }
        }

        private void updateOpCode(string code)
        {
            opCode = code;
            textBoxOpCode.Text = opCode;
        }

        private void Number_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            String now = button.Text;

            if (Calculated)
            {
                textBoxInput.Text = now;
                Calculated = false;
            }
            else
            {
                if (NumButtonLastClick == "." && now == ".")
                {
                    //ignore this click
                }
                else if (textBoxInput.Text == "0" && now == "0")
                {
                    //ignore this click
                }
                else if (textBoxInput.Text == "" && now == ".")
                {
                    textBoxInput.Text = "0" + now;
                }
                else if (textBoxInput.Text.Contains(".") && now == ".")
                {
                    //ignore this click
                }
                else if (textBoxInput.Text == "0")
                {
                    textBoxInput.Text = now;
                }
                else
                {
                    textBoxInput.Text += now;
                }
            }

            NumButtonLastClick = now;
        }

        private void ClearCurrent_Click(object sender, EventArgs e)
        {
            textBoxInput.Clear();
        }

        private void ClearAll_Click(object sender, EventArgs e)
        {
            textBoxInput.Clear();
            updateFirstNum(0, true);
            updateSecondNum(0, true);
            updateOpCode("");

            textBoxShow.Text = "";

            Calculated = false;
            NumButtonLastClick = "";
        }

        private void Op_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (NumButtonLastClick == "")
            {
                updateOpCode(button.Text);
                //ignore this click
            }
            else
            {
                if (textBoxInput.Text != "")
                {
                    if (opCode == "")
                    {
                        updateFirstNum(float.Parse(textBoxInput.Text), false);
                        updateOpCode(button.Text);
                        textBoxInput.Text = "";
                    }
                    else
                    {
                        float result;

                        updateSecondNum(float.Parse(textBoxInput.Text), false);
                        result = Calc(firstNum, secondNum, opCode);
                        textBoxInput.Text = result.ToString();
                        Calculated = true;

                        updateFirstNum(result, false);
                        updateOpCode(button.Text);
                    }
                }
            }           

            textBoxShow.Text = firstNum.ToString() + " " + opCode;
            updateSecondNum(0, true);
            NumButtonLastClick = "";
        }

        private float Calc(float x, float y, String op)
        {
            float result = 0;
            switch (op)
            {
                case "+":
                    result = x + y;
                    break;
                case "-":
                    result = x - y;
                    break;
                case "x":
                    result = x * y;
                    break;
                case "/":
                    result = x / y;
                    break;
                default:
                    break;
            }
            return result;
        }

        private void Equal_Click(object sender, EventArgs e)
        {
            float result;
            float tmp;

            if (textBoxInput.Text != "")
            {
                tmp = float.Parse(textBoxInput.Text);
            }
            else
            {
                tmp = firstNum;
            }


            if (opCode != "" && textBoxSecondNum.Text == "")
            {
                updateSecondNum(tmp, false);
            }
            else
            {
                updateFirstNum(tmp, false);
            }

            if (opCode != "")
            {
                textBoxShow.Text = firstNum.ToString() + " " + opCode + " " + secondNum.ToString() + " = ";
                result = Calc(firstNum, secondNum, opCode);
                updateFirstNum(result, true);
            }
            else
            {
                textBoxShow.Text = firstNum.ToString();
                result = firstNum;
            }

            textBoxInput.Text = result.ToString();
            Calculated = true;
        }

        private void Backspace_Click(object sender, EventArgs e)
        {
            String tmp = textBoxInput.Text;
            if (tmp.Length > 0)
            {
                textBoxInput.Text = tmp.Remove(tmp.Length - 1, 1);
            }
        }
    }
}