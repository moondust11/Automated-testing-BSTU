namespace F.Steps1_2.Utils
{
    public static class MonthHelper
    {
        public static string DetermineMonthByName(string name)
        {
            string result = string.Empty;
            switch (name)
            {
                case "января":
                    result = "january";
                    break;
                case "февраля":
                    result = "february";
                    break;
                case "марта":
                    result = "march";
                    break;
                case "апреля":
                    result = "april";
                    break;
                case "мая":
                    result = "may";
                    break;
                case "июня":
                    result = "june";
                    break;
                case "июля":
                    result = "july";
                    break;
                case "августа":
                    result = "august";
                    break;
                case "сентября":
                    result = "september";
                    break;
                case "октября":
                    result = "october";
                    break;
                case "ноября":
                    result = "november";
                    break;
                case "декабря":
                    result = "december";
                    break;
            } 
            return result;
        }
    }
}
