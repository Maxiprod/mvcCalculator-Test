using System;
using System.Collections;
using DomainEntities;

namespace WebCalculator.Models
{
    public class Calculator
    {
        public string expression { get; set; }

        public ArrayList steps { get; set; }

        public string result { get; set; }
        public string errorMessage { get; set; }


        public Calculator()
        {
            expression = "";
            steps = new ArrayList();
            result = "";
            errorMessage = "";
        }

        internal void Solve()
        {
            Expression userExpression = new Expression();

            userExpression.rawValue = expression;
            result = userExpression.Evaluate();
            steps = userExpression.results;
            errorMessage = userExpression.errorMessage;
        }
    }
}