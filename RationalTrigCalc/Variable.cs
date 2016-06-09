/* 
*  Rational Trigonometry Calculator (v1.0)
*  Author: Peter Bollhorn
*  Email: pbollhorn@gmail.com
*  Date: 10th of April 2016
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebApplication1
{

    // Variable class which is parent to Spread class and Quadrance class
    abstract class Variable
    {

        // The value of the variable which is a fraction
        protected Fraction Value;

        // The state of the variable which can be either 'Unknown', 'Known' or 'Error'
        protected enum VarState { Unknown, Known, Error };
        protected VarState State;

        // The name of the variable, e.g. "Spread, s2" or "Quadrance, Q3"
        protected string Name;

        // Control objects associated with the variable
        public TextBox TextBox;
        public Label Label;
        public Label ClassicTrigLabel;


        // Function to return the value of the variable
        public Fraction GetValue()
        {
            return Value;
        }


        // Function to set the value of the variable. Also sets state to 'Known'.
        public void SetValue(Fraction Input)
        {
            Value = Input;
            State = VarState.Known;
        }


        // Check if variable is unknown
        public bool IsUnknown()
        {
            if (State == VarState.Unknown)
                return true;
            else
                return false;
        }


        // Check if variable is known
        public bool IsKnown()
        {
            if (State == VarState.Known)
                return true;
            else
                return false;
        }


        // Check if variable has an error
        public bool IsError()
        {
            if (State == VarState.Error)
                return true;
            else
                return false;
        }


        // Set state to 'Error' and display error message for variable using red color
        public void SetError(string Message)
        {
            Label.Text = Name + " (" + Message + ")";
            Label.ForeColor = System.Drawing.Color.Red;
            TextBox.BorderColor = System.Drawing.Color.Red;
            State = VarState.Error;
        }


        // Clear variable for content and error messages and set state to 'Unknown' 
        // Clearing text in TextBox is optional
        public void Clear(bool ClearTextInTextBox)
        {
            // Clear text in textbox if chosen
            if (ClearTextInTextBox == true)
                TextBox.Text = "";

            // Set BorderColor of TextBox to default value
            TextBox.BorderColor = System.Drawing.Color.Empty;

            // Clear label and set ForeColor to default value
            Label.Text = Name;
            Label.ForeColor = System.Drawing.Color.Empty;

            // Clear classic trigonometry label
            if (this is Spread)
                ClassicTrigLabel.Text = "Angle (Deg):";
            else
                ClassicTrigLabel.Text = "Length:";

            // Set state to 'Unknown'
            State = VarState.Unknown;
        }


        // Read variable value from textbox
        public void ReadFromTextBox()
        {

            // Set variable value from text in textbox (Fraction class allows text to be used as input).
            // Empty/bad input text is handle by catching exceptions thrown by Fraction class.
            try
            {
                Value = TextBox.Text;
            }
            catch (ArgumentNullException) // Exception thrown by Fraction class in case of empty input text
            {
                // Set state to 'Unknown' and return
                State = VarState.Unknown;
                return;
            }
            catch (Exception)  // Exception thrown by Fraction class in case of other problems with input text
            {
                // Set state to 'Error'
                if (TextBox.Text.IndexOf(',') != -1)
                    SetError("use . as decimal symbol"); // Error message in case ',' is found in input text
                else
                    SetError("bad input"); // Generic error message

                // Return
                return;
            }

            // If this line is reached, the variable value has been set to some fraction value
            // It is now checked if the value is valid:
            // - A spread value must be >0 and <1
            // - A quadrance value must be >0
            // If the value is valid then state is set to 'Known'
            // Otherwise state is set to 'Error' 
            if (this is Spread)
            {
                // Check if spread is valid: Spread must be >0 and <1
                if (Value > 0 & Value < 1)
                    State = VarState.Known;
                else
                    SetError("must be &#62&#48 and &#60&#49"); // "must be >0 and <1"
            }
            else
            { 
                // Check if quadrance is valid: Quadrance must be >0
                if (Value > 0)
                    State = VarState.Known;
                else
                    SetError("must be &#62&#48"); // "must be >0"
            }

        }


        // Write variable value to textbox
        // Also write corresponding angle/length to classic trigonometry label
        public void WriteToTextBox()
        {
            // Write value to textbox
            TextBox.Text = Value.ToString();

            // Write angle/length to classic trigonometry label:
            // - Convert spread to angle (in degrees)
            // - Convert quadrance to length
            if (this is Spread)
                ClassicTrigLabel.Text = "Angle (Deg): " + (Math.Asin(Math.Sqrt(Value.ToDouble())) * 180 / Math.PI).ToString();
            else
                ClassicTrigLabel.Text = "Length: " + Math.Sqrt(Value.ToDouble()).ToString();
        }

    }


    // Spread class which inherits from Variable class
    class Spread : Variable
    {
        public Spread(string Input)
        {
            Name = Input;
        }
    }


    // Quadrance class which inherits from Variable class
    class Quadrance : Variable
    {
        public Quadrance(string Input)
        {
            Name = Input;
        }
    }
}