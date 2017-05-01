using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace DomainEntities
{
    public class Expression
    {
        private List<string> m_steps;
        private ArrayList m_results;
        private List<Power> m_powers;
        private string m_rawValue;
        private string m_errorMessage;
        private static readonly Regex whitespace = new Regex(@"\s+");

        public string rawValue
        {
            get
            {
                return m_rawValue;
            }
            set
            {
                m_rawValue = value;
            }
        }
        
        public ArrayList results
        {
            get
            {
                return m_results;
            }
        }

        public string errorMessage
        {
            get
            {
                return m_errorMessage;
            }
        }
        
        /// <summary>
        /// Constructor for calculator object.
        /// </summary>
        public Expression()
        {
            m_rawValue = "";
            m_steps = new List<string>();
            m_results = new ArrayList();
            m_errorMessage = "";
            m_powers = new List<Power>();
        }

        public string Evaluate()
        {
            string workingValue;
            Regex algebraicExpression = new Regex(@"[0-9]+(\.[0-9]+)?|\+|-|\/|\*|\^|\%|\(|\)|\[|\]|\{|\}");
            Stack<string> paren = new Stack<string>();
            string localExpression;
            string lastTerm = "";
            bool powerFound = false;
            int lastPosition = 0;
            bool operatorExpected = false;
            bool powerSolved = false;
            double numericValue;

            if (string.IsNullOrWhiteSpace(m_rawValue)) return "No Value Entered";

            localExpression = "";
            m_steps.Add(localExpression);

            workingValue = Regex.Replace(m_rawValue, @"\s+", ""); // removing all whitespace
            m_rawValue = workingValue;

            foreach(Match itemMatch in algebraicExpression.Matches(workingValue))
            {
                powerSolved = false;

                switch (itemMatch.Value)
                {
                    case "(":
                    case "{":
                    case "[":
                        if (operatorExpected)
                        {
                            HandleError(localExpression, itemMatch.Value);
                            return "";
                        }

                        if (powerFound)
                        {
                            m_powers.Add(new DomainEntities.Power(Convert.ToDouble(lastTerm), paren.Count));  // Zero based so we stored the count before adding
                            powerFound = false;
                            lastTerm = "";
                        }
                        paren.Push(itemMatch.Value);

                        if (!string.IsNullOrEmpty(localExpression))
                        {
                            localExpression = "";
                            m_steps.Add(localExpression);
                        }

                        localExpression = AddValueToSteps(itemMatch.Value);
                        break;
                    case ")":
                    case "}":
                    case "]":
                        if (!IsCorrectCloseParen(operatorExpected, itemMatch.Value, paren))
                        {
                            HandleError(localExpression, itemMatch.Value);
                            return "";
                        }
                        localExpression = AddValueToSteps(itemMatch.Value);
                        lastTerm = Solve(true);
                        lastTerm = CheckPowers(itemMatch.Value, paren.Count, lastTerm, out powerSolved);
                        localExpression = m_steps[m_steps.Count - 1];

                        break;
                    case "+":
                    case "-":
                    case "*":
                    case "/":
                    case "%":
                        if (!operatorExpected)
                        {
                            HandleError(localExpression, itemMatch.Value);
                            return "";
                        }

                        operatorExpected = false;
                        localExpression = AddValueToSteps(itemMatch.Value);
                        break;
                    case "^":
                        if (!operatorExpected)
                        {
                            HandleError(localExpression, itemMatch.Value);
                            return "";
                        }

                        operatorExpected = false;
                        powerFound = true;
                        localExpression = AddValueToSteps(itemMatch.Value);
                        break;
                    default:
                        if (operatorExpected || !itemMatch.Value.IsNumeric())
                        {
                            HandleError(localExpression, itemMatch.Value);
                            return "";
                        }

                        numericValue = Convert.ToDouble(itemMatch.Value);
                        operatorExpected = true;
                        localExpression = AddValueToSteps(itemMatch.Value);

                        if (powerFound)
                        {
                            if (localExpression != lastTerm + "^" + itemMatch.Value)
                            {
                                localExpression = lastTerm + "^" + itemMatch.Value;
                                m_steps.Add(localExpression);
                            }
                            lastTerm = Solve(false);
                            powerFound = false;
                            powerSolved = true;
                        }
                        else
                        {
                            lastTerm = itemMatch.Value;
                        }

                        break;
                }
                if (workingValue.Substring(lastPosition).StartsWith(itemMatch.Value))
                {
                    lastPosition += itemMatch.Value.Length;
                }
                else
                {
                    HandleError(workingValue.Substring(lastPosition), "");
                    return "";
                }
            }
            if (paren.Count > 0)
            {
                HandleError(localExpression, "");
                return "";
            }
            if (lastPosition != workingValue.Length)
            {
                HandleError(workingValue.Substring(lastPosition), "");
                return "";
            }

            if (!powerSolved)
            {
                Solve(false);
            }

            return m_rawValue;
        }

        private string CheckPowers(string value, int index, string exponent, out bool powerSolved)
        {
            string result;
            powerSolved = false;

            if (m_powers.Count == 0) return exponent;

            Power power = m_powers[m_powers.Count - 1];

            if (index == power.parenIndex)
            {
                power.exponent = Convert.ToDouble(exponent);

                result = power.Solve().ToString();
                ReplaceValueInSteps(power.GetExpression(), result);
                m_results.AddValue(m_rawValue);
                m_powers.Remove(power);
                powerSolved = true;

                return result;
            }

            return exponent;
        }

        private void HandleError(string localExpression, string value)
        {
            m_errorMessage = string.Format("Invalid expression.  Error with '{0}', fix and resubmit.", localExpression + value);
        }

        private string Solve(bool includesParen)
        {
            string localExpression;
            string workExpress;
            double result = 0.0;
            string[] exponentValues;

            localExpression = m_steps.LastOrDefault();

            if (string.IsNullOrWhiteSpace(localExpression)) return "";

            workExpress = localExpression;

            if (includesParen)
            {
                workExpress = workExpress.Substring(1, workExpress.Length - 2);
            }

            if (localExpression.Contains("^"))
            {
                exponentValues = localExpression.Split('^');

                if (exponentValues[0].IsNumeric() &&
                    exponentValues[1].IsNumeric())
                {
                    Power power = new Power(Convert.ToDouble(exponentValues[0]));
                    result = power.Solve(Convert.ToDouble(exponentValues[1]));
                }                
            }
            else
            {
                result = Convert.ToDouble(new DataTable().Compute(workExpress, null));
            }

            if (m_steps.Count > 1)
            {
                m_steps.Remove(localExpression);
            }

            ReplaceValueInSteps(localExpression, result.ToString());
            
            m_results.AddValue(m_rawValue);

            return result.ToString();
        }

        private bool IsCorrectCloseParen(bool operatorExpected, string value, Stack<string> paren)
        {
            string lastParen;
            string[] validParens = new string[] { "()", "[]", "{}", "<>" };

            if (!operatorExpected || paren.Count == 0) return false;

            lastParen = paren.Pop();

            if (validParens.Contains<string>(lastParen + value))
            {
                return true;
            }

            return false;
        }

        private string AddValueToSteps(string value)
        {
            string express = "";

            for (int i = 0; i < m_steps.Count; i++)
            {
                express = m_steps[i];
                express += value;
                m_steps[i] = express;
            }

            return express;
        }

        private void ReplaceValueInSteps(string origValue, string newValue)
        {
            m_rawValue = m_rawValue.ReplaceFirst(origValue, newValue);

            for (int i = 0; i < m_steps.Count; i++)
            {
                string express = m_steps[i];
                express = express.ReplaceFirst(origValue, newValue);
                m_steps[i] = express;
            }
        }
    }
}
