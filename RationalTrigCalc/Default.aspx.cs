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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{

    public partial class WebForm1 : System.Web.UI.Page
    {
        // The Spread/Quadrance variables used in the calculator
        Spread s1 = new Spread("Spread, s1");
        Spread s2 = new Spread("Spread, s2");
        Quadrance Q1 = new Quadrance("Quadrance, Q1");
        Quadrance Q2 = new Quadrance("Quadrance, Q2");
        Quadrance Q3 = new Quadrance("Quadrance, Q3");

        // Counters for the number of known spreads/quadrances
        int KnownSpreads;
        int KnownQuadrances;


        protected void Page_Load(object sender, EventArgs e)
        {
            // Assign textboxes to variables
            s1.TextBox = s1TextBox;
            s2.TextBox = s2TextBox;
            Q1.TextBox = Q1TextBox;
            Q2.TextBox = Q2TextBox;
            Q3.TextBox = Q3TextBox;

            // Assign labels to variables
            s1.Label = s1Label;
            s2.Label = s2Label;
            Q1.Label = Q1Label;
            Q2.Label = Q2Label;
            Q3.Label = Q3Label;
                        
            // Assign classic trigonometry labels to variables
            s1.ClassicTrigLabel = s1ClassicTrigLabel;
            s2.ClassicTrigLabel = s2ClassicTrigLabel;
            Q1.ClassicTrigLabel = Q1ClassicTrigLabel;
            Q2.ClassicTrigLabel = Q2ClassicTrigLabel;
            Q3.ClassicTrigLabel = Q3ClassicTrigLabel;
        }

        protected void CalculateButton_Click(object sender, EventArgs e)
        {
            // Clear everything, except text in textboxes
            ErrorLabel.Text = "";
            s1.Clear(false);
            s2.Clear(false);
            Q1.Clear(false);
            Q2.Clear(false);
            Q3.Clear(false);
            
            // Read variables from textboxes
            s1.ReadFromTextBox();
            s2.ReadFromTextBox();
            Q1.ReadFromTextBox();
            Q2.ReadFromTextBox();
            Q3.ReadFromTextBox();

            // Return if any variable has an error
            if (s1.IsError() | s2.IsError() | Q1.IsError() | Q2.IsError() | Q3.IsError())
                return;

            // Count how many spreads and quadrances are known
            KnownSpreads = 0;
            KnownQuadrances = 0;
            if (s1.IsKnown())
                KnownSpreads++;
            if (s2.IsKnown())
                KnownSpreads++;
            if (Q1.IsKnown())
                KnownQuadrances++;
            if (Q2.IsKnown())
                KnownQuadrances++;
            if (Q3.IsKnown())
                KnownQuadrances++;

            // Returns if too few or two many variables are known
            // The correct amount is 1 spread and 1 quadrance OR 2 quadrances
            if (!((KnownSpreads == 1 & KnownQuadrances == 1) | (KnownSpreads == 0 & KnownQuadrances == 2)))
            {
                ErrorLabel.Text = "Please input either:<br/>- 1 spread and 1 quadrance<br/>- 2 quadrances<br/>";
                return;
            }


            // Try block is used here as Fraction class might throw overflow exceptions
            try
            {
                // Return if Q1 is larger than or equal to Q3
                if (Q1.IsKnown() & Q3.IsKnown())
                {
                    if (Q1.GetValue() >= Q3.GetValue())
                    {
                        Q1.SetError("must be &#60Q3");  // "must be <Q3"
                        Q3.SetError("must be &#62Q1");  // "must be >Q1"
                        return;
                    }
                }

                // Return if Q2 is larger than or equal to Q3
                if (Q2.IsKnown() & Q3.IsKnown())
                {
                    if (Q2.GetValue() >= Q3.GetValue())
                    {
                        Q2.SetError("must be &#60Q3");  // "must be <Q3"
                        Q3.SetError("must be &#62Q2");  // "must be >Q2"
                        return;
                    }
                }

                // Calculate unknown variables. Loop until all variables are calculated.
                while (s1.IsUnknown() | s2.IsUnknown() | Q1.IsUnknown() | Q2.IsUnknown() | Q3.IsUnknown())
                {
                    // Calculate s1 from s2
                    if (s1.IsUnknown() & s2.IsKnown())
                        s1.SetValue(1 - s2.GetValue());

                    // Calculate s2 from s1
                    if (s2.IsUnknown() & s1.IsKnown())
                        s2.SetValue(1 - s1.GetValue());

                    // Calculate s1 from Q1 and Q3
                    if (s1.IsUnknown() & Q1.IsKnown() & Q3.IsKnown())
                        s1.SetValue(Q1.GetValue() / Q3.GetValue());

                    // Calculate Q1 from s1 and Q3
                    if (Q1.IsUnknown() & s1.IsKnown() & Q3.IsKnown())
                        Q1.SetValue(s1.GetValue() * Q3.GetValue());

                    // Calculate Q3 from s1 and Q1
                    if (Q3.IsUnknown() & s1.IsKnown() & Q1.IsKnown())
                        Q3.SetValue(Q1.GetValue() / s1.GetValue());

                    // Calculate Q3 from s2 and Q2
                    if (Q3.IsUnknown() & s2.IsKnown() & Q2.IsKnown())
                        Q3.SetValue(Q2.GetValue() / s2.GetValue());

                    // Calculate Q1 from Q2 and Q3 (Pythagoras's theorem)
                    if (Q1.IsUnknown() & Q2.IsKnown() & Q3.IsKnown())
                        Q1.SetValue(Q3.GetValue() - Q2.GetValue());

                    // Calculate Q2 from Q1 and Q3 (Pythagoras's theorem)
                    if (Q2.IsUnknown() & Q1.IsKnown() & Q3.IsKnown())
                        Q2.SetValue(Q3.GetValue() - Q1.GetValue());

                    // Calculate Q3 from Q1 and Q2 (Pythagoras's theorem)
                    if (Q3.IsUnknown() & Q1.IsKnown() & Q2.IsKnown())
                        Q3.SetValue(Q1.GetValue() + Q2.GetValue());
                }

            }
            catch (Exception)
            {
                // Return if any exception is caught during the calculations
                ErrorLabel.Text = "Error in calculations<br/>";
                return;
            }

            // Write variables to textboxes
            s1.WriteToTextBox();
            s2.WriteToTextBox();
            Q1.WriteToTextBox();
            Q2.WriteToTextBox();
            Q3.WriteToTextBox();
        }


        protected void ClearButton_Click(object sender, EventArgs e)
        {
            // Clear everything, including text in textboxes
            ErrorLabel.Text = "";
            s1.Clear(true);
            s2.Clear(true);
            Q1.Clear(true);
            Q2.Clear(true);
            Q3.Clear(true);
        }

    }
}
