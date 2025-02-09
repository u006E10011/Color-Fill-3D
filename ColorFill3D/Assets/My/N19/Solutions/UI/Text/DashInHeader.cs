namespace N19
{
    public static class DashInHeader
    {
        public static string Dash(this string text, int count = 30)
        {
            var bash = new string('-',  count);

            return bash + text + bash;
        }

        public static string Dash(this string text, int countLeft = 30, int countRight = 30)
        {
            var bashLeft = new string('-', countLeft);
            var bashRight = new string('-', countRight);

            return bashLeft + text + bashRight;
        }
    }
}