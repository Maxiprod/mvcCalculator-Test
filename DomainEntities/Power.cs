using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities
{
    public class Power
    {
        private double m_number;
        private int m_parenIndex;
        private double m_exponent;

        public double number
        {
            get
            {
                return m_number;
            }
        }
        public double exponent
        {
            get
            {
                return m_exponent;
            }
            set
            {
                m_exponent = value;
            }
        }

        public int parenIndex
        {
            get
            {
                return m_parenIndex;
            }
        }

        public Power(double number)
        {
            m_number = number;
            m_parenIndex = -1;
        }

        public Power(double number, int parenIndex)
        {
            m_number = number;
            m_parenIndex = parenIndex;
        }

        public double Solve(double exponent)
        {
            m_exponent = exponent;
            return Solve();
        }

        public double Solve()
        {
            return Math.Pow(m_number, m_exponent);
        }

        public string GetExpression()
        {
            return m_number.ToString() + "^" + m_exponent.ToString();
        }

    }
}
