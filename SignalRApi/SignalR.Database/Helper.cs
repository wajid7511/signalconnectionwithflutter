using System;
namespace SignalR.Database
{
    public static class Helper
    {
        /// <summary>
        /// 24 is limiit on Mongo, if count is greater than 24 then default 24 will taken 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GetId(int count = 24)
        {
            if (count > 24)
            {
                count = 24;
            }
            return Guid.NewGuid().ToString("N").Substring(0, count);
        }
    }
}

