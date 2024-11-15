namespace ClassManagement.Mvc.Utilities
{
    internal static class Mapper
    {
        public static List<int> Map<T>(List<int> des, List<T> src)
        {
            foreach (var item in src)
            {
                _ = Int32.TryParse(item.ToString(), out int result);

                des.Add(result);
            }
            //int[] dataIdArray = model[0].Split(",").Select(int.Parse).ToArray();

            return des;
        }
    }
}
