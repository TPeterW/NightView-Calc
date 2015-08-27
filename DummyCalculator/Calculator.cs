using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace DummyCalculator
{
    class Calculator
    {
        private string currentNum;
        private bool doCalc;
        private bool overwrite;
        private enum sign { plus, minus, multi, div, none}
        private sign lastEntered;

        private double lastNum;

        public Calculator(){
            currentNum = "0";
            lastNum = 0;
            lastEntered = sign.none;
            doCalc = false;
            overwrite = false;
        }

        public string Click(string tag)
        {
            string display;

            try
            {
                if (Char.IsDigit(charAt(tag, 0)))
                    display = append(tag);
                else if (tag.Equals("+"))
                    display = plus();
                else if (tag.Equals("-"))
                    display = minus();
                else if (tag.Equals("×"))
                    display = multi();
                else if (tag.Equals("÷"))
                    display = div();
                else if (tag.Equals("C") || tag.Equals("c"))
                    display = allClear();
                else if (tag.Equals("DEL") || tag.Equals("c"))
                    display = del();
                else if (tag.Equals("="))
                    display = equals();
                else if (tag.Equals("."))
                    display = addDot();
                else
                    display = "Invalid Input";
            }
            catch (Exception e)
            {
                display = "ERROR";
            }

            return display;
        }

        private String plus()
        {
            // Ignore the last dot is exists
            if (charAt(currentNum, currentNum.Length - 1) == '.')
                currentNum = currentNum.Substring(0, currentNum.Length - 1);


            if (!Char.IsDigit(charAt(currentNum, 0)))
            {
                lastEntered = sign.plus;
                currentNum = "+";
            }
            else if (doCalc)
            {
                doCalculation();
                doCalc = true;
                lastNum = Double.Parse(currentNum);
                lastEntered = sign.plus;
                currentNum = "+";
            }
            else
            {
                lastNum = Double.Parse(currentNum);
                lastEntered = sign.plus;
                currentNum = "+";
                doCalc = true;
            }

            return currentNum;
        }

        private String minus()
        {
            // Ignore the last dot is exists
            if (charAt(currentNum, currentNum.Length - 1) == '.')
                currentNum = currentNum.Substring(0, currentNum.Length - 1);


            if (!Char.IsDigit(charAt(currentNum, 0)))
            {
                lastEntered = sign.minus;
                currentNum = "-";
            }
            else if (doCalc)
            {
                doCalculation();
                doCalc = true;
                lastNum = Double.Parse(currentNum);
                lastEntered = sign.minus;
                currentNum = "-";
            }
            else
            {
                lastNum = Double.Parse(currentNum);
                lastEntered = sign.minus;
                currentNum = "-";
                doCalc = true;
            }

            return currentNum;
        }

        private String multi()
        {
            // Ignore the last dot is exists
            if (charAt(currentNum, currentNum.Length - 1) == '.')
                currentNum = currentNum.Substring(0, currentNum.Length - 1);


            if (!Char.IsDigit(charAt(currentNum, 0)))
            {
                lastEntered = sign.multi;
                currentNum = "×";
            }
            else if (doCalc)
            {
                doCalculation();
                doCalc = true;
                lastNum = Double.Parse(currentNum);
                lastEntered = sign.multi;
                currentNum = "×";
            }
            else
            {
                lastNum = Double.Parse(currentNum);
                lastEntered = sign.multi;
                currentNum = "×";
                doCalc = true;
            }

            return currentNum;
        }

        private String div()
        {
            // Ignore the last dot is exists
            if (charAt(currentNum, currentNum.Length - 1) == '.')
                currentNum = currentNum.Substring(0, currentNum.Length - 1);


            if (!Char.IsDigit(charAt(currentNum, 0)))
            {
                lastEntered = sign.div;
                currentNum = "÷";
            }
            else if (doCalc)
            {
                doCalculation();
                doCalc = true;
                lastNum = Double.Parse(currentNum);
                lastEntered = sign.div;
                currentNum = "÷";
            }
            else
            {
                lastNum = Double.Parse(currentNum);
                lastEntered = sign.div;
                currentNum = "÷";
                doCalc = true;
            }

            return currentNum;
        }

        private string equals()
        {
            if (!doCalc && currentNum == "0")
                return currentNum;

            // Ignore the last dot is exists
            if (charAt(currentNum, currentNum.Length - 1) == '.')
                currentNum = currentNum.Substring(0, currentNum.Length - 1);

            if (!Char.IsDigit(charAt(currentNum, 0)))
                return currentNum;
            else if (doCalc)
            {
                doCalculation();
                doCalc = false;
                lastNum = Double.Parse(currentNum);
                overwrite = true;
                return currentNum;
            }
            else
            {
                doCalc = false;
                return currentNum;
            }
        }

        private string del()
        {
            if (Char.IsDigit(charAt(currentNum, 0)) || charAt(currentNum, 0) == '-')
            {
                if (charAt(currentNum, currentNum.Length - 1) == '0' && charAt(currentNum, currentNum.Length - 2) == '.')
                    currentNum = currentNum.Substring(0, currentNum.Length - 2);

                // The new length
                int length = currentNum.Length;

                if (length == 1 || currentNum.Equals("0"))
                    currentNum = "0";
                else
                {
                    currentNum = currentNum.Substring(0, length - 1);
                    overwrite = false;
                }
                
                /**
                 * 
                 * Whether to go ahead and just delete the proceeding dot is debatable
                 * 
                 */

                return currentNum;

            }
            else
                return currentNum;

        }

        private string addDot()
        {
            if (currentNum.Contains(".") || !Char.IsDigit(charAt(currentNum, 0)))
                return currentNum;
            //else if (overwrite)
            //{
            //    currentNum = "0.";
            //    return currentNum;
            //}
            else if (currentNum.Length >= 18)
                return currentNum;
            else if (charAt(currentNum, currentNum.Length - 1) == '.')
                return currentNum;
            else
            {
                currentNum += '.';
                overwrite = false;
                return currentNum;
            }
        }

        private string allClear()
        {
            currentNum = "0";
            doCalc = false;
            lastEntered = sign.none;

            return currentNum;
        }

        private string append(String button)
        {
            if (overwrite)
            {
                currentNum = button;
                overwrite = false;
            }
            else if (currentNum.Length >= 18)
                return currentNum;
            else if (currentNum.Length >= 17 && currentNum.Contains("."))
                return currentNum;
            else if (currentNum.Equals("0"))
                currentNum = button;
            else if (!Char.IsDigit(charAt(currentNum, 0)))
                currentNum = button;
            else
                currentNum += button;

            return currentNum;
        }

        private void doCalculation()
        {
            switch (lastEntered)
            {
                case sign.plus:
                    currentNum = (lastNum + Double.Parse(currentNum)).ToString();
                    break;
                case sign.minus:
                    currentNum = (lastNum - Double.Parse(currentNum)).ToString();
                    break;
                case sign.multi:
                    currentNum = (lastNum * Double.Parse(currentNum)).ToString();
                    break;
                case sign.div:
                    currentNum = (lastNum / Double.Parse(currentNum)).ToString();
                    break;
            }
        }

        private char charAt(string str, int index)
        {
            char ch;

            char[] array = str.ToCharArray();

            try
            {
                ch = array[index];
            }
            catch (Exception e)
            {
                return '0';
            }

            return ch;
        }
    }
}
