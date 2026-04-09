using Portfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain
{
    public static class PortfolioLimits
    {

        public static int MaxActiveServices(PortfolioType type) => type switch
        {
            PortfolioType.Free => 3,
            PortfolioType.Pro => 20,
            PortfolioType.Entreprise => 100,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
