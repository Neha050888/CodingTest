using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTest.Entities.Utility
{
    public static class CodingTestUtility
    {
        public static string GenerateUniqueCode()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
}
